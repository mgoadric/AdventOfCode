using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapNode {
    
    private MapNode previous;

    private Vector3 location;

    public MapNode(Vector3 location, MapNode previous) {
        this.location = location;
        this.previous = previous;
    }

    public override int GetHashCode() { return location.GetHashCode(); }
    public override bool Equals(object obj) { 
        // If the passed object is null
        if (obj == null)
        {
            return false;
        }
        if (!(obj is MapNode))
        {
            return false;
        }
        return this.location == ((MapNode)obj).location;
    }
}