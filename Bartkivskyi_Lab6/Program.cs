using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Bartkivkyi_Lab6_ASD
{
    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Лабораторна робота - Комплексна програма роботи з деревами та геометрією");

            while (true)
            {
                Console.WriteLine("\nМЕНЮ");
                Console.WriteLine("1. ЧЧ-Дерево: Тестова послідовна вставка ключів (41, 38, 31, 12, 19, 8)");
                Console.WriteLine("2. ЧЧ-Дерево: Демонстрація Вставки, Видалення, Перефарбування та Поворотів");
                Console.WriteLine("3. Дерево відрізків: LEFT-ROTATE з оновленням max за O(1)");
                Console.WriteLine("4. Геометрія: Перевірка перекриття множини прямокутників");
                Console.WriteLine("5. БСП-Дерево: Вставка та Видалення вузла");
                Console.WriteLine("6. АВЛ-Дерево: Вставка, Видалення та Балансуючі повороти");
                Console.WriteLine("7. Splay-Дерево: Вставка, Видалення та Splay-алгоритм");
                Console.WriteLine("0. Вихід");
                Console.Write("Виберіть опцію: ");

                if (!byte.TryParse(Console.ReadLine(), out byte option) || option == 0) break;

                switch (option)
                {
                    case 1: RBSequentialInsert(); break;
                    case 2: RedBlackFull(); break;
                    case 3: IntervalTree(); break;
                    case 4: RectangleOverlap(); break;
                    case 5: BSTree(); break;
                    case 6: AVLTree(); break;
                    case 7: SplayTree(); break;
                    default: Console.WriteLine("Невірний вибір!"); break;
                }
            }
        }

        private static void TimeAction(string label, Action action)
        {
            var sw = Stopwatch.StartNew();
            action();
            Console.WriteLine($"{label}: Виконано за {sw.Elapsed.TotalMilliseconds} мс");
        }

        private static void RBSequentialInsert()
        {
            var tree = new RBTree();
            int[] keys = { 41, 38, 31, 12, 19, 8 };
            Console.WriteLine("Послідовна вставка ключів: 41, 38, 31, 12, 19, 8");
            foreach (int key in keys) tree.Insert(key);
            Console.WriteLine($"Дерево побудовано. Корінь дерева: {tree.GetRootValue()}");
        }

        private static void RedBlackFull()
        {
            var tree = new RBTree();
            TimeAction("Вставка вузлів у ЧЧ-Дерево", () => {
                tree.Insert(15); tree.Insert(10); tree.Insert(20); tree.Insert(5);
            });
            Console.WriteLine($"Корінь після вставки: {tree.GetRootValue()}");

            var node = tree.Insert(8);
            tree.ToggleSiblingColors(node);

            TimeAction("Видалення вузла з ЧЧ-Дерева", () => tree.Delete(10));
            Console.WriteLine($"Новий корінь після видалення вузла 10: {tree.GetRootValue()}");
        }

        private static void IntervalTree()
        {
            var tree = new IntervalTree();
            var x = new IntervalNode(15, 20, 30);
            var y = new IntervalNode(10, 30, 30) { Parent = x };
            x.Right = y;

            Console.WriteLine($"До LEFT-ROTATE: x.Max = {x.Max}, y.Max = {y.Max}");
            tree.RotateLeft(x);
            Console.WriteLine($"Після LEFT-ROTATE: x.Max = {x.Max}, y.Max = {y.Max}");
        }

        private static void RectangleOverlap()
        {
            var rects = new List<Rectangle>
        {
            new Rectangle(0, 0, 10, 10),
            new Rectangle(20, 20, 30, 30),
            new Rectangle(5, 5, 15, 15)
        };
            Console.WriteLine($"Чи є перекриття у множині прямокутників? {RectangleHelper.CheckOverlap(rects)}");
        }

        private static void BSTree()
        {
            var tree = new BSTree();
            tree.Insert(50); tree.Insert(30); tree.Insert(70);
            Console.WriteLine($"БСП Корінь: {tree.Root?.Value}");
            tree.Delete(50);
            Console.WriteLine($"БСП Корінь після видалення 50: {tree.Root?.Value}");
        }

        private static void AVLTree()
        {
            var tree = new AVLTree();
            TimeAction("Вставка в АВЛ", () => {
                foreach (int key in new[] { 10, 20, 30, 40, 50, 25 }) tree.Insert(key);
            });
            Console.WriteLine($"АВЛ Корінь: {tree.Root.Value}");
            tree.Delete(30);
            Console.WriteLine($"АВЛ Корінь після видалення 30: {tree.Root.Value}");
        }

        private static void SplayTree()
        {
            var tree = new SplayTree();
            tree.Insert(10); tree.Insert(20); tree.Insert(30);
            Console.WriteLine($"Splay Корінь після вставки 30: {tree.Root.Value}");
            tree.Delete(20);
            Console.WriteLine($"Splay Корінь після видалення 20 (піднятий елемент): {tree.Root.Value}");
        }
    }

    public enum NodeColor { Red, Black }

    public class RBNode
    {
        public int Value;
        public NodeColor Color = NodeColor.Red;
        public RBNode Left, Right, Parent;
        public RBNode(int value) => Value = value;
    }

    public class RBTree
    {
        private readonly RBNode TNULL;
        private RBNode root;

        public RBTree()
        {
            TNULL = new RBNode(0) { Color = NodeColor.Black };
            root = TNULL;
        }

        public string GetRootValue() => root == TNULL ? "Порожнє" : root.Value.ToString();

        public void RotateLeft(RBNode x)
        {
            var y = x.Right;
            x.Right = y.Left;
            if (y.Left != TNULL) y.Left.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null) root = y;
            else if (x == x.Parent.Left) x.Parent.Left = y;
            else x.Parent.Right = y;
            y.Left = x;
            x.Parent = y;
        }

        public void RotateRight(RBNode x)
        {
            var y = x.Left;
            x.Left = y.Right;
            if (y.Right != TNULL) y.Right.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null) root = y;
            else if (x == x.Parent.Right) x.Parent.Right = y;
            else x.Parent.Left = y;
            y.Right = x;
            x.Parent = y;
        }

        public RBNode Insert(int value)
        {
            var z = new RBNode(value) { Left = TNULL, Right = TNULL };
            RBNode y = null, x = root;

            while (x != TNULL)
            {
                y = x;
                x = z.Value < x.Value ? x.Left : x.Right;
            }

            z.Parent = y;
            if (y == null) root = z;
            else if (z.Value < y.Value) y.Left = z;
            else y.Right = z;

            if (z.Parent == null) { z.Color = NodeColor.Black; return z; }
            if (z.Parent.Parent == null) return z;

            InsertFixup(z);
            return z;
        }

        private void InsertFixup(RBNode k)
        {
            while (k.Parent.Color == NodeColor.Red)
            {
                if (k.Parent == k.Parent.Parent.Right)
                {
                    var u = k.Parent.Parent.Left;
                    if (u.Color == NodeColor.Red)
                    {
                        u.Color = pColor(k, NodeColor.Black);
                        k.Parent.Parent.Color = NodeColor.Red;
                        k = k.Parent.Parent;
                    }
                    else
                    {
                        if (k == k.Parent.Left) { k = k.Parent; RotateRight(k); }
                        k.Parent.Color = NodeColor.Black;
                        k.Parent.Parent.Color = NodeColor.Red;
                        RotateLeft(k.Parent.Parent);
                    }
                }
                else
                {
                    var u = k.Parent.Parent.Right;
                    if (u.Color == NodeColor.Red)
                    {
                        u.Color = pColor(k, NodeColor.Black);
                        k.Parent.Parent.Color = NodeColor.Red;
                        k = k.Parent.Parent;
                    }
                    else
                    {
                        if (k == k.Parent.Right) { k = k.Parent; RotateLeft(k); }
                        k.Parent.Color = NodeColor.Black;
                        k.Parent.Parent.Color = NodeColor.Red;
                        RotateRight(k.Parent.Parent);
                    }
                }
                if (k == root) break;
            }
            root.Color = NodeColor.Black;

            NodeColor pColor(RBNode node, NodeColor c) => node.Parent.Color = c;
        }

        public void Delete(int data)
        {
            var z = TNULL;
            var x = root;
            while (x != TNULL)
            {
                if (x.Value == data) z = x;
                x = x.Value <= data ? x.Right : x.Left;
            }
            if (z == TNULL) return;

            RBNode y = z, xNode;
            var yOriginalColor = y.Color;
            if (z.Left == TNULL) { xNode = z.Right; Transplant(z, z.Right); }
            else if (z.Right == TNULL) { xNode = z.Left; Transplant(z, z.Left); }
            else
            {
                y = z.Right;
                while (y.Left != TNULL) y = y.Left;
                yOriginalColor = y.Color;
                xNode = y.Right;
                if (y.Parent == z) xNode.Parent = y;
                else { Transplant(y, y.Right); y.Right = z.Right; y.Right.Parent = y; }
                Transplant(z, y);
                y.Left = z.Left;
                y.Left.Parent = y;
                y.Color = z.Color;
            }
            if (yOriginalColor == NodeColor.Black) DeleteFixup(xNode);
        }

        private void Transplant(RBNode u, RBNode v)
        {
            if (u.Parent == null) root = v;
            else if (u == u.Parent.Left) u.Parent.Left = v;
            else u.Parent.Right = v;
            v.Parent = u.Parent;
        }

        private void DeleteFixup(RBNode x)
        {
            while (x != root && x.Color == NodeColor.Black)
            {
                if (x == x.Parent.Left)
                {
                    var w = x.Parent.Right;
                    if (w.Color == NodeColor.Red) { w.Color = NodeColor.Black; x.Parent.Color = NodeColor.Red; RotateLeft(x.Parent); w = x.Parent.Right; }
                    if (w.Left.Color == NodeColor.Black && w.Right.Color == NodeColor.Black) { w.Color = NodeColor.Red; x = x.Parent; }
                    else
                    {
                        if (w.Right.Color == NodeColor.Black) { w.Left.Color = NodeColor.Black; w.Color = NodeColor.Red; RotateRight(w); w = x.Parent.Right; }
                        w.Color = x.Parent.Color; x.Parent.Color = NodeColor.Black; w.Right.Color = NodeColor.Black; RotateLeft(x.Parent); x = root;
                    }
                }
                else
                {
                    var w = x.Parent.Left;
                    if (w.Color == NodeColor.Red) { w.Color = NodeColor.Black; x.Parent.Color = NodeColor.Red; RotateRight(x.Parent); w = x.Parent.Left; }
                    if (w.Right.Color == NodeColor.Black && w.Left.Color == NodeColor.Black) { w.Color = NodeColor.Red; x = x.Parent; }
                    else
                    {
                        if (w.Left.Color == NodeColor.Black) { w.Right.Color = NodeColor.Black; w.Color = NodeColor.Red; RotateLeft(w); w = x.Parent.Left; }
                        w.Color = x.Parent.Color; x.Parent.Color = NodeColor.Black; w.Left.Color = NodeColor.Black; RotateRight(x.Parent); x = root;
                    }
                }
            }
            x.Color = NodeColor.Black;
        }

        public void ToggleSiblingColors(RBNode node)
        {
            if (node?.Parent == null) return;
            var sibling = node == node.Parent.Left ? node.Parent.Right : node.Parent.Left;
            if (sibling != null && sibling != TNULL && sibling.Color == node.Color)
                node.Color = sibling.Color = node.Color == NodeColor.Red ? NodeColor.Black : NodeColor.Red;
        }
    }

    public class IntervalNode
    {
        public int Low, High, Max;
        public IntervalNode Left, Right, Parent;
        public IntervalNode(int low, int high, int max) { Low = low; High = high; Max = max; }
    }

    public class IntervalTree
    {
        public void RotateLeft(IntervalNode x)
        {
            var y = x.Right;
            if (y == null) return;

            x.Right = y.Left;
            if (y.Left != null) y.Left.Parent = x;
            y.Parent = x.Parent;

            y.Left = x;
            x.Parent = y;

            y.Max = x.Max; // y займає позицію x, копіює його колишній максимум
            x.Max = Math.Max(x.High, Math.Max(x.Left?.Max ?? int.MinValue, x.Right?.Max ?? int.MinValue)); // O(1) перерахунок для x
        }
    }

    public struct Rectangle
    {
        public int X1, Y1, X2, Y2;
        public Rectangle(int x1, int y1, int x2, int y2) { X1 = x1; Y1 = y1; X2 = x2; Y2 = y2; }
    }

    public static class RectangleHelper
    {
        public static bool CheckOverlap(List<Rectangle> rects)
        {
            for (int i = 0; i < rects.Count; i++)
                for (int j = i + 1; j < rects.Count; j++)
                    if (!(rects[i].X2 <= rects[j].X1 || rects[i].X1 >= rects[j].X2 || rects[i].Y2 <= rects[j].Y1 || rects[i].Y1 >= rects[j].Y2))
                        return true;
            return false;
        }
    }

    public class BSTNode
    {
        public int Value;
        public BSTNode Left, Right;
        public BSTNode(int value) => Value = value;
    }

    public class BSTree
    {
        public BSTNode Root;

        public void Insert(int value) => Root = InsertRec(Root, value);
        private BSTNode InsertRec(BSTNode root, int val) => root == null ? new BSTNode(val) : (val < root.Value ? root.Left = InsertRec(root.Left, val) : root.Right = InsertRec(root.Right, val), root).root;

        public void Delete(int value) => Root = DeleteRec(Root, value);
        private BSTNode DeleteRec(BSTNode root, int val)
        {
            if (root == null) return null;
            if (val < root.Value) root.Left = DeleteRec(root.Left, val);
            else if (val > root.Value) root.Right = DeleteRec(root.Right, val);
            else
            {
                if (root.Left == null) return root.Right;
                if (root.Right == null) return root.Left;
                var minNode = root.Right;
                while (minNode.Left != null) minNode = minNode.Left;
                root.Value = minNode.Value;
                root.Right = DeleteRec(root.Right, minNode.Value);
            }
            return root;
        }
    }

    public class AVLNode
    {
        public int Value, Height = 1;
        public AVLNode Left, Right;
        public AVLNode(int value) => Value = value;
    }

    public class AVLTree
    {
        public AVLNode Root;

        private int H(AVLNode n) => n?.Height ?? 0;
        private int GetB(AVLNode n) => n == null ? 0 : H(n.Left) - H(n.Right);

        private AVLNode RotateRight(AVLNode y)
        {
            var x = y.Left; var T2 = x.Right;
            x.Right = y; y.Left = T2;
            y.Height = Math.Max(H(y.Left), H(y.Right)) + 1;
            x.Height = Math.Max(H(x.Left), H(x.Right)) + 1;
            return x;
        }

        private AVLNode RotateLeft(AVLNode x)
        {
            var y = x.Right; var T2 = y.Left;
            y.Left = x; x.Right = T2;
            x.Height = Math.Max(H(x.Left), H(x.Right)) + 1;
            y.Height = Math.Max(H(y.Left), H(y.Right)) + 1;
            return y;
        }

        public void Insert(int key) => Root = Ins(Root, key);
        private AVLNode Ins(AVLNode node, int key)
        {
            if (node == null) return new AVLNode(key);
            if (key < node.Value) node.Left = Ins(node.Left, key);
            else if (key > node.Value) node.Right = Ins(node.Right, key);
            else return node;

            node.Height = 1 + Math.Max(H(node.Left), H(node.Right));
            int b = GetB(node);

            if (b > 1 && key < node.Left.Value) return RotateRight(node);
            if (b < -1 && key > node.Right.Value) return RotateLeft(node);
            if (b > 1 && key > node.Left.Value) { node.Left = RotateLeft(node.Left); return RotateRight(node); }
            if (b < -1 && key < node.Right.Value) { node.Right = RotateRight(node.Right); return RotateLeft(node); }
            return node;
        }

        public void Delete(int key) => Root = Del(Root, key);
        private AVLNode Del(AVLNode root, int key)
        {
            if (root == null) return null;
            if (key < root.Value) root.Left = Del(root.Left, key);
            else if (key > root.Value) root.Right = Del(root.Right, key);
            else
            {
                if (root.Left == null || root.Right == null) root = root.Left ?? root.Right;
                else
                {
                    var temp = root.Right;
                    while (temp.Left != null) temp = temp.Left;
                    root.Value = temp.Value;
                    root.Right = Del(root.Right, temp.Value);
                }
            }
            if (root == null) return null;

            root.Height = Math.Max(H(root.Left), H(root.Right)) + 1;
            int b = GetB(root);

            if (b > 1 && GetB(root.Left) >= 0) return RotateRight(root);
            if (b > 1 && GetB(root.Left) < 0) { root.Left = RotateLeft(root.Left); return RotateRight(root); }
            if (b < -1 && GetB(root.Right) <= 0) return RotateLeft(root);
            if (b < -1 && GetB(root.Right) > 0) { root.Right = RotateRight(root.Right); return RotateLeft(root); }
            return root;
        }
    }

    public class SplayNode
    {
        public int Value;
        public SplayNode Left, Right;
        public SplayNode(int value) => Value = value;
    }

    public class SplayTree
    {
        public SplayNode Root;

        private SplayNode RotateRight(SplayNode x)
        {
            var y = x.Left; x.Left = y.Right; y.Right = x; return y;
        }

        private SplayNode RotateLeft(SplayNode x)
        {
            var y = x.Right; x.Right = y.Left; y.Left = x; return y;
        }

        public SplayNode Splay(SplayNode root, int key)
        {
            if (root == null || root.Value == key) return root;

            if (root.Value > key)
            {
                if (root.Left == null) return root;
                if (root.Left.Value > key)
                {
                    root.Left.Left = Splay(root.Left.Left, key);
                    root = RotateRight(root);
                }
                else if (root.Left.Value < key)
                {
                    root.Left.Right = Splay(root.Left.Right, key);
                    if (root.Left.Right != null) root.Left = RotateLeft(root.Left);
                }
                return root.Left == null ? root : RotateRight(root);
            }
            else
            {
                if (root.Right == null) return root;
                if (root.Right.Value > key)
                {
                    root.Right.Left = Splay(root.Right.Left, key);
                    if (root.Right.Left != null) root.Right = RotateRight(root.Right);
                }
                else if (root.Right.Value < key)
                {
                    root.Right.Right = Splay(root.Right.Right, key);
                    root = RotateLeft(root);
                }
                return root.Right == null ? root : RotateLeft(root);
            }
        }

        public void Insert(int key)
        {
            if (Root == null) { Root = new SplayNode(key); return; }
            Root = Splay(Root, key);
            if (Root.Value == key) return;

            var n = new SplayNode(key);
            if (Root.Value > key) { n.Right = Root; n.Left = Root.Left; Root.Left = null; }
            else { n.Left = Root; n.Right = Root.Right; Root.Right = null; }
            Root = n;
        }

        public void Delete(int key)
        {
            if (Root == null) return;
            Root = Splay(Root, key);
            if (Root.Value != key) return;

            if (Root.Left == null) Root = Root.Right;
            else
            {
                var temp = Root.Right;
                Root = Splay(Root.Left, key);
                Root.Right = temp;
            }
        }
    }
}