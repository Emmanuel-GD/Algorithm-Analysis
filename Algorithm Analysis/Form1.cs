using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

namespace AnalysisOfAlgorithms
{
    public partial class Form1 : Form
    {
        public static JObject JAllObject { get; set; }
        public long OperationCounter { get; set; }

        static int[][] JaggedArray = new int[3][];

        public Stopwatch Watch { get; set; }

        public Form1()
        {
            InitializeComponent();
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            path += @"\AppData\numbers.json";
            //string appPath = Path.GetDirectoryName(Application.ExecutablePath);

            //System.IO.DirectoryInfo directoryInfo = System.IO.Directory.GetParent(appPath);
            //System.IO.DirectoryInfo directoryInfo2 = System.IO.Directory.GetParent(directoryInfo.FullName);

            //string path = directoryInfo2.FullName + @"\AppData";

            JAllObject = ParseJson(path);
        }

        /// <summary>
        /// Insertion Sort
        /// </summary>
        /// <param name="unsortedInput"></param>
        /// <returns></returns>
        int[] InsertionSort(int[] unsortedInput)
        {
            Watch = new Stopwatch();
            Watch.Start();
            int temp;
            for (int i = 1; i < unsortedInput.Length; i++)
            {
                OperationCounter += 1;
                int j = i;
                while (j > 0 && unsortedInput[j] < unsortedInput[j - 1])
                {
                    OperationCounter += 3; //while has triple comparisons
                    temp = unsortedInput[j];
                    unsortedInput[j] = unsortedInput[j - 1];
                    unsortedInput[j - 1] = temp;

                    j -= 1;
                }
                OperationCounter += 3;
            }
            Watch.Stop();
            OperationCounter += 1;
            return unsortedInput;
        }

        /// <summary>
        /// Merge Sort
        /// </summary>
        /// <param name="unsorted"></param>
        /// <returns></returns>
        int[] MergeSort(int[] unsorted)
        {
            int[] left;
            int[] right;
            int[] result = new int[unsorted.Length];
            int[] input = unsorted;
            //As this is a recursive algorithm, we need to have a base case to 
            //avoid an infinite recursion and therfore a stackoverflow
            if (input.Length <= 1)
            {
                OperationCounter += 1;
                return input;
            }
            OperationCounter += 1;
            // The exact midpoint of our input  
            int midPoint = input.Length / 2;
            //Will represent our 'left' input
            left = new int[midPoint];

            //if input has an even number of elements, the left and right input will have the same number of 
            //elements
            //if input has an odd number of elements, the right input will have one more element than left
            if (input.Length % 2 == 0)
            {
                OperationCounter += 1;
                right = new int[midPoint];
            }
            else
            {
                OperationCounter += 1;
                right = new int[midPoint + 1];
            }
            //populate left input
            for (int i = 0; i < midPoint; i++)
            {
                OperationCounter += 1;
                left[i] = input[i];
            }
            OperationCounter += 1;
            //populate right input   
            int x = 0;
            //We start our index from the midpoint, as we have already populated the left input from 0 to midpont
            for (int i = midPoint; i < input.Length; i++)
            {
                OperationCounter += 1;
                right[x] = input[i];
                x++;
            }
            OperationCounter += 1;
            //Recursively sort the left input
            left = MergeSort(left);
            //Recursively sort the right input
            right = MergeSort(right);
            //Merge our two sorted inputs
            result = Merge(left, right);
            return result;
        }

        /// <summary>
        /// Combine left and right
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        int[] Merge(int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];
            //
            int indexLeft = 0, indexRight = 0, indexResult = 0;
            //while either input still has an element
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                OperationCounter += 3;
                //if both inputs have elements  
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    OperationCounter += 3;
                    //If item on left input is less than item on right input, add that item to the result input 
                    if (left[indexLeft] <= right[indexRight])
                    {
                        OperationCounter += 1;
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    // else the item in the right input wll be added to the results input
                    else
                    {
                        //OperationCounter += 1;
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                //if only the left input still has elements, add all its items to the results input
                else if (indexLeft < left.Length)
                {
                    OperationCounter += 1;
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                //if only the right input still has elements, add all its items to the results input
                else if (indexRight < right.Length)
                {
                    OperationCounter += 1;
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
                OperationCounter += 1;
            }
            OperationCounter += 3;
            return result;
        }

        /// <summary>
        /// Quik Sort
        /// </summary>
        /// <param name="unsorted"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        int[] Sort(int[] unsorted, int low, int high)
        {
            try
            {
                int i;
                if (low < high)
                {
                    OperationCounter += 1;
                    i = Partition(unsorted, low, high);

                    Sort(unsorted, low, i - 1);
                    Sort(unsorted, i + 1, high);
                }
                OperationCounter += 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{unsorted.Length} " + ex.Message);
            }
            return unsorted;
        }

        int Partition(int[] arrayToPartition, int start, int end)
        {
            int temp;
            int p = arrayToPartition[end];
            int i = start - 1;

            for (int j = start; j <= end - 1; j++)
            {
                OperationCounter += 1;
                if (arrayToPartition[j] <= p)
                {
                    OperationCounter += 1;
                    i++;
                    temp = arrayToPartition[i];
                    arrayToPartition[i] = arrayToPartition[j];
                    arrayToPartition[j] = temp;
                }
                OperationCounter += 1;
            }
            OperationCounter += 1;
            temp = arrayToPartition[i + 1];
            arrayToPartition[i + 1] = arrayToPartition[end];
            arrayToPartition[end] = temp;
            return i + 1;
        }

        JObject ParseJson(string path)
        {
            JObject json = JObject.Parse(File.ReadAllText(path));
            return json;
        }

        private void Insertion_Sort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        var random = (JArray)JAllObject["random1000"];
                        var sortedASC = (JArray)JAllObject["asc1000"];
                        var sortedDESC = (JArray)JAllObject["dsc1000"];

                        JaggedArray[0] = random.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC.Select(t => (int)t).ToArray();


                        groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[0]);
                        label1.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label2.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[1]);
                        label4.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label3.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[2]);
                        label6.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label5.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 1:
                        var random10000 = (JArray)JAllObject["random2000"];
                        var sortedASC10000 = (JArray)JAllObject["asc2000"];
                        var sortedDESC10000 = (JArray)JAllObject["dsc2000"];

                        JaggedArray[0] = random10000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC10000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC10000.Select(t => (int)t).ToArray();


                        groupBox5.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[0]);
                        label12.Text = $"Execution Time : { Watch.ElapsedMilliseconds } Milli Sec(s)";
                        label11.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[1]);
                        label10.Text = $"Execution Time : { Watch.ElapsedMilliseconds } Milli Sec(s)";
                        label9.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[2]);
                        label8.Text = $"Execution Time : { Watch.ElapsedMilliseconds } Milli Sec(s)";
                        label7.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 2:
                        var random20000 = (JArray)JAllObject["random4000"];
                        var sortedASC20000 = (JArray)JAllObject["asc4000"];
                        var sortedDESC20000 = (JArray)JAllObject["dsc4000"];

                        JaggedArray[0] = random20000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC20000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC20000.Select(t => (int)t).ToArray();


                        groupBox9.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[0]);
                        label18.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label17.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[1]);
                        label16.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label15.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[2]);
                        label14.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label13.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 3:
                        var random30000 = (JArray)JAllObject["random6000"];
                        var sortedASC30000 = (JArray)JAllObject["asc6000"];
                        var sortedDESC30000 = (JArray)JAllObject["dsc6000"];

                        JaggedArray[0] = random30000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC30000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC30000.Select(t => (int)t).ToArray();


                        groupBox13.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[0]);
                        label24.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label23.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[1]);
                        label22.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label21.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[2]);
                        label20.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label19.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 4:
                        var random40000 = (JArray)JAllObject["random8000"];
                        var sortedASC40000 = (JArray)JAllObject["asc8000"];
                        var sortedDESC40000 = (JArray)JAllObject["dsc8000"];

                        JaggedArray[0] = random40000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC40000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC40000.Select(t => (int)t).ToArray();


                        groupBox17.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[0]);
                        label30.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label29.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[1]);
                        label28.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label27.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[2]);
                        label26.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label25.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 5:

                        var random50000 = (JArray)JAllObject["random10000"];
                        var sortedASC50000 = (JArray)JAllObject["asc10000"];
                        var sortedDESC50000 = (JArray)JAllObject["dsc10000"];

                        JaggedArray[0] = random50000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC50000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC50000.Select(t => (int)t).ToArray();

                        groupBox21.Text = JaggedArray[0].Length + " Elements";
                        Application.DoEvents();
                        InsertionSort(JaggedArray[0]);
                        label36.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label35.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[1]);
                        label34.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label33.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[2]);
                        label32.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label31.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Merge_Sort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        var random = (JArray)JAllObject["random1000"];
                        var sortedASC = (JArray)JAllObject["asc1000"];
                        var sortedDESC = (JArray)JAllObject["dsc1000"];

                        JaggedArray[0] = random.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC.Select(t => (int)t).ToArray();


                        groupBox1.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[0]);
                        Watch.Stop();
                        label1.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label2.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[1]);
                        Watch.Stop();
                        label4.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label3.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[2]);
                        Watch.Stop();
                        label6.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label5.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 1:
                        var random10000 = (JArray)JAllObject["random2000"];
                        var sortedASC10000 = (JArray)JAllObject["asc2000"];
                        var sortedDESC10000 = (JArray)JAllObject["dsc2000"];

                        JaggedArray[0] = random10000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC10000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC10000.Select(t => (int)t).ToArray();


                        groupBox5.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[0]);
                        Watch.Stop();
                        label12.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label11.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[1]);
                        Watch.Stop();
                        label10.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label9.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[2]);
                        Watch.Stop();
                        label8.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label7.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 2:
                        var random20000 = (JArray)JAllObject["random4000"];
                        var sortedASC20000 = (JArray)JAllObject["asc4000"];
                        var sortedDESC20000 = (JArray)JAllObject["dsc4000"];

                        JaggedArray[0] = random20000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC20000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC20000.Select(t => (int)t).ToArray();


                        groupBox9.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[0]);
                        Watch.Stop();
                        label18.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label17.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[1]);
                        Watch.Stop();
                        label16.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label15.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[2]);
                        Watch.Stop();
                        label14.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label13.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 3:
                        var random30000 = (JArray)JAllObject["random6000"];
                        var sortedASC30000 = (JArray)JAllObject["asc6000"];
                        var sortedDESC30000 = (JArray)JAllObject["dsc6000"];

                        JaggedArray[0] = random30000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC30000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC30000.Select(t => (int)t).ToArray();


                        groupBox13.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[0]);
                        Watch.Stop();
                        label24.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label23.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[1]);
                        Watch.Stop();
                        label22.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label21.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[2]);
                        Watch.Stop();
                        label20.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label19.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 4:
                        var random40000 = (JArray)JAllObject["random8000"];
                        var sortedASC40000 = (JArray)JAllObject["asc8000"];
                        var sortedDESC40000 = (JArray)JAllObject["dsc8000"];

                        JaggedArray[0] = random40000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC40000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC40000.Select(t => (int)t).ToArray();


                        groupBox17.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[0]);
                        Watch.Stop();

                        label30.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label29.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[1]);
                        Watch.Stop();
                        label28.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label27.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[2]);
                        Watch.Stop();

                        label26.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label25.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 5:

                        var random50000 = (JArray)JAllObject["random10000"];
                        var sortedASC50000 = (JArray)JAllObject["asc10000"];
                        var sortedDESC50000 = (JArray)JAllObject["dsc10000"];

                        JaggedArray[0] = random50000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC50000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC50000.Select(t => (int)t).ToArray();

                        groupBox21.Text = JaggedArray[0].Length + " Elements";
                        Application.DoEvents();

                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[0]);
                        Watch.Stop();

                        label36.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label35.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[1]);
                        Watch.Stop();
                        label34.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label33.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[2]);
                        Watch.Stop();
                        label32.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label31.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Quick_Sort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        var random = (JArray)JAllObject["random1000"];
                        var sortedASC = (JArray)JAllObject["asc1000"];
                        var sortedDESC = (JArray)JAllObject["dsc1000"];

                        JaggedArray[0] = random.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC.Select(t => (int)t).ToArray();


                        groupBox1.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[0], 0, JaggedArray[0].Length - 1);
                        Watch.Stop();
                        label1.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label2.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[1], 0, JaggedArray[1].Length - 1);
                        Watch.Stop();
                        label4.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label3.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[2], 0, JaggedArray[2].Length - 1);
                        Watch.Stop();
                        label6.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label5.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 1:
                        var random10000 = (JArray)JAllObject["random2000"];
                        var sortedASC10000 = (JArray)JAllObject["asc2000"];
                        var sortedDESC10000 = (JArray)JAllObject["dsc2000"];

                        JaggedArray[0] = random10000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC10000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC10000.Select(t => (int)t).ToArray();


                        groupBox5.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[0], 0, JaggedArray[0].Length - 1);
                        Watch.Stop();
                        label12.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label11.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[1], 0, JaggedArray[1].Length - 1);
                        Watch.Stop();
                        label10.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label9.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[2], 0, JaggedArray[2].Length - 1);
                        Watch.Stop();
                        label8.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label7.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 2:
                        var random20000 = (JArray)JAllObject["random4000"];
                        var sortedASC20000 = (JArray)JAllObject["asc4000"];
                        var sortedDESC20000 = (JArray)JAllObject["dsc4000"];

                        JaggedArray[0] = random20000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC20000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC20000.Select(t => (int)t).ToArray();


                        groupBox9.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[0], 0, JaggedArray[0].Length - 1);
                        Watch.Stop();
                        label18.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label17.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[1], 0, JaggedArray[1].Length - 1);
                        Watch.Stop();
                        label16.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label15.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[2], 0, JaggedArray[2].Length - 1);
                        Watch.Stop();
                        label14.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label13.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 3:
                        var random30000 = (JArray)JAllObject["random6000"];
                        var sortedASC30000 = (JArray)JAllObject["asc6000"];
                        var sortedDESC30000 = (JArray)JAllObject["dsc6000"];

                        JaggedArray[0] = random30000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC30000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC30000.Select(t => (int)t).ToArray();


                        groupBox13.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[0], 0, JaggedArray[0].Length - 1);
                        Watch.Stop();
                        label24.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label23.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[1], 0, JaggedArray[1].Length - 1);
                        Watch.Stop();
                        label22.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label21.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[2], 0, JaggedArray[2].Length - 1);
                        Watch.Stop();
                        label20.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label19.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 4:
                        var random40000 = (JArray)JAllObject["random8000"];
                        var sortedASC40000 = (JArray)JAllObject["asc8000"];
                        var sortedDESC40000 = (JArray)JAllObject["dsc8000"];

                        JaggedArray[0] = random40000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC40000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC40000.Select(t => (int)t).ToArray();


                        groupBox17.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[0], 0, JaggedArray[0].Length - 1);
                        Watch.Stop();

                        label30.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label29.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[1], 0, JaggedArray[1].Length - 1);
                        Watch.Stop();
                        label28.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label27.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[2], 0, JaggedArray[2].Length - 1);
                        Watch.Stop();

                        label26.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label25.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 5:

                        var random50000 = (JArray)JAllObject["random10000"];
                        var sortedASC50000 = (JArray)JAllObject["asc10000"];
                        var sortedDESC50000 = (JArray)JAllObject["dsc10000"];

                        JaggedArray[0] = random50000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC50000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC50000.Select(t => (int)t).ToArray();

                        groupBox21.Text = JaggedArray[0].Length + " Elements";
                        Application.DoEvents();

                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[0], 0, JaggedArray[0].Length - 1);
                        Watch.Stop();

                        label36.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label35.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[1], 0, JaggedArray[1].Length - 1);
                        Watch.Stop();
                        label34.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label33.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[2], 0, JaggedArray[2].Length - 1);
                        Watch.Stop();
                        label32.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label31.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;
                    default:
                        break;
                }
            }
        }

        /*
        private void Insertion_Sort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        var random = (JArray)JAllObject["random5000"];
                        var sortedASC = (JArray)JAllObject["sortedASC5000"];
                        var sortedDESC = (JArray)JAllObject["sortedDESC5000"];

                        JaggedArray[0] = random.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC.Select(t => (int)t).ToArray();


                        groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[0]);
                        label1.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label2.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[1]);
                        label4.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label3.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[2]);
                        label6.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label5.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 1:
                        var random10000 = (JArray)JAllObject["random10000"];
                        var sortedASC10000 = (JArray)JAllObject["sortedASC10000"];
                        var sortedDESC10000 = (JArray)JAllObject["sortedDESC10000"];

                        JaggedArray[0] = random10000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC10000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC10000.Select(t => (int)t).ToArray();


                        groupBox5.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[0]);
                        label12.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label11.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[1]);
                        label10.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label9.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[2]);
                        label8.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label7.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 2:
                        var random20000 = (JArray)JAllObject["random20000"];
                        var sortedASC20000 = (JArray)JAllObject["sortedASC20000"];
                        var sortedDESC20000 = (JArray)JAllObject["sortedDESC20000"];

                        JaggedArray[0] = random20000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC20000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC20000.Select(t => (int)t).ToArray();


                        groupBox9.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[0]);
                        label18.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label17.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[1]);
                        label16.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label15.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[2]);
                        label14.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label13.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 3:
                        var random30000 = (JArray)JAllObject["random30000"];
                        var sortedASC30000 = (JArray)JAllObject["sortedASC30000"];
                        var sortedDESC30000 = (JArray)JAllObject["sortedDESC30000"];

                        JaggedArray[0] = random30000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC30000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC30000.Select(t => (int)t).ToArray();


                        groupBox13.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[0]);
                        label24.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label23.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[1]);
                        label22.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label21.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[2]);
                        label20.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label19.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 4:
                        var random40000 = (JArray)JAllObject["random40000"];
                        var sortedASC40000 = (JArray)JAllObject["sortedASC40000"];
                        var sortedDESC40000 = (JArray)JAllObject["sortedDESC40000"];

                        JaggedArray[0] = random40000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC40000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC40000.Select(t => (int)t).ToArray();


                        groupBox17.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[0]);
                        label30.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label29.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[1]);
                        label28.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label27.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[2]);
                        label26.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label25.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 5:

                        var random50000 = (JArray)JAllObject["random50000"];
                        var sortedASC50000 = (JArray)JAllObject["sortedASC50000"];
                        var sortedDESC50000 = (JArray)JAllObject["sortedDESC50000"];

                        JaggedArray[0] = random50000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC50000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC50000.Select(t => (int)t).ToArray();

                        groupBox21.Text = JaggedArray[0].Length + " Elements";
                        Application.DoEvents();
                        InsertionSort(JaggedArray[0]);
                        label36.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label35.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[1]);
                        label34.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label33.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        InsertionSort(JaggedArray[2]);
                        label32.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label31.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Merge_Sort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        var random = (JArray)JAllObject["random5000"];
                        var sortedASC = (JArray)JAllObject["sortedASC5000"];
                        var sortedDESC = (JArray)JAllObject["sortedDESC5000"];

                        JaggedArray[0] = random.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC.Select(t => (int)t).ToArray();


                        groupBox1.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[0]);
                        Watch.Stop();
                        label1.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label2.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[1]);
                        Watch.Stop();
                        label4.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label3.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[2]);
                        Watch.Stop();
                        label6.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label5.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 1:
                        var random10000 = (JArray)JAllObject["random10000"];
                        var sortedASC10000 = (JArray)JAllObject["sortedASC10000"];
                        var sortedDESC10000 = (JArray)JAllObject["sortedDESC10000"];

                        JaggedArray[0] = random10000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC10000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC10000.Select(t => (int)t).ToArray();


                        groupBox5.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[0]);
                        Watch.Stop();
                        label12.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label11.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[1]);
                        Watch.Stop();
                        label10.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label9.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[2]);
                        Watch.Stop();
                        label8.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label7.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 2:
                        var random20000 = (JArray)JAllObject["random20000"];
                        var sortedASC20000 = (JArray)JAllObject["sortedASC20000"];
                        var sortedDESC20000 = (JArray)JAllObject["sortedDESC20000"];

                        JaggedArray[0] = random20000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC20000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC20000.Select(t => (int)t).ToArray();


                        groupBox9.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[0]);
                        Watch.Stop();
                        label18.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label17.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[1]);
                        Watch.Stop();
                        label16.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label15.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[2]);
                        Watch.Stop();
                        label14.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label13.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 3:
                        var random30000 = (JArray)JAllObject["random30000"];
                        var sortedASC30000 = (JArray)JAllObject["sortedASC30000"];
                        var sortedDESC30000 = (JArray)JAllObject["sortedDESC30000"];

                        JaggedArray[0] = random30000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC30000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC30000.Select(t => (int)t).ToArray();


                        groupBox13.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[0]);
                        Watch.Stop();
                        label24.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label23.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[1]);
                        Watch.Stop();
                        label22.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label21.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[2]);
                        Watch.Stop();
                        label20.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label19.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 4:
                        var random40000 = (JArray)JAllObject["random40000"];
                        var sortedASC40000 = (JArray)JAllObject["sortedASC40000"];
                        var sortedDESC40000 = (JArray)JAllObject["sortedDESC40000"];

                        JaggedArray[0] = random40000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC40000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC40000.Select(t => (int)t).ToArray();


                        groupBox17.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[0]);
                        Watch.Stop();

                        label30.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label29.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[1]);
                        Watch.Stop();
                        label28.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label27.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[2]);
                        Watch.Stop();

                        label26.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label25.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 5:

                        var random50000 = (JArray)JAllObject["random50000"];
                        var sortedASC50000 = (JArray)JAllObject["sortedASC50000"];
                        var sortedDESC50000 = (JArray)JAllObject["sortedDESC50000"];

                        JaggedArray[0] = random50000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC50000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC50000.Select(t => (int)t).ToArray();

                        groupBox21.Text = JaggedArray[0].Length + " Elements";
                        Application.DoEvents();

                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[0]);
                        Watch.Stop();

                        label36.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label35.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[1]);
                        Watch.Stop();
                        label34.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label33.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        MergeSort(JaggedArray[2]);
                        Watch.Stop();
                        label32.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label31.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Quick_Sort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        var random = (JArray)JAllObject["random5000"];
                        var sortedASC = (JArray)JAllObject["sortedASC5000"];
                        var sortedDESC = (JArray)JAllObject["sortedDESC5000"];

                        JaggedArray[0] = random.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC.Select(t => (int)t).ToArray();


                        groupBox1.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[0], 0, JaggedArray[0].Length - 1);
                        Watch.Stop();
                        label1.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label2.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[1], 0, JaggedArray[1].Length - 1);
                        Watch.Stop();
                        label4.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label3.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[2], 0, JaggedArray[2].Length - 1);
                        Watch.Stop();
                        label6.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label5.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 1:
                        var random10000 = (JArray)JAllObject["random10000"];
                        var sortedASC10000 = (JArray)JAllObject["sortedASC10000"];
                        var sortedDESC10000 = (JArray)JAllObject["sortedDESC10000"];

                        JaggedArray[0] = random10000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC10000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC10000.Select(t => (int)t).ToArray();


                        groupBox5.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[0], 0, JaggedArray[0].Length - 1);
                        Watch.Stop();
                        label12.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label11.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[1], 0, JaggedArray[1].Length - 1);
                        Watch.Stop();
                        label10.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label9.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[2], 0, JaggedArray[2].Length - 1);
                        Watch.Stop();
                        label8.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label7.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 2:
                        var random20000 = (JArray)JAllObject["random20000"];
                        var sortedASC20000 = (JArray)JAllObject["sortedASC20000"];
                        var sortedDESC20000 = (JArray)JAllObject["sortedDESC20000"];

                        JaggedArray[0] = random20000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC20000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC20000.Select(t => (int)t).ToArray();


                        groupBox9.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[0], 0, JaggedArray[0].Length - 1);
                        Watch.Stop();
                        label18.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label17.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[1], 0, JaggedArray[1].Length - 1);
                        Watch.Stop();
                        label16.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label15.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[2], 0, JaggedArray[2].Length - 1);
                        Watch.Stop();
                        label14.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label13.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 3:
                        var random30000 = (JArray)JAllObject["random30000"];
                        var sortedASC30000 = (JArray)JAllObject["sortedASC30000"];
                        var sortedDESC30000 = (JArray)JAllObject["sortedDESC30000"];

                        JaggedArray[0] = random30000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC30000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC30000.Select(t => (int)t).ToArray();


                        groupBox13.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[0], 0, JaggedArray[0].Length - 1);
                        Watch.Stop();
                        label24.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label23.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[1], 0, JaggedArray[1].Length - 1);
                        Watch.Stop();
                        label22.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label21.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[2], 0, JaggedArray[2].Length - 1);
                        Watch.Stop();
                        label20.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label19.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 4:
                        var random40000 = (JArray)JAllObject["random40000"];
                        var sortedASC40000 = (JArray)JAllObject["sortedASC40000"];
                        var sortedDESC40000 = (JArray)JAllObject["sortedDESC40000"];

                        JaggedArray[0] = random40000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC40000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC40000.Select(t => (int)t).ToArray();


                        groupBox17.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[0], 0, JaggedArray[0].Length - 1);
                        Watch.Stop();

                        label30.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label29.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[1], 0, JaggedArray[1].Length - 1);
                        Watch.Stop();
                        label28.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label27.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[2], 0, JaggedArray[2].Length - 1);
                        Watch.Stop();

                        label26.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label25.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;

                    case 5:

                        var random50000 = (JArray)JAllObject["random50000"];
                        var sortedASC50000 = (JArray)JAllObject["sortedASC50000"];
                        var sortedDESC50000 = (JArray)JAllObject["sortedDESC50000"];

                        JaggedArray[0] = random50000.Select(t => (int)t).ToArray();
                        JaggedArray[1] = sortedASC50000.Select(t => (int)t).ToArray();
                        JaggedArray[2] = sortedDESC50000.Select(t => (int)t).ToArray();

                        groupBox21.Text = JaggedArray[0].Length + " Elements";
                        Application.DoEvents();

                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[0], 0, JaggedArray[0].Length - 1);
                        Watch.Stop();

                        label36.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label35.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";

                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[1], 0, JaggedArray[1].Length - 1);
                        Watch.Stop();
                        label34.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label33.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        //groupBox1.Text = JaggedArray[0].Length + " Elements";
                        Watch = new Stopwatch();
                        Watch.Start();
                        Sort(JaggedArray[2], 0, JaggedArray[2].Length - 1);
                        Watch.Stop();
                        label32.Text = $"Execution Time : {Watch.ElapsedMilliseconds} Milli Sec(s)";
                        label31.Text = $"Operations(Comparison) : {OperationCounter}";

                        OperationCounter = 0;
                        Application.DoEvents();
                        break;
                    default:
                        break;
                }
            }
        }
    */
    }
}
