using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSteganography
{
    public partial class Form1 : Form
    {
        VideoFile coverVideo;
        VideoFile newVideo;

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
            panel2.BackColor = Color.FromArgb(100, 0, 0, 0);
            panel3.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                Multiselect = false, ValidateNames = true, Filter = "All Video Files|*.wmv;*.wav;*.mp3;*.mp4;*.mkv;*.avi"
            })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi= new FileInfo(ofd.FileName);
                    coverVideo = new VideoFile(fi.FullName, Path.GetFileName(fi.FullName));
                    label7.Text = Path.GetFileName(fi.FullName) + " <- Ready";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //File.Copy( Path.GetFullPath(coverVideo.getPath()), @"D:\Workspace\asp_net\test\test.mp4");
            newVideo = new VideoFile(coverVideo.getFrameList(), coverVideo.getWidth(), coverVideo.getHeight(), coverVideo.getFilename());
        }
    }
}
