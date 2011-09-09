using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MetalMax.Ripper
{
    public class bmp
    {
        // Methods
        public static Bitmap CreateBitmap(byte[] originalImageData, int originalWidth, int originalHeight)
        {
            Bitmap bitmap = new Bitmap(originalWidth, originalHeight, PixelFormat.Format8bppIndexed);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Bmp);
            stream.Flush();
            int num = ((((originalWidth * 8) + 0x1f) / 0x20) * 4) - originalWidth;
            int count = ((((originalWidth * 8) + 0x1f) / 0x20) * 4) * originalHeight;
            int num3 = ReadData(stream, 10, 4);
            byte[] buffer = new byte[count];
            int num4 = originalWidth + num;
            for (int i = originalHeight - 1; i >= 0; i--)
            {
                int num6 = (originalHeight - i) - 1;
                for (int j = 0; j < originalWidth; j++)
                {
                    buffer[(num6 * num4) + j] = originalImageData[(i * originalWidth) + j];
                }
            }
            stream.Position = num3;
            stream.Write(buffer, 0, count);
            stream.Flush();
            return new Bitmap(stream);
        }

        public static int ReadData(MemoryStream curStream, int startPosition, int length)
        {
            byte[] buffer = new byte[length];
            curStream.Position = startPosition;
            curStream.Read(buffer, 0, length);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static void WriteData(MemoryStream curStream, int startPosition, int length, int value)
        {
            curStream.Position = startPosition;
            curStream.Write(BitConverter.GetBytes(value), 0, length);
        }
    }



}
