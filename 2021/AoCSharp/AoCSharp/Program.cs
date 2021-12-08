using System;
using System.Collections.Generic;
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
            };

            for (int day = 1; day < (puzzles.Length / 2) + 1; day++)
            {
                for (int part = 1; part < 3; part++)
                {
                    Console.WriteLine(String.Format("Day {0,2} Part {1}", day, part));
                    Console.WriteLine(" sample: " +
                                  puzzles[day - 1, part - 1](File.ReadLines(path + String.Format("input{0}sample.txt", day))));
                    Console.WriteLine(" full: " +
                                  puzzles[day - 1, part - 1](File.ReadLines(path + String.Format("input{0}.txt", day))));
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
                    maping[c] = new HashSet<char> {'a', 'b', 'c', 'd', 'e', 'f', 'g'};
                }

                string[][] patterns = line.Split(" | ").Select(s => s.Split(" ")).ToArray();
                HashSet<int>[] potential = new HashSet<int>[patterns[1].Length];

                for (int i = 0; i < patterns[1].Length; i++) {
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
            return -1;
        }
    }
}