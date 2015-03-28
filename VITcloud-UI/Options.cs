using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace VITcloud_UI
{
    public partial class Options : Form
    {
        private String hostel;
        private String block;
        private String room;
        private String[] directories;

        BackgroundWorker worker;

        public Options()
        {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            directories = new String[4];
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void browse_button_1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            directory_1.Text = folderBrowserDialog.SelectedPath;
        }

        private void browse_button_2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            directory_2.Text = folderBrowserDialog.SelectedPath;
        }

        private void browse_button_3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            directory_3.Text = folderBrowserDialog.SelectedPath;
        }

        private void browse_button_4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            directory_4.Text = folderBrowserDialog.SelectedPath;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            hostel = hostel_dropdown.Text;
            block = block_text.Text;
            room = room_text.Text;

            directories[0] = directory_1.Text;
            directories[1] = directory_2.Text;
            directories[2] = directory_3.Text;
            directories[3] = directory_4.Text;

            if (!worker.IsBusy)
            {
                progressBar.Visible = true;
                progressBar.Minimum = 1;
                progressBar.Maximum = directories.Length;
                progressBar.Value = 1;
                progressBar.Step = 1;

                worker.RunWorkerAsync();
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            if (worker.WorkerSupportsCancellation && worker.IsBusy)
            {
                progressBar.Visible = false;

                worker.CancelAsync();
            }
        }

        private void worker_doWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int count = 0; count < 4; ++count)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    scan(directories[count]);
                    worker.ReportProgress(count * 25);
                }
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.PerformStep();
        }

        private void worker_workComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            String message;
            String caption;
            if (e.Cancelled == true)
            {
                caption = "Cancelled Operation";
                message = "You have cancelled the operation. ";
            }
            else if (e.Error != null)
            {
                caption = "Error Occured";
                message = e.Error.Message;
            }
            else
            {
                caption = "Successful Operation";
                message = "Successfully Scanned all Media Files";
            }

            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void scan(String path)
        {

        }
    }
}
