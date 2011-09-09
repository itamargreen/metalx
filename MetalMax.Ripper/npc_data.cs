using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;

namespace MetalMax.Ripper
{
public class npc_data
{
    // Fields
    public Bitmap npc_image;
    public byte[] npc_store_data;

    // Methods
    public npc_data(byte[] data, int PattenTable1, ref commondata chrdata)
    {
        int[] numArray = new int[10];
        this.npc_store_data = data;
        byte[] array = new byte[0x4000];
        chrdata.chrimagedata[4].CopyTo(array, 0);
        chrdata.chrimagedata[5].CopyTo(array, 0x1000);
        chrdata.chrimagedata[PattenTable1].CopyTo(array, 0x2000);
        chrdata.chrimagedata[PattenTable1 + 1].CopyTo(array, 0x3000);
        int num = Form1.getfilecontent((long) (0x345ae + this.image)) + Form1.npc_data_a597[this.direct];
        int num2 = Form1.getfilecontent((long) (0x2648b + num));
        numArray = new int[] { Form1.getfilecontent((long) (0x26562 + num)), (Form1.getfilecontent((long) (0x26402 + (num2 & 7))) + numArray[0]) & 0xff, Form1.getfilecontent((long) (0x26639 + num)), (Form1.getfilecontent((long) (0x2640a + (num2 & 7))) + numArray[2]) & 0xff };
        num2 = Form1.getfilecontent((long) (0x2648b + num));
        int num3 = Form1.getfilecontent((long) (0x345ed + this.image));
        int num4 = (num3 | Form1.npc_data_A59B[this.direct]) & 3;
        IList<Bitmap> list = new List<Bitmap>();
        byte[] originalImageData = new byte[0x40];
        for (int i = 0; i < 4; i++)
        {
            int num6 = numArray[i];
            for (int j = 0; j < 8; j++)
            {
                for (int m = 0; m < 8; m++)
                {
                    originalImageData[j + (m * 8)] = (byte) (array[((((num6 / 0x10) * 0x400) + (0x80 * m)) + ((num6 % 0x10) * 8)) + j] + (num4 * 4));
                }
            }
            for (int k = 0; k < originalImageData.Length; k++)
            {
                originalImageData[k] = commondata.sprite_palette[originalImageData[k]];
            }
            Bitmap item = bmp.CreateBitmap(originalImageData, 8, 8);
            item.Palette = Form1.cp;
            if ((num3 & 0x80) != 0)
            {
                item.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
            if ((((num3 << (4 - i)) & 0x40) ^ (num3 & 0x40)) != 0)
            {
                item.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            list.Add(item);
        }
        Image image = new Bitmap(0x10, 0x10, PixelFormat.Format32bppPArgb);
        Graphics graphics = Graphics.FromImage(image);
        graphics.DrawImage(list[0], 0, 0);
        graphics.DrawImage(list[1], 8, 0);
        graphics.DrawImage(list[2], 0, 8);
        graphics.DrawImage(list[3], 8, 8);
        this.npc_image = (Bitmap) image;
    }

    // Properties
    public int _3CD
    {
        get
        {
            return ((this.npc_store_data[1] & 0xc0) | ((this.npc_store_data[2] & 0xc0) >> 2));
        }
        set
        {
            this.npc_store_data[1] = (byte) ((this.npc_store_data[1] & 0x3f) | (value & 0xc0));
            this.npc_store_data[2] = (byte) ((this.npc_store_data[2] & 0x3f) | ((value & 0x30) << 2));
        }
    }

    public int _3DB
    {
        get
        {
            return this.npc_store_data[4];
        }
        set
        {
            this.npc_store_data[4] = (byte) value;
        }
    }

    public int _3E9
    {
        get
        {
            return this.npc_store_data[3];
        }
        set
        {
            this.npc_store_data[3] = (byte) value;
        }
    }

    public int _3F7
    {
        get
        {
            return this.npc_store_data[5];
        }
        set
        {
            this.npc_store_data[5] = (byte) value;
        }
    }

    public int direct
    {
        get
        {
            return (this.npc_store_data[0] & 3);
        }
        set
        {
            this.npc_store_data[0] = (byte) ((this.npc_store_data[0] & 0xfc) | value);
        }
    }

    public int image
    {
        get
        {
            return (this.npc_store_data[0] >> 2);
        }
        set
        {
            this.npc_store_data[0] = (byte) ((this.npc_store_data[0] & 3) | (value << 2));
        }
    }

    public int X
    {
        get
        {
            return (this.npc_store_data[1] & 0x3f);
        }
        set
        {
            this.npc_store_data[1] = (byte) ((this.npc_store_data[1] & 0xc0) | value);
        }
    }

    public int Y
    {
        get
        {
            return (this.npc_store_data[2] & 0x3f);
        }
        set
        {
            this.npc_store_data[2] = (byte) ((this.npc_store_data[2] & 0xc0) | value);
        }
    }
}

 
 

}
