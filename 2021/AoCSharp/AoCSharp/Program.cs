using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AoCSharp
{
    class Program
    {

        delegate int Puzzle(IEnumerable<String> input);

        static void Main(string[] args)
        {
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-a-text-file-one-line-at-a-time

            string path = "/Users/goadrich/Github/AdventOfCode/2021/data/";


            Puzzle[,] puzzles = new Puzzle[,]{
                { day1part1, day1part2 },
                { day2part1, day2part2 },
                { day3part1, day3part2 },
                { day4part1, day4part2 },
                { day5part1, day5part2 },
                { day6part1, day6part2 },
                { day7part1, day7part2 },
                { day8part1, day8part2 },
                { day9part1, day9part2 },
                { day10part1, day10part2 },
                { day11part1, day11part2 },
                { day12part1, day12part2 },
                { day13part1, day13part2 },
                { day14part1, day14part2 },
            };

            for (int day = 1; day < (puzzles.Length / 2) + 1; day++)
            {
                for (int part = 1; part < 3; part++)
                {
                    Console.WriteLine(String.Format("Day {0,2} Part {1}", day, part));
                    Console.WriteLine(" sample: " +
                                  puzzles[day - 1, part - 1](File.ReadLines(path + String.Format("input{0}sample.txt", day))));

                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    Console.WriteLine(" full: " +
                                  puzzles[day - 1, part - 1](File.ReadLines(path + String.Format("input{0}.txt", day))));

                    sw.Stop();
                    Console.WriteLine("Elapsed={0}", sw.Elapsed);
                }
            }

            //day3part2Viz(File.ReadLines(path + "input3sample.txt"));
        }

        static int day1part1(IEnumerable<string> input)
        {
            int count = 0;
            int prev = -1;
            foreach (string line in input)
            {
                // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number
                int reading = Int32.Parse(line);
                if (prev != -1 && reading > prev)
                {
                    count++;
                }
                prev = reading;
            }
            return count;
        }

        static int day1part2(IEnumerable<string> input)
        {
            int window = 3;
            int count = 0;
            Queue<int> q = new Queue<int>();
            foreach (string line in input)
            {
                int reading = Int32.Parse(line);
                q.Enqueue(reading);
                if (q.Count > window)
                {
                    int total = q.Sum();
                    int r = q.Dequeue();
                    if (total - r > total - reading)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        static int day2part1(IEnumerable<string> input)
        {
            int h = 0;
            int d = 0;
            foreach (string line in input)
            {
                String[] commands = line.Split(" ");
                switch (commands[0])
                {
                    case "forward":
                        h += Int32.Parse(commands[1]);
                        break;
                    case "up":
                        d -= Int32.Parse(commands[1]);
                        break;
                    case "down":
                        d += Int32.Parse(commands[1]);
                        break;
                }
            }
            return h * d;
        }

        static int day2part2(IEnumerable<string> input)
        {
            int h = 0;
            int d = 0;
            int aim = 0;
            foreach (string line in input)
            {
                String[] commands = line.Split(" ");
                switch (commands[0])
                {
                    case "forward":
                        h += Int32.Parse(commands[1]);
                        d += aim * Int32.Parse(commands[1]);
                        break;
                    case "up":
                        aim -= Int32.Parse(commands[1]);
                        break;
                    case "down":
                        aim += Int32.Parse(commands[1]);
                        break;
                }
            }
            return h * d;
        }

        static int day3part1(IEnumerable<string> input)
        {
            int gamma = 0;
            int epsilon = 0;
            int[] counts = new int[input.First().Length];
            foreach (string line in input)
            {
                char[] bits = line.ToCharArray();
                for (int i = 0; i < bits.Length; i++)
                {
                    int val = Int32.Parse("" + bits[i]);
                    if (val == 1)
                    {
                        counts[i]++;
                    }
                    else
                    {
                        counts[i]--;
                    }
                }
            }

            string gammaStr = "";
            string epsStr = "";
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] > 0)
                {
                    gamma = (gamma << 1) + 1;
                    epsilon = epsilon << 1;

                    gammaStr += "1";
                    epsStr += "0";
                }
                else
                {

                    gamma = gamma << 1;
                    epsilon = (epsilon << 1) + 1;

                    gammaStr += "0";
                    epsStr += "1";
                }
            }

            Console.WriteLine(gammaStr + ", " + epsStr);
            return gamma * epsilon;
        }

        static int day3part2(IEnumerable<string> input)
        {
            BCNode root = new BCNode('0', input.First().Length + 1);
            foreach (string line in input)
            {
                root.insert(line);
            }

            Console.WriteLine(root.o2string());
            Console.WriteLine(root.co2string());

            return root.o2() * root.co2();
        }

        static void day3part2Viz(IEnumerable<string> input)
        {
            BCNode root = new BCNode('0', input.First().Length + 1);
            int frame = 0;
            foreach (string line in input)
            {
                root.insert(line);
                File.WriteAllText("bntree" + frame + ".dot", "digraph {\n" +
                "\n" + root.toDot(Path.NONE) + "}\n");
                frame++;
            }
            File.WriteAllText("bntree" + frame + ".dot", "digraph {\n" +
                "\n" + root.toDot(Path.BOTH) + "}\n");
        }

        static int day4part1(IEnumerable<string> input)
        {
            int[] calls = input.First().Split(",").Select(s => Int32.Parse(s)).ToArray();

            List<BingoBoard> boards = new List<BingoBoard>();

            int numBoards = (input.Count() - 1) / 6;
            //Console.WriteLine((input.Count() - 1) % 6);
            String[] input2 = input.ToArray();
            for (int i = 0; i < numBoards; i++)
            {
                string members = "";
                for (int j = 0; j < 5; j++)
                {
                    members += input2[1 + (6 * i) + j + 1] + " ";
                }
                //Console.WriteLine(members.Replace("  ", " ").Trim());
                int[] nums = members.Replace("  ", " ").Trim().Split(" ").Select(s => Int32.Parse(s)).ToArray();
                boards.Add(new BingoBoard(nums));
            }

            foreach (int c in calls)
            {
                //Console.WriteLine(c);
                foreach (BingoBoard bb in boards)
                {
                    int bingo = bb.mark(c);
                    if (bingo > -1)
                    {
                        return bingo;
                    }
                }
            }
            return -1;
        }

        static int day4part2(IEnumerable<string> input)
        {
            int[] calls = input.First().Split(",").Select(s => Int32.Parse(s)).ToArray();

            List<BingoBoard> boards = new List<BingoBoard>();

            int numBoards = (input.Count() - 1) / 6;
            //Console.WriteLine((input.Count() - 1) % 6);
            String[] input2 = input.ToArray();
            for (int i = 0; i < numBoards; i++)
            {
                string members = "";
                for (int j = 0; j < 5; j++)
                {
                    members += input2[1 + (6 * i) + j + 1] + " ";
                }
                //Console.WriteLine(members.Replace("  ", " ").Trim());
                int[] nums = members.Replace("  ", " ").Trim().Split(" ").Select(s => Int32.Parse(s)).ToArray();
                boards.Add(new BingoBoard(nums));
            }

            int won = numBoards;
            bool[] whoWon = new bool[numBoards];
            foreach (int c in calls)
            {
                //Console.WriteLine(c);
                int i = 0;
                foreach (BingoBoard bb in boards)
                {
                    int bingo = bb.mark(c);
                    if (!whoWon[i] && bingo != -1)
                    {
                        won--;
                        whoWon[i] = true;
                        if (won == 0)
                        {
                            return bingo;
                        }
                    }
                    i++;
                }
            }
            return -1;
        }

        static int day5part1(IEnumerable<string> input)
        {
            Dictionary<Tuple<int, int>, int> points = new Dictionary<Tuple<int, int>, int>();
            foreach (string line in input)
            {
                int[] coords = string.Join(',', line.Split(" -> ")).Split(",").Select(s => Int32.Parse(s)).ToArray();
                if (coords[0] == coords[2])
                {
                    int min = Math.Min(coords[1], coords[3]);
                    int max = Math.Max(coords[1], coords[3]);
                    for (int j = min; j <= max; j++)
                    {
                        Tuple<int, int> t = new Tuple<int, int>(coords[0], j);
                        if (!points.ContainsKey(t))
                        {
                            points[t] = 0;
                        }
                        points[t] += 1;
                    }
                }
                else if (coords[1] == coords[3])
                {
                    int min = Math.Min(coords[0], coords[2]);
                    int max = Math.Max(coords[0], coords[2]);
                    for (int i = min; i <= max; i++)
                    {
                        Tuple<int, int> t = new Tuple<int, int>(i, coords[1]);
                        if (!points.ContainsKey(t))
                        {
                            points[t] = 0;
                        }
                        points[t] += 1;
                    }
                }
            }
            int overlap = 0;
            foreach (Tuple<int, int> t in points.Keys)
            {
                if (points[t] > 1)
                {
                    overlap++;
                }
            }
            return overlap;
        }

        static int day5part2(IEnumerable<string> input)
        {
            Dictionary<Tuple<int, int>, int> points = new Dictionary<Tuple<int, int>, int>();
            foreach (string line in input)
            {
                int[] coords = string.Join(',', line.Split(" -> ")).Split(",").Select(s => Int32.Parse(s)).ToArray();

                Tuple<int, int> t1 = new Tuple<int, int>(coords[0], coords[1]);
                Tuple<int, int> t2 = new Tuple<int, int>(coords[2], coords[3]);
                //Console.WriteLine("T2:" + t2);
                do
                {
                    //Console.WriteLine("T1: " + t1);
                    if (!points.ContainsKey(t1))
                    {
                        points[t1] = 0;
                    }
                    points[t1] += 1;
                    int xdir = Math.Sign(t2.Item1 - t1.Item1);
                    int ydir = Math.Sign(t2.Item2 - t1.Item2);
                    t1 = new Tuple<int, int>(t1.Item1 + xdir, t1.Item2 + ydir);
                } while (t1.Item1 != t2.Item1 || t1.Item2 != t2.Item2);
                if (!points.ContainsKey(t1))
                {
                    points[t1] = 0;
                }
                points[t1] += 1;

            }
            int overlap = 0;
            foreach (Tuple<int, int> t in points.Keys)
            {
                if (points[t] > 1)
                {
                    overlap++;
                }
            }
            return overlap;
        }

        static int day6part1(IEnumerable<string> input)
        {
            int[] fish = input.First().Split(",").Select(s => Int32.Parse(s)).ToArray();

            int[] starting = new int[7];
            foreach (int f in fish)
            {
                starting[f]++;
            }

            Queue<int> days = new Queue<int>();
            foreach (int fp in starting)
            {
                days.Enqueue(fp);
            }

            Queue<int> babies = new Queue<int>();
            babies.Enqueue(0);
            babies.Enqueue(0);


            for (int i = 0; i < 80; i++)
            {
                int repos = days.Dequeue();
                days.Enqueue(repos + babies.Dequeue());
                babies.Enqueue(repos);
            }

            return days.Sum() + babies.Sum();
        }

        static int day6part2(IEnumerable<string> input)
        {
            int[] fish = input.First().Split(",").Select(s => Int32.Parse(s)).ToArray();

            int[] starting = new int[7];
            foreach (int f in fish)
            {
                starting[f]++;
            }

            Queue<long> days = new Queue<long>();
            foreach (int fp in starting)
            {
                days.Enqueue(fp);
            }

            Queue<long> babies = new Queue<long>();
            babies.Enqueue(0);
            babies.Enqueue(0);


            for (int i = 0; i < 256; i++)
            {
                long repos = days.Dequeue();
                days.Enqueue(repos + babies.Dequeue());
                babies.Enqueue(repos);
            }

            Console.WriteLine(days.Sum() + babies.Sum());
            return -1;

        }

        static int day7part1(IEnumerable<string> input)
        {
            int[] crabs = input.First().Split(",").Select(s => Int32.Parse(s)).ToArray();
            int besti = -1;
            int bestv = Int32.MaxValue;
            for (int i = crabs.Min(); i <= crabs.Max(); i++)
            {
                int fuel = 0;
                foreach (int c in crabs)
                {
                    fuel += Math.Abs(c - i);
                }
                if (fuel < bestv)
                {
                    bestv = fuel;
                    besti = i;
                }
            }

            return bestv;
        }

        static int day7part2(IEnumerable<string> input)
        {
            int[] crabs = input.First().Split(",").Select(s => Int32.Parse(s)).ToArray();
            int besti = -1;
            int bestv = Int32.MaxValue;
            for (int i = crabs.Min(); i <= crabs.Max(); i++)
            {
                int fuel = 0;
                foreach (int c in crabs)
                {
                    int n = Math.Abs(c - i);
                    fuel += ((n + 1) * n) / 2;
                }
                if (fuel < bestv)
                {
                    bestv = fuel;
                    besti = i;
                }
            }

            return bestv;
        }

        static int day8part1(IEnumerable<string> input)
        {
            Dictionary<int, HashSet<char>> digits = new Dictionary<int, HashSet<char>>()
            {
                {0, new HashSet<char>{'a', 'b', 'c',      'e', 'f', 'g'} },
                {1, new HashSet<char>{          'c',      'e',         } },
                {2, new HashSet<char>{'a',      'c', 'd', 'e',      'g'} },
                {3, new HashSet<char>{'a',      'c', 'd',      'f', 'g'} },
                {4, new HashSet<char>{     'b', 'c', 'd',      'f',    } },
                {5, new HashSet<char>{'a', 'b',      'd',      'f', 'g'} },
                {6, new HashSet<char>{'a', 'b',      'd', 'e', 'f', 'g'} },
                {7, new HashSet<char>{'a',      'c',           'f',    } },
                {8, new HashSet<char>{'a', 'b', 'c', 'd', 'e', 'f', 'g'} },
                {9, new HashSet<char>{'a', 'b', 'c', 'd',      'f', 'g'} },
            };

            int count = 0;
            foreach (string line in input)
            {
                Dictionary<char, HashSet<char>> maping = new Dictionary<char, HashSet<char>>();
                foreach (char c in "abcdefg")
                {
                    maping[c] = new HashSet<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
                }

                string[][] patterns = line.Split(" | ").Select(s => s.Split(" ")).ToArray();
                HashSet<int>[] potential = new HashSet<int>[patterns[1].Length];

                for (int i = 0; i < patterns[1].Length; i++)
                {
                    potential[i] = new HashSet<int>();

                    for (int j = 0; j < 10; j++)
                    {
                        if (digits[j].Count() == patterns[1][i].Length)
                        {
                            potential[i].Add(j);
                            //Console.WriteLine(patterns[1][i] + " -> " + j);
                        }
                    }

                    if (potential[i].Count() == 1)
                    {
                        count++;
                        //Console.WriteLine(patterns[1][i] + " -> " + potential[i]);
                    }
                }

            }
            return count;
        }

        static int day8part2(IEnumerable<string> input)
        {
            Dictionary<int, HashSet<char>> digits = new Dictionary<int, HashSet<char>>()
            {
                {1, new HashSet<char>{          'c',           'f',    } },
                {7, new HashSet<char>{'a',      'c',           'f',    } },
                {4, new HashSet<char>{     'b', 'c', 'd',      'f',    } },
                {2, new HashSet<char>{'a',      'c', 'd', 'e',      'g'} },
                {3, new HashSet<char>{'a',      'c', 'd',      'f', 'g'} },
                {5, new HashSet<char>{'a', 'b',      'd',      'f', 'g'} },
                {6, new HashSet<char>{'a', 'b',      'd', 'e', 'f', 'g'} },
                {0, new HashSet<char>{'a', 'b', 'c',      'e', 'f', 'g'} },
                {9, new HashSet<char>{'a', 'b', 'c', 'd',      'f', 'g'} },
                {8, new HashSet<char>{'a', 'b', 'c', 'd', 'e', 'f', 'g'} },
            };

            int count = 0;
            foreach (string line in input)
            {
                Dictionary<char, int> counts = new Dictionary<char, int>();
                foreach (char c in "abcdefg")
                {
                    counts[c] = 0;
                }

                string[][] patterns = line.Split(" | ").Select(s => s.Split(" ")).ToArray();
                HashSet<int>[] potential = new HashSet<int>[patterns[0].Length];

                int seven = -1;
                int one = -1;
                int four = -1;

                for (int i = 0; i < patterns[0].Length; i++)
                {
                    potential[i] = new HashSet<int>();

                    for (int j = 0; j < 10; j++)
                    {
                        if (digits[j].Count() == patterns[0][i].Length)
                        {
                            potential[i].Add(j);
                            //Console.WriteLine(patterns[0][i] + " -> " + j);
                            if (j == 7)
                            {
                                seven = i;
                            }
                            else if (j == 1)
                            {
                                one = i;
                            }
                            else if (j == 4)
                            {
                                four = i;
                            }
                        }
                    }

                    for (int ci = 0; ci < patterns[0][i].Length; ci++)
                    {
                        counts[patterns[0][i][ci]] += 1;
                    }
                }

                Dictionary<char, HashSet<char>> mapping = new Dictionary<char, HashSet<char>>();
                Dictionary<char, HashSet<char>> revmapping = new Dictionary<char, HashSet<char>>();
                foreach (char c in "abcdefg")
                {
                    revmapping[c] = new HashSet<char>();
                }
                foreach (char c in "abcdefg")
                {
                    switch (counts[c])
                    {
                        case 6:
                            mapping[c] = new HashSet<char> { 'b' };
                            revmapping['b'].Add(c);
                            break;
                        case 9:
                            mapping[c] = new HashSet<char> { 'f' };
                            revmapping['f'].Add(c);
                            break;
                        case 4:
                            mapping[c] = new HashSet<char> { 'e' };
                            revmapping['e'].Add(c);
                            break;
                        case 8:
                            mapping[c] = new HashSet<char> { 'a', 'c' };
                            revmapping['a'].Add(c);
                            revmapping['c'].Add(c);
                            break;
                        case 7:
                            mapping[c] = new HashSet<char> { 'd', 'g' };
                            revmapping['d'].Add(c);
                            revmapping['g'].Add(c);
                            break;
                    }
                    //Console.WriteLine(c + " -> " + mapping[c].Count());
                }

                var spot = new HashSet<char>(patterns[0][seven].ToArray());
                var opot = new HashSet<char>(patterns[0][one].ToArray());
                spot.ExceptWith(opot);
                //Console.WriteLine("a is " + spot.First());
                revmapping['a'].Remove(spot.First());
                //Console.WriteLine("What is left is " + revmapping['a'].First());

                mapping[revmapping['a'].First()] = new HashSet<char> { 'c' };
                mapping[spot.First()] = new HashSet<char> { 'a' };
                //Console.WriteLine("a? " + mapping[spot.First()].First());
                //Console.WriteLine("c? " + mapping[revmapping['a'].First()].First());

                foreach (char p in revmapping['d'])
                {
                    if (patterns[0][four].Contains(p))
                    {
                        //Console.WriteLine("Found it" + p);
                        revmapping['d'].Remove(p);
                        //Console.WriteLine("What is left is " + revmapping['d'].First());

                        mapping[revmapping['d'].First()] = new HashSet<char> { 'g' };
                        mapping[p] = new HashSet<char> { 'd' };
                        //Console.WriteLine("d? " + mapping[p].First());
                        //Console.WriteLine("g? " + mapping[revmapping['d'].First()].First());
                    }
                }

                foreach (char c in "abcdefg")
                {
                    //Console.WriteLine(c + " -> " + mapping[c].First());
                }

                string sd = "";
                for (int i = 0; i < patterns[1].Length; i++)
                {

                    HashSet<char> rew = new HashSet<char>();
                    foreach (char c in patterns[1][i])
                    {
                        rew.Add(mapping[c].First());
                    }

                    for (int d = 0; d < 10; d++)
                    {
                        if (digits[d].SetEquals(rew))
                        {
                            sd += d;
                        }
                    }
                }
                //Console.WriteLine(sd);
                count += Int32.Parse(sd);
            }
            return count;
        }

        static int day9part1(IEnumerable<string> input)
        {
            List<int[]> heightmap = new List<int[]>();
            foreach (string s in input)
            {
                heightmap.Add(s.ToCharArray().Select(s => Int32.Parse("" + s)).ToArray());
            }

            int lows = 0;

            List<Tuple<int, int>> orthogs = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(-1, 0),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(0, -1),
            };

            for (int i = 0; i < heightmap.Count(); i++)
            {
                for (int j = 0; j < heightmap[i].Length; j++)
                {

                    bool smallest = true;
                    foreach (Tuple<int, int> d in orthogs)
                    {
                        int nx = i + d.Item1;
                        int ny = j + d.Item2;
                        if (nx >= 0 && nx < heightmap.Count() &&
                            ny >= 0 && ny < heightmap[i].Length &&
                            heightmap[nx][ny] <= heightmap[i][j]) // why <=? not clear from program def
                        {
                            smallest = false;
                            break;
                        }


                    }
                    if (smallest)
                    {
                        lows += 1 + heightmap[i][j];
                        //Console.WriteLine(heightmap[i][j]);
                    }
                }
            }

            return lows;
        }

        // Flood fill time!
        static int day9part2(IEnumerable<string> input)
        {
            List<int[]> heightmap = new List<int[]>();
            foreach (string s in input)
            {
                heightmap.Add(s.ToCharArray().Select(s => Int32.Parse("" + s)).ToArray());
            }

            Dictionary<Tuple<int, int>, int> lows = new Dictionary<Tuple<int, int>, int>();

            List<Tuple<int, int>> orthogs = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(-1, 0),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(0, -1),
            };

            for (int i = 0; i < heightmap.Count(); i++)
            {
                for (int j = 0; j < heightmap[i].Length; j++)
                {

                    bool smallest = true;
                    foreach (Tuple<int, int> d in orthogs)
                    {
                        int nx = i + d.Item1;
                        int ny = j + d.Item2;
                        if (nx >= 0 && nx < heightmap.Count() &&
                            ny >= 0 && ny < heightmap[i].Length &&
                            heightmap[nx][ny] <= heightmap[i][j]) // why <=? not clear from program def
                        {
                            smallest = false;
                            break;
                        }
                    }
                    if (smallest)
                    {
                        // collect instead of add in
                        lows[new Tuple<int, int>(i, j)] = 1;
                        heightmap[i][j] = -1;
                        //Console.WriteLine(heightmap[i][j]);
                    }
                }
            }

            foreach (Tuple<int, int> b in lows.Keys)
            {
                Queue<Tuple<int, int>> next = new Queue<Tuple<int, int>>();
                next.Enqueue(b);
                while (next.Count() > 0)
                {
                    Tuple<int, int> where = next.Dequeue();
                    foreach (Tuple<int, int> d in orthogs)
                    {
                        int nx = where.Item1 + d.Item1;
                        int ny = where.Item2 + d.Item2;
                        if (nx >= 0 && nx < heightmap.Count() &&
                            ny >= 0 && ny < heightmap[0].Length &&
                            heightmap[nx][ny] >= 0 && heightmap[nx][ny] != 9)
                        {
                            next.Enqueue(new Tuple<int, int>(nx, ny));
                            heightmap[nx][ny] = -1;
                            lows[b] += 1;
                        }

                    }
                }
                //Console.WriteLine(lows[b]);
            }

            int sum = 1;
            int count = 0;
            var sortedbasins = lows.Values.OrderByDescending(x => x);
            foreach (int v in sortedbasins)
            {

                if (count < 3)
                {
                    //Console.WriteLine(v);
                    sum *= v;
                    count++;
                }
                else { break; }
            }
            return sum;
        }

        static int day10part1(IEnumerable<string> input)
        {
            Dictionary<char, int> points = new Dictionary<char, int> {
                {')', 3 },
                {']', 57 },
                {'}', 1197 },
                {'>', 25137 },
            };

            Dictionary<char, char> pairsrev = new Dictionary<char, char> {
                {')', '(' },
                {']', '[' },
                {'}', '{' },
                {'>', '<' },
            };

            int p = 0;
            foreach (string s in input)
            {
                Stack<char> st = new Stack<char>();
                foreach (char c in s)
                {
                    if (pairsrev.Keys.Contains(c))
                    {
                        if (st.Count() == 0 || st.Peek() != pairsrev[c])
                        {
                            p += points[c];
                            break;
                        }
                        else
                        {
                            st.Pop();
                        }
                    }
                    else
                    {
                        st.Push(c);
                    }
                }
            }

            return p;
        }

        static int day10part2(IEnumerable<string> input)
        {
            Dictionary<char, int> points = new Dictionary<char, int> {
                {'(', 1 },
                {'[', 2 },
                {'{', 3 },
                {'<', 4 },
            };

            Dictionary<char, char> pairsrev = new Dictionary<char, char> {
                {')', '(' },
                {']', '[' },
                {'}', '{' },
                {'>', '<' },
            };

            List<long> allpoints = new List<long>();
            foreach (string s in input)
            {
                bool valid = true;
                Stack<char> st = new Stack<char>();
                foreach (char c in s)
                {
                    if (pairsrev.Keys.Contains(c))
                    {
                        if (st.Count() == 0 || st.Peek() != pairsrev[c])
                        {
                            valid = false;
                            break;
                        }
                        else
                        {
                            st.Pop();
                        }
                    }
                    else
                    {
                        st.Push(c);
                    }
                }

                if (valid)
                {
                    //Console.WriteLine(string.Join("", st.ToArray()));
                    string comp = string.Join("", st.ToArray());
                    long v = 0;
                    foreach (char c in comp)
                    {
                        v *= 5;
                        v += points[c];
                    }
                    //Console.WriteLine(v);
                    allpoints.Add(v);
                }
            }
            allpoints.Sort();
            Console.WriteLine(allpoints[allpoints.Count() / 2]);
            return -1;
        }

        static int day11part1(IEnumerable<string> input)
        {

            Octopi g = new Octopi(input);

            int flashed = 0;

            for (int i = 0; i < 100; i++)
            {
                flashed += g.step();
                //Console.WriteLine(g);
            }

            Console.WriteLine(g);

            return flashed;
        }

        static int day11part2(IEnumerable<string> input)
        {

            Octopi g = new Octopi(input);

            int count = 1;
            while (g.step() != 100)
            {
                count++;
            }

            Console.WriteLine(g);

            return count;
        }


        static int day12part1(IEnumerable<string> input)
        {
            Dictionary<string, List<string>> edges = new Dictionary<string, List<string>>();

            foreach (string s in input)
            {
                string[] nodes = s.Split("-");
                if (!edges.ContainsKey(nodes[0]))
                {
                    edges[nodes[0]] = new List<string>();
                }
                if (!edges.ContainsKey(nodes[1]))
                {
                    edges[nodes[1]] = new List<string>();
                }

                edges[nodes[0]].Add(nodes[1]); // Don't use append
                edges[nodes[1]].Add(nodes[0]);
            }

            int enders = 0;
            Queue<Stack<string>> trails = new Queue<Stack<string>>();
            Stack<string> start = new Stack<string>();
            start.Push("start");
            trails.Enqueue(start);
            while (trails.Count() != 0)
            {
                Stack<string> t = trails.Dequeue();
                if (t.Peek() == "end")
                {
                    enders++;
                    //Console.WriteLine(string.Join(",", t.Reverse()));
                }
                else
                {
                    foreach (string c in edges[t.Peek()])
                    {
                        //Console.WriteLine(c);
                        if (c.All(char.IsUpper) || !t.Contains(c))
                        {
                            Stack<string> p = new Stack<string>(new Stack<string>(t));
                            p.Push(c);
                            trails.Enqueue(p);
                        }
                    }
                }
            }

            return enders;
        }

        static int day12part2(IEnumerable<string> input)
        {
            Dictionary<string, List<string>> edges = new Dictionary<string, List<string>>();

            foreach (string s in input)
            {
                string[] nodes = s.Split("-");
                if (!edges.ContainsKey(nodes[0]))
                {
                    edges[nodes[0]] = new List<string>();
                }
                if (!edges.ContainsKey(nodes[1]))
                {
                    edges[nodes[1]] = new List<string>();
                }

                edges[nodes[0]].Add(nodes[1]); // Don't use append
                edges[nodes[1]].Add(nodes[0]);
            }

            int enders = 0;
            Queue<Stack<string>> trails = new Queue<Stack<string>>();
            Stack<string> start = new Stack<string>();
            start.Push("start");
            trails.Enqueue(start);
            while (trails.Count() != 0)
            {
                Stack<string> t = trails.Dequeue();
                if (t.Peek() == "end")
                {
                    enders++;
                    //Console.WriteLine(string.Join(",", t.Reverse()));
                }
                else
                {
                    foreach (string c in edges[t.Peek()])
                    {
                        //Console.WriteLine(c);
                        if (c != "start" && (c.All(char.IsUpper) || !t.Contains(c) || noLowerRepeats(t)))
                        {
                            Stack<string> p = new Stack<string>(new Stack<string>(t));
                            p.Push(c);
                            trails.Enqueue(p);
                        }
                    }
                }
            }

            return enders;
        }

        static bool noLowerRepeats(Stack<string> st)
        {
            HashSet<string> nodes = new HashSet<string>();
            foreach (string s in st)
            {
                if (s.All(char.IsLower) && nodes.Contains(s))
                {
                    return false;
                }
                else
                {
                    nodes.Add(s);
                }
            }
            return true;
        }

        static int day13part1(IEnumerable<string> input)
        {
            HashSet<Tuple<int, int>> points = new HashSet<Tuple<int, int>>();
            bool folding = false;
            foreach (string s in input)
            {
                if (s == "")
                {
                    folding = true;
                }
                else if (folding)
                {
                    string[] fold = s.Split("=");
                    int where = Int32.Parse(fold[1]);
                    if (fold[0].Last() == 'x')
                    {
                        HashSet<Tuple<int, int>> points2 = new HashSet<Tuple<int, int>>();
                        foreach (Tuple<int, int> p in points)
                        {
                            if (p.Item1 > where)
                            {
                                points2.Add(new Tuple<int, int>(where - (p.Item1 - where), p.Item2));
                            }
                            else
                            {
                                points2.Add(p);
                            }
                        }
                        points = points2;
                    }
                    else
                    {
                        HashSet<Tuple<int, int>> points2 = new HashSet<Tuple<int, int>>();
                        foreach (Tuple<int, int> p in points)
                        {
                            if (p.Item2 > where)
                            {
                                points2.Add(new Tuple<int, int>(p.Item1, where - (p.Item2 - where)));
                            }
                            else
                            {
                                points2.Add(p);
                            }
                        }
                        points = points2;

                    }
                    break; // For part 1
                }
                else
                {
                    int[] p = s.Split(",").Select(s => Int32.Parse(s)).ToArray();
                    points.Add(new Tuple<int, int>(p[0], p[1]));
                }
            }


            return points.Count();
        }

        static int day13part2(IEnumerable<string> input)
        {
            HashSet<Tuple<int, int>> points = new HashSet<Tuple<int, int>>();
            bool folding = false;

            foreach (string s in input)
            {
                if (s == "")
                {
                    folding = true;
                }
                else if (folding)
                {
                    string[] fold = s.Split("=");
                    int where = Int32.Parse(fold[1]);
                    if (fold[0].Last() == 'x')
                    {
                        HashSet<Tuple<int, int>> points2 = new HashSet<Tuple<int, int>>();
                        foreach (Tuple<int, int> p in points)
                        {
                            if (p.Item1 > where)
                            {
                                points2.Add(new Tuple<int, int>(where - (p.Item1 - where), p.Item2));
                            }
                            else
                            {
                                points2.Add(p);
                            }
                        }
                        points = points2;
                    }
                    else
                    {
                        HashSet<Tuple<int, int>> points2 = new HashSet<Tuple<int, int>>();
                        foreach (Tuple<int, int> p in points)
                        {
                            if (p.Item2 > where)
                            {
                                points2.Add(new Tuple<int, int>(p.Item1, where - (p.Item2 - where)));
                            }
                            else
                            {
                                points2.Add(p);
                            }
                        }
                        points = points2;

                    }
                }
                else
                {
                    int[] p = s.Split(",").Select(s => Int32.Parse(s)).ToArray();
                    points.Add(new Tuple<int, int>(p[0], p[1]));
                }
            }

            int maxx = 0;
            int maxy = 0;
            foreach (Tuple<int, int> p in points)
            {
                maxx = Math.Max(maxx, p.Item1);
                maxy = Math.Max(maxy, p.Item2);
            }

            bool[,] letters = new bool[maxy + 1, maxx + 1];
            foreach (Tuple<int, int> p in points)
            {
                letters[p.Item2, p.Item1] = true;
            }

            string message = "";
            for (int i = 0; i < maxy + 1; i++)
            {
                for (int j = 0; j < maxx + 1; j++)
                {
                    if (letters[i, j])
                    {
                        message += "#";
                    }
                    else
                    {
                        message += " ";
                    }
                }
                message += "\n";
            }


            Console.WriteLine(message);
            return points.Count();
        }

        static int day14part1(IEnumerable<string> input)
        {
            Dictionary<string, Tuple<string, string>> rules = new Dictionary<string, Tuple<string, string>>();

            string start = "";

            bool second = false;
            foreach (string s in input)
            {
                if (s == "")
                {
                    second = true;
                } else if (second)
                {
                    string[] r = s.Split(" -> ");
                    rules[r[0]] = new Tuple<string, string>(r[0][0] + r[1], r[1] + r[0][1]);
                } else
                {
                    start = s;
                }
            }

            Dictionary<string, int> counts = new Dictionary<string, int>();

            for (int i = 0; i < start.Length - 1; i++)
            {
                string sub = start.Substring(i, 2);
                if (!counts.ContainsKey(sub))
                {
                    counts[sub] = 0;
                }
                counts[sub] += 1;
            }

            for (int i = 0; i < 10; i++)
            {
                Dictionary<string, int> counts2 = new Dictionary<string, int>();

                string[] curkeys = counts.Keys.ToArray();
                foreach (String let in curkeys)
                {
                    if (rules.ContainsKey(let))
                    {
                        int c = counts[let];

                        if (!counts2.ContainsKey(rules[let].Item1))
                        {
                            counts2[rules[let].Item1] = 0;
                        }
                        counts2[rules[let].Item1] += c;

                        if (!counts2.ContainsKey(rules[let].Item2))
                        {
                            counts2[rules[let].Item2] = 0;
                        }
                        counts2[rules[let].Item2] += c;
                    } else
                    {
                        Console.WriteLine("ERROR!");
                    }
                }

                counts = counts2;
            }

            Dictionary<char, int> letters = new Dictionary<char, int>();
            foreach(String let in counts.Keys)
            {
                if (!letters.ContainsKey(let[0]))
                {
                    letters[let[0]] = 0;
                }
                letters[let[0]] += counts[let];
            }
            char lett = start[start.Length - 1];
            if (!letters.ContainsKey(lett))
            {
                letters[lett] = 0;
            }
            letters[lett] += 1;

            int max = 0;
            int min = Int32.MaxValue;
            foreach (char c in letters.Keys)
            {
                Console.WriteLine(c + " = " + letters[c]);
                max = Math.Max(letters[c], max);
                min = Math.Min(letters[c], min);
            }

            return max - min;
        }


        static int day14part2(IEnumerable<string> input)
        {
            Dictionary<string, Tuple<string, string>> rules = new Dictionary<string, Tuple<string, string>>();

            string start = "";

            bool second = false;
            foreach (string s in input)
            {
                if (s == "")
                {
                    second = true;
                }
                else if (second)
                {
                    string[] r = s.Split(" -> ");
                    rules[r[0]] = new Tuple<string, string>(r[0][0] + r[1], r[1] + r[0][1]);
                }
                else
                {
                    start = s;
                }
            }

            Dictionary<string, long> counts = new Dictionary<string, long>();

            for (int i = 0; i < start.Length - 1; i++)
            {
                string sub = start.Substring(i, 2);
                if (!counts.ContainsKey(sub))
                {
                    counts[sub] = 0;
                }
                counts[sub] += 1;
            }

            for (int i = 0; i < 40; i++)
            {
                Dictionary<string, long> counts2 = new Dictionary<string, long>();

                string[] curkeys = counts.Keys.ToArray();
                foreach (String let in curkeys)
                {
                    if (rules.ContainsKey(let))
                    {
                        long c = counts[let];

                        if (!counts2.ContainsKey(rules[let].Item1))
                        {
                            counts2[rules[let].Item1] = 0;
                        }
                        counts2[rules[let].Item1] += c;

                        if (!counts2.ContainsKey(rules[let].Item2))
                        {
                            counts2[rules[let].Item2] = 0;
                        }
                        counts2[rules[let].Item2] += c;
                    }
                    else
                    {
                        Console.WriteLine("ERROR!");
                    }
                }

                counts = counts2;
            }

            Dictionary<char, long> letters = new Dictionary<char, long>();
            foreach (String let in counts.Keys)
            {
                if (!letters.ContainsKey(let[0]))
                {
                    letters[let[0]] = 0;
                }
                letters[let[0]] += counts[let];
            }
            char lett = start[start.Length - 1];
            if (!letters.ContainsKey(lett))
            {
                letters[lett] = 0;
            }
            letters[lett] += 1;

            long max = 0;
            long min = Int64.MaxValue;
            foreach (char c in letters.Keys)
            {
                Console.WriteLine(c + " = " + letters[c]);
                max = Math.Max(letters[c], max);
                min = Math.Min(letters[c], min);
            }

            Console.WriteLine(max - min);
            return -1;
        }
    }
}
