from mesa.visualization.modules import CanvasGrid
from mesa.visualization.ModularVisualization import ModularServer

from .portrayal import portrayCell
from .model import ConwaysGameOfLife

fin = open("seating/data/input11.txt")
data = fin.readlines()
fin.close()


# Make a world that is 50x50, on a 250x250 display.
canvas_element = CanvasGrid(portrayCell, len(data[0].strip()), len(data), 5 * len(data[0].strip()), 5 * len(data))

server = ModularServer(
    ConwaysGameOfLife, [canvas_element], "Game of Life", {"height": len(data[0].strip()), "width": len(data), "data": data}
)
