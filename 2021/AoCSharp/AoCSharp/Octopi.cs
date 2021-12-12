using System;
using System.Collections.Generic;
using System.Linq;

namespace AoCSharp
{
    public class Octopi
    {

        private List<int[]> grid = new List<int[]>();
        private List<Tuple<int, int>> neighbors = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(-1, 0),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(0, -1),

                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(1, -1),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(-1, 1),
            };

        override
        public string ToString()
        {
            string s = "";
            for (int i = 0; i < grid.Count(); i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    s += grid[i][j];
                }
                s += "\n";
            }
            return s;
        }

        public Octopi(IEnumerable<string> input)
        {
            foreach (string s in input)
            {
                grid.Add(s.ToCharArray().Select(s => Int32.Parse("" + s)).ToArray());
            }
        }

        public int step()
        {
            Queue<Tuple<int, int>> toflash = new Queue<Tuple<int, int>>();
            HashSet<Tuple<int, int>> flashing = new HashSet<Tuple<int, int>>();
            for (int i = 0; i < grid.Count(); i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    grid[i][j]++;
                    if (grid[i][j] > 9)
                    {
                        toflash.Enqueue(new Tuple<int, int>(i, j));
                    }
                }
            }

            while (toflash.Count() > 0)
            {
                Tuple<int, int> pos = toflash.Dequeue();
                flashing.Add(pos);

                foreach (Tuple<int, int> n in neighbors)
                {
                    int nx = pos.Item1 + n.Item1;
                    int ny = pos.Item2 + n.Item2;
                    if (nx >= 0 && nx < grid.Count() &&
                        ny >= 0 && ny < grid[0].Length)
                    {
                        grid[nx][ny]++;
                        Tuple<int, int> t = new Tuple<int, int>(nx, ny);
                        if (grid[nx][ny] > 9 &&
                            !toflash.Contains(t) &&
                            !flashing.Contains(t))
                        {
                            toflash.Enqueue(t);
                        }
                    }
                }
            }

            foreach(Tuple<int, int> t in flashing)
            {
                grid[t.Item1][t.Item2] = 0;
            }
            
            return flashing.Count();
        }
    }
}
