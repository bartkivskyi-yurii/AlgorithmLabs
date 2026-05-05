using System;

namespace Bartkivskyi_Lab5_ASD
{
    class Program
    {
        public static void Main(string[] args)
        {
            TreeBuilder tree = new TreeBuilder();
            TreeFunctions treeF = new TreeFunctions();

            treeF.PreOrder(tree.BuildTreeHeight3());
            Console.WriteLine();
            treeF.PostOrder(tree.BuildTreeHeight6());

            Console.WriteLine();
            treeF.InOrderIterative(tree.BuildTreeHeight4());

            treeF.TreeMaximum(tree.BuildTreeHeight5());
            treeF.TreeMinimum(tree.BuildTreeHeight2());

            treeF.TreePredecessor(tree.BuildTreeHeight2());
            treeF.TreeInsert(tree.BuildTreeHeight3(), 20, tree.BuildTreeHeight2());
        }
    }
}