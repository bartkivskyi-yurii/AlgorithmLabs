using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
class Program
{
    static Random rand = new Random();

    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("===== 1) Поділ =====");
        int[] A1 = { 13, 19, 9, 5, 12, 8, 7, 4, 21, 2, 6, 11 };
        int q = Partition(A1, 0, A1.Length - 1);
        Console.WriteLine("q = " + q);
        Console.WriteLine("After Partition: " + string.Join(", ", A1));

        Console.WriteLine("\n===== 2) Поділ однакових чисел =====");
        int[] equalArr2 = { 5, 5, 5, 5, 5 };
        int qMod = PartitionModified(equalArr2, 0, equalArr2.Length - 1);
        Console.WriteLine("q (modified) = " + qMod);

        Console.WriteLine("\n===== 3) QUICKSORT незростаючий порядок =====");
        int[] A2 = { 13, 19, 9, 5, 12, 8, 7, 4, 21, 2, 6, 11 };
        QuickSortDesc(A2, 0, A2.Length - 1);
        Console.WriteLine("Sorted desc: " + string.Join(", ", A2));

        Console.WriteLine("\n===== 4) RANDOMIZED-QUICKSORT незростаючий порядок =====");
        int[] A3 = { 13, 19, 9, 5, 12, 8, 7, 4, 21, 2, 6, 11 };
        RandomizedQuickSort(A3, 0, A3.Length - 1);
        Console.WriteLine("Sorted desc (random): " + string.Join(", ", A3));

        Console.WriteLine("\n===== 5) COUNTING SORT =====");
        int[] A4 = { 6, 0, 2, 0, 1, 3, 4, 6, 1, 3, 2 };
        CountingSort(A4, 6);

        Console.WriteLine("\n===== 6) Алгоритм попередньої обробки =====");
        int[] prefix = Preprocess(A4, 6);
        Console.WriteLine("Count in [1..3] = " + Query(prefix, 1, 3));

        Console.WriteLine("\n===== 7) RADIX SORT (WORDS) =====");
        string[] words = { "COW","DOG","SEA","RUG","ROW","MOB","BOX","TAB",
                           "BAR","EAR","TAR","DIG","BIG","TEA","NOW","FOX" };
        RadixSort(words);
        Console.WriteLine(string.Join(", ", words));

        Console.WriteLine("\n===== 8) BUCKET SORT =====");
        double[] A5 = { .79, .13, .16, .64, .39, .20, .89, .53, .71, .42 };
        BucketSort(A5);
    }
    static int Partition(int[] A, int p, int r)
    {
        int pivot = A[r];
        int i = p - 1;

        for (int j = p; j < r; j++)
        {
            if (A[j] <= pivot)
            {
                i++;
                Swap(A, i, j);
            }
        }

        Swap(A, i + 1, r);
        return i + 1;
    }
    static int PartitionModified(int[] A, int p, int r)
    {
        bool allEqual = true;
        for (int i = p + 1; i <= r; i++)
        {
            if (A[i] != A[p])
            {
                allEqual = false;
                break;
            }
        }

        if (allEqual)
            return (p + r) / 2;

        return Partition(A, p, r);
    }
    static void Swap(int[] A, int i, int j)
    {
        int temp = A[i];
        A[i] = A[j];
        A[j] = temp;
    }
    static void QuickSortDesc(int[] A, int p, int r)
    {
        if (p < r)
        {
            int q = PartitionDesc(A, p, r);
            QuickSortDesc(A, p, q - 1);
            QuickSortDesc(A, q + 1, r);
        }
    }
    static int PartitionDesc(int[] A, int p, int r)
    {
        int pivot = A[r];
        int i = p - 1;

        for (int j = p; j < r; j++)
        {
            if (A[j] >= pivot)
            {
                i++;
                Swap(A, i, j);
            }
        }

        Swap(A, i + 1, r);
        return i + 1;
    }
    static void RandomizedQuickSort(int[] A, int p, int r)
    {
        if (p < r)
        {
            int randomIndex = rand.Next(p, r + 1);
            Swap(A, randomIndex, r);

            int q = PartitionDesc(A, p, r);

            RandomizedQuickSort(A, p, q - 1);
            RandomizedQuickSort(A, q + 1, r);
        }
    }
    static void CountingSort(int[] A, int k)
    {
        int n = A.Length;
        int[] C = new int[k + 1];
        int[] B = new int[n];

        for (int i = 0; i < n; i++)
            C[A[i]]++;

        for (int i = 1; i <= k; i++)
            C[i] += C[i - 1];

        for (int j = n - 1; j >= 0; j--)
        {
            B[C[A[j]] - 1] = A[j];
            C[A[j]]--;
        }

        Console.WriteLine("Counting sorted: " + string.Join(", ", B));
    }
    static int[] Preprocess(int[] A, int k)
    {
        int[] C = new int[k + 1];

        foreach (int x in A)
            C[x]++;

        for (int i = 1; i <= k; i++)
            C[i] += C[i - 1];

        return C;
    }
    static int Query(int[] C, int a, int b)
    {
        if (a == 0) return C[b];
        return C[b] - C[a - 1];
    }
    static void RadixSort(string[] arr)
    {
        for (int pos = 2; pos >= 0; pos--)
            CountingSortByChar(arr, pos);
    }
    static void CountingSortByChar(string[] arr, int pos)
    {
        int k = 26;
        int[] count = new int[k];
        string[] output = new string[arr.Length];

        foreach (string s in arr)
            count[s[pos] - 'A']++;

        for (int i = 1; i < k; i++)
            count[i] += count[i - 1];

        for (int i = arr.Length - 1; i >= 0; i--)
        {
            int index = arr[i][pos] - 'A';
            output[count[index] - 1] = arr[i];
            count[index]--;
        }

        for (int i = 0; i < arr.Length; i++)
            arr[i] = output[i];
    }
    static void BucketSort(double[] A)
    {
        int n = A.Length;
        List<double>[] buckets = new List<double>[n];

        for (int i = 0; i < n; i++)
            buckets[i] = new List<double>();

        foreach (double x in A)
            buckets[(int)(n * x)].Add(x);

        for (int i = 0; i < n; i++)
            buckets[i].Sort();

        List<double> result = new List<double>();
        foreach (var bucket in buckets)
            result.AddRange(bucket);

        Console.WriteLine("Bucket sorted: " + string.Join(", ", result));
    }
}