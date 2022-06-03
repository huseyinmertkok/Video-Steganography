using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoSteganography
{
    internal class Stegano
    {

        public Stegano() 
        { 
        }


        public static List<Bitmap> HideTextToFrames(List<Bitmap> frames, string message, int width, int height)
        {
            List<Bitmap> tempFrames = frames;
            Color pixel, newColor;
            int charIndex = 0;
            string binaryCharIndex = "";
            int x = width - 3;
            int y = height - 1;
            int R = 0; int G = 0; int B = 0;


            //First Frame Settings
            newColor = Color.FromArgb(0, 0, 0);
            pixel = tempFrames[0].GetPixel(x, y);
            string binMessageLength = IntegerToBinary(message.Length);
            R = pixel.R; G = pixel.G; B = pixel.B;
            string binR = IntegerToBinary(R);
            string binG = IntegerToBinary(G);
            string binB = IntegerToBinary(B);

            binR = binR.Remove(binR.Length - 2);
            binR = binR + binMessageLength[0] + binMessageLength[1];

            binG = binG.Remove(binG.Length - 3);
            binG = binG + binMessageLength[2] + binMessageLength[3] + binMessageLength[4];

            binB = binB.Remove(binB.Length - 3);
            binB = binB + binMessageLength[5] + binMessageLength[6] + binMessageLength[7];

            R = BinaryToInteger(binR); G = BinaryToInteger(binG); B = BinaryToInteger(binB);

            newColor = Color.FromArgb(R, G, B);
            tempFrames[0].SetPixel(x, y, newColor);
            //End of First Frame Settings

            int frameCounter = 1;
            int messageCounter = 0;
            while (true)
            {
                newColor = Color.FromArgb(0, 0, 0);
                R = 0; G = 0; B = 0;
                pixel = tempFrames[frameCounter].GetPixel(x, y);

                charIndex = (int)message[messageCounter];
                binaryCharIndex = IntegerToBinary(charIndex);

                R = pixel.R;G = pixel.G;B = pixel.B;

                binR = IntegerToBinary(R);binG = IntegerToBinary(G);binB = IntegerToBinary(B);

                binR = binR.Remove(binR.Length - 2);
                binR = binR + binaryCharIndex[0] + binaryCharIndex[1];

                binG = binG.Remove(binG.Length - 3);
                binG = binG + binaryCharIndex[2] + binaryCharIndex[3] + binaryCharIndex[4];

                binB = binB.Remove(binB.Length - 3);
                binB = binB + binaryCharIndex[5] + binaryCharIndex[6] + binaryCharIndex[7];

                R = BinaryToInteger(binR); G = BinaryToInteger(binG); B = BinaryToInteger(binB);

                newColor = Color.FromArgb(R, G, B);
                tempFrames[frameCounter].SetPixel(x, y, newColor);

                messageCounter++;
                frameCounter++;
                if(messageCounter == message.Length)
                {
                    break;
                }
                if(frameCounter == tempFrames.Count())
                {
                    frameCounter = 1;
                    x--;
                    if(x < 0)
                    {
                        x = width - 1;
                        y--;
                        if(y < 0)
                        {
                            return frames;
                        }
                    }
                }
            }

            return tempFrames;
        }

        public static string UncoverMessageFromImage(Bitmap frame)
        {
            var message = "";
            Color pixel;
            int R, G, B;
            pixel = frame.GetPixel(frame.Width - 3, frame.Height - 1);
            R = pixel.R; G = pixel.G; B = pixel.B;
            string binR = IntegerToBinary(R);
            string binG = IntegerToBinary(G);
            string binB = IntegerToBinary(B);
            string binLetter = binR[6].ToString() + binR[7] + binG[5] + binG[6] + binG[7] + binB[5] + binB[6] + binB[7];
            message += Convert.ToChar(BinaryToInteger(binLetter));
            return message;
        }

        public static string UncoverMessageFromFrames(List<Bitmap> frames, int width, int height)
        {
            string message = "";
            List<Bitmap> tempFrames = frames;
            Color pixel;
            int x = width - 3;
            int y = height - 1;

            int R = 0; int G = 0; int B = 0;
            pixel = tempFrames[0].GetPixel(x, y);
            
            R = pixel.R; G = pixel.G; B = pixel.B;
            string binR = IntegerToBinary(R);
            string binG = IntegerToBinary(G);
            string binB = IntegerToBinary(B);
            string binMessageLength =  "" + binR[6] + binR[7] + binG[5] + binG[6] + binG[7] + binB[5] + binB[6] + binB[7];
            int messageLength = BinaryToInteger(binMessageLength);

            int frameCounter = 1;
            int messageCounter = 0;
            int asciiLetter = 0;
            string binLetter = "";
            while (true)
            {
                R = 0; G = 0; B = 0;
                pixel = tempFrames[frameCounter].GetPixel(x, y);

                R = pixel.R; G = pixel.G; B = pixel.B;
                binR = IntegerToBinary(R);
                binG = IntegerToBinary(G);
                binB = IntegerToBinary(B);

                binLetter = "" + binR[6] + binR[7] + binG[5] + binG[6] + binG[7] + binB[5] + binB[6] + binB[7];
                asciiLetter = BinaryToInteger(binLetter);

                message += Convert.ToChar(asciiLetter);

                messageCounter++;
                frameCounter++;
                if (messageCounter == messageLength)
                {
                    break;
                }
                if (frameCounter == tempFrames.Count())
                {
                    frameCounter = 1;
                    x--;
                    if (x < 0)
                    {
                        x = width - 1;
                        y--;
                        if (y < 0)
                        {
                            return "";
                        }
                    }
                }
            }

            return message;
        }

        public static string IntegerToBinary(int value)
        {
            string binary = Convert.ToString(value, 2);
            if(binary.Length == 8)
            {
                return binary;
            }
            else
            {
                for(int i = binary.Length; i < 8; i++)
                {
                    binary = "0" + binary;
                }
                return binary;
            }
        }

        public static int BinaryToInteger(string binary)
        {
            return Convert.ToInt32(binary, 2);
        }

    }
}
