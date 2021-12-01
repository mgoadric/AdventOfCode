using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-a-text-file-one-line-at-a-time

            string path = "/Users/goadrich/Github/AdventOfCode/2021/data/";

            Console.WriteLine("1.1 sample: " +
                day1part1(File.ReadLines(path + "input1sample.txt")));
            Console.WriteLine("1.1 full: " +
                day1part1(File.ReadLines(path + "input1.txt")));

            Console.WriteLine("1.2 sample: " +
                day1part2(File.ReadLines(path + "input1sample.txt")));
            Console.WriteLine("1.2 full: " +
                day1part2(File.ReadLines(path + "input1.txt")));
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
                if (q.Count > window) {
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
    }
}
