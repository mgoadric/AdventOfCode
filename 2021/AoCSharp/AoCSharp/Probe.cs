using System;
namespace AoCSharp
{
    public class Probe
    {
        private int x;
        private int y;
        private int dx;
        private int dy;
        private int maxy;
        private int xmin;
        private int xmax;
        private int ymin;
        private int ymax;

        public Probe(int dx, int dy, int xmin, int xmax, int ymin, int ymax)
        {
            this.dx = dx;
            this.dy = dy;
            this.xmin = xmin;
            this.xmax = xmax;
            this.ymin = ymin;
            this.ymax = ymax;
        }

        public bool Step()
        {
            x += dx;
            y += dy;
            if (dx > 0)
            {
                dx--;
            }
            dy--;
            if (y > maxy)
            {
                maxy = y;
            }

            return x >= xmin && x <= xmax && y >= ymin && y <= ymax;
        }

        public int GetMaxY()
        {
            return maxy;
        }

        public bool TooLow()
        {
            return y < ymin;
        }

        public bool TooFar()
        {
            return x > xmax;
        }

        override
        public string ToString()
        {
            return "x:" + x + " y:" + y + " dx:" + dx + " dy:" + dy;
        }
    }
}

