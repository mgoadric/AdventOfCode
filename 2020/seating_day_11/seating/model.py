from mesa import Model
from mesa.time import SimultaneousActivation
from mesa.space import Grid

from .cell import Cell


class ConwaysGameOfLife(Model):
    """
    Represents the 2-dimensional array of cells in Conway's
    Game of Life.
    """

    def __init__(self, data, height=10, width=10):
        """
        Create a new playing area of (height, width) cells.
        """

        # Set up the grid and schedule.

        # Use SimultaneousActivation which simulates all the cells
        # computing their next state simultaneously.  This needs to
        # be done because each cell's next state depends on the current
        # state of all its neighbors -- before they've changed.
        self.schedule = SimultaneousActivation(self)

        # Use a simple grid, where edges wrap around.
        self.grid = Grid(height, width, torus=False)

        self.numalive = 0
        
        # Place a cell at each location, with some initialized to
        # ALIVE and some to DEAD.
        for (contents, x, y) in self.grid.coord_iter():
            if data[(len(data) - 1) - y][x] == ".":
                state = Cell.FLOOR
            else:
                state = Cell.DEAD
            cell = Cell((x, y), self, state)
           
            self.grid.place_agent(cell, (x, y))
            self.schedule.add(cell)

        self.running = True

    def step(self):
        """
        Have the scheduler advance each cell by one step
        """
        print(self.numalive)
        prev = self.numalive
        self.schedule.step()
        self.numalive = self.count_type(self, Cell.ALIVE)
        if prev == self.numalive:
            self.running = False
        
    @staticmethod
    def count_type(model, condition):
        """
        Helper method to count trees in a given condition in a given model.
        """
        count = 0
        for spot in model.schedule.agents:
            if spot.state == condition:
                count += 1
        return count

