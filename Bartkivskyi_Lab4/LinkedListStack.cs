using System;
using System.Collections.Generic;
using System.Text;

namespace Bartkivskyi_Lab4_ASD
{
    internal class LinkedListStack
    {
        private class Node
        {
            public int Data;
            public Node Next;
            public Node(int data)
            {
                Data = data;
                Next = null;
            }
        }
        private Node head;
        public LinkedListStack()
        {
            head = null;
        }
        public void Push(int x)
        {
            Node newNode = new Node(x);
            newNode.Next = head;
            head = newNode;
        }
        public int Pop()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Спустошення стека (Underflow)");
            }

            int value = head.Data;
            head = head.Next;

            return value;
        }
    }
}