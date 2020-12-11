def portrayCell(cell):
    """
    This function is registered with the visualization server to be called
    each tick to indicate how to draw the cell in its current state.
    :param cell:  the cell in the simulation
    :return: the portrayal dictionary.
    """
    assert cell is not None
    p = {
        "Shape": "rect",
        "w": 1,
        "h": 1,
        "Filled": "true",
        "Layer": 0,
        "x": cell.x,
        "y": cell.y,
    }
    if cell.isAlive:
        p["Color"] = "red"
    elif cell.isDead:
        p["Color"] = "green"
    else:
        p["Color"] = "black"
    return p
