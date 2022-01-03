using System;
using System.Collections.Generic;

namespace AoCSharp
{
    public class SnailNode
    {
        private SnailNode left;
        private SnailNode right;
        private int value;

        public SnailNode(string s) : this(new Queue<char>(s.ToCharArray())) { }

        public SnailNode(Queue<char> queue)
        {
            if (queue.Peek() == '[')
            {
                queue.Dequeue();
                left = new SnailNode(queue);
                queue.Dequeue();
                right = new SnailNode(queue);
                queue.Dequeue();
            } else
            {
                String toParse = "";
                while (queue.Peek() != ',' && queue.Peek() != ']')
                {
                    toParse += queue.Dequeue();
                }
                value = Int32.Parse(toParse);
            }
        }

        public SnailNode(SnailNode left, SnailNode right)
        {
            this.left = left;
            this.right = right;
        }

        public SnailNode(int value)
        {
            this.value = value;
        }

        public int Magnitude()
        {
            if (IsLeaf())
            {
                return value;
            } else
            {
                return 3 * left.Magnitude() + 2 * right.Magnitude();
            }
        }

        public bool IsLeaf()
        {
            return left == null && right == null;
        }

        public bool IsPair()
        {
            return left != null && right != null && left.IsLeaf() && right.IsLeaf();
        }

        public bool Reduce()
        {
            return Explode() || Split();
        }

        public bool Explode()
        {
            List<Tuple<SnailNode, SnailDirection>> p = new List<Tuple<SnailNode, SnailDirection>> {
                new Tuple<SnailNode, SnailDirection>(this, SnailDirection.ROOT) };
            return ExplodeHelper(p);
        }

        public bool ExplodeHelper(List<Tuple<SnailNode, SnailDirection>> parents)
        {
            if (parents.Count == 5 && !IsLeaf())
            {
                int leftadd = left.value;
                
                int i = 4;
                for (; i >= 0; i--)
                {
                    if (parents[i].Item2 == SnailDirection.RIGHT)
                    {
                        break;
                    }
                }
                if (i != -1)
                {
                    SnailNode start = parents[i].Item1.left;
                    while (!start.IsLeaf())
                    {
                        start = start.right;
                    }
                    start.value += leftadd;
                }

                int rightadd = right.value;

                i = 4;
                for (; i >= 0; i--)
                {
                    if (parents[i].Item2 == SnailDirection.LEFT)
                    {
                        break;
                    }
                }
                if (i != -1)
                {
                    SnailNode start = parents[i].Item1.right;
                    while (!start.IsLeaf())
                    {
                        start = start.left;
                    }
                    start.value += rightadd;
                }
                left = null;
                right = null;
                value = 0;
                return true;
            }
            else if (IsLeaf())
            {
                return false;
            }
            else
            {
                List<Tuple<SnailNode, SnailDirection>> pleft = new List<Tuple<SnailNode, SnailDirection>>(parents.ToArray());
                List<Tuple<SnailNode, SnailDirection>> pright = new List<Tuple<SnailNode, SnailDirection>>(parents.ToArray());
                pleft.Add(new Tuple<SnailNode, SnailDirection>(this, SnailDirection.LEFT));
                pright.Add(new Tuple<SnailNode, SnailDirection>(this, SnailDirection.RIGHT));
                return left.ExplodeHelper(pleft) || right.ExplodeHelper(pright);
            }
        }

        public bool Split()
        {
            if (IsLeaf() && value >= 10)
            {
                left = new SnailNode(value / 2);
                right = new SnailNode((value / 2) + (value % 2));
                value = 0;
                return true;
            } else if (IsLeaf())
            {
                return false;
            } else 
            {
                return left.Split() || right.Split(); 
            }
        }

        public SnailNode Add(SnailNode sn)
        {
            return new SnailNode(this, sn);
        }

        override
        public string ToString()
        {
            if (IsLeaf())
            {
                return "" + value;
            }
            return "[" + left + "," + right + "]";
        }
    }
}

