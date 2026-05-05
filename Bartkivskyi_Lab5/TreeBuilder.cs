using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;

namespace Bartkivskyi_Lab5_ASD
{
    internal class TreeBuilder
    {
        public Node BuildTreeHeight2()
        {
            Node root = new Node(10);

            root.Left = new Node(4);
            root.Right = new Node(17);

            root.Left.Left = new Node(1);
            root.Left.Right = new Node(5);

            root.Right.Left = new Node(16);
            root.Right.Right = new Node(21);

            return root;
        }
        public Node BuildTreeHeight3()
        {
            Node root = new Node(5);

            root.Left = new Node(4);
            root.Right = new Node(16);

            root.Left.Left = new Node(1);

            root.Right.Left = new Node(10);
            root.Right.Right = new Node(17);

            root.Right.Right.Right = new Node(21);

            return root;
        }
        public Node BuildTreeHeight4()
        {
            Node root = new Node(4);

            root.Left = new Node(1);
            root.Right = new Node(10);

            root.Right.Left = new Node(5);
            root.Right.Right = new Node(16);

            root.Right.Right.Right = new Node(17);

            root.Right.Right.Right.Right = new Node(21);

            return root;
        }
        public Node BuildTreeHeight5()
        {
            Node root = new Node(4);

            root.Left = new Node(1);
            root.Right = new Node(5);

            root.Right.Right = new Node(10);

            root.Right.Right.Right = new Node(16);

            root.Right.Right.Right.Right = new Node(17);

            root.Right.Right.Right.Right.Right = new Node(21);

            return root;
        }
        public Node BuildTreeHeight6()
        {
            Node root = new Node(1);

            root.Right = new Node(4);

            root.Right.Right = new Node(5);

            root.Right.Right.Right = new Node(10);

            root.Right.Right.Right.Right = new Node(16);

            root.Right.Right.Right.Right.Right = new Node(17);

            root.Right.Right.Right.Right.Right.Right = new Node(21);

            return root;
        }
    }
}