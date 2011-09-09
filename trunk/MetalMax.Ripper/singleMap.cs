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
public class singleMap
{
    // Fields
    public Bitmap cityBmp;
    public byte[] dynamicdata;
    public byte[] enterdata;
    public List<enterPoint> enters;
    public IList<hideItemdata> hideItem = new List<hideItemdata>();
    public int map_addr;
    public Bitmap[] map_item_image = new Bitmap[0x80];
    public byte[] map_store_data;
    public int mapentershare;
    public int mapnpcshare;
    public int mapstoreshare;
    public byte[] maptitles;
    public byte[] maptitles_1 = new byte[640];
    public byte[] maptitlestore;
    public byte[] nametable;
    public int npc_addr;
    public IList<npc_data> npc_datas = new List<npc_data>();
    public byte[] pallete = new byte[0x10];
    public List<specPosition> specP = new List<specPosition>();
    public byte[] uper2;

    // Properties
    public int _8b
    {
        get
        {
            return this.map_store_data[20];
        }
        set
        {
            this.map_store_data[20] = (byte) value;
        }
    }

    public int _8c
    {
        get
        {
            return this.map_store_data[0x15];
        }
        set
        {
            this.map_store_data[0x15] = (byte) value;
        }
    }

    public int _8d
    {
        get
        {
            return this.map_store_data[0x16];
        }
        set
        {
            this.map_store_data[0x16] = (byte) value;
        }
    }

    public int _8e
    {
        get
        {
            return this.map_store_data[0x17];
        }
        set
        {
            this.map_store_data[0x17] = (byte) value;
        }
    }

    public int bottomline
    {
        get
        {
            return this.map_store_data[6];
        }
        set
        {
            this.map_store_data[6] = (byte) value;
        }
    }

    public int dynamicMapaddr
    {
        get
        {
            if ((this.map_store_data[0] & 4) == 0)
            {
                return 0;
            }
            return ((this.map_store_data[this.map_store_data.Length - 1] * 0x100) + this.map_store_data[this.map_store_data.Length - 2]);
        }
        set
        {
            if ((this.map_store_data[0] & 4) != 0)
            {
                if (value == 0)
                {
                    byte[] buffer = new byte[this.map_store_data.Length - 2];
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] = this.map_store_data[i];
                    }
                    this.map_store_data = buffer;
                    this.map_store_data[0] = (byte) (this.map_store_data[0] & 0xfb);
                }
                else
                {
                    this.map_store_data[this.map_store_data.Length - 2] = (byte) (value & 0xff);
                    this.map_store_data[this.map_store_data.Length - 1] = (byte) ((value >> 8) & 0xff);
                }
            }
            else if (value != 0)
            {
                byte[] buffer2 = new byte[this.map_store_data.Length + 2];
                for (int j = 0; j < this.map_store_data.Length; j++)
                {
                    buffer2[j] = this.map_store_data[j];
                }
                this.map_store_data = buffer2;
                this.map_store_data[this.map_store_data.Length - 2] = (byte) (value & 0xff);
                this.map_store_data[this.map_store_data.Length - 1] = (byte) ((value >> 8) & 0xff);
                this.map_store_data[0] = (byte) (this.map_store_data[0] | 4);
            }
        }
    }

    public int enterAddr
    {
        get
        {
            return (this.map_store_data[11] + (this.map_store_data[12] * 0x100));
        }
        set
        {
            this.map_store_data[11] = (byte) (value & 0xff);
            this.map_store_data[12] = (byte) ((value >> 8) & 0xff);
        }
    }

    public int leftline
    {
        get
        {
            return this.map_store_data[3];
        }
        set
        {
            this.map_store_data[3] = (byte) value;
        }
    }

    public int MapHeight
    {
        get
        {
            return this.map_store_data[2];
        }
        set
        {
            this.map_store_data[2] = (byte) value;
        }
    }

    public int mapTitleAddr
    {
        get
        {
            return (this.map_store_data[7] + (this.map_store_data[8] * 0x100));
        }
        set
        {
            this.map_store_data[7] = (byte) (value & 0xff);
            this.map_store_data[8] = (byte) ((value >> 8) & 0xff);
        }
    }

    public int maptitlestore_len
    {
        get
        {
            return this.maptitlestore.Length;
        }
    }

    public int MapWidth
    {
        get
        {
            return this.map_store_data[1];
        }
        set
        {
            this.map_store_data[1] = (byte) value;
        }
    }

    public byte[] outjump
    {
        get
        {
            byte[] buffer;
            if (this.enterdata[0] == 0xff)
            {
                return null;
            }
            if (this.enterdata[0] == 0xfe)
            {
                buffer = new byte[3];
                Array.Copy(this.enterdata, 1, buffer, 0, 3);
                return buffer;
            }
            buffer = new byte[12];
            Array.Copy(this.enterdata, 0, buffer, 0, 12);
            return buffer;
        }
    }

    public int palleteaddr
    {
        get
        {
            return ((this.map_store_data[14] * 0x100) + this.map_store_data[13]);
        }
        set
        {
            this.map_store_data[13] = (byte) (value & 0xff);
            this.map_store_data[14] = (byte) ((value >> 8) & 0xff);
        }
    }

    public byte[] PattenTable
    {
        get
        {
            return new byte[] { this.map_store_data[0x10], this.map_store_data[0x11], this.map_store_data[0x12], this.map_store_data[0x13] };
        }
        set
        {
            this.map_store_data[0x10] = value[0];
            this.map_store_data[0x11] = value[1];
            this.map_store_data[0x12] = value[2];
            this.map_store_data[0x13] = value[3];
        }
    }

    public int PattenTable1
    {
        get
        {
            return this.map_store_data[15];
        }
        set
        {
            this.map_store_data[15] = (byte) value;
        }
    }

    public int rightline
    {
        get
        {
            return this.map_store_data[5];
        }
        set
        {
            this.map_store_data[5] = (byte) value;
        }
    }

    public int topline
    {
        get
        {
            return this.map_store_data[4];
        }
        set
        {
            this.map_store_data[4] = (byte) value;
        }
    }

    public int updergroundmap
    {
        get
        {
            return ((this.map_store_data[0] & 8) >> 3);
        }
        set
        {
            if (value != 0)
            {
                this.map_store_data[0] = (byte) ((this.map_store_data[0] & 0xf7) | 8);
            }
            else
            {
                this.map_store_data[0] = (byte) (this.map_store_data[0] & 0xf7);
            }
        }
    }

    public int X1
    {
        get
        {
            return ((this.map_store_data[0] & 0x10) >> 4);
        }
        set
        {
            this.map_store_data[0] = (byte) ((this.map_store_data[0] | 0xef) & (value << 4));
        }
    }
}

 
 

}
