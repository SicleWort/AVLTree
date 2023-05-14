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
            AVLTree<int> tree = new AVLTree<int>();
            int[] array = WorkTools.GenerateRandomArray(10000);

            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < array.Length; i++)
            {
                sw.Start();
                tree.Add(array[i]);
                sw.Stop();
                Console.WriteLine($"Вставка {array[i]}: время = {sw.ElapsedTicks}, операций = {tree.Iterations}");
                sw.Reset();
                tree.Iterations = 0;
            }

            int[] searchKeys = WorkTools.GetRandomSubarray(array, 100);
            for (int i = 0; i < searchKeys.Length; i++)
            {
                sw.Start();
                AVLTreeNode<int> node = tree.Find(searchKeys[i]);
                sw.Stop();
                Console.WriteLine($"Поиск {searchKeys[i]}: время = {sw.ElapsedTicks}, операций = {tree.Iterations}");
                sw.Reset();
                tree.Iterations = 0;
            }

            int[] removeKeys = WorkTools.GetRandomSubarray(array, 1000);
            for (int i = 0; i < removeKeys.Length; i++)
            {
                sw.Start();
                tree.Remove(removeKeys[i]);
                sw.Stop();
                Console.WriteLine($"Удаление {removeKeys[i]}: время = {sw.ElapsedTicks}, операций = {tree.Iterations}");
                sw.Reset();
                tree.Iterations = 0;
            }
        }


    }
}