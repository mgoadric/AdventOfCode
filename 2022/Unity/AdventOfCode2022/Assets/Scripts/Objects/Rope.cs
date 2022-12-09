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
        public override int GetHashCode() { return x * 1000000 + y; }
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
    }

    private Position head;
    private Position tail;

    private HashSet<Position> tailpos;

    public Rope(int x, int y) {
        head = new Position(x, y);
        tail = new Position(x, y);
        tailpos = new HashSet<Position>();
        tailpos.Add(tail);
    }

    public void Move(Tuple<char, int> movement) {
        for (int i = 0; i < movement.Item2; i++) {
            switch(movement.Item1) {
                case 'U':
                    head.y++;
                    break;
                case 'D':
                    head.y--;
                    break;
                case 'R':
                    head.x++;
                    break;
                case 'L':
                    head.x--;
                    break;
            }
            TailFollow();
        }
    }

    private void TailFollow() {
        if (head.Distance(tail) > 1.5) {
            tail.x += Math.Sign(head.x - tail.x);
            tail.y += Math.Sign(head.y - tail.y);
            tailpos.Add(tail);
            Debug.Log("Tailmove: " + tail.x + "," + tail.y);
        }
    }

    public int TailSpaces() {
        return tailpos.Count();
    }

}