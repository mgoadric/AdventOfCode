from mesa import Agent


class Cell(Agent):
    """Represents a single ALIVE or DEAD cell in the simulation."""

    DEAD = 0
    ALIVE = 1
    FLOOR = 2

    def __init__(self, pos, model, init_state=DEAD):
        """
        Create a cell, in the given state, at the given x, y position.
        """
        super().__init__(pos, model)
        self.x, self.y = pos
        self.state = init_state
        self._nextState = None

    @property
    def isAlive(self):
        return self.state == self.ALIVE
    
    @property
    def isDead(self):
        return self.state == self.DEAD

    @property
    def neighbors(self):
        return self.model.grid.neighbor_iter((self.x, self.y), True)

    def step1(self):
        """
        Compute if the cell will be dead or alive at the next tick.  This is
        based on the number of alive or dead neighbors.  The state is not
        changed here, but is just computed and stored in self._nextState,
        because our current state may still be necessary for our neighbors
        to calculate their next state.
        """

        # Get the neighbors and apply the rules on whether to be alive or dead
        # at the next tick.
        live_neighbors = sum(neighbor.isAlive for neighbor in self.neighbors)

        # Assume nextState is unchanged, unless changed below.
        self._nextState = self.state
        if self.isAlive:
            if live_neighbors >= 4:
                self._nextState = self.DEAD
        elif self.isDead:
            if live_neighbors == 0:
                self._nextState = self.ALIVE

    def step(self):
        """
        Compute if the cell will be dead or alive at the next tick.  This is
        based on the number of alive or dead neighbors.  The state is not
        changed here, but is just computed and stored in self._nextState,
        because our current state may still be necessary for our neighbors
        to calculate their next state.
        """

        # Get the neighbors and apply the rules on whether to be alive or dead
        # at the next tick.
        #live_neighbors = sum(neighbor.isAlive for neighbor in self.neighbors)
        live_neighbors = 0
        for dx, dy in [(-1, -1), (-1, 0), (-1, 1), 
                        (0, -1),           (0, 1), 
                        (1, -1),  (1, 0),  (1, 1)]:
            finished = False
            i = 0
            while not finished:
                i += 1
                looking = (self.x + dx * i, self.y + dy * i)
                if self.model.grid.out_of_bounds(looking):
                    finished = True
                elif self.model.grid[looking[0]][looking[1]].isAlive:
                    live_neighbors += 1
                    finished = True
                elif self.model.grid[looking[0]][looking[1]].isDead:
                    finished = True

            
        # Assume nextState is unchanged, unless changed below.
        self._nextState = self.state
        if self.isAlive:
            if live_neighbors >= 5:
                self._nextState = self.DEAD
        elif self.isDead:
            if live_neighbors == 0:
                self._nextState = self.ALIVE

    def advance(self):
        """
        Set the state to the new computed state -- computed in step().
        """
        self.state = self._nextState
