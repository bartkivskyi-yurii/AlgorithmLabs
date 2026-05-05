using System;
using System.Collections.Generic;
using System.Text;

namespace Bartkivskyi_Lab5_ASD
{
    internal class TreeFunctions
    {
        public void PreOrder(Node root)
        {
            if (root != null)
            {
                Console.Write(root.Key + " ");
                PreOrder(root.Left);
                PreOrder(root.Right);
            }
        }
        public void PostOrder(Node root)
        {
            if (root != null)
            {
                PostOrder(root.Left);
                PostOrder(root.Right);
                Console.Write(root.Key + " ");
            }
        }
        public void InOrderIterative(Node root)
        {
            Stack<Node> stack = new Stack<Node>();
            Node current = root;

            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }

                current = stack.Pop();
                Console.Write(current.Key + " ");

                current = current.Right;
            }
        }
        public Node TreeMinimum(Node node)
        {
            if (node == null) return null;
            if (node.Left == null) return node;

            return TreeMinimum(node.Left);
        }
        public Node TreeMaximum(Node node)
        {
            if (node == null) return null;
            if (node.Right == null) return node;

            return TreeMaximum(node.Right);
        }
        public Node TreePredecessor(Node node)
        {
            if (node == null) return null;

            if (node.Left != null)
            {
                return TreeMaximum(node.Left);
            }

            Node parent = node.Parent;
            while (parent != null && node == parent.Left)
            {
                node = parent;
                parent = parent.Parent;
            }

            return parent;
        }
        public Node TreeInsert(Node root, int key, Node parent = null)
        {
            if (root == null)
            {
                Node newNode = new Node(key);
                newNode.Parent = parent;
                return newNode;
            }

            if (key < root.Key)
            {
                root.Left = TreeInsert(root.Left, key, root);
            }
            else if (key > root.Key)
            {
                root.Right = TreeInsert(root.Right, key, root);
            }

            return root;
        }
    }
}