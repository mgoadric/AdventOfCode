using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rope {

private class Position {
    public int x; 
    public int y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    // https://stackoverflow.com/questions/8952003/how-does-hashset-compare-elements-for-equality
    public override int GetHashCode() { return x * 1000000 + y; } // TODO Why does x * y not work??
    public override bool Equals(object obj) { 
        // https://dotnettutorials.net/lesson/why-we-should-override-equals-method/
        // If the passed object is null
        if (obj == null)
        {
            return false;
        }
        if (!(obj is Position))
        {
            return false;
        }
        return (this.x == ((Position)obj).x)
            && (this.y == ((Position)obj).y);
    }

    public float Distance(Position other) {
        return MathF.Sqrt(MathF.Pow(x - other.x, 2) + MathF.Pow(y - other.y, 2));
    }

    public bool Follow(Position other) {
        if (Distance(other) > 1.5) {
            x += Math.Sign(other.x - x);
            y += Math.Sign(other.y - y);
            return true;
        }
        return false;
    }
}
    private Position[] knots;

    private HashSet<Tuple<int, int>> tailpos;

    public Rope(int x, int y, int length) {
        knots = new Position[length];
        for (int i = 0; i < length; i++) {
            knots[i] = new Position(x, y);
        }
        tailpos = new HashSet<Tuple<int, int>>();
        AddTail();
    }

    public void Move(Tuple<char, int> movement) {
        for (int i = 0; i < movement.Item2; i++) {
            switch(movement.Item1) {
                case 'U':
                    knots[0].y++;
                    break;
                case 'D':
                    knots[0].y--;
                    break;
                case 'R':
                    knots[0].x++;
                    break;
                case 'L':
                    knots[0].x--;
                    break;
            }
            TailFollow();
        }
    }

    private void TailFollow() {
        for (int i = 1; i < knots.Length; i++) {
            if (!knots[i].Follow(knots[i - 1])) {
                break;
            }
        }
        AddTail();
    }

    private void AddTail() {
        tailpos.Add(new Tuple<int, int>(knots[knots.Length - 1].x, knots[knots.Length - 1].y));
        Debug.Log("Tailmove: " + knots[knots.Length - 1].x + "," + knots[knots.Length - 1].y);
    }

    public int TailSpaces() {
        return tailpos.Count();
    }

}