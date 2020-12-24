from mesa import Model
from mesa.time import SimultaneousActivation
from mesa.space import HexGrid

from hex_snowflake.cell import Cell


class HexSnowflake(Model):
    """
    Represents the hex grid of cells. The grid is represented by a 2-dimensional array of cells with adjacency rules specific to hexagons.
    """

    def __init__(self, height=150, width=150):
        """
        Create a new playing area of (height, width) cells.
        """

        # Set up the grid and schedule.

        # Use SimultaneousActivation which simulates all the cells
        # computing their next state simultaneously.  This needs to
        # be done because each cell's next state depends on the current
        # state of all its neighbors -- before they've changed.
        self.schedule = SimultaneousActivation(self)

        # Use a hexagonal grid, where edges wrap around.
        self.grid = HexGrid(height, width, torus=True)

        # Place a dead cell at each location.
        for (contents, x, y) in self.grid.coord_iter():
            cell = Cell((x, y), self)
            self.grid.place_agent(cell, (x, y))
            self.schedule.add(cell)
            
        data = self.get_data("../data/input24.txt")

        # activate the center(ish) cell.
        centerishCell = self.grid[width // 2][height // 2]
        centerishPos = (width // 2, height // 2)
        
        for d in data:
            pos = self.find_cell(centerishPos, d)
            c = self.grid[pos[0]][pos[1]]
            c.toggle()
            for a in c.neighbors:
                a.isConsidered = True
        #centerishCell.state = Cell.ALIVE
        #for a in centerishCell.neighbors:
        #    a.isConsidered = True

        print(self.count_type(self, Cell.ALIVE))
        
        self.running = True

    def step(self):
        """
        Have the scheduler advance each cell by one step
        """
        self.schedule.step()
        print(self.schedule.time, self.count_type(self, Cell.ALIVE))
        if self.schedule.time == 100:
            self.running = False
        
    @staticmethod
    def find_cell(pos, d):
        for nd in d:
            pos = HexSnowflake.adj_pos(pos, nd)
        return pos
    
    @staticmethod
    def adj_pos(pos, direction):
        x, y = pos
        if direction == "e":
            return (x, y + 1)
        elif direction == "w":
            return (x, y - 1)
        if x % 2 == 0:
            if direction == "ne":
                return (x + 1, y + 1)
            elif direction == "nw":
                return (x + 1, y)
            elif direction == "se":
                return (x - 1, y + 1)
            elif direction == "sw":
                return (x - 1, y)
        else:
            if direction == "ne":
                return (x + 1, y)
            elif direction == "nw":
                return (x + 1, y - 1)
            elif direction == "se":
                return (x - 1, y)
            elif direction == "sw":
                return (x - 1, y - 1)
    
    @staticmethod
    def get_data(filename):
        fin = open(filename)
        data = fin.readlines()
        fin.close()
        fixed = []
        for d in data:
            d = d.strip()
            fixed.append([])
            i = 0
            while i < len(d):
                if d[i] == "e" or d[i] == "w":
                    fixed[-1].append(d[i])
                    i += 1
                else:
                    fixed[-1].append(d[i:i+2])
                    i += 2
        return fixed
    
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


