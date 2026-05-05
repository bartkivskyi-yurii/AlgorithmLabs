using System;
using System.Collections.Generic;
using System.Text;

namespace Bartkivskyi_Lab5_ASD
{
    internal class Node
    {
        public int Key;
        public Node Left;
        public Node Right;
        public Node Parent;
        public Node(int item)
        {
            Key = item;
            Left = Right = Parent = null;
        }
    }
}