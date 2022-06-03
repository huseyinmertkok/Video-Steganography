using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Accord.Video.FFMPEG;
using FFMpegSharp;
using FFMpegSharp.FFMPEG;

namespace VideoSteganography
{
    internal class VideoFile
    {
        private string Path;
        private string Filename;
        private long FrameCount;
        private int width;
        private int height;
        private double fps;
        private int bitrate;

        VideoFileReader reader;
        VideoFileWriter writer;

        private List<Bitmap> FrameList;

        

        public VideoFile(string path, string filename)
        {
            this.Path = path;
            this.Filename = filename;
            reader = new VideoFileReader();
            reader.Open(path);
            FrameCount = reader.FrameCount;
            this.width = reader.Width;
            this.height = reader.Height;
            this.fps = reader.FrameRate.Value;
            this.bitrate = reader.BitRate;
            FrameList = GetVideoFrames(FrameCount);
        }

        public VideoFile(List<Bitmap> frames, int width, int height, double fps, int bitrate, string message)
        {
            Path = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\META_DATA\test.mp4";
            this.FrameCount = frames.Count();
            this.width = width;
            this.height = height;
            this.fps = fps;
            this.bitrate = bitrate;

            if (File.Exists(Path))
            {
                File.Delete(Path);
            }

            frames = Stegano.HideTextToFrames(frames, message, width, height);
            FrameList = frames;

            int i = 0;
            foreach (Bitmap frame in frames)
            {
                frame.Save(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\META_DATA\images\"+ i + ".bmp");
                i++;
            }

            FramesToVideo(FrameList);
        }

        public List<Bitmap> GetFrameList()
        {
            return FrameList;
        }

        public string GetPath()
        {
            return Path;
        }

        public string GetFilename()
        {
            return Filename;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public VideoFileWriter GetWriter()
        {
            return writer;
        }

        public int GetBitrate()
        {
            return bitrate;
        }

        public double GetFps()
        {
            return fps;
        }


        private List<Bitmap> GetVideoFrames(long frameCount)
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

        public void FramesToVideo(List<Bitmap> frames)
        {
            using (FileStream fs = File.Create(Path))
                writer = new VideoFileWriter();
            writer.Open(Path, width, height, (int)fps, VideoCodec.MPEG4, bitrate);

            for (int i = 0; i < FrameCount; i++)
            {
                writer.WriteVideoFrame(frames[i]);
            }

            writer.Close();
        }


        public void AddSoundtoTheVideo(VideoFile audioVideo, String output)
        {
            var audioDir = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\META_DATA\audio.mp3";
            var newVideoDir = output;

            if (File.Exists(audioDir))
            {
                File.Delete(audioDir);
            }
            if (File.Exists(newVideoDir))
            {
                File.Delete(newVideoDir);
            }

            new FFMpeg().ExtractAudio(
                VideoInfo.FromPath(audioVideo.GetPath()),
                new FileInfo(audioDir));

            new FFMpeg().ReplaceAudio(VideoInfo.FromPath(this.Path),
                new FileInfo(audioDir),
                new FileInfo(newVideoDir));
            File.Delete(audioDir);
            File.Delete(this.Path);

            MessageBox.Show("Process is over!\n The location of your video: " + newVideoDir, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
