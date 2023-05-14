using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    public class WorkTools
    {
        public static int[] GenerateRandomArray(int size, int minV = 0, int maxV = int.MaxValue)
        {
            int[] arr = new int[size];
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                arr[i] = r.Next(minV, maxV);
            }
            return arr;
        }
        public static int[] GetRandomSubarray(int[] array, int length)
        {
            Random rand = new Random();
            int[] subarray = new int[length];
            for (int i = 0; i < length; i++)
            {
                subarray[i] = array[rand.Next(array.Length)];
            }
            return subarray;
        }
    }
}
