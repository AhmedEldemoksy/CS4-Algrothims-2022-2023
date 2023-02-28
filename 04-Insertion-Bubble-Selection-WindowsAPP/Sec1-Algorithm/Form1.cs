using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sec1_Algorithm
{
    public partial class Form1 : Form
    {
        // Internal variables
        int[] array; // the original array
        int[] arrayBub; // data array after bubble sort
        int[] arrayIns; // array after insertion sort
        int[] arraySel; // array after selection sort

        // Variables fixing the start time of the execution of algorithms
        TimeSpan tsBubble; // bubble algorithm
        TimeSpan tsIns;   // insertion sorting algorithm
        TimeSpan tsSel;   // selection sorting algorithm

        bool fCancelBub; // If true, then the Stop button was pressed - stop all threads
        bool fCancelIns;
        bool fCancelSel;
        // Internal method that displays an array in a ListBox control
        private void DisplayArray(int[] A, ListBox LB)
        {
            LB.Items.Clear();
            for (int i = 0; i < A.Length; i++)
                LB.Items.Add(A[i]);
        }
        // Internal method for activating controls
        private void Active(bool active)
        {
            // Make some controls active/inactive
            label2.Enabled = active;
            label3.Enabled = active;
            label4.Enabled = active;
            label5.Enabled = active;
            label6.Enabled = active;
            label7.Enabled = active;
            listBox1.Enabled = active;
            listBox2.Enabled = active;
            listBox3.Enabled = active;
            progressBar1.Enabled = active;
            progressBar2.Enabled = active;
            progressBar3.Enabled = active;
            button2.Enabled = active;
            button3.Enabled = active;
        }

        public Form1()
        {
            InitializeComponent();

            // Clear textBox1 field
            textBox1.Text = "";

            // Clear ListBox
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            // Clear ProgressBar
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;

            // Deactivate some controls
            Active(false);

            // Configure internal variables
            fCancelBub = false;
            fCancelIns = false;
            fCancelSel = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Deactivate some controls
            Active(false);

            // Customize labels
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";

            // Start generating an array in a thread
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync(); // generate the DoWork event
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Deactivate the generate array button
            button1.Enabled = false;

            // Starting sorting methods in threads
            if (!backgroundWorker2.IsBusy)
                backgroundWorker2.RunWorkerAsync();

            if (!backgroundWorker3.IsBusy)
                backgroundWorker3.RunWorkerAsync();

            if (!backgroundWorker4.IsBusy)
                backgroundWorker4.RunWorkerAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker2.CancelAsync(); // stop bubble sort
                backgroundWorker3.CancelAsync(); // stop the insertion sort
                backgroundWorker4.CancelAsync(); // stop the selection sort
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // 1. Declaring internal variables
            Random rnd = new Random();

            // 2. Get the number of elements in the array
            int n = Convert.ToInt32(textBox1.Text);

            // 3. Allocate memory for arrays and fill them with values
            array = new int[n];
            arrayBub = new int[n];
            arrayIns = new int[n];
            arraySel = new int[n];

            for (int i = 0; i < n; i++)
            {
                Thread.Sleep(1);

                array[i] = rnd.Next(1, n + 1); // random number
                arrayBub[i] = arraySel[i] = arrayIns[i] = array[i]; // copy this number

                // Call the display of progress (change) of thread execution
                try
                {
                    backgroundWorker1.ReportProgress((i * 100) / n);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            // The arrayBub array is sorted
            int x;

            // Initialize time.
            tsBubble = new TimeSpan(DateTime.Now.Ticks);

            for (int i = 0; i < arrayBub.Length; i++)
            {
                Thread.Sleep(1); // allow other threads to run in parallel

                for (int j = arrayBub.Length - 1; j > i; j--)
                {
                    if (arrayBub[j - 1] > arrayBub[j]) // Sort ascending
                    {
                        x = arrayBub[j];
                        arrayBub[j] = arrayBub[j - 1];
                        arrayBub[j - 1] = x;
                    }
                }

                // Display change progress
                try
                {
                    backgroundWorker2.ReportProgress((i * 100) / arrayBub.Length);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                // Checking if the thread was stopped
                if (backgroundWorker2.CancellationPending)
                {
                    fCancelBub = true;
                    break;
                }
            }
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            // 1. Declare internal variables
            int x, i, j;

            // Initialize time
            tsIns = new TimeSpan(DateTime.Now.Ticks);

            // 2. Sorting cycle
            for (i = 0; i < arrayIns.Length; i++)
            {
                // allow other threads to run in parallel
                Thread.Sleep(1);

                x = arrayIns[i];

                // Finding the place of an element in a sequence
                for (j = i - 1; j >= 0 && arrayIns[j] > x; j--)
                    arrayIns[j + 1] = arrayIns[j]; // right shift the element

                arrayIns[j + 1] = x;

                // Display change progress
                try
                {
                    backgroundWorker3.ReportProgress((i * 100) / arrayIns.Length);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                // Checking if the thread was stopped
                if (backgroundWorker3.CancellationPending)
                {
                    fCancelIns = true;
                    break;
                }
            }
        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            // 1. Declare variables
            int i, j, k;
            int x;

            // 2. Set the initial time
            tsSel = new TimeSpan(DateTime.Now.Ticks);

            // 3. The loop of selection sorting
            for (i = 0; i < arraySel.Length; i++)
            {
                // allow other threads to run in parallel
                Thread.Sleep(1);

                k = i;

                // smallest element search
                x = arraySel[i];

                for (j = i + 1; j < arraySel.Length; j++)
                    if (arraySel[j] < x)
                    {
                        k = j; // k - index of the smallest element
                        x = arraySel[j];
                    }

                // swap smallest element with arraySel[i]
                arraySel[k] = arraySel[i];
                arraySel[i] = x;

                // Show progress change
                try
                {
                    backgroundWorker4.ReportProgress((i * 100) / arraySel.Length);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                // Checking if the thread was stopped
                if (backgroundWorker4.CancellationPending)
                {
                    fCancelSel = true;
                    break;
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Display changes to the text "Generate array" button
            button1.Text = "Generate array " + e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label5.Text = Convert.ToString(e.ProgressPercentage) + " %";
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label6.Text = Convert.ToString(e.ProgressPercentage) + " %";
            progressBar2.Value = e.ProgressPercentage;
        }

        private void backgroundWorker4_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label7.Text = Convert.ToString(e.ProgressPercentage) + " %";
            progressBar3.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // After the array is generated, make the appropriate settings
            button1.Text = "Generate array";

            // Make visible active controls
            Active(true);

            // Display the original array in controls of type ListBox
            DisplayArray(array, listBox1);
            DisplayArray(array, listBox2);
            DisplayArray(array, listBox3);
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // If sorting was canceled
            if (fCancelBub)
            {
                // Customize controls accordingly
                label5.Text = "";

                // Display original array
                DisplayArray(array, listBox1);

                fCancelBub = false;
            }
            else
            {
                // Fix the time and display it
                TimeSpan time = new TimeSpan(DateTime.Now.Ticks) - tsBubble;
                label5.Text = String.Format("{0}.{1}.{2}.{3}", time.Hours, time.Minutes,
                time.Seconds, time.Milliseconds);

                // Display the sorted array
                DisplayArray(arrayBub, listBox1);
            }

            // Configure other controls
            progressBar1.Value = 0;
            button1.Enabled = true;
        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // If sorting was canceled
            if (fCancelIns)
            {
                // Customize controls accordingly
                label6.Text = "";

                // Display the original array
                DisplayArray(array, listBox2);

                fCancelIns = false;
            }
            else
            {
                // Fix the time and display it
                TimeSpan time = new TimeSpan(DateTime.Now.Ticks) - tsIns;
                label6.Text = String.Format("{0}.{1}.{2}.{3}", time.Hours, time.Minutes,
                time.Seconds, time.Milliseconds);

                // Display the sorted array
                DisplayArray(arrayIns, listBox2);
            }

            // Customize other controls
            progressBar2.Value = 0;
            button1.Enabled = true;
        }

        private void backgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // If sorting was canceled
            if (fCancelSel)
            {
                // Customize controls accordingly
                label7.Text = "";

                // Display original array
                DisplayArray(array, listBox3);

                fCancelSel = false;
            }
            else
            {
                // Fix the time and display it
                TimeSpan time = new TimeSpan(DateTime.Now.Ticks) - tsSel;
                label7.Text = String.Format("{0}.{1}.{2}.{3}", time.Hours, time.Minutes,
                time.Seconds, time.Milliseconds);

                // Display sorted array
                DisplayArray(arraySel, listBox3);
            }

            // Customize other controls
            progressBar3.Value = 0;
            button1.Enabled = true;
        }
    }
}
