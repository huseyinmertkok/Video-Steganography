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
        VideoFile alreadyCoveredVideo;

        Stegano stegano;

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
            stegano = new Stegano();
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

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                Multiselect = false,
                ValidateNames = true,
                Filter = "All Video Files|*.wmv;*.wav;*.mp3;*.mp4;*.mkv;*.avi"
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    alreadyCoveredVideo = new VideoFile(fi.FullName, Path.GetFileName(fi.FullName));
                    label8.Text = Path.GetFileName(fi.FullName) + " <- Ready";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(coverVideo == null)
            {
                MessageBox.Show("Please Select Cover Video!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                newVideo = new VideoFile(coverVideo.GetFrameList(), coverVideo.GetWidth(), coverVideo.GetHeight(), coverVideo.GetFps(), coverVideo.GetBitrate(), richTextBox1.Text);
                SaveFileDialog save = new SaveFileDialog();
                save.DefaultExt = "mp4";
                save.Filter = "Video File (*.mp4;)|*.mp4;";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(save.FileName);
                    if (coverVideo.GetPath().Equals(fi.FullName))
                    {
                        MessageBox.Show("Please Choose a Different Path from Cover Video!", "Same Folders", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        label10.Visible = true; button4.Visible = false; button2.Visible = false;
                        newVideo.AddSoundtoTheVideo(coverVideo, fi.FullName);
                        label10.Visible = false; button4.Visible = true;button2.Visible = true;
                    }
                }
            } 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = Stegano.UncoverMessageFromFrames(alreadyCoveredVideo.GetFrameList(), alreadyCoveredVideo.GetWidth(), alreadyCoveredVideo.GetHeight());
            //Bitmap bitmap = new Bitmap(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\META_DATA\images\2.bmp");
            //richTextBox2.Text = Stegano.UncoverMessageFromImage(bitmap);
        }
    }
}
