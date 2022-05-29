using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Video.FFMPEG;

namespace VideoSteganography
{
    internal class VideoFile
    {
        private string Path;
        private string Filename;
        private long FrameCount;
        private int width;
        private int height;

        VideoFileReader reader;
        VideoFileWriter writer;

        private List<Bitmap> FrameList;


        public VideoFile(string path, string filename)
        {
            Console.Out.WriteLine();
            Console.Out.WriteLine(Environment.CurrentDirectory);
            this.Path = path;
            this.Filename = filename;
            reader = new VideoFileReader();
            reader.Open(path);
            FrameCount = reader.FrameCount;
            this.width = reader.Width;
            this.height = reader.Height;
            FrameList = getVideoFrames(FrameCount);
        }

        public VideoFile(List<Bitmap> frames, int width, int height, string name)
        {
            using (FileStream fs = File.Create(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\test\test.mp4")) 
            writer = new VideoFileWriter();
            writer.Open(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Test\test.mp4", width, height);
            for (int i = 0; i < frames.Count(); i++)
            {
                writer.WriteVideoFrame(frames[i]);
            }
            writer.Close();
        }


        public List<Bitmap> getFrameList()
        {
            return FrameList;
        }

        public string getPath()
        {
            return Path;
        }

        public string getFilename()
        {
            return Filename;
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        public VideoFileWriter GetWriter()
        {
            return writer;
        }


        private List<Bitmap> getVideoFrames(long frameCount)
        {
            List<Bitmap> frames = new List<Bitmap>();
            Bitmap videoFrame;
            for (int i = 0; i < frameCount; i++)
            {
                videoFrame = reader.ReadVideoFrame();
                frames.Add(videoFrame);
            }
            return frames;
        }


    }
}
