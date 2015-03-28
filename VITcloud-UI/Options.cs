using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VITcloud_UI
{
    public partial class Options : Form
    {
        private String hostel;
        private String block;
        private String room;
        private String[] directories;

        public Options()
        {
            InitializeComponent();
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

            Data data = new Data(hostel, block, room, directories);
            data.Scan();

            this.Close();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
