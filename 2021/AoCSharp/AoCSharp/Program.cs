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

            day3part2Viz(File.ReadLines(path + "input3sample.txt"));
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
                    } else
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
                } else
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

            Console.WriteLine("digraph {\n" + root + "}\n");

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
    }
}
