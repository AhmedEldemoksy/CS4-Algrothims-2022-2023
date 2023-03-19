using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        public void sort(int[] arr)
        {
            int N = arr.Length;         
            // One by one extract an element from heap
            for (int i = N - 1; i > 0; i--)
            {
                // Move current root to end
                int temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;

                // call max heapify on the reduced heap
                heapify(arr, i, 0);
            }
        }
        //Build max_heap
        public void build_max_heap(int[] arr)
        {
            // Build heap (rearrange array)
            int N = arr.Length;
            for (int i = N / 2 - 1; i >= 0; i--)
                heapify(arr, N, i);
        }
        // To heapify a subtree rooted with node i which is
        // an index in arr[]. n is size of heap
        void heapify(int[] arr, int N, int i)
        {
            int largest = i; // Initialize largest as root
            int l = 2 * i + 1; // left = 2*i + 1
            int r = 2 * i + 2; // right = 2*i + 2

            // If left child is larger than root
            if (l < N && arr[l] > arr[largest])
                largest = l;

            // If right child is larger than largest so far
            if (r < N && arr[r] > arr[largest])
                largest = r;

            // If largest is not root
            if (largest != i)
            {
                int swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;

                // Recursively heapify the affected sub-tree
                heapify(arr, N, largest);
            }
        }

        /* A utility function to print array of size n */
        static void printArray(int[] arr)
        {
            int N = arr.Length;
            for (int i = 0; i < N; ++i)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            int[] arr = { 12, 11, 13, 5, 6, 7 };
            int N = arr.Length;

            // Function call
            Program ob = new Program();
            ob.build_max_heap(arr);
            printArray(arr);
            ob.sort(arr);
            Console.WriteLine("Sorted array is: ");
            printArray(arr);
        }
    }
}
