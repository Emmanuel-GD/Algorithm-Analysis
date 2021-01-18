using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalysisOfAlgorithms
{
    public partial class Form2 : Form
    {
        public static JObject JAllObject { get; set; }

        public BigInteger OperationCounter { get; set; }

        private static readonly int[][] JaggedArray = new int[6][];

        public Stopwatch Watch { get; set; }

        JArray[] ArrayValues = new JArray[6];

        readonly DataSet DataSet = new DataSet();

        public Form2()
        {
            InitializeComponent();
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            path += @"\AppData\numbers.json";
            JAllObject = ParseJson(path);

            BuildDataTables(DataSet);
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

            if (input.Length <= 1)
            {
                OperationCounter += 1;
                return input;
            }
            OperationCounter += 1;
            int midPoint = input.Length / 2;
            left = new int[midPoint];

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

            for (int i = 0; i < midPoint; i++)
            {
                OperationCounter += 1;
                left[i] = input[i];
            }
            OperationCounter += 1;

            int x = 0;

            for (int i = midPoint; i < input.Length; i++)
            {
                OperationCounter += 1;
                right[x] = input[i];
                x++;
            }
            OperationCounter += 1;
            left = MergeSort(left);
            right = MergeSort(right);
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

            int indexLeft = 0, indexRight = 0, indexResult = 0;

            while (indexLeft < left.Length || indexRight < right.Length)
            {
                OperationCounter += 3;
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    OperationCounter += 3;

                    if (left[indexLeft] <= right[indexRight])
                    {
                        OperationCounter += 1;
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    else
                    {
                        OperationCounter += 1;
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                else if (indexLeft < left.Length)
                {
                    OperationCounter += 1;
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
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
            catch (StackOverflowException ex)
            {
                //this is no showing for unknown reason...check it again
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

        /// <summary>
        /// Read the data from the json file. This file contains array of numbers. The arrays have 
        /// different size ranging from 1000 to 10000. The file is in AppData Folder.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        JObject ParseJson(string path)
        {
            JObject json = JObject.Parse(File.ReadAllText(path));
            return json;
        }

        void BuildDataTables(DataSet dataSet)
        {
            dataSet.Tables.Add("Random");
            dataSet.Tables.Add("ASC");
            dataSet.Tables.Add("DESC");
            CreateColumns(dataSet);
        }

        void CreateColumns(DataSet dataSet)
        {
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                dataSet.Tables[i].Columns.Add("Size", typeof(Int64));
                dataSet.Tables[i].Columns.Add("Insertion", typeof(Int64));
                dataSet.Tables[i].Columns.Add("Merge", typeof(Int64));
                dataSet.Tables[i].Columns.Add("Quick", typeof(Int64));
            }
        }

        void UpdateDataTable(DataTable table, int rowIndex, params long[] values)
        {
            DataRow newRow = table.NewRow();

            if (table.Rows.Count == 6)
            {
                table.Rows[rowIndex]["Size"] = values[0];
                table.Rows[rowIndex]["Insertion"] = Convert.ToInt64(table.Rows[rowIndex]["Insertion"]) + values[1];
                table.Rows[rowIndex]["Merge"] = Convert.ToInt64(table.Rows[rowIndex]["Merge"]) + values[2];
                table.Rows[rowIndex]["Quick"] = Convert.ToInt64(table.Rows[rowIndex]["Quick"]) + values[3];
            }
            else
            {
                newRow["Size"] = values[0];
                newRow["Insertion"] = values[1];
                newRow["Merge"] = values[2];
                newRow["Quick"] = values[3];
                table.Rows.Add(newRow);
            }
            Application.DoEvents();
        }

        private void RandomArray_Click(object sender, EventArgs e)
        {


            ArrayValues[0] = (JArray)JAllObject["random1000"];
            ArrayValues[1] = (JArray)JAllObject["random2000"];
            ArrayValues[2] = (JArray)JAllObject["random4000"];
            ArrayValues[3] = (JArray)JAllObject["random6000"];
            ArrayValues[4] = (JArray)JAllObject["random8000"];
            ArrayValues[5] = (JArray)JAllObject["random10000"];


            long[] values = new long[4];
            for (int i = 0; i < 6; i++)
            {

                InsertionSort(ArrayValues[i].Select(t => (int)t).ToArray());
                //UpdateDataTable(DataSet.Tables["Random"], 0, ArrayValues[0].Select(t => (int)t).ToArray().Length, Watch.ElapsedMilliseconds, 0, 0);
                values[0] = ArrayValues[i].Select(t => (int)t).ToArray().Length;
                values[1] = Watch.ElapsedMilliseconds;
                Watch.Reset();
                Watch.Start();
                MergeSort(ArrayValues[i].Select(t => (int)t).ToArray());
                Watch.Stop();
                //UpdateDataTable(DataSet.Tables["Random"], 0, ArrayValues[0].Select(t => (int)t).ToArray().Length, 0, Watch.ElapsedMilliseconds, 0);
                values[2] = Watch.ElapsedMilliseconds;
                Watch.Reset();
                Watch.Start();
                Sort(ArrayValues[i].Select(t => (int)t).ToArray(), 0, ArrayValues[i].Select(t => (int)t).ToArray().Length - 1);
                Watch.Stop();
                values[3] = Watch.ElapsedMilliseconds;

                UpdateDataTable(DataSet.Tables["Random"], i, values[0], values[1], values[2], values[3]);
                BindingSource bs = new BindingSource
                {
                    DataSource = DataSet.Tables["Random"]
                };
                dataGridView1.DataSource = bs;

                Application.DoEvents();
            }

        }

        private void SortedArray_Click(object sender, EventArgs e)
        {
            ArrayValues[0] = (JArray)JAllObject["asc1000"];
            ArrayValues[1] = (JArray)JAllObject["asc2000"];
            ArrayValues[2] = (JArray)JAllObject["asc4000"];
            ArrayValues[3] = (JArray)JAllObject["asc6000"];
            ArrayValues[4] = (JArray)JAllObject["asc8000"];
            ArrayValues[5] = (JArray)JAllObject["asc10000"];


            long[] values = new long[4];
            for (int i = 0; i < 6; i++)
            {

                InsertionSort(ArrayValues[i].Select(t => (int)t).ToArray());
                //UpdateDataTable(DataSet.Tables["Random"], 0, ArrayValues[0].Select(t => (int)t).ToArray().Length, Watch.ElapsedMilliseconds, 0, 0);
                values[0] = ArrayValues[i].Select(t => (int)t).ToArray().Length;
                values[1] = Watch.ElapsedMilliseconds;
                Watch.Reset();
                Watch.Start();
                MergeSort(ArrayValues[i].Select(t => (int)t).ToArray());
                Watch.Stop();
                //UpdateDataTable(DataSet.Tables["Random"], 0, ArrayValues[0].Select(t => (int)t).ToArray().Length, 0, Watch.ElapsedMilliseconds, 0);
                values[2] = Watch.ElapsedMilliseconds;
                Watch.Reset();
                Watch.Start();
                Sort(ArrayValues[i].Select(t => (int)t).ToArray(), 0, ArrayValues[i].Select(t => (int)t).ToArray().Length - 1);
                Watch.Stop();
                values[3] = Watch.ElapsedMilliseconds;

                UpdateDataTable(DataSet.Tables["Random"], i, values[0], values[1], values[2], values[3]);
                BindingSource bs = new BindingSource
                {
                    DataSource = DataSet.Tables["Random"]
                };
                dataGridView2.DataSource = bs;

                Application.DoEvents();
            }
        }

        private async void SortedDESC_Click(object sender, EventArgs e)
        {
            ArrayValues[0] = (JArray)JAllObject["dsc1000"];
            ArrayValues[1] = (JArray)JAllObject["dsc2000"];
            ArrayValues[2] = (JArray)JAllObject["dsc4000"];
            ArrayValues[3] = (JArray)JAllObject["dsc6000"];
            ArrayValues[4] = (JArray)JAllObject["dsc8000"];
            ArrayValues[5] = (JArray)JAllObject["dsc10000"];


            long[] values = new long[4];
            for (int i = 0; i < 6; i++)
            {

                InsertionSort(ArrayValues[i].Select(t => (int)t).ToArray());
                //UpdateDataTable(DataSet.Tables["Random"], 0, ArrayValues[0].Select(t => (int)t).ToArray().Length, Watch.ElapsedMilliseconds, 0, 0);
                values[0] = ArrayValues[i].Select(t => (int)t).ToArray().Length;
                values[1] = Watch.ElapsedMilliseconds;
                Watch.Reset();
                Watch.Start();
                MergeSort(ArrayValues[i].Select(t => (int)t).ToArray());
                Watch.Stop();
                //UpdateDataTable(DataSet.Tables["Random"], 0, ArrayValues[0].Select(t => (int)t).ToArray().Length, 0, Watch.ElapsedMilliseconds, 0);
                values[2] = Watch.ElapsedMilliseconds;
                Watch.Reset();
                Watch.Start();

                try
                {
                    await Task.Run(() => this.Sort(ArrayValues[i].Select(t => (int)t).ToArray(), 0, ArrayValues[i].Select(t => (int)t).ToArray().Length - 1));
                }
                catch (Exception)
                {

                    throw;
                }

                Watch.Stop();
                values[3] = Watch.ElapsedMilliseconds;

                UpdateDataTable(DataSet.Tables["Random"], i, values[0], values[1], values[2], values[3]);
                BindingSource bs = new BindingSource
                {
                    DataSource = DataSet.Tables["Random"]
                };
                dataGridView3.DataSource = bs;

                Application.DoEvents();
            }
        }
    }
}
