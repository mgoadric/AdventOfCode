from mesa.visualization.modules import CanvasGrid
from mesa.visualization.ModularVisualization import ModularServer
from mesa.visualization.modules import ChartModule
from mesa.visualization.UserParam import UserSettableParameter

from .portrayal import portrayCell
from .model import ConwaysGameOfLife

fin = open("seating/data/input11.txt")
data = fin.readlines()
fin.close()


# Make a world that is 50x50, on a 250x250 display.
scale = 600 // len(data)
canvas_element = CanvasGrid(portrayCell, len(data[0].strip()), len(data), scale * len(data[0].strip()), scale * len(data))

chart = ChartModule([{"Label": "Occupied",
                      "Color": "Red"}],
                    data_collector_name='dc')

server = ModularServer(
    ConwaysGameOfLife, [canvas_element, chart], "Advent of Code 2020 Day 11 Ferry Seating", {"height": len(data[0].strip()), "width": len(data), "data": data,  "part":UserSettableParameter('choice', 'Which Part?', value='Part 1',

choices=['Part 1', 'Part 2'])

}
)
