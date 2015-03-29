/*
 *  VITcloud: Windows Desktop Client Application
 *  Copyright (C) 2015  Aneesh Neelam <neelam.aneesh@gmail.com>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace VITcloud_UI
{
    public partial class Options : Form
    {
        private const String VITcloud_Interface = "https://vitcloud-biocross.rhcloud.com/interface";

        private String hostel;
        private String block;
        private String room;
        List <String> fileData;

        private String[] directories;

        BackgroundWorker worker;

        public Options()
        {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += new DoWorkEventHandler(worker_doWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_workComplete);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);

            directories = new String[4];
            fileData = new List <String> ();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar.Visible = false;
            cancel_button.Enabled = false;
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
                ok_button.Enabled = false;
                cancel_button.Enabled = true;

                progressBar.Visible = true;
                progressBar.Minimum = 0;
                progressBar.Maximum = directories.Length + 1;
                progressBar.Value = 1;
                progressBar.Step = 1;

                worker.RunWorkerAsync();
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            if (worker.WorkerSupportsCancellation && worker.IsBusy)
            {
                ok_button.Enabled = true;
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
                    worker.ReportProgress(count * 20);
                }
            }
            upload();
            worker.ReportProgress(100);
            
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.PerformStep();
        }

        private void worker_workComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            ok_button.Enabled = true;
            cancel_button.Enabled = false;
            progressBar.Visible = false;

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
                message = "Successfully Scanned all Media Files. ";
            }

            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void scan(String path)
        {
            if (Directory.Exists(path))
            {
                getProperties(Directory.GetFiles(path, "*.*", SearchOption.AllDirectories));
            }
        }

        private void getProperties(String[] files)
        {
            foreach(String file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                fileData.Add(fileInfo.Name);
                fileData.Add(fileInfo.Length.ToString());
            }
        }

        private void upload()
        {
            Data data = new Data(hostel, block, room, fileData.ToArray());
            String json = JsonConvert.SerializeObject(data);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(VITcloud_Interface);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}
