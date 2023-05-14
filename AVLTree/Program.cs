using AVLTree;
using System;
using System.Diagnostics;
using System.IO;

namespace AVLTree
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> avg = new List<int>();
            AVLTree<int> tree = new AVLTree<int>();
            int[] array = WorkTools.GenerateRandomArray(10000);

            // Add elements to the tree
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < array.Length; i++)
            {
                sw.Start();
                tree.Add(array[i]);
                sw.Stop();  
                string resultString = ($"Вставка {array[i]}: время = {sw.ElapsedTicks}, операций = {tree.Iterations}");
                avg.Add(resultString);
                sw.Reset();
                tree.Iterations = 0;
            }

            // Search for random elements in the tree
            int[] searchKeys = WorkTools.GetRandomSubarray(array, 100);
            for (int i = 0; i < searchKeys.Length; i++)
            {
                sw.Start();
                AVLTreeNode<int> node = tree.Find(searchKeys[i]);
                sw.Stop();
                string resultString = ($"Поиск {searchKeys[i]}: время = {sw.ElapsedTicks}, операций = {tree.Iterations}");
                avg.Add(resultString);
                sw.Reset();
                tree.Iterations = 0;
            }

            //  Remove random elements from the tree
            int[] removeKeys = WorkTools.GetRandomSubarray(array, 1000);
            for (int i = 0; i < removeKeys.Length; i++)
            {
                sw.Start();
                tree.Remove(removeKeys[i]);
                sw.Stop();
                string resultString = ($"Удаление {removeKeys[i]}: время = {sw.ElapsedTicks}, операций = {tree.Iterations}");
                avg.Add(resultString);
                sw.Reset();
                tree.Iterations = 0;
            }
        }

       
    }
}
