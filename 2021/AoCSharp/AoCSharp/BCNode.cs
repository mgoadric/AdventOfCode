using System;
namespace AoCSharp
{
    public class BCNode
    {
        public static int nextID = 0;
        public int ID;
        public char bit;
        public int length;
        public int count;
        public BCNode left;
        public BCNode right;

        public BCNode(char bit, int length)
        {
            this.bit = bit;
            this.length = length;
            ID = nextID++;
        }

        public bool isLeaf()
        {
            return left == null && right == null;
        }

        public void insert(String s)
        {
            if (s[0] == '0')
            {
                if (left == null)
                {
                    left = new BCNode(s[0], s.Length);
                }
                left.count += 1;
                if (s.Length > 1)
                {
                    left.insert(s.Substring(1));
                }
            }
            else
            {
                if (right == null)
                {
                    right = new BCNode(s[0], s.Length);
                }
                right.count += 1;
                if (s.Length > 1)
                {
                    right.insert(s.Substring(1));
                  
                }

            }

        }

        public BCNode most()
        {
            if (left == null)
            {
                return right;
            }
            else if (right == null)
            {
                return left;
            }
            else
            {
                return left.count > right.count ? left : right;
            }
        }

        public BCNode least()
        {
            if (left == null)
            {
                return right;
            }
            else if (right == null)
            {
                return left;
            }
            else
            {
                return left.count <= right.count ? left : right;
            }
        }

        public int o2()
        {
            if (isLeaf())
            {
                return Int32.Parse("" + bit);
            }
            else
            {
                BCNode m = most();
                return (Int32.Parse("" + bit) << (length - 1)) + m.o2();
            }
        }

        public int co2()
        {
            if (isLeaf())
            {
                return Int32.Parse("" + bit);
            }
            else
            {
                BCNode m = least();
                return (Int32.Parse("" + bit) << (length - 1)) + m.co2();
            }
        }

        public string o2string()
        {
            if (isLeaf())
            {
                return "";
            }
            else
            {
                BCNode m = most();
                return "" + m.bit + m.o2string();
            }
        }

        public string co2string()
        {
            if (isLeaf())
            {
                return "";
            }
            else
            {
                BCNode m = least();
                return "" + m.bit + m.co2string();
            }
        }

        public string toDot(Path path)
        {
            string s = "";
            s += "Node" + ID + " [style=filled,shape=circle,width=" + (Math.Sqrt(count)) +
                (length != 6 ? ",label=\""+ count+"\"" : ",label=\"\"") +
                ",";
            s += (bit == '0' ? "fontcolor = white,fillcolor=red" : "fontcolor = black,fillcolor=white") + ", fontsize=" + 3 * (10 + length);
            s += "]\n";
            if (left != null)
            {
                
                    if ((path == Path.MOST || path == Path.BOTH) && left == most())
                    {
                    s += "Node" + ID + " -> " + "Node" + left.ID + " ";

                    s += "[penwidth=10]\n";
                        s += left.toDot(Path.MOST);
                    } else if ((path == Path.LEAST || path == Path.BOTH) && left == least())
                    {
                    s += "Node" + ID + " -> " + "Node" + left.ID + " ";

                    s += "[penwidth=10,style=dashed]\n";

                        s += left.toDot(Path.LEAST);
                    } else //if (path != Path.NONE)
                    {
                    s += "Node" + ID + " -> " + "Node" + left.ID + " ";

                    s += "\n" + left.toDot(Path.NONE);
                    }
                
            }
            if (right != null)
            {
                    if ((path == Path.MOST || path == Path.BOTH) && right == most())
                    {
                    s += "Node" + ID + " -> " + "Node" + right.ID + " ";

                    s += "[penwidth=10]\n";
                        s += right.toDot(Path.MOST);
                    }
                    else if ((path == Path.LEAST || path == Path.BOTH) && right == least())
                    {
                    s += "Node" + ID + " -> " + "Node" + right.ID + " ";

                    s += "[penwidth=10,style=dashed]\n";

                        s += right.toDot(Path.LEAST);
                    } else //if (path != Path.NONE)
                    {
                    s += "Node" + ID + " -> " + "Node" + right.ID + " ";

                    s += right.toDot(Path.NONE);
                    }
               
            }
            return s;
        }
    }

    public enum Path
    {
        BOTH, MOST, LEAST, NONE
    }
}
