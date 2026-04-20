using System;
using System.Collections.Generic;
using System.Text;

namespace Bartkivskyi_Lab4_ASD
{
    internal class LinkedListQueue
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
        private Node tail;
        public LinkedListQueue()
        {
            head = null;
            tail = null;
        }
        public void Enqueue(int x)
        {
            Node newNode = new Node(x);

            if (tail == null)
            {
                head = newNode;
                tail = newNode;
                return;
            }

            tail.Next = newNode;
            tail = newNode;
        }
        public int Dequeue(int x)
        {
            if (head == null)
            {
                throw new InvalidOperationException("Спустошення черги (Underflow)");
            }

            int value = head.Data;
            head = head.Next;

            if (head == null)
            {
                tail = null;
            }

            return value;
        }
    }
}