using System;
using System.Collections.Generic;
using System.Linq;

namespace AoCSharp
{
    public class BingoBoard
    {
        private int[] marked = new int[10];
        private Dictionary<int, Tuple<int, int>> numbers = new Dictionary<int, Tuple<int, int>>();

        public BingoBoard(int[] members)
        {
            if (members.Distinct<int>().Count() != 25)
            {
                Console.WriteLine("EEERRRROOORRR!");
            }
            int i = 0;
            int j = 0;
            foreach (int m in members)
            {
                numbers[m] = new Tuple<int, int>(i, j);
                j++;
                j %= 5;
                if (j == 0)
                {
                    i++;
                }
            }
        }

        public int mark(int num)
        {
            if (numbers.ContainsKey(num))
            {
                Tuple<int, int> value = numbers[num];
                marked[value.Item1]++;
                marked[5 + value.Item2]++;
                numbers.Remove(num);
                if (marked[value.Item1] == 5 || marked[value.Item2] == 5)
                {
                    Console.WriteLine("" + num + "*(" + string.Join('+', numbers.Keys.ToArray()) + ")");
                    return num * numbers.Keys.Sum();
                }
            }
            return -1;
        }
    }
}
