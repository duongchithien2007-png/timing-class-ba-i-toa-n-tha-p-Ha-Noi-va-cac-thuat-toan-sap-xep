using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timing_class_bài_toán_tháp_Hn
{
    internal class Program
    {
        class Timing
        {
            private DateTime startTime;
            private DateTime endTime;

            public void Start()
            {
                startTime = DateTime.Now;
            }

            public void Stop()
            {
                endTime = DateTime.Now;
            }

            public double Duration()
            {
                return (endTime - startTime).TotalMilliseconds;
            }
        }
        class SortAlgorithms// chứa các thuật toán sắp xếp khác nhau
        {
            // Thuật toán Seclection Sort
            public static void SelectionSort(int[] a)
            {
                int n = a.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    int min = i;
                    for (int j = i + 1; j < n; j++)
                        if (a[j] < a[min])
                            min = j;

                    Swap(ref a[i], ref a[min]);
                }
            }

            // Thuật toán Bubble Sort
            public static void BubbleSort(int[] a)
            {
                int n = a.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (a[j] > a[j + 1])
                            Swap(ref a[j], ref a[j + 1]);
                    }
                }
            }

            //Thuật toán Insertion Sort
            public static void InsertionSort(int[] a)
            {
                for (int i = 1; i < a.Length; i++)
                {
                    int key = a[i];
                    int j = i - 1;

                    while (j >= 0 && a[j] > key)
                    {
                        a[j + 1] = a[j];
                        j--;
                    }
                    a[j + 1] = key;
                }
            }

            //Thuật toán Exchange Sort
            public static void ExchangeSort(int[] a)
            {
                int n = a.Length;
                for (int i = 0; i < n - 1; i++)
                    for (int j = i + 1; j < n; j++)
                        if (a[i] > a[j])
                            Swap(ref a[i], ref a[j]);
            }

            //Thuật toán Quick Sort
            public static void QuickSort(int[] a, int left, int right)
            {
                int i = left, j = right;
                int pivot = a[(left + right) / 2];

                while (i <= j)
                {
                    while (a[i] < pivot) i++;
                    while (a[j] > pivot) j--;

                    if (i <= j)
                    {
                        Swap(ref a[i], ref a[j]);
                        i++; j--;
                    }
                }

                if (left < j) QuickSort(a, left, j);
                if (i < right) QuickSort(a, i, right);
            }

            // Thuật toán Heap Sort
            public static void HeapSort(int[] a)
            {
                int n = a.Length;

                for (int i = n / 2 - 1; i >= 0; i--)
                    Heapify(a, n, i);

                for (int i = n - 1; i > 0; i--)
                {
                    Swap(ref a[0], ref a[i]);
                    Heapify(a, i, 0);
                }
            }

            private static void Heapify(int[] a, int n, int i)// xây dựng heap từ mảng con tại chỉ số i
            {
                int largest = i;
                int left = 2 * i + 1;
                int right = 2 * i + 2;

                if (left < n && a[left] > a[largest])
                    largest = left;

                if (right < n && a[right] > a[largest])
                    largest = right;

                if (largest != i)
                {
                    Swap(ref a[i], ref a[largest]);
                    Heapify(a, n, largest);
                }
            }

            private static void Swap(ref int a, ref int b)// hoán đổi giá trị của hai biến
            {
                int temp = a;
                a = b;
                b = temp;
            }
        }
        static void TestSort(string name, int[] original, Action<int[]> sortFunc)// kiểm tra thời gian thực thi của một thuật toán sắp xếp cụ thể
        {
            int[] a = (int[])original.Clone();

            Timing t = new Timing();
            t.Start();

            sortFunc(a);

            t.Stop();

            Console.WriteLine($"{name}: {t.Duration()} ms");
        }
        static void TestQuickSort(int[] original)// kiểm tra thời gian thực thi của thuật toán Quick Sort
        {
            int[] a = (int[])original.Clone();

            Timing t = new Timing();
            t.Start();

            SortAlgorithms.QuickSort(a, 0, a.Length - 1);

            t.Stop();

            Console.WriteLine($"Quick Sort: {t.Duration()} ms");
        }

        class Hanoi// giải thuật tháp Hà Nội
        {
            public static void Solve(int n, char A, char B, char C)
            {
                if (n == 1)
                {
                    Console.WriteLine($"Di chuyen dia 1 tu cot {A} sang cot {C}");
                    return;
                }

                Solve(n - 1, A, C, B);
                Console.WriteLine($"Di chuyen dia {n} tu cot {A} sang cot {C}");
                Solve(n - 1, B, A, C);
            }
        }
        static void TestHanoi(int n)
        {
            Timing t = new Timing();
            t.Start();

            Hanoi.Solve(n, 'A', 'B', 'C');

            t.Stop();

            Console.WriteLine($"\nHanoi voi {n} dia: {t.Duration()} ms");
        }
        static void Main(string[] args)
        {
            int n = 10000;
            int[] arr = new int[n];// tạo một mảng có kích thước n để chứa các số nguyên ngẫu nhiên
            Random rand = new Random();

            for (int i = 0; i < n; i++)
                arr[i] = rand.Next(100000);

            Console.WriteLine("Bang kiem tra thoi gian cua cac sort");

            TestSort("Selection", arr, SortAlgorithms.SelectionSort);
            TestSort("Bubble", arr, SortAlgorithms.BubbleSort);
            TestSort("Insertion", arr, SortAlgorithms.InsertionSort);
            TestSort("Exchange", arr, SortAlgorithms.ExchangeSort);
            TestQuickSort(arr);
            TestSort("Heap", arr, SortAlgorithms.HeapSort);

            Console.WriteLine("\n    Cach bai toan thap Ha Noi van hanh   ");
            TestHanoi(3);

            Console.ReadLine();
        }
    }
}
