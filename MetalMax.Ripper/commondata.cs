using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace MetalMax.Ripper
{
    public class commondata
    {
        // Fields
        public Dictionary<int, byte[]> chrimagedata = new Dictionary<int, byte[]>();
        public static Color[] nes_Palette = new Color[] { 
        Color.FromArgb(0xff, 0x80, 0x80, 0x80), Color.FromArgb(0xff, 0, 0x3d, 0xa6), Color.FromArgb(0xff, 0, 0x12, 0xb0), Color.FromArgb(0xff, 0x44, 0, 150), Color.FromArgb(0xff, 0xa1, 0, 0x5e), Color.FromArgb(0xff, 0xc7, 0, 40), Color.FromArgb(0xff, 0xba, 6, 0), Color.FromArgb(0xff, 140, 0x17, 0), Color.FromArgb(0xff, 0x5c, 0x2f, 0), Color.FromArgb(0xff, 0x10, 0x45, 0), Color.FromArgb(0xff, 5, 0x4a, 0), Color.FromArgb(0xff, 0, 0x47, 0x2e), Color.FromArgb(0xff, 0, 0x41, 0x66), Color.FromArgb(0xff, 0, 0, 0), Color.FromArgb(0xff, 5, 5, 5), Color.FromArgb(0xff, 5, 5, 5), 
        Color.FromArgb(0xff, 0xc7, 0xc7, 0xc7), Color.FromArgb(0xff, 0, 0x77, 0xff), Color.FromArgb(0xff, 0x21, 0x55, 0xff), Color.FromArgb(0xff, 130, 0x37, 250), Color.FromArgb(0xff, 0xeb, 0x2f, 0xb5), Color.FromArgb(0xff, 0xff, 0x29, 80), Color.FromArgb(0xff, 0xff, 0x22, 0), Color.FromArgb(0xff, 0xd6, 50, 0), Color.FromArgb(0xff, 0xc4, 0x62, 0), Color.FromArgb(0xff, 0x35, 0x80, 0), Color.FromArgb(0xff, 5, 0x8f, 0), Color.FromArgb(0xff, 0, 0x8a, 0x55), Color.FromArgb(0xff, 0, 0x99, 0xcc), Color.FromArgb(0xff, 0x21, 0x21, 0x21), Color.FromArgb(0xff, 9, 9, 9), Color.FromArgb(0xff, 9, 9, 9), 
        Color.FromArgb(0xff, 0xff, 0xff, 0xff), Color.FromArgb(0xff, 15, 0xd7, 0xff), Color.FromArgb(0xff, 0x69, 0xa2, 0xff), Color.FromArgb(0xff, 0xd4, 0x80, 0xff), Color.FromArgb(0xff, 0xff, 0x45, 0xf3), Color.FromArgb(0xff, 0xff, 0x61, 0x8b), Color.FromArgb(0xff, 0xff, 0x88, 0x33), Color.FromArgb(0xff, 0xff, 0x9c, 0x12), Color.FromArgb(0xff, 250, 0xbc, 0x20), Color.FromArgb(0xff, 0x9f, 0xe3, 14), Color.FromArgb(0xff, 0x2b, 240, 0x35), Color.FromArgb(0xff, 12, 240, 0xa4), Color.FromArgb(0xff, 5, 0xfb, 0xff), Color.FromArgb(0xff, 0x5e, 0x5e, 0x5e), Color.FromArgb(0xff, 13, 13, 13), Color.FromArgb(0xff, 13, 13, 13), 
        Color.FromArgb(0xff, 0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xa6, 0xfc, 0xff), Color.FromArgb(0xff, 0xb3, 0xec, 0xff), Color.FromArgb(0xff, 0xda, 0xab, 0xeb), Color.FromArgb(0xff, 0xff, 0xa8, 0xf9), Color.FromArgb(0xff, 0xff, 0xab, 0xb3), Color.FromArgb(0xff, 0xff, 210, 0xb0), Color.FromArgb(0xff, 0xff, 0xef, 0xa6), Color.FromArgb(0xff, 0xff, 0xf7, 0x9c), Color.FromArgb(0xff, 0xd7, 0xe8, 0x95), Color.FromArgb(0xff, 0xa6, 0xed, 0xaf), Color.FromArgb(0xff, 0xa2, 0xf2, 0xda), Color.FromArgb(0xff, 0x99, 0xff, 0xfc), Color.FromArgb(0xff, 0xdd, 0xdd, 0xdd), Color.FromArgb(0xff, 0x11, 0x11, 0x11), Color.FromArgb(0xff, 0x11, 0x11, 0x11)
     };
        public const int Palette_cellspace = 1;
        public const int Palette_width = 15;
        public static byte[] sprite_palette = new byte[] { 15, 0x36, 15, 0x16, 15, 0x36, 15, 0x21, 15, 0x37, 15, 0x18, 15, 0x36, 15, 0x12 };

        // Methods
        public void Add_CHR_index(int index, byte[] data)
        {
            if (!this.chrimagedata.ContainsKey(index))
            {
                byte[] buffer = new byte[0x1000];
                byte[] buffer2 = new byte[8];
                byte[] buffer3 = new byte[8];
                for (int i = 0; i < 0x40; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        buffer2[j] = data[(i * 0x10) + j];
                    }
                    for (int k = 0; k < 8; k++)
                    {
                        buffer3[k] = data[((i * 0x10) + k) + 8];
                    }
                    int num4 = i / 0x10;
                    int num5 = i % 0x10;
                    for (int m = 0; m < 8; m++)
                    {
                        for (int n = 0; n < 8; n++)
                        {
                            buffer[(((0x400 * num4) + (8 * num5)) + (0x80 * m)) + n] = (byte)(((buffer2[m] >> (7 - n)) % 2) + (((buffer3[m] >> (7 - n)) % 2) * 2));
                        }
                    }
                }
                this.chrimagedata.Add(index, buffer);
            }
        }

        public static void writetofile(string filename, string[] content)
        {
            StreamWriter writer = new StreamWriter(filename, false, Encoding.Default);
            for (int i = 0; i < content.Length; i++)
            {
                writer.Write(content[i]);
                writer.WriteLine();
            }
            writer.Flush();
            writer.Close();
        }
    }




}
