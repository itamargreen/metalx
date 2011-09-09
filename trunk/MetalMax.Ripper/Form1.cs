using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MetalMax.Ripper
{
    public class Form1 : Form
    {
        // Fields
        public commondata _commdata;
        private byte[] _MapTileData = new byte[8];
        public IList<singleMap> allmapdata = new List<singleMap>();
        public byte[] bigmapdynmicdata;
        public IList<hideItemdata> bigmaphideItem = new List<hideItemdata>();
        private Button button1;
        private Button button2;
        private Button button3;
        public IList<MapItemData> clipmapitems = new List<MapItemData>();
        private IContainer components;
        public static ColorPalette cp;
        private Point curorP = new Point(0, 0);
        public int currentbigitemid;
        public MapItemData currentitemdata;
        public int currentmapid;
        public static int enterspnum;
        private Form2 form;
        public 小地图元件 froms;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private HScrollBar hScrollBar1;
        private ImageList imageList1;
        private byte[] kongjian = new byte[0x2000];
        private Label label1;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label2;
        private ListBox listBox1;
        private int map_addr;
        private int map_data_index;
        private int maptitlestore_len;
        private MenuStrip menuStrip1;
        public mondata mondatas;
        public static MemoryStream ms;
        public static int[] npc_data_a597 = new int[] { 0, 2, 4, 4 };
        public static int[] npc_data_A59B;
        private int[] pallete1 = new int[] { 
        100, 100, 100, 100, 100, 100, 100, 100, 100, 0xba, 100, 100, 0x66, 100, 100, 100, 
        0x66, 100, 0x66, 100, 100, 100, 100, 100, 100, 0x66, 0x66, 0xb8, 0x66, 100, 100, 100, 
        100, 100, 100, 100, 100, 0xb8, 100, 0x66, 100, 100, 100, 100, 100, 100, 100, 0x74, 
        100, 100, 100, 100, 100, 100, 100, 0xb8, 100, 0xb8, 100, 100, 0xbc, 100, 100, 100, 
        100, 0xb8, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 
        0x66, 100, 100, 100, 100, 100, 0xbc, 0xb8, 100, 100, 150, 100, 100, 100, 100, 100, 
        100, 0xbc, 0xb8, 0x66, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 
        100, 0xbc, 0x6a, 0x66, 100, 100, 0x6a, 0xbc, 100, 100, 0x74, 0x74, 0x74, 100, 100, 100, 
        100, 0xb8, 100, 100, 100, 0xba, 0xb8, 0xb8, 100, 100, 0x74, 100, 100, 100, 100, 100, 
        100, 0x66, 0xbc, 0xb8, 100, 0xb8, 100, 0xb8, 0x6a, 100, 100, 100, 100, 100, 100, 100, 
        100, 0xba, 0xba, 0xba, 0xba, 0xba, 0x6a, 100, 100, 0x6a, 0x6a, 100, 100, 100, 100, 100, 
        100, 100, 100, 0x66, 100, 100, 100, 100, 100, 100, 100, 100, 0x6b, 0x6b, 100, 100, 
        100, 100, 100, 100, 100, 100, 0xbc, 0x74, 100, 100, 100, 0xba, 0xba, 100, 100, 100, 
        100, 0x74, 0x74, 100, 100, 100, 150, 100, 100, 100, 0xbc, 0xbc, 100, 100, 0x66, 100, 
        0xbc, 0x66, 100, 100, 100, 0x74, 150, 100, 100, 0xbc, 100, 100, 0xbc, 0xbc, 0xbc
     };
        private Panel panel1;
        private Panel panel2;
        public static int pbanknum;
        public static string romf = "机甲战士-简体中文修正版.nes";
        private TextBox textBox1;
        private TextBox textBox14;
        private TextBox textBox15;
        private TextBox textBox16;
        private TextBox textBox17;
        private TextBox textBox18;
        private TextBox textBox19;
        private TextBox textBox2;
        private TextBox textBox4;
        private VScrollBar vScrollBar1;
        private ToolStripMenuItem 保存ToolStripMenuItem;
        private ToolStripMenuItem 打开romToolStripMenuItem;
        private ToolStripMenuItem 导出npc数据ToolStripMenuItem;
        private ToolStripMenuItem 导出地图ToolStripMenuItem;
        private ToolStripMenuItem 导出地图数据ToolStripMenuItem;
        private ToolStripMenuItem 工具ToolStripMenuItem;
        private ToolStripMenuItem 计算空间占用ToolStripMenuItem;
        private ToolStripMenuItem 文件ToolStripMenuItem;

        // Methods
        static Form1()
        {
            int[] numArray = new int[4];
            numArray[3] = 0x40;
            npc_data_A59B = numArray;
        }

        public Form1()
        {
            this.InitializeComponent();
        }

        public void addMapItem(int[,] data, int mapid, string name)
        {
            MapItemData item = new MapItemData(data.GetLength(0), data.GetLength(1), mapid);
            for (int i = 0; i < item.height; i++)
            {
                for (int k = 0; k < item.width; k++)
                {
                    item.subItems[k, i] = data[k, i];
                }
            }
            for (int j = 0; j < item.height; j++)
            {
                for (int m = 0; m < item.width; m++)
                {
                    int index = item.subItems[m, j];
                    for (int n = 0; n < 0x10; n++)
                    {
                        for (int num7 = 0; num7 < 0x10; num7++)
                        {
                            item.ItemImage.SetPixel((m * 0x10) + num7, (j * 0x10) + n, this.allmapdata[mapid].map_item_image[index].GetPixel(num7, n));
                        }
                    }
                }
            }
            item.name = name;
            this.clipmapitems.Add(item);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
            {
                if (this.allmapdata[this.listBox1.SelectedIndex].mapstoreshare != 0)
                {
                    MessageBox.Show("该地图图形是公用的，不能修改，请先更改！");
                }
                else
                {
                    new editform { editmapid = this.listBox1.SelectedIndex, map = this.allmapdata[this.listBox1.SelectedIndex] }.Show(this);
                    this.froms = new 小地图元件();
                    this.froms.Show(this);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
            {
                if (this.allmapdata[this.listBox1.SelectedIndex].mapstoreshare != 0)
                {
                    MessageBox.Show("该地图图形是公用的，不能修改，请先更改！");
                }
                else
                {
                    newmap newmap = new newmap
                    {
                        mapid = this.listBox1.SelectedIndex
                    };
                    bool flag = false;
                    int mapid = 0;
                    int itemid = 0;
                    int maplenth = 0;
                    int mapwidth = 0;
                    if (newmap.ShowDialog(this) == DialogResult.OK)
                    {
                        mapid = newmap.return_mapid;
                        itemid = newmap.return_init_item;
                        maplenth = newmap.return_maplenth;
                        mapwidth = newmap.return_mapwidth;
                        flag = true;
                    }
                    newmap.Dispose();
                    if (flag)
                    {
                        this.createmap(mapid, itemid, maplenth, mapwidth);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
            {
                Form3 form = new Form3();
                int selectedIndex = this.listBox1.SelectedIndex;
                form.mapid = selectedIndex;
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    int mapid = form.mapid;
                    this.allmapdata[selectedIndex].mapstoreshare = mapid;
                    if (mapid != 0)
                    {
                        this.allmapdata[selectedIndex].MapHeight = this.allmapdata[mapid].MapHeight;
                        this.allmapdata[selectedIndex].MapWidth = this.allmapdata[mapid].MapWidth;
                        this.allmapdata[selectedIndex].leftline = this.allmapdata[mapid].leftline;
                        this.allmapdata[selectedIndex].rightline = this.allmapdata[mapid].rightline;
                        this.allmapdata[selectedIndex].topline = this.allmapdata[mapid].topline;
                        this.allmapdata[selectedIndex].bottomline = this.allmapdata[mapid].bottomline;
                    }
                }
                form.Dispose();
            }
        }

        private static float[][] CreateAlphaMatrix(float alpha)
        {
            if (alpha > 1f)
            {
                alpha = 1f;
            }
            if (alpha < 0f)
            {
                alpha = 0f;
            }
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = alpha;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            return numArray2;
        }

        private void createmap(int mapid, int itemid, int maplenth, int mapwidth)
        {
            singleMap map = new singleMap();
            int selectedIndex = this.listBox1.SelectedIndex;
            map = new singleMap
            {
                map_addr = this.allmapdata[mapid].map_addr,
                map_store_data = (byte[])this.allmapdata[selectedIndex].map_store_data.Clone(),
                MapWidth = mapwidth,
                MapHeight = maplenth,
                maptitles = new byte[map.MapHeight * map.MapWidth]
            };
            for (int i = 0; i < map.maptitles.Length; i++)
            {
                map.maptitles[i] = (byte)itemid;
            }
            map.nametable = new byte[(map.MapHeight * map.MapWidth) * 4];
            map.uper2 = new byte[map.MapHeight * map.MapWidth];
            map.PattenTable = this.allmapdata[mapid].PattenTable;
            map.PattenTable1 = this.allmapdata[this.listBox1.SelectedIndex].PattenTable1;
            map.palleteaddr = this.allmapdata[mapid].palleteaddr;
            map.maptitles_1 = (byte[])this.allmapdata[mapid].maptitles_1.Clone();
            map.map_store_data[9] = this.allmapdata[mapid].map_store_data[9];
            map.map_store_data[10] = this.allmapdata[mapid].map_store_data[10];
            map.pallete = (byte[])this.allmapdata[mapid].pallete.Clone();
            int mapWidth = map.MapWidth;
            int mapHeight = map.MapHeight;
            byte[] nametable = map.nametable;
            for (int j = 0; j < mapWidth; j++)
            {
                for (int num6 = 0; num6 < mapHeight; num6++)
                {
                    nametable[((num6 * mapWidth) * 4) + (j * 2)] = map.maptitles_1[map.maptitles[(num6 * mapWidth) + j] * 4];
                    nametable[(((num6 * mapWidth) * 4) + (j * 2)) + 1] = map.maptitles_1[(map.maptitles[(num6 * mapWidth) + j] * 4) + 1];
                    nametable[((num6 * mapWidth) * 4) + ((mapWidth + j) * 2)] = map.maptitles_1[(map.maptitles[(num6 * mapWidth) + j] * 4) + 2];
                    nametable[(((num6 * mapWidth) * 4) + ((mapWidth + j) * 2)) + 1] = map.maptitles_1[(map.maptitles[(num6 * mapWidth) + j] * 4) + 3];
                    map.uper2[(num6 * mapWidth) + j] = (byte)(map.maptitles_1[0x200 + map.maptitles[(num6 * mapWidth) + j]] & 3);
                }
            }
            byte[] originalImageData = new byte[(((map.MapHeight * map.MapWidth) * 4) * 8) * 8];
            byte[] array = new byte[0x4000];
            this._commdata.chrimagedata[map.PattenTable[0]].CopyTo(array, 0);
            this._commdata.chrimagedata[map.PattenTable[1]].CopyTo(array, 0x1000);
            this._commdata.chrimagedata[map.PattenTable[2]].CopyTo(array, 0x2000);
            this._commdata.chrimagedata[map.PattenTable[3]].CopyTo(array, 0x3000);
            for (int k = 0; k < (map.MapWidth * 2); k++)
            {
                for (int num8 = 0; num8 < (map.MapHeight * 2); num8++)
                {
                    int num9 = nametable[k + ((num8 * map.MapWidth) * 2)];
                    int num10 = map.uper2[((num8 / 2) * map.MapWidth) + (k / 2)] * 4;
                    for (int num11 = 0; num11 < 8; num11++)
                    {
                        for (int num12 = 0; num12 < 8; num12++)
                        {
                            originalImageData[(((((num8 * 0x40) * map.MapWidth) * 2) + (((num11 * 8) * map.MapWidth) * 2)) + (k * 8)) + num12] = (byte)(array[((((num9 / 0x10) * 0x400) + (0x80 * num11)) + ((num9 % 0x10) * 8)) + num12] + num10);
                        }
                    }
                }
            }
            for (int m = 0; m < originalImageData.Length; m++)
            {
                originalImageData[m] = map.pallete[originalImageData[m]];
            }
            Bitmap original = bmp.CreateBitmap(originalImageData, (map.MapWidth * 8) * 2, (map.MapHeight * 8) * 2);
            original.Palette = cp;
            Bitmap bitmap2 = new Bitmap(original);
            original.Dispose();
            original = null;
            map.cityBmp = bitmap2.Clone(new Rectangle(0, 0, map.MapWidth * 0x10, map.MapHeight * 0x10), PixelFormat.Format24bppRgb);
            for (int n = 0; n < 0x80; n++)
            {
                byte[] buffer4 = new byte[0x100];
                for (int num15 = 0; num15 < 2; num15++)
                {
                    for (int num16 = 0; num16 < 2; num16++)
                    {
                        int num17 = map.maptitles_1[((n * 4) + (num15 * 2)) + num16];
                        for (int num18 = 0; num18 < 8; num18++)
                        {
                            for (int num19 = 0; num19 < 8; num19++)
                            {
                                buffer4[(((((num15 * 8) * 8) * 2) + ((num18 * 8) * 2)) + (num16 * 8)) + num19] = (byte)(array[((((num17 / 0x10) * 0x400) + (0x80 * num18)) + ((num17 % 0x10) * 8)) + num19] + ((map.maptitles_1[0x200 + n] & 3) * 4));
                            }
                        }
                    }
                }
                for (int num20 = 0; num20 < buffer4.Length; num20++)
                {
                    buffer4[num20] = map.pallete[buffer4[num20]];
                }
                Bitmap bitmap3 = bmp.CreateBitmap(buffer4, 0x10, 0x10);
                bitmap3.Palette = cp;
                map.map_item_image[n] = new Bitmap(bitmap3);
            }
            map.enterdata = this.allmapdata[selectedIndex].enterdata;
            map.enters = this.allmapdata[selectedIndex].enters;
            map.npc_datas = this.allmapdata[selectedIndex].npc_datas;
            map.hideItem = this.allmapdata[selectedIndex].hideItem;
            map.specP = this.allmapdata[selectedIndex].specP;
            new editform { editmapid = selectedIndex, map = map }.Show(this);
            this.froms = new 小地图元件();
            this.froms.Show(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int num3;
            this.panel2.Width = (0x101 + this.panel2.Width) - this.panel2.ClientRectangle.Width;
            this.panel2.Height = ((this.panel2.Height - this.panel2.ClientRectangle.Height) + 15) + 2;
            FileStream input = new FileStream(romf, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader reader = new BinaryReader(input);
            ms = new MemoryStream(reader.ReadBytes((int)reader.BaseStream.Length));
            reader.BaseStream.Seek(4, SeekOrigin.Begin);
            pbanknum = reader.ReadByte();
            reader.BaseStream.Seek(0x2914c, SeekOrigin.Begin);
            enterspnum = reader.ReadByte();
            input.Close();
            cp = this.GetColorPalette(0x40);
            for (int i = 0; i < 0x40; i++)
            {
                cp.Entries[i] = commondata.nes_Palette[i];
            }
            for (int j = 0; j < 0x80; j++)
            {
                this.imageList1.Images.Add(j.ToString(), new Bitmap(1, 1));
            }
            this._commdata = new commondata();
            byte[] buffer = getfilecontents(0xbe12, 0x7e);
            for (num3 = 0; num3 < 0x7e; num3++)
            {
                singleMap item = new singleMap
                {
                    map_addr = (buffer[num3 + 1] * 0x100) + buffer[num3]
                };
                this.allmapdata.Add(item);
                num3++;
            }
            num3 = 0;
            buffer = getfilecontents(0x1deb0, 0x160);
            while (num3 < 0x160)
            {
                singleMap map2 = new singleMap
                {
                    map_addr = (buffer[num3 + 1] * 0x100) + buffer[num3]
                };
                this.allmapdata.Add(map2);
                num3++;
                num3++;
            }
            for (int k = 0; k < this.allmapdata.Count; k++)
            {
                int num5 = this.allmapdata[k].map_addr;
                byte num6 = getfilecontent((long)((0xe010 + num5) - 0x8000));
                int count = 0x18;
                if ((num6 & 1) != 0)
                {
                    count += 2;
                }
                if ((num6 & 4) != 0)
                {
                    count += 2;
                }
                this.allmapdata[k].map_store_data = new byte[count];
                getfilecontents((0xe010 + num5) - 0x8000, count).CopyTo(this.allmapdata[k].map_store_data, 0);
                this.allmapdata[k].maptitles = new byte[this.allmapdata[k].MapHeight * this.allmapdata[k].MapWidth];
                this.allmapdata[k].nametable = new byte[(this.allmapdata[k].MapHeight * this.allmapdata[k].MapWidth) * 4];
                this.allmapdata[k].uper2 = new byte[this.allmapdata[k].MapHeight * this.allmapdata[k].MapWidth];
                int mapTitleAddr = this.allmapdata[k].mapTitleAddr;
                if (mapTitleAddr >= 0xc000)
                {
                    int num9 = mapTitleAddr - 0xc000;
                    this.map_addr = (((pbanknum * 0x4000) + 0x10) + ((0xec + (((num9 >> 8) & 240) >> 2)) * 0x400)) + (num9 & 0xfff);
                }
                else
                {
                    int num10 = mapTitleAddr & 0x1fff;
                    int num11 = mapTitleAddr >> 13;
                    this.map_addr = ((num11 * 0x2000) + 0x10) + num10;
                }
                int off = this.map_addr;
                this.map_data_index = 0;
                this.maptitlestore_len = 0;
                int index = 0;
            Label_0465:
                while (index < this.allmapdata[k].maptitles.Length)
                {
                    byte mapTileData = this.GetMapTileData();
                    if (mapTileData == 0)
                    {
                        byte num15 = this.GetMapTileData();
                        int num16 = this.GetMapTileData();
                        for (int num17 = 0; num17 < num16; num17++)
                        {
                            if (index >= this.allmapdata[k].maptitles.Length)
                            {
                                goto Label_0465;
                            }
                            this.allmapdata[k].maptitles[index] = num15;
                            index++;
                        }
                    }
                    else
                    {
                        this.allmapdata[k].maptitles[index] = mapTileData;
                        index++;
                    }
                }
                this.allmapdata[k].maptitlestore = getfilecontents(off, this.maptitlestore_len);
                getfilecontents((((pbanknum * 0x4000) + 0x10) + ((0xd4 + ((this.allmapdata[k].map_store_data[9] & 240) >> 2)) * 0x400)) + ((this.allmapdata[k].map_store_data[9] & 15) * 0x100), 0x100).CopyTo(this.allmapdata[k].maptitles_1, 0);
                getfilecontents((((pbanknum * 0x4000) + 0x10) + ((0xd4 + ((this.allmapdata[k].map_store_data[10] & 240) >> 2)) * 0x400)) + ((this.allmapdata[k].map_store_data[10] & 15) * 0x100), 0x100).CopyTo(this.allmapdata[k].maptitles_1, 0x100);
                int num18 = ((this.allmapdata[k].map_store_data[9] * 0x100) >> 2) + 0x3700;
                getfilecontents((((pbanknum * 0x4000) + 0x10) + ((0xd4 + ((num18 & 0xf000) >> 10)) * 0x400)) + (num18 & 0xfff), 0x40).CopyTo(this.allmapdata[k].maptitles_1, 0x200);
                num18 = ((this.allmapdata[k].map_store_data[10] * 0x100) >> 2) + 0x3700;
                getfilecontents((((pbanknum * 0x4000) + 0x10) + ((0xd4 + ((num18 & 0xf000) >> 10)) * 0x400)) + (num18 & 0xfff), 0x40).CopyTo(this.allmapdata[k].maptitles_1, 0x240);
                byte[] buffer5 = getfilecontents((0x1c010 + this.allmapdata[k].palleteaddr) - 0x8000, 9);
                int num19 = 0;
                for (int num20 = 0; num20 < 13; num20++)
                {
                    if ((num20 % 4) == 0)
                    {
                        this.allmapdata[k].pallete[num20] = 15;
                    }
                    else
                    {
                        this.allmapdata[k].pallete[num20] = buffer5[num19++];
                    }
                }
                this.allmapdata[k].pallete[13] = 0x30;
                this.allmapdata[k].pallete[14] = 0x10;
                this.allmapdata[k].pallete[15] = 0;
                for (int num21 = 0; num21 < 4; num21++)
                {
                    int key = this.allmapdata[k].PattenTable[num21];
                    if (!this._commdata.chrimagedata.ContainsKey(key))
                    {
                        byte[] buffer6 = getfilecontents(((key * 0x400) + (pbanknum * 0x4000)) + 0x10, 0x400);
                        this._commdata.Add_CHR_index(key, buffer6);
                    }
                }
                if (!this._commdata.chrimagedata.ContainsKey(this.allmapdata[k].PattenTable1))
                {
                    int num23 = this.allmapdata[k].PattenTable1;
                    byte[] buffer7 = getfilecontents(((num23 * 0x400) + (pbanknum * 0x4000)) + 0x10, 0x400);
                    this._commdata.Add_CHR_index(num23, buffer7);
                    num23++;
                    buffer7 = getfilecontents(((num23 * 0x400) + (pbanknum * 0x4000)) + 0x10, 0x400);
                    this._commdata.Add_CHR_index(num23, buffer7);
                }
            }
            byte[] data = getfilecontents((0x1000 + (pbanknum * 0x4000)) + 0x10, 0x400);
            this._commdata.Add_CHR_index(4, data);
            data = getfilecontents((0x1400 + (pbanknum * 0x4000)) + 0x10, 0x400);
            this._commdata.Add_CHR_index(5, data);
            for (int m = 0; m < this.allmapdata.Count; m++)
            {
                int num45;
                singleMap map3 = this.allmapdata[m];
                int mapWidth = map3.MapWidth;
                int mapHeight = map3.MapHeight;
                byte[] nametable = map3.nametable;
                for (int num27 = 0; num27 < mapWidth; num27++)
                {
                    for (int num28 = 0; num28 < mapHeight; num28++)
                    {
                        nametable[((num28 * mapWidth) * 4) + (num27 * 2)] = map3.maptitles_1[map3.maptitles[(num28 * mapWidth) + num27] * 4];
                        nametable[(((num28 * mapWidth) * 4) + (num27 * 2)) + 1] = map3.maptitles_1[(map3.maptitles[(num28 * mapWidth) + num27] * 4) + 1];
                        nametable[((num28 * mapWidth) * 4) + ((mapWidth + num27) * 2)] = map3.maptitles_1[(map3.maptitles[(num28 * mapWidth) + num27] * 4) + 2];
                        nametable[(((num28 * mapWidth) * 4) + ((mapWidth + num27) * 2)) + 1] = map3.maptitles_1[(map3.maptitles[(num28 * mapWidth) + num27] * 4) + 3];
                        map3.uper2[(num28 * mapWidth) + num27] = (byte)(map3.maptitles_1[0x200 + map3.maptitles[(num28 * mapWidth) + num27]] & 3);
                    }
                }
                byte[] originalImageData = new byte[(((map3.MapHeight * map3.MapWidth) * 4) * 8) * 8];
                byte[] array = new byte[0x4000];
                this._commdata.chrimagedata[map3.PattenTable[0]].CopyTo(array, 0);
                this._commdata.chrimagedata[map3.PattenTable[1]].CopyTo(array, 0x1000);
                this._commdata.chrimagedata[map3.PattenTable[2]].CopyTo(array, 0x2000);
                this._commdata.chrimagedata[map3.PattenTable[3]].CopyTo(array, 0x3000);
                for (int num29 = 0; num29 < (map3.MapWidth * 2); num29++)
                {
                    for (int num30 = 0; num30 < (map3.MapHeight * 2); num30++)
                    {
                        int num31 = nametable[num29 + ((num30 * map3.MapWidth) * 2)];
                        int num32 = map3.uper2[((num30 / 2) * map3.MapWidth) + (num29 / 2)] * 4;
                        for (int num33 = 0; num33 < 8; num33++)
                        {
                            for (int num34 = 0; num34 < 8; num34++)
                            {
                                originalImageData[(((((num30 * 0x40) * map3.MapWidth) * 2) + (((num33 * 8) * map3.MapWidth) * 2)) + (num29 * 8)) + num34] = (byte)(array[((((num31 / 0x10) * 0x400) + (0x80 * num33)) + ((num31 % 0x10) * 8)) + num34] + num32);
                            }
                        }
                    }
                }
                for (int num35 = 0; num35 < originalImageData.Length; num35++)
                {
                    originalImageData[num35] = map3.pallete[originalImageData[num35]];
                }
                Bitmap original = bmp.CreateBitmap(originalImageData, (map3.MapWidth * 8) * 2, (map3.MapHeight * 8) * 2);
                original.Palette = cp;
                Bitmap bitmap2 = new Bitmap(original);
                original.Dispose();
                original = null;
                map3.cityBmp = bitmap2.Clone(new Rectangle(0, 0, map3.MapWidth * 0x10, map3.MapHeight * 0x10), PixelFormat.Format24bppRgb);
                for (int num36 = 0; num36 < 0x80; num36++)
                {
                    byte[] buffer12 = new byte[0x100];
                    for (int num37 = 0; num37 < 2; num37++)
                    {
                        for (int num38 = 0; num38 < 2; num38++)
                        {
                            int num39 = map3.maptitles_1[((num36 * 4) + (num37 * 2)) + num38];
                            for (int num40 = 0; num40 < 8; num40++)
                            {
                                for (int num41 = 0; num41 < 8; num41++)
                                {
                                    buffer12[(((((num37 * 8) * 8) * 2) + ((num40 * 8) * 2)) + (num38 * 8)) + num41] = (byte)(array[((((num39 / 0x10) * 0x400) + (0x80 * num40)) + ((num39 % 0x10) * 8)) + num41] + ((map3.maptitles_1[0x200 + num36] & 3) * 4));
                                }
                            }
                        }
                    }
                    for (int num42 = 0; num42 < buffer12.Length; num42++)
                    {
                        buffer12[num42] = map3.pallete[buffer12[num42]];
                    }
                    Bitmap bitmap3 = bmp.CreateBitmap(buffer12, 0x10, 0x10);
                    bitmap3.Palette = cp;
                    map3.map_item_image[num36] = new Bitmap(bitmap3);
                }
                int num43 = getfilecontent((long)((((enterspnum * 0x2000) + 0x10) + map3.enterAddr) - 0x8000));
                int num44 = 0;
                switch (num43)
                {
                    case 0xff:
                        num44 = getfilecontent((long)((((enterspnum * 0x2000) + 0x11) + map3.enterAddr) - 0x8000));
                        map3.enterdata = getfilecontents((((enterspnum * 0x2000) + 0x10) + map3.enterAddr) - 0x8000, 2 + (5 * num44));
                        num45 = 2;
                        break;

                    case 0xfe:
                        num44 = getfilecontent((long)((((enterspnum * 0x2000) + 20) + map3.enterAddr) - 0x8000));
                        map3.enterdata = getfilecontents((((enterspnum * 0x2000) + 0x10) + map3.enterAddr) - 0x8000, 5 + (5 * num44));
                        num45 = 5;
                        break;

                    default:
                        num44 = getfilecontent((long)((((enterspnum * 0x2000) + 0x1c) + map3.enterAddr) - 0x8000));
                        map3.enterdata = getfilecontents((((enterspnum * 0x2000) + 0x10) + map3.enterAddr) - 0x8000, 13 + (5 * num44));
                        num45 = 13;
                        break;
                }
                map3.enters = new List<enterPoint>();
                for (int num46 = 0; num46 < num44; num46++)
                {
                    enterPoint point = new enterPoint
                    {
                        X = map3.enterdata[num45 + (num46 * 2)],
                        Y = map3.enterdata[(num45 + (num46 * 2)) + 1],
                        switchmap = map3.enterdata[(num45 + (2 * num44)) + (num46 * 3)],
                        map_x = map3.enterdata[((num45 + (2 * num44)) + (num46 * 3)) + 1],
                        map_y = map3.enterdata[((num45 + (2 * num44)) + (num46 * 3)) + 2]
                    };
                    map3.enters.Add(point);
                }
                if (map3.dynamicMapaddr != 0)
                {
                    int dynamicMapaddr = map3.dynamicMapaddr;
                    List<byte> list = new List<byte>();
                    for (byte num48 = getfilecontent((long)((0x1c010 + dynamicMapaddr) - 0x8000)); num48 != 0; num48 = getfilecontent((long)((0x1c010 + dynamicMapaddr) - 0x8000)))
                    {
                        list.Add(num48);
                        dynamicMapaddr++;
                    }
                    map3.dynamicdata = list.ToArray();
                }
            }
            int num49 = getfilecontent(0x28ac3) + (getfilecontent(0x28ac7) * 0x100);
            List<byte> list2 = new List<byte>();
            for (byte n = getfilecontent((long)((0x1c010 + num49) - 0x8000)); n != 0; n = getfilecontent((long)((0x1c010 + num49) - 0x8000)))
            {
                list2.Add(n);
                num49++;
            }
            this.bigmapdynmicdata = list2.ToArray();
            byte[] buffer13 = getfilecontents(0x24012, 0x1de);
            for (int num51 = 0; num51 < buffer13.Length; num51++)
            {
                singleMap map4 = this.allmapdata[num51 / 2];
                map4.npc_addr = buffer13[num51] + (buffer13[num51 + 1] * 0x100);
                if (map4.npc_addr != 0)
                {
                    int num52 = getfilecontent((long)((0x24010 + map4.npc_addr) - 0x8000));
                    for (int num53 = 0; num53 < num52; num53++)
                    {
                        npc_data _data = new npc_data(getfilecontents(((0x24011 + map4.npc_addr) - 0x8000) + (num53 * 6), 6), map4.PattenTable1, ref this._commdata);
                        map4.npc_datas.Add(_data);
                    }
                }
                num51++;
            }
            byte[] buffer15 = getfilecontents(0x39c50, 0x182);
            for (int num54 = 0; num54 < 0x5b; num54++)
            {
                if (buffer15[num54] > 0)
                {
                    hideItemdata itemdata = new hideItemdata
                    {
                        x = buffer15[0x5b + num54],
                        y = buffer15[0xb6 + num54],
                        itemdata = buffer15[0x111 + num54]
                    };
                    this.allmapdata[buffer15[num54] - 1].hideItem.Add(itemdata);
                }
                else
                {
                    hideItemdata itemdata2 = new hideItemdata
                    {
                        x = buffer15[0x5b + num54],
                        y = buffer15[0xb6 + num54],
                        itemdata = buffer15[0x111 + num54]
                    };
                    this.bigmaphideItem.Add(itemdata2);
                }
            }
            byte[] buffer16 = getfilecontents(0x39343, 0x70);
            this.mondatas = new mondata(buffer16);
            byte[] buffer17 = getfilecontents(0x39dd2, 0x1ec);
            for (int num55 = 0; num55 < 0x7b; num55++)
            {
                if (buffer17[num55] > 0)
                {
                    specPosition position = new specPosition
                    {
                        mapid = buffer17[num55],
                        content = buffer17[0x7b + num55],
                        X = buffer17[0xf6 + num55],
                        Y = buffer17[0x171 + num55]
                    };
                    this.allmapdata[position.mapid - 1].specP.Add(position);
                }
            }
            for (int num56 = 0; num56 < this.allmapdata.Count; num56++)
            {
                int num58 = num56 + 1;
                this.listBox1.Items.Add(string.Format("{0}:\t{1}", num58.ToString("X"), this.allmapdata[num56].map_addr.ToString("X")));
            }
            IList<int> list3 = new List<int>();
            IList<int> list4 = new List<int>();
            IList<int> list5 = new List<int>();
            for (int num57 = 0; num57 < this.allmapdata.Count; num57++)
            {
                if (!list3.Contains(this.allmapdata[num57].mapTitleAddr))
                {
                    list3.Add(this.allmapdata[num57].mapTitleAddr);
                }
                else
                {
                    list3.Add(0);
                    this.allmapdata[num57].mapstoreshare = list3.IndexOf(this.allmapdata[num57].mapTitleAddr);
                }
                if (!list4.Contains(this.allmapdata[num57].enterAddr))
                {
                    list4.Add(this.allmapdata[num57].enterAddr);
                }
                else
                {
                    list4.Add(0);
                    this.allmapdata[num57].mapentershare = list4.IndexOf(this.allmapdata[num57].enterAddr);
                }
                if (!list5.Contains(this.allmapdata[num57].npc_addr))
                {
                    list5.Add(this.allmapdata[num57].npc_addr);
                }
                else
                {
                    list5.Add(0);
                    this.allmapdata[num57].mapnpcshare = list5.IndexOf(this.allmapdata[num57].npc_addr);
                }
            }
            this.loadItemFromfile();
        }

        private ColorPalette GetColorPalette(uint nColors)
        {
            PixelFormat format = PixelFormat.Format1bppIndexed;
            if (nColors > 2)
            {
                format = PixelFormat.Format4bppIndexed;
            }
            if (nColors > 0x10)
            {
                format = PixelFormat.Format8bppIndexed;
            }
            Bitmap bitmap = new Bitmap(1, 1, format);
            ColorPalette palette = bitmap.Palette;
            bitmap.Dispose();
            return palette;
        }

        public static byte getfilecontent(long off)
        {
            ms.Seek(off, SeekOrigin.Begin);
            return (byte)ms.ReadByte();
        }

        public static byte[] getfilecontents(int off, int count)
        {
            byte[] buffer = new byte[count];
            ms.Seek((long)off, SeekOrigin.Begin);
            ms.Read(buffer, 0, count);
            return buffer;
        }

        private byte GetMapTileData()
        {
            if (this.map_data_index == 0)
            {
                this.maptitlestore_len++;
                byte[] buffer = getfilecontents(this.map_addr, 7);
                this.map_addr += 7;
                byte num = 0;
                for (int i = 0; i < 7; i++)
                {
                    this._MapTileData[i] = (byte)((buffer[i] >> (i + 1)) + num);
                    num = (byte)(((buffer[i] << ((7 - i) & 0x1f)) & 0xff) >> 1);
                }
                this._MapTileData[7] = num;
                this.map_data_index++;
                return this._MapTileData[0];
            }
            byte num3 = this._MapTileData[this.map_data_index];
            this.map_data_index++;
            if (this.map_data_index == 8)
            {
                this.map_data_index = 0;
                return num3;
            }
            this.maptitlestore_len++;
            return num3;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.menuStrip1 = new MenuStrip();
            this.文件ToolStripMenuItem = new ToolStripMenuItem();
            this.打开romToolStripMenuItem = new ToolStripMenuItem();
            this.保存ToolStripMenuItem = new ToolStripMenuItem();
            this.工具ToolStripMenuItem = new ToolStripMenuItem();
            this.导出npc数据ToolStripMenuItem = new ToolStripMenuItem();
            this.导出地图数据ToolStripMenuItem = new ToolStripMenuItem();
            this.计算空间占用ToolStripMenuItem = new ToolStripMenuItem();
            this.导出地图ToolStripMenuItem = new ToolStripMenuItem();
            this.listBox1 = new ListBox();
            this.panel1 = new Panel();
            this.hScrollBar1 = new HScrollBar();
            this.vScrollBar1 = new VScrollBar();
            this.groupBox1 = new GroupBox();
            this.groupBox6 = new GroupBox();
            this.label17 = new Label();
            this.label16 = new Label();
            this.label15 = new Label();
            this.label14 = new Label();
            this.label13 = new Label();
            this.label12 = new Label();
            this.textBox19 = new TextBox();
            this.textBox18 = new TextBox();
            this.textBox17 = new TextBox();
            this.textBox16 = new TextBox();
            this.textBox15 = new TextBox();
            this.textBox14 = new TextBox();
            this.textBox2 = new TextBox();
            this.textBox1 = new TextBox();
            this.textBox4 = new TextBox();
            this.groupBox5 = new GroupBox();
            this.button3 = new Button();
            this.button2 = new Button();
            this.button1 = new Button();
            this.label11 = new Label();
            this.groupBox4 = new GroupBox();
            this.label2 = new Label();
            this.groupBox2 = new GroupBox();
            this.panel2 = new Panel();
            this.label1 = new Label();
            this.imageList1 = new ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.menuStrip1.Items.AddRange(new ToolStripItem[] { this.文件ToolStripMenuItem, this.工具ToolStripMenuItem });
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(0x3ad, 0x18);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.打开romToolStripMenuItem, this.保存ToolStripMenuItem });
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new Size(0x29, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            this.打开romToolStripMenuItem.Name = "打开romToolStripMenuItem";
            this.打开romToolStripMenuItem.Size = new Size(0x70, 0x16);
            this.打开romToolStripMenuItem.Text = "打开rom";
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new Size(0x70, 0x16);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new EventHandler(this.保存ToolStripMenuItem_Click);
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.导出npc数据ToolStripMenuItem, this.导出地图数据ToolStripMenuItem, this.计算空间占用ToolStripMenuItem, this.导出地图ToolStripMenuItem });
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new Size(0x29, 20);
            this.工具ToolStripMenuItem.Text = "工具";
            this.导出npc数据ToolStripMenuItem.Name = "导出npc数据ToolStripMenuItem";
            this.导出npc数据ToolStripMenuItem.Size = new Size(0x8e, 0x16);
            this.导出npc数据ToolStripMenuItem.Text = "导出npc数据";
            this.导出npc数据ToolStripMenuItem.Click += new EventHandler(this.导出npc数据ToolStripMenuItem_Click);
            this.导出地图数据ToolStripMenuItem.Name = "导出地图数据ToolStripMenuItem";
            this.导出地图数据ToolStripMenuItem.Size = new Size(0x8e, 0x16);
            this.导出地图数据ToolStripMenuItem.Text = "导出地图数据";
            this.导出地图数据ToolStripMenuItem.Click += new EventHandler(this.导出地图数据ToolStripMenuItem_Click);
            this.计算空间占用ToolStripMenuItem.Name = "计算空间占用ToolStripMenuItem";
            this.计算空间占用ToolStripMenuItem.Size = new Size(0x8e, 0x16);
            this.计算空间占用ToolStripMenuItem.Text = "计算空间占用";
            this.计算空间占用ToolStripMenuItem.Click += new EventHandler(this.计算空间占用ToolStripMenuItem_Click);
            this.导出地图ToolStripMenuItem.Name = "导出地图ToolStripMenuItem";
            this.导出地图ToolStripMenuItem.Size = new Size(0x8e, 0x16);
            this.导出地图ToolStripMenuItem.Text = "导出地图";
            this.导出地图ToolStripMenuItem.Click += new EventHandler(this.导出地图ToolStripMenuItem_Click);
            this.listBox1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(6, 0x20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0x48, 520);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.panel1.Controls.Add(this.hScrollBar1);
            this.panel1.Controls.Add(this.vScrollBar1);
            this.panel1.Location = new Point(0x54, 0x77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x34a, 0x1b1);
            this.panel1.TabIndex = 7;
            this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseMove += new MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseDown += new MouseEventHandler(this.panel1_MouseDown);
            this.hScrollBar1.Dock = DockStyle.Bottom;
            this.hScrollBar1.Location = new Point(0, 0x1a0);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new Size(0x339, 0x11);
            this.hScrollBar1.TabIndex = 1;
            this.hScrollBar1.Scroll += new ScrollEventHandler(this.hScrollBar1_Scroll);
            this.vScrollBar1.Dock = DockStyle.Right;
            this.vScrollBar1.Location = new Point(0x339, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new Size(0x11, 0x1b1);
            this.vScrollBar1.TabIndex = 0;
            this.vScrollBar1.Scroll += new ScrollEventHandler(this.vScrollBar1_Scroll);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Location = new Point(0, 0x18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x3ad, 0x22e);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.textBox19);
            this.groupBox6.Controls.Add(this.textBox18);
            this.groupBox6.Controls.Add(this.textBox17);
            this.groupBox6.Controls.Add(this.textBox16);
            this.groupBox6.Controls.Add(this.textBox15);
            this.groupBox6.Controls.Add(this.textBox14);
            this.groupBox6.Location = new Point(0x37c, 14);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x2b, 20);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "NPC";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(6, 0x9e);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x2f, 12);
            this.label17.TabIndex = 0;
            this.label17.Text = "图像2：";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(6, 0x85);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x2f, 12);
            this.label16.TabIndex = 0;
            this.label16.Text = "图像1：";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(6, 0x6b);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x41, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "走动属性：";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(6, 0x4f);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x29, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "速度：";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(6, 0x34);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x29, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "朝向：";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(6, 0x1a);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x29, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "图像：";
            this.textBox19.Location = new Point(80, 0x9b);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Size(0x1f, 0x15);
            this.textBox19.TabIndex = 1;
            this.textBox18.Location = new Point(80, 0x83);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Size(0x1f, 0x15);
            this.textBox18.TabIndex = 1;
            this.textBox17.Location = new Point(80, 0x68);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Size(0x1f, 0x15);
            this.textBox17.TabIndex = 1;
            this.textBox16.Location = new Point(80, 0x4d);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Size(0x1f, 0x15);
            this.textBox16.TabIndex = 1;
            this.textBox15.Location = new Point(80, 0x2f);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Size(0x1f, 0x15);
            this.textBox15.TabIndex = 1;
            this.textBox14.Location = new Point(80, 20);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Size(0x1f, 0x15);
            this.textBox14.TabIndex = 1;
            this.textBox2.Location = new Point(0x394, 0x39);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(10, 10);
            this.textBox2.TabIndex = 12;
            this.textBox1.Location = new Point(0x395, 0x26);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(12, 13);
            this.textBox1.TabIndex = 12;
            this.textBox4.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.textBox4.Location = new Point(0x2ca, 20);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(0xac, 0x56);
            this.textBox4.TabIndex = 11;
            this.groupBox5.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Location = new Point(0x57, 0x47);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x1a2, 0x29);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "功能";
            this.button3.Location = new Point(0xe4, 12);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x59, 0x17);
            this.button3.TabIndex = 2;
            this.button3.Text = "共享图形数据";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.button2.Location = new Point(0x67, 12);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x67, 0x17);
            this.button2.TabIndex = 1;
            this.button2.Text = "全新设计此地图";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button1.Location = new Point(6, 12);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 1;
            this.button1.Text = "编辑此地图";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(15, 0x1b);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0, 12);
            this.label11.TabIndex = 0;
            this.groupBox4.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new Point(0x1d5, 14);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0xef, 0x39);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "地图";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(15, 0x1b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0, 12);
            this.label2.TabIndex = 0;
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Location = new Point(0x54, 0x11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x17b, 0x36);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "调色板";
            this.panel2.BorderStyle = BorderStyle.Fixed3D;
            this.panel2.Location = new Point(0x6a, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(200, 0x21);
            this.panel2.TabIndex = 0;
            this.panel2.Paint += new PaintEventHandler(this.panel2_Paint);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "地图编号：";
            this.imageList1.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new Size(0x10, 0x10);
            this.imageList1.TransparentColor = Color.Transparent;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3ad, 0x246);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.menuStrip1);
            base.MainMenuStrip = this.menuStrip1;
            base.Name = "Form1";
            this.Text = "Form1";
            base.Load += new EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.listBox1.Items.Count > 0) && (this.listBox1.SelectedIndex >= 0))
            {
                int mapTitleAddr;
                this.currentmapid = this.listBox1.SelectedIndex;
                singleMap map = this.allmapdata[this.listBox1.SelectedIndex];
                this.label2.Text = string.Format("编号:{0} 高:{1}宽:{2}", this.listBox1.SelectedIndex + 1, map.MapHeight, map.MapWidth);
                byte[] dynamicdata = map.dynamicdata;
                byte[] buffer1 = map.maptitles_1;
                byte[] pallete = map.pallete;
                string[] strArray = new string[4];
                int num = 0;
                if (dynamicdata != null)
                {
                    foreach (byte num2 in dynamicdata)
                    {
                        string[] strArray2;
                        (strArray2 = strArray)[0] = strArray2[0] + num2.ToString("X2") + "  ";
                        num++;
                        if (num == map.MapWidth)
                        {
                            string[] strArray3;
                            (strArray3 = strArray)[0] = strArray3[0] + "\r\n";
                            num = 0;
                        }
                    }
                    this.textBox4.Text = strArray[0];
                }
                else
                {
                    this.textBox4.Text = "";
                }
                if (map.mapTitleAddr >= 0xc000)
                {
                    mapTitleAddr = map.mapTitleAddr - 0xc000;
                    strArray[0] = string.Format("chr块：{0},位置:{1}~{2}", (0xec + (((mapTitleAddr >> 8) & 240) >> 2)).ToString("x2"), (mapTitleAddr & 0xfff).ToString("x4"), ((map.maptitlestore_len + (mapTitleAddr & 0xfff)) - 1).ToString("x4"));
                }
                else
                {
                    mapTitleAddr = map.mapTitleAddr;
                    strArray[0] = string.Format("prg块:{0},位置:{1}~{2}", (mapTitleAddr >> 13).ToString("x2"), (mapTitleAddr & 0x1fff).ToString("x4"), ((map.maptitlestore_len + (mapTitleAddr & 0xfff)) - 1).ToString("x4"));
                }
                mapTitleAddr = map.map_store_data[9];
                strArray[1] = string.Format("chr块:{0},偏移:{1}", (((mapTitleAddr & 240) >> 2) + 0xd4).ToString("x2"), ((mapTitleAddr & 15) * 0x100).ToString("x4"));
                mapTitleAddr = map.map_store_data[10];
                strArray[2] = string.Format("chr块:{0},偏移:{1}", (((mapTitleAddr & 240) >> 2) + 0xd4).ToString("x2"), ((mapTitleAddr & 15) * 0x100).ToString("x4"));
                strArray[3] = string.Format("动态图块地址:{0}", map.dynamicMapaddr.ToString("x4"));
                this.textBox1.Lines = strArray;
                this.vScrollBar1.Maximum = Math.Max(0, (((map.cityBmp.Height - this.panel1.Height) + 10) - 1) + this.hScrollBar1.Height);
                this.hScrollBar1.Maximum = Math.Max(0, (((map.cityBmp.Width - this.panel1.Width) + 10) - 1) + this.vScrollBar1.Width);
                this.panel1.Invalidate();
                this.panel2.Invalidate();
            }
        }

        private void loadItemFromfile()
        {
            if (File.Exists("item.txt"))
            {
                FileStream stream = new FileStream("item.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                while (reader.Peek() != -1)
                {
                    string[] strArray = reader.ReadLine().Split(new char[] { ',' });
                    int num = int.Parse(strArray[0]);
                    int num2 = int.Parse(strArray[1]);
                    int mapid = int.Parse(strArray[2]);
                    string name = strArray[3];
                    int index = 4;
                    int[,] data = new int[num, num2];
                    for (int i = 0; i < num2; i++)
                    {
                        int num6 = 0;
                        while (num6 < num)
                        {
                            data[num6, i] = int.Parse(strArray[index]);
                            num6++;
                            index++;
                        }
                    }
                    this.addMapItem(data, mapid, name);
                }
                reader.Close();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                singleMap map = this.allmapdata[this.listBox1.SelectedIndex];
                for (int i = 0; i < map.npc_datas.Count; i++)
                {
                    if ((this.curorP.X == map.npc_datas[i].X) && (this.curorP.Y == map.npc_datas[i].Y))
                    {
                        this.textBox14.Text = map.npc_datas[i].image.ToString("X2");
                        this.textBox15.Text = map.npc_datas[i].direct.ToString("X2");
                        this.textBox16.Text = map.npc_datas[i]._3CD.ToString("X2");
                        this.textBox17.Text = map.npc_datas[i]._3F7.ToString("X2");
                        this.textBox18.Text = map.npc_datas[i]._3DB.ToString("X2");
                        this.textBox19.Text = map.npc_datas[i]._3E9.ToString("X2");
                        return;
                    }
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (((((e.X + this.hScrollBar1.Value) - (this.curorP.X * 0x10)) < 0) || (((e.X + this.hScrollBar1.Value) - (this.curorP.X * 0x10)) >= 0x10)) || ((((e.Y + this.vScrollBar1.Value) - (this.curorP.Y * 0x10)) < 0) || (((e.Y + this.vScrollBar1.Value) - (this.curorP.Y * 0x10)) >= 0x10)))
            {
                this.curorP.X = (e.X + this.hScrollBar1.Value) / 0x10;
                this.curorP.Y = (e.Y + this.vScrollBar1.Value) / 0x10;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
            {
                Graphics graphics = this.panel1.CreateGraphics();
                Rectangle clientRectangle = this.panel1.ClientRectangle;
                clientRectangle.Offset(this.hScrollBar1.Value, this.vScrollBar1.Value);
                Bitmap cityBmp = this.allmapdata[this.listBox1.SelectedIndex].cityBmp;
                graphics.DrawImage(cityBmp, this.panel1.ClientRectangle, clientRectangle, GraphicsUnit.Pixel);
                singleMap map = this.allmapdata[this.listBox1.SelectedIndex];
                if (map.hideItem.Count >= 0)
                {
                    clientRectangle = new Rectangle(0, 0, 0x10, 0x10);
                    clientRectangle.Offset(-this.hScrollBar1.Value, -this.vScrollBar1.Value);
                    Rectangle rectangle2 = clientRectangle;
                    for (int i = 0; i < map.hideItem.Count; i++)
                    {
                        clientRectangle = rectangle2;
                        clientRectangle.Offset(map.hideItem[i].x * 0x10, map.hideItem[i].y * 0x10);
                        graphics.DrawImage(map.map_item_image[1], clientRectangle, new Rectangle(0, 0, 0x10, 0x10), GraphicsUnit.Pixel);
                    }
                }
                if (map.specP.Count >= 0)
                {
                    clientRectangle = new Rectangle(0, 0, 0x10, 0x10);
                    clientRectangle.Offset(-this.hScrollBar1.Value, -this.vScrollBar1.Value);
                    Rectangle rectangle3 = clientRectangle;
                    for (int j = 0; j < map.specP.Count; j++)
                    {
                        clientRectangle = rectangle3;
                        clientRectangle.Offset(map.specP[j].X * 0x10, map.specP[j].Y * 0x10);
                        graphics.DrawRectangle(new Pen(Color.Blue, 1f), clientRectangle);
                    }
                }
                if (map.enters.Count > 0)
                {
                    clientRectangle = new Rectangle(0, 0, 0x10, 0x10);
                    clientRectangle.Offset(-this.hScrollBar1.Value, -this.vScrollBar1.Value);
                    Rectangle rectangle4 = clientRectangle;
                    for (int k = 0; k < map.enters.Count; k++)
                    {
                        clientRectangle = rectangle4;
                        clientRectangle.Offset(map.enters[k].X * 0x10, map.enters[k].Y * 0x10);
                        graphics.DrawRectangle(new Pen(Color.Red, 1f), clientRectangle);
                    }
                }
                if (map.npc_datas.Count > 0)
                {
                    clientRectangle = new Rectangle(0, 0, 0x10, 0x10);
                    clientRectangle.Offset(-this.hScrollBar1.Value, -this.vScrollBar1.Value);
                    Rectangle rectangle5 = clientRectangle;
                    for (int m = 0; m < map.npc_datas.Count; m++)
                    {
                        npc_data _data = map.npc_datas[m];
                        clientRectangle = rectangle5;
                        clientRectangle.Offset(_data.X * 0x10, _data.Y * 0x10);
                        graphics.DrawImage(_data.npc_image, clientRectangle, new Rectangle(0, 0, 0x10, 0x10), GraphicsUnit.Pixel);
                    }
                }
                clientRectangle = new Rectangle(-this.hScrollBar1.Value, -this.vScrollBar1.Value, (map.rightline - map.leftline) * 0x10, (map.bottomline - map.topline) * 0x10);
                clientRectangle.Offset(map.leftline * 0x10, map.topline * 0x10);
                graphics.DrawRectangle(new Pen(Color.Black, 1f), clientRectangle);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if ((this.listBox1.Items.Count > 0) && (this.listBox1.SelectedIndex >= 0))
            {
                singleMap map = this.allmapdata[this.listBox1.SelectedIndex];
                Graphics graphics = this.panel2.CreateGraphics();
                for (int i = 0; i < map.pallete.Length; i++)
                {
                    Rectangle rect = new Rectangle(1 + (0x10 * i), 1, 15, 15);
                    graphics.FillRectangle(new SolidBrush(commondata.nes_Palette[map.pallete[i]]), rect);
                }
            }
        }

        private void savemap(int room, int roomenter)
        {
            IList<int> list = new List<int>();
            for (int i = 0; i < this.allmapdata.Count; i++)
            {
                if (this.allmapdata[i].mapstoreshare == 0)
                {
                    list.Add(i);
                }
            }
            int[] array = new int[list.Count];
            list.CopyTo(array, 0);
            int[] good = new int[list.Count];
            list.Clear();
            for (int j = 0; j < array.Length; j++)
            {
                good[j] = this.allmapdata[array[j]].maptitlestore_len;
            }
            int[] numArray3 = this.背包算法(ref good);
            int num3 = 0;
            int num4 = 0;
            for (int k = 0; k < numArray3.Length; k++)
            {
                if (numArray3[k] == 1)
                {
                    num3 += good[k];
                }
                else
                {
                    num4 += good[k];
                }
            }
            if (num3 > 0xb0c3)
            {
                int num46 = num3 - 0xb0c2;
                MessageBox.Show(string.Format("rpg空间超过{0}", num46.ToString("X2")));
            }
            else if (num4 > 0x4000)
            {
                int num47 = num4 - 0x4000;
                MessageBox.Show(string.Format("chr空间超过{0}", num47.ToString("X2")));
            }
            else
            {
                num3 = 0;
                for (int m = 0; m < this.allmapdata.Count; m++)
                {
                    num3 += this.allmapdata[m].map_store_data.Length;
                }
                if (num3 > 0x16ae)
                {
                    int num48 = num3 - 0x16ae;
                    MessageBox.Show(string.Format("二级指针空间超过{0}", num48.ToString("X2")));
                }
                else
                {
                    num3 = 0;
                    for (int n = 0; n < this.allmapdata.Count; n++)
                    {
                        if ((this.allmapdata[n].npc_addr != 0) && (this.allmapdata[n].mapnpcshare == 0))
                        {
                            num3 += (6 * this.allmapdata[n].npc_datas.Count) + 1;
                        }
                    }
                    if (num3 > 0xe74)
                    {
                        int num49 = num3 - 0xe74;
                        MessageBox.Show(string.Format("npc数据空间超过{0}", num49.ToString("X2")));
                    }
                    else
                    {
                        int num8 = 0x600;
                        int num9 = 0xc000;
                        for (int num10 = 0; num10 < numArray3.Length; num10++)
                        {
                            if (numArray3[num10] == 1)
                            {
                                this.allmapdata[array[num10]].mapTitleAddr = num8;
                                num8 += good[num10];
                            }
                            else
                            {
                                this.allmapdata[array[num10]].mapTitleAddr = num9;
                                num9 += good[num10];
                            }
                        }
                        if (enterspnum == 0x3f)
                        {
                            num8 = 0x87a0;
                        }
                        else
                        {
                            num8 = 0x8b0d;
                        }
                        for (int num11 = 0; num11 < this.allmapdata.Count; num11++)
                        {
                            if (this.allmapdata[num11].mapentershare == 0)
                            {
                                this.allmapdata[num11].enterAddr = num8;
                                num8 += this.allmapdata[num11].enterdata.Length;
                            }
                            else
                            {
                                this.allmapdata[num11].enterAddr = this.allmapdata[this.allmapdata[num11].mapentershare].enterAddr;
                            }
                        }
                        FileStream output = new FileStream(romf, FileMode.Open, FileAccess.Write, FileShare.Read);
                        BinaryWriter writer = new BinaryWriter(output);
                        writer.Seek(0xbe12, SeekOrigin.Begin);
                        num8 = 0x8500;
                        for (int num12 = 0; num12 < 0x3f; num12++)
                        {
                            writer.Write((byte)(num8 & 0xff));
                            writer.Write((byte)(num8 / 0x100));
                            num8 += this.allmapdata[num12].map_store_data.Length;
                        }
                        writer.Flush();
                        writer.Seek(0x1deb0, SeekOrigin.Begin);
                        for (int num13 = 0x3f; num13 < 0xef; num13++)
                        {
                            writer.Write((byte)(num8 & 0xff));
                            writer.Write((byte)(num8 / 0x100));
                            num8 += this.allmapdata[num13].map_store_data.Length;
                        }
                        writer.Flush();
                        num8 = 0x9cbf;
                        writer.Seek(0x1dccf, SeekOrigin.Begin);
                        for (int num14 = 0; num14 < this.allmapdata.Count; num14++)
                        {
                            if (this.allmapdata[num14].dynamicdata != null)
                            {
                                this.allmapdata[num14].dynamicMapaddr = num8;
                                num8 += this.allmapdata[num14].dynamicdata.Length + 1;
                                writer.Write(this.allmapdata[num14].dynamicdata);
                                writer.Write(false);
                            }
                            else
                            {
                                this.allmapdata[num14].dynamicMapaddr = 0;
                            }
                        }
                        writer.Write(this.bigmapdynmicdata);
                        writer.Write(false);
                        writer.Flush();
                        getfilecontent(0x28ac3);
                        getfilecontent(0x28ac7);
                        writer.Seek(0x28ac3, SeekOrigin.Begin);
                        writer.Write((byte)(num8 & 0xff));
                        writer.Seek(0x28ac7, SeekOrigin.Begin);
                        writer.Write((byte)((num8 & 0xff00) / 0x100));
                        writer.Flush();
                        for (int num15 = 0; num15 < this.allmapdata.Count; num15++)
                        {
                            if (this.allmapdata[num15].mapstoreshare == 0)
                            {
                                if (this.allmapdata[num15].mapTitleAddr >= 0xc000)
                                {
                                    writer.Seek(((0xbb010 + ((pbanknum - 0x20) * 0x4000)) + this.allmapdata[num15].mapTitleAddr) - 0xc000, SeekOrigin.Begin);
                                    writer.Write(this.allmapdata[num15].maptitlestore);
                                }
                                else
                                {
                                    writer.Seek(0x10 + this.allmapdata[num15].mapTitleAddr, SeekOrigin.Begin);
                                    writer.Write(this.allmapdata[num15].maptitlestore);
                                }
                                writer.Flush();
                            }
                            else
                            {
                                this.allmapdata[num15].mapTitleAddr = this.allmapdata[this.allmapdata[num15].mapstoreshare].mapTitleAddr;
                            }
                            if (this.allmapdata[num15].mapentershare == 0)
                            {
                                writer.Seek((0x7e010 + this.allmapdata[num15].enterAddr) - 0x8000, SeekOrigin.Begin);
                                writer.Write(this.allmapdata[num15].enterdata);
                            }
                            if ((this.allmapdata[num15].mapnpcshare == 0) && (this.allmapdata[num15].npc_addr != 0))
                            {
                                writer.Seek((0x24010 + this.allmapdata[num15].npc_addr) - 0x8000, SeekOrigin.Begin);
                                writer.Write((byte)this.allmapdata[num15].npc_datas.Count);
                                for (int num16 = 0; num16 < this.allmapdata[num15].npc_datas.Count; num16++)
                                {
                                    writer.Write(this.allmapdata[num15].npc_datas[num16].npc_store_data);
                                }
                                writer.Flush();
                            }
                        }
                        writer.Seek(0xe510, SeekOrigin.Begin);
                        for (int num17 = 0; num17 < this.allmapdata.Count; num17++)
                        {
                            writer.Write(this.allmapdata[num17].map_store_data);
                        }
                        writer.Flush();
                        int num18 = 0;
                        for (int num19 = 0; num19 < this.allmapdata.Count; num19++)
                        {
                            if (this.allmapdata[num19].specP.Count != 0)
                            {
                                for (int num20 = 0; num20 < this.allmapdata[num19].specP.Count; num20++)
                                {
                                    num18++;
                                }
                            }
                        }
                        if (num18 > 0x7b)
                        {
                            MessageBox.Show(string.Format("电脑售货机等数据超过了数据限制（请至少去掉{0}个）", num18 - 0x7b));
                        }
                        else
                        {
                            writer.Seek(0x39dd2, SeekOrigin.Begin);
                            for (int num21 = 0; num21 < this.allmapdata.Count; num21++)
                            {
                                if (this.allmapdata[num21].specP.Count != 0)
                                {
                                    for (int num22 = 0; num22 < this.allmapdata[num21].specP.Count; num22++)
                                    {
                                        writer.Write((byte)(num21 + 1));
                                    }
                                }
                            }
                            writer.Flush();
                            writer.Seek(0x39e4d, SeekOrigin.Begin);
                            for (int num23 = 0; num23 < this.allmapdata.Count; num23++)
                            {
                                if (this.allmapdata[num23].specP.Count != 0)
                                {
                                    for (int num24 = 0; num24 < this.allmapdata[num23].specP.Count; num24++)
                                    {
                                        writer.Write(this.allmapdata[num23].specP[num24].content);
                                    }
                                }
                            }
                            writer.Flush();
                            writer.Seek(0x39ec8, SeekOrigin.Begin);
                            for (int num25 = 0; num25 < this.allmapdata.Count; num25++)
                            {
                                if (this.allmapdata[num25].specP.Count != 0)
                                {
                                    for (int num26 = 0; num26 < this.allmapdata[num25].specP.Count; num26++)
                                    {
                                        writer.Write((byte)this.allmapdata[num25].specP[num26].X);
                                    }
                                }
                            }
                            writer.Flush();
                            writer.Seek(0x39f43, SeekOrigin.Begin);
                            for (int num27 = 0; num27 < this.allmapdata.Count; num27++)
                            {
                                if (this.allmapdata[num27].specP.Count != 0)
                                {
                                    for (int num28 = 0; num28 < this.allmapdata[num27].specP.Count; num28++)
                                    {
                                        writer.Write((byte)this.allmapdata[num27].specP[num28].Y);
                                    }
                                }
                            }
                        }
                        int num29 = 0;
                        for (int num30 = 0; num30 < this.allmapdata.Count; num30++)
                        {
                            num29 += this.allmapdata[num30].hideItem.Count;
                        }
                        if (num29 != 0x52)
                        {
                            MessageBox.Show("请调整小地图箱子总数为0x52个，当前 " + num29.ToString("X2") + " 个");
                        }
                        else
                        {
                            writer.Seek(0x39c50, SeekOrigin.Begin);
                            for (int num31 = 0; num31 < this.allmapdata.Count; num31++)
                            {
                                if (this.allmapdata[num31].hideItem.Count != 0)
                                {
                                    for (int num32 = 0; num32 < this.allmapdata[num31].hideItem.Count; num32++)
                                    {
                                        writer.Write((byte)(num31 + 1));
                                    }
                                }
                            }
                            for (int num33 = 0; num33 < this.bigmaphideItem.Count; num33++)
                            {
                                writer.Write((byte)0);
                            }
                            for (int num34 = 0; num34 < this.allmapdata.Count; num34++)
                            {
                                if (this.allmapdata[num34].hideItem.Count != 0)
                                {
                                    for (int num35 = 0; num35 < this.allmapdata[num34].hideItem.Count; num35++)
                                    {
                                        writer.Write((byte)this.allmapdata[num34].hideItem[num35].x);
                                    }
                                }
                            }
                            for (int num36 = 0; num36 < this.bigmaphideItem.Count; num36++)
                            {
                                writer.Write((byte)this.bigmaphideItem[num36].x);
                            }
                            for (int num37 = 0; num37 < this.allmapdata.Count; num37++)
                            {
                                if (this.allmapdata[num37].hideItem.Count != 0)
                                {
                                    for (int num38 = 0; num38 < this.allmapdata[num37].hideItem.Count; num38++)
                                    {
                                        writer.Write((byte)this.allmapdata[num37].hideItem[num38].y);
                                    }
                                }
                            }
                            for (int num39 = 0; num39 < this.bigmaphideItem.Count; num39++)
                            {
                                writer.Write((byte)this.bigmaphideItem[num39].y);
                            }
                            for (int num40 = 0; num40 < this.allmapdata.Count; num40++)
                            {
                                if (this.allmapdata[num40].hideItem.Count != 0)
                                {
                                    for (int num41 = 0; num41 < this.allmapdata[num40].hideItem.Count; num41++)
                                    {
                                        writer.Write((byte)this.allmapdata[num40].hideItem[num41].itemdata);
                                    }
                                }
                            }
                            for (int num42 = 0; num42 < this.bigmaphideItem.Count; num42++)
                            {
                                writer.Write((byte)this.bigmaphideItem[num42].itemdata);
                            }
                        }
                        writer.Flush();
                        writer.Seek(0x24012, SeekOrigin.Begin);
                        num8 = 0x8213;
                        for (int num43 = 0; num43 < this.allmapdata.Count; num43++)
                        {
                            if (this.allmapdata[num43].npc_datas.Count == 0)
                            {
                                writer.Write((byte)0);
                                writer.Write((byte)0);
                            }
                            else if (this.allmapdata[num43].mapnpcshare == 0)
                            {
                                writer.Write((byte)(num8 & 0xff));
                                writer.Write((byte)(num8 >> 8));
                                this.allmapdata[num43].npc_addr = num8;
                                num8 += (6 * this.allmapdata[num43].npc_datas.Count) + 1;
                            }
                            else
                            {
                                writer.Write((byte)(this.allmapdata[this.allmapdata[num43].mapnpcshare].npc_addr & 0xff));
                                writer.Write((byte)(this.allmapdata[this.allmapdata[num43].mapnpcshare].npc_addr >> 8));
                            }
                        }
                        writer.Flush();
                        writer.Seek(0x24223, SeekOrigin.Begin);
                        for (int num44 = 0; num44 < this.allmapdata.Count; num44++)
                        {
                            if ((this.allmapdata[num44].npc_datas.Count != 0) && (this.allmapdata[num44].mapnpcshare == 0))
                            {
                                writer.Write((byte)this.allmapdata[num44].npc_datas.Count);
                                for (int num45 = 0; num45 < this.allmapdata[num44].npc_datas.Count; num45++)
                                {
                                    writer.Write(this.allmapdata[num44].npc_datas[num45].npc_store_data);
                                }
                            }
                        }
                        writer.Flush();
                        writer.Seek(0x39343, SeekOrigin.Begin);
                        writer.Write(this.mondatas.data_store);
                        float num50 = (100f * room) / 61635f;
                        MessageBox.Show(string.Format("空间使用率{0}%", num50.ToString("F2")));
                        writer.Flush();
                        writer.Close();
                    }
                }
            }
        }

        public static Bitmap VitrificationImage(Image img, float alpha)
        {
            Bitmap image = new Bitmap(img.Width, img.Height);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(new ColorMatrix(CreateAlphaMatrix(alpha)));
                    graphics.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 1, 1, img.Width, img.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return image;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num4;
            int room = 0;
            int roomenter = 0;
            for (int i = 0; i < this.allmapdata.Count; i++)
            {
                singleMap map = this.allmapdata[i];
                if (map.mapstoreshare == 0)
                {
                    room += map.maptitlestore_len;
                }
                if (map.mapentershare == 0)
                {
                    roomenter += map.enterdata.Length;
                }
            }
            if (enterspnum == 0x3f)
            {
                num4 = 0x1860;
            }
            else
            {
                num4 = 0xe7d;
            }
            if ((room <= 0xf0c3) && (roomenter <= num4))
            {
                this.savemap(room, roomenter);
            }
            else
            {
                MessageBox.Show("空间不够！");
            }
        }

        public int[] 背包算法(ref int[] good)
        {
            int num3;
            int length = good.Length;
            long num2 = 0xb0c3;
            int[,] numArray = new int[length + 1, num2 + 1];
            int[] numArray2 = new int[length];
            for (num3 = 0; num3 <= length; num3++)
            {
                numArray[num3, 0] = 0;
            }
            for (num3 = 0; num3 <= num2; num3++)
            {
                numArray[0, num3] = 0;
            }
            for (num3 = 1; num3 <= length; num3++)
            {
                for (int i = 1; i <= num2; i++)
                {
                    numArray[num3, i] = numArray[num3 - 1, i];
                    if ((good[num3 - 1] <= i) && ((numArray[num3 - 1, i - good[num3 - 1]] + good[num3 - 1]) > numArray[num3, i]))
                    {
                        numArray[num3, i] = numArray[num3 - 1, i - good[num3 - 1]] + good[num3 - 1];
                    }
                }
            }
            long num5 = num2;
            for (num3 = length; num3 > 0; num3--)
            {
                if (numArray[(int)((IntPtr)num3), (int)((IntPtr)num5)] > numArray[(int)((IntPtr)(num3 - 1)), (int)((IntPtr)num5)])
                {
                    numArray2[num3 - 1] = 1;
                    num5 -= good[num3 - 1];
                }
            }
            return numArray2;
        }

        private void 导出npc数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = 0;
            for (int i = 0; i < this.allmapdata.Count; i++)
            {
                num += this.allmapdata[i].map_store_data.Length;
            }
            MessageBox.Show(num.ToString("X4"));
        }

        private void 导出地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.allmapdata.Count; i++)
            {
                new Bitmap(this.allmapdata[i].cityBmp).Save(Path.Combine(Application.StartupPath, i.ToString() + ".bmp"), ImageFormat.Bmp);
            }
            MessageBox.Show("保存成功！");
        }

        private void 导出地图数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
            {
                List<string> list = new List<string>();
                singleMap map = this.allmapdata[this.listBox1.SelectedIndex];
                list.Add(string.Format("编号:{0}", this.listBox1.SelectedIndex + 1));
                list.Add(string.Format("地图数据第一级指针:{0}", map.map_addr.ToString("X")));
                list.Add("*************控制数据****************");
                list.Add(string.Format("Z49:{0}", map.map_store_data[0].ToString("X2")));
                list.Add(string.Format("地图宽(Z4a):{0}", map.map_store_data[1].ToString("X2")));
                list.Add(string.Format("地图长(Z4b):{0}", map.map_store_data[2].ToString("X2")));
                list.Add(string.Format("地图左边界(Z4c):{0}", map.map_store_data[3].ToString("X2")));
                list.Add(string.Format("地图上边界(Z4d):{0}", map.map_store_data[4].ToString("X2")));
                list.Add(string.Format("地图右边界(Z4e):{0}", map.map_store_data[5].ToString("X2")));
                list.Add(string.Format("地图下边界(Z4f):{0}", map.map_store_data[6].ToString("X2")));
                list.Add(string.Format("主地图数据起始地址:{0}", map.mapTitleAddr.ToString("X4")));
                list.Add(string.Format("地图小块数据1:{0}", map.map_store_data[9].ToString("X2")));
                list.Add(string.Format("地图小块数据2:{0}", map.map_store_data[10].ToString("X2")));
                list.Add(string.Format("入口数据地址:{0}", map.enterAddr.ToString("X4")));
                list.Add(string.Format("调色板数据地址:{0}", map.palleteaddr.ToString("X4")));
                list.Add(string.Format("精灵模式表数据:{0}", map.PattenTable1.ToString("X2")));
                list.Add(string.Format("地图模式表数据:{0},{1},{2},{3}", new object[] { map.PattenTable[0].ToString("X2"), map.PattenTable[1].ToString("X2"), map.PattenTable[2].ToString("X2"), map.PattenTable[3].ToString("X2") }));
                list.Add(string.Format("未知数据(a2,a3):{0}", ((map.map_store_data[0] & 1) == 1) ? (((map.map_store_data[0x19] * 0x100) + map.map_store_data[0x19])).ToString("X4") : "无"));
                list.Add(string.Format("随剧情动态图块数据地址:{0}", ((map.map_store_data[0] & 4) != 0) ? map.dynamicMapaddr.ToString("X4") : "无"));
                commondata.writetofile(string.Format("地图数据{0}.txt", this.listBox1.SelectedIndex + 1), list.ToArray());
                MessageBox.Show("完成！");
            }
        }

        private void 导出入口地址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 计算空间占用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.form = new Form2();
            this.form.ShowDialog(this);
            this.form.Dispose();
        }
    }




}
