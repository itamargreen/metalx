using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;


namespace MetalMax.Ripper
{
    public class editform : Form
    {
        // Fields
        private Button button1;
        private Button button10;
        private Button button11;
        private Button button12;
        private Button button13;
        private Button button14;
        private Button button15;
        private Button button16;
        private Button button17;
        private Button button18;
        private Button button19;
        private Button button2;
        private Button button20;
        private Button button21;
        private Button button22;
        private Button button23;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private ComboBox cb_npc_2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private ComboBox comboBox4;
        private ComboBox comboBox5;
        private IContainer components;
        private Point curorP = new Point(0, 0);
        public Rectangle currentDrawRect;
        private bool displayAreaLine;
        public int editmapid;
        private Form1 f;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private GroupBox groupBox8;
        private HScrollBar hScrollBar1;
        private ImageList imageList1;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label2;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label label27;
        private Label label28;
        private Label label29;
        private Label label3;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label33;
        private Label label34;
        private Label label35;
        private Label label36;
        private Label label37;
        private Label label38;
        private Label label39;
        private Label label4;
        private Label label40;
        private Label label41;
        private Label label42;
        private Label label43;
        private Label label44;
        private Label label45;
        private Label label46;
        private Label label47;
        private Label label48;
        private Label label49;
        private Label label5;
        private Label label50;
        private Label label51;
        private Label label52;
        private Label label53;
        private Label label54;
        private Label label55;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private ListView listView1;
        private ListView listView2;
        public singleMap map;
        private NumericUpDown nu_map1;
        private NumericUpDown nu_map2;
        private NumericUpDown nu_map3;
        private NumericUpDown nu_map4;
        private NumericUpDown nu_npc_1;
        private NumericUpDown nu_npc_3;
        private NumericUpDown nu_npc_4;
        private NumericUpDown nu_npc_5;
        private NumericUpDown nu_npc_6;
        private NumericUpDown nu_npc_x;
        private NumericUpDown nu_npc_y;
        private NumericUpDown nu_point_x;
        private NumericUpDown nu_x1;
        private NumericUpDown nu_x2;
        private NumericUpDown nu_x3;
        private NumericUpDown nu_x4;
        private NumericUpDown nu_y1;
        private NumericUpDown nu_y2;
        private NumericUpDown nu_y3;
        private NumericUpDown nu_y4;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown10;
        private NumericUpDown numericUpDown11;
        private NumericUpDown numericUpDown12;
        private NumericUpDown numericUpDown13;
        private NumericUpDown numericUpDown14;
        private NumericUpDown numericUpDown15;
        private NumericUpDown numericUpDown16;
        private NumericUpDown numericUpDown17;
        private NumericUpDown numericUpDown18;
        private NumericUpDown numericUpDown19;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown20;
        private NumericUpDown numericUpDown21;
        private NumericUpDown numericUpDown22;
        private NumericUpDown numericUpDown23;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown4;
        private NumericUpDown numericUpDown5;
        private NumericUpDown numericUpDown6;
        private NumericUpDown numericUpDown7;
        private NumericUpDown numericUpDown8;
        private NumericUpDown numericUpDown9;
        private Panel panel1;
        private Panel panel2;
        private Rectangle panelPaintRect = new Rectangle();
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private Rectangle SelectedRect = new Rectangle();
        private bool showdynmic;
        private bool showhideItem;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TextBox textBox1;
        private TextBox textBox2;
        private NumericUpDown un_point_map;
        private NumericUpDown un_point_x1;
        private NumericUpDown un_point_y;
        private NumericUpDown un_point_y1;
        private VScrollBar vScrollBar1;

        // Methods
        public editform()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.SelectedRect != Rectangle.Empty)
            {
                int[,] data = new int[this.SelectedRect.Width, this.SelectedRect.Height];
                for (int i = 0; i < this.SelectedRect.Height; i++)
                {
                    for (int j = 0; j < this.SelectedRect.Width; j++)
                    {
                        data[j, i] = this.map.maptitles[(((this.SelectedRect.Y + i) * this.map.MapWidth) + this.SelectedRect.X) + j];
                    }
                }
                this.f.addMapItem(data, this.editmapid, "");
                this.f.froms.refresh();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (this.comboBox5.SelectedIndex >= 0)
            {
                specPosition position = this.map.specP[this.comboBox5.SelectedIndex];
                position.X = (int)this.numericUpDown11.Value;
                position.Y = (int)this.numericUpDown10.Value;
                position.content = (byte)((this.numericUpDown16.Value * 16M) + this.numericUpDown15.Value);
                MessageBox.Show("保存完成!");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (this.comboBox5.SelectedIndex >= 0)
            {
                this.map.specP.RemoveAt(this.comboBox5.SelectedIndex);
                int selectedIndex = this.comboBox5.SelectedIndex;
                this.comboBox5.Items.RemoveAt(this.comboBox5.Items.Count - 1);
                if (this.comboBox5.Items.Count > 0)
                {
                    this.comboBox5.SelectedIndex = Math.Min(this.comboBox5.Items.Count - 1, selectedIndex);
                }
                MessageBox.Show("删除完成!");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            specPosition item = new specPosition
            {
                X = (int)this.numericUpDown11.Value,
                Y = (int)this.numericUpDown10.Value,
                content = (byte)((this.numericUpDown16.Value * 16M) + this.numericUpDown15.Value)
            };
            this.comboBox5.Items.Add(this.comboBox5.Items.Count.ToString("d2"));
            this.map.specP.Add(item);
            MessageBox.Show("添加完成!");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int num = (int)this.numericUpDown14.Value;
            int num2 = (int)this.numericUpDown12.Value;
            int num3 = (int)this.numericUpDown13.Value;
            int num4 = (int)this.numericUpDown17.Value;
            ListViewItem item = new ListViewItem();
            item.SubItems[0].Text = num.ToString("X2");
            item.SubItems.Add(num2.ToString("X2"));
            item.SubItems.Add(num3.ToString("X2"));
            item.SubItems.Add(num4.ToString("X2"));
            this.listView2.Items.Add(item);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count > 0)
            {
                this.listView2.Items.RemoveAt(this.listView2.SelectedItems[0].Index);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (this.listView2.Items.Count > 0)
            {
                List<byte> list = new List<byte>();
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                for (int i = 0; i < this.listView2.Items.Count; i++)
                {
                    num2 = int.Parse(this.listView2.Items[i].SubItems[0].Text, NumberStyles.HexNumber);
                    if (num2 != num)
                    {
                        list.Add((byte)num2);
                        list.Add((byte)num3);
                        if (i != 0)
                        {
                            list[(list.Count - 1) - (3 * num3)] = (byte)num3;
                        }
                        num3 = 0;
                        num = num2;
                    }
                    int num4 = int.Parse(this.listView2.Items[i].SubItems[1].Text, NumberStyles.HexNumber);
                    list.Add((byte)num4);
                    num4 = int.Parse(this.listView2.Items[i].SubItems[2].Text, NumberStyles.HexNumber);
                    list.Add((byte)num4);
                    num4 = int.Parse(this.listView2.Items[i].SubItems[3].Text, NumberStyles.HexNumber);
                    list.Add((byte)num4);
                    num3++;
                }
                list[(list.Count - 1) - (3 * num3)] = (byte)num3;
                this.map.dynamicdata = list.ToArray();
            }
            else
            {
                this.map.dynamicdata = null;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count > 0)
            {
                int index = this.listView2.SelectedItems[0].Index;
                this.listView2.Items[index].SubItems[0].Text = ((int)this.numericUpDown14.Value).ToString("X2");
                this.listView2.Items[index].SubItems[1].Text = ((int)this.numericUpDown12.Value).ToString("X2");
                this.listView2.Items[index].SubItems[2].Text = ((int)this.numericUpDown13.Value).ToString("X2");
                this.listView2.Items[index].SubItems[3].Text = ((int)this.numericUpDown17.Value).ToString("X2");
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (this.comboBox4.SelectedIndex >= 0)
            {
                this.map.hideItem.RemoveAt(this.comboBox4.SelectedIndex);
                int selectedIndex = this.comboBox4.SelectedIndex;
                this.comboBox4.Items.RemoveAt(this.comboBox4.Items.Count - 1);
                if (this.comboBox4.Items.Count > 0)
                {
                    this.comboBox4.SelectedIndex = Math.Min(this.comboBox4.Items.Count - 1, selectedIndex);
                }
                MessageBox.Show("删除完成!");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            hideItemdata item = new hideItemdata
            {
                x = (int)this.numericUpDown21.Value,
                y = (int)this.numericUpDown20.Value,
                itemdata = (int)this.numericUpDown19.Value
            };
            this.comboBox4.Items.Add(this.comboBox4.Items.Count.ToString("d2"));
            this.map.hideItem.Add(item);
            MessageBox.Show("添加完成!");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (this.comboBox4.SelectedIndex >= 0)
            {
                hideItemdata itemdata = this.map.hideItem[this.comboBox4.SelectedIndex];
                itemdata.x = (int)this.numericUpDown21.Value;
                itemdata.y = (int)this.numericUpDown20.Value;
                itemdata.itemdata = (int)this.numericUpDown19.Value;
                MessageBox.Show("保存完成!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.map.maptitles.Length != 0)
            {
                byte[] buffer;
                IList<int> list = new List<int>();
                int item = this.map.maptitles[0];
                int num3 = 1;
                for (int i = 1; i < this.map.maptitles.Length; i++)
                {
                    int num2 = this.map.maptitles[i];
                    if (num2 == item)
                    {
                        num3++;
                    }
                    else
                    {
                        if (num3 > 0x7f)
                        {
                            while (num3 > 0x7f)
                            {
                                list.Add(0);
                                list.Add(item);
                                list.Add(0x7f);
                                num3 -= 0x7f;
                            }
                        }
                        if (num3 > 2)
                        {
                            list.Add(0);
                            list.Add(item);
                            list.Add(num3);
                        }
                        else
                        {
                            for (int m = 0; m < num3; m++)
                            {
                                list.Add(item);
                            }
                        }
                        num3 = 1;
                        item = num2;
                    }
                }
                if (num3 > 0x7f)
                {
                    while (num3 > 0x7f)
                    {
                        list.Add(0);
                        list.Add(item);
                        list.Add(0x7f);
                        num3 -= 0x7f;
                    }
                }
                if (num3 > 2)
                {
                    list.Add(0);
                    list.Add(item);
                    list.Add(num3);
                }
                else
                {
                    for (int n = 0; n < num3; n++)
                    {
                        list.Add(item);
                    }
                }
                Queue<bool> queue = new Queue<bool>();
                for (int j = 0; j < list.Count; j++)
                {
                    queue.Enqueue((list[j] & 0x40) != 0);
                    queue.Enqueue((list[j] & 0x20) != 0);
                    queue.Enqueue((list[j] & 0x10) != 0);
                    queue.Enqueue((list[j] & 8) != 0);
                    queue.Enqueue((list[j] & 4) != 0);
                    queue.Enqueue((list[j] & 2) != 0);
                    queue.Enqueue((list[j] & 1) != 0);
                }
                int index = queue.Count / 8;
                int num9 = queue.Count % 8;
                if (num9 == 0)
                {
                    buffer = new byte[index];
                }
                else
                {
                    buffer = new byte[index + 1];
                }
                for (int k = 0; k < index; k++)
                {
                    if (queue.Dequeue())
                    {
                        buffer[k] = (byte)(buffer[k] | 0x80);
                    }
                    if (queue.Dequeue())
                    {
                        buffer[k] = (byte)(buffer[k] | 0x40);
                    }
                    if (queue.Dequeue())
                    {
                        buffer[k] = (byte)(buffer[k] | 0x20);
                    }
                    if (queue.Dequeue())
                    {
                        buffer[k] = (byte)(buffer[k] | 0x10);
                    }
                    if (queue.Dequeue())
                    {
                        buffer[k] = (byte)(buffer[k] | 8);
                    }
                    if (queue.Dequeue())
                    {
                        buffer[k] = (byte)(buffer[k] | 4);
                    }
                    if (queue.Dequeue())
                    {
                        buffer[k] = (byte)(buffer[k] | 2);
                    }
                    if (queue.Dequeue())
                    {
                        buffer[k] = (byte)(buffer[k] | 1);
                    }
                }
                if (num9 != 0)
                {
                    for (int num11 = 0; num11 < num9; num11++)
                    {
                        if (queue.Dequeue())
                        {
                            buffer[index] = (byte)(buffer[index] | (((int)1) << (7 - num11)));
                        }
                    }
                }
                this.map.maptitlestore = buffer;
                this.f.allmapdata[this.editmapid] = this.map;
                MessageBox.Show("完成！");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (this.comboBox2.SelectedIndex >= 0)
            {
                this.map.npc_datas.RemoveAt(this.comboBox2.SelectedIndex);
                int selectedIndex = this.comboBox2.SelectedIndex;
                this.comboBox2.Items.RemoveAt(this.comboBox2.Items.Count - 1);
                if (this.comboBox2.Items.Count > 0)
                {
                    this.comboBox2.SelectedIndex = Math.Min(this.comboBox2.Items.Count - 1, selectedIndex);
                }
                MessageBox.Show("删除完成!");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[6];
            npc_data _data = new npc_data(data, this.map.PattenTable1, ref this.f._commdata)
            {
                image = (int)this.nu_npc_1.Value,
                direct = this.cb_npc_2.SelectedIndex,
                _3CD = (int)this.nu_npc_3.Value,
                _3F7 = (int)this.nu_npc_4.Value,
                _3DB = (int)this.nu_npc_5.Value,
                _3E9 = (int)this.nu_npc_6.Value,
                X = (int)this.nu_npc_x.Value,
                Y = (int)this.nu_npc_y.Value
            };
            npc_data item = new npc_data(_data.npc_store_data, this.map.PattenTable1, ref this.f._commdata);
            this.comboBox2.Items.Add(this.comboBox2.Items.Count.ToString("d2"));
            this.map.npc_datas.Add(item);
            MessageBox.Show("添加完成!");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (this.checkBox4.Checked)
            {
                this.map.mapnpcshare = 0;
            }
            else
            {
                this.map.mapnpcshare = ((int)this.numericUpDown18.Value) - 1;
            }
            MessageBox.Show("完成!重新打开修改器才能看到效果");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            this.map.PattenTable1 = this.f.allmapdata[((int)this.numericUpDown22.Value) - 1].PattenTable1;
            MessageBox.Show("完成");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.map.leftline = (int)this.numericUpDown1.Value;
            this.map.rightline = (int)this.numericUpDown2.Value;
            this.map.topline = (int)this.numericUpDown3.Value;
            this.map.bottomline = (int)this.numericUpDown4.Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            int index = 0;
            if (this.radioButton1.Checked)
            {
                buffer = new byte[(5 * this.map.enters.Count) + 2];
                buffer[0] = 0xff;
                index = 1;
            }
            else if (this.radioButton2.Checked)
            {
                buffer = new byte[(5 * this.map.enters.Count) + 5];
                buffer[0] = 0xfe;
                buffer[1] = (byte)this.nu_map1.Value;
                buffer[2] = (byte)this.nu_x1.Value;
                buffer[3] = (byte)this.nu_y1.Value;
                index = 4;
            }
            else
            {
                buffer = new byte[(5 * this.map.enters.Count) + 13];
                buffer[0] = (byte)this.nu_map1.Value;
                buffer[1] = (byte)this.nu_x1.Value;
                buffer[2] = (byte)this.nu_y1.Value;
                buffer[3] = (byte)this.nu_map2.Value;
                buffer[4] = (byte)this.nu_x2.Value;
                buffer[5] = (byte)this.nu_y2.Value;
                buffer[6] = (byte)this.nu_map3.Value;
                buffer[7] = (byte)this.nu_x3.Value;
                buffer[8] = (byte)this.nu_y3.Value;
                buffer[9] = (byte)this.nu_map4.Value;
                buffer[10] = (byte)this.nu_x4.Value;
                buffer[11] = (byte)this.nu_y4.Value;
                index = 12;
            }
            for (int i = 0; i < this.map.enters.Count; i++)
            {
                buffer[index] = (byte)this.map.enters.Count;
                buffer[(index + (i * 2)) + 1] = (byte)this.map.enters[i].X;
                buffer[(index + (i * 2)) + 2] = (byte)this.map.enters[i].Y;
                buffer[((index + 1) + (buffer[index] * 2)) + (3 * i)] = (byte)this.map.enters[i].switchmap;
                buffer[(((index + 1) + (buffer[index] * 2)) + (3 * i)) + 1] = (byte)this.map.enters[i].map_x;
                buffer[(((index + 1) + (buffer[index] * 2)) + (3 * i)) + 2] = (byte)this.map.enters[i].map_y;
            }
            this.map.enterdata = buffer;
            MessageBox.Show("完成！");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addpoint addpoint = new addpoint();
            if (addpoint.ShowDialog(this) == DialogResult.OK)
            {
                enterPoint item = new enterPoint
                {
                    X = addpoint.X,
                    Y = addpoint.Y,
                    switchmap = addpoint.mapid,
                    map_x = addpoint.map_X,
                    map_y = addpoint.map_Y
                };
                this.map.enters.Add(item);
                this.comboBox1.Items.Add(this.comboBox1.Items.Count.ToString("d2"));
                MessageBox.Show("添加完成!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex >= 0)
            {
                this.map.enters.RemoveAt(this.comboBox1.SelectedIndex);
                int selectedIndex = this.comboBox1.SelectedIndex;
                this.comboBox1.Items.RemoveAt(this.comboBox1.Items.Count - 1);
                if (this.comboBox1.Items.Count > 0)
                {
                    this.comboBox1.SelectedIndex = Math.Min(this.comboBox1.Items.Count - 1, selectedIndex);
                }
                MessageBox.Show("删除完成!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex >= 0)
            {
                enterPoint point = this.map.enters[this.comboBox1.SelectedIndex];
                point.X = (int)this.nu_point_x.Value;
                point.Y = (int)this.un_point_y.Value;
                point.switchmap = (int)this.un_point_map.Value;
                point.map_x = (int)this.un_point_x1.Value;
                point.map_y = (int)this.un_point_y1.Value;
                MessageBox.Show("修改完成!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (this.comboBox2.SelectedIndex >= 0)
            {
                npc_data _data = this.map.npc_datas[this.comboBox2.SelectedIndex];
                _data.image = (int)this.nu_npc_1.Value;
                _data.direct = this.cb_npc_2.SelectedIndex;
                _data._3CD = (int)this.nu_npc_3.Value;
                _data._3F7 = (int)this.nu_npc_4.Value;
                _data._3DB = (int)this.nu_npc_5.Value;
                _data._3E9 = (int)this.nu_npc_6.Value;
                _data.X = (int)this.nu_npc_x.Value;
                _data.Y = (int)this.nu_npc_y.Value;
                npc_data _data2 = new npc_data(_data.npc_store_data, this.map.PattenTable1, ref this.f._commdata);
                this.map.npc_datas[this.comboBox2.SelectedIndex] = _data2;
                MessageBox.Show("修改完成!");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.map._8b = (int)this.numericUpDown5.Value;
            this.map._8c = (int)this.numericUpDown6.Value;
            this.map._8d = (int)this.numericUpDown7.Value;
            this.map._8e = (int)this.numericUpDown8.Value;
            this.map.updergroundmap = this.comboBox3.SelectedIndex;
            MessageBox.Show("完成!");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.displayAreaLine = this.checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.showdynmic = this.checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.showhideItem = this.checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.numericUpDown18.ReadOnly = true;
            }
            else
            {
                this.numericUpDown18.ReadOnly = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.Items.Count >= 0)
            {
                enterPoint point = this.map.enters[this.comboBox1.SelectedIndex];
                this.nu_point_x.Value = point.X;
                this.un_point_y.Value = point.Y;
                this.un_point_map.Value = point.switchmap;
                this.un_point_x1.Value = point.map_x;
                this.un_point_y1.Value = point.map_y;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox2.SelectedIndex >= 0)
            {
                npc_data _data = this.map.npc_datas[this.comboBox2.SelectedIndex];
                this.nu_npc_1.Value = _data.image;
                this.cb_npc_2.SelectedIndex = _data.direct;
                this.nu_npc_3.Value = _data._3CD;
                this.nu_npc_4.Value = _data._3F7;
                this.nu_npc_5.Value = _data._3DB;
                this.nu_npc_6.Value = _data._3E9;
                this.nu_npc_x.Value = _data.X;
                this.nu_npc_y.Value = _data.Y;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox4.SelectedIndex >= 0)
            {
                hideItemdata itemdata = this.map.hideItem[this.comboBox4.SelectedIndex];
                this.numericUpDown21.Value = itemdata.x;
                this.numericUpDown20.Value = itemdata.y;
                this.numericUpDown19.Value = itemdata.itemdata;
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox5.SelectedIndex >= 0)
            {
                specPosition position = this.map.specP[this.comboBox5.SelectedIndex];
                this.numericUpDown11.Value = position.X;
                this.numericUpDown10.Value = position.Y;
                this.numericUpDown16.Value = position.content >> 4;
                this.numericUpDown15.Value = position.content & 15;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void editform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.f.froms != null)
            {
                this.f.froms.Dispose();
            }
        }

        private void editform_Load(object sender, EventArgs e)
        {
            this.f = (Form1)base.Owner;
            this.vScrollBar1.Maximum = Math.Max(0, (((this.map.cityBmp.Height - this.panel1.Height) + 10) - 1) + this.hScrollBar1.Height);
            this.hScrollBar1.Maximum = Math.Max(0, (((this.map.cityBmp.Width - this.panel1.Width) + 10) - 1) + this.vScrollBar1.Width);
            for (int i = 0; i < 0x80; i++)
            {
                this.imageList1.Images.Add(this.map.map_item_image[i]);
                this.map.map_item_image[i].Save(i.ToString("X2") + ".bmp");
                this.listView1.Items.Add(i.ToString("X2"), i);
            }
            if (this.editmapid < 0x7f)
            {
                this.numericUpDown23.ReadOnly = true;
                this.numericUpDown23.Enabled = false;
            }
            else
            {
                this.numericUpDown23.Value = this.f.mondatas.city_monareadata(this.editmapid + 1);
            }
            this.numericUpDown1.Value = this.map.leftline;
            this.numericUpDown2.Value = this.map.rightline;
            this.numericUpDown3.Value = this.map.topline;
            this.numericUpDown4.Value = this.map.bottomline;
            this.numericUpDown5.Value = this.map._8b;
            this.numericUpDown6.Value = this.map._8c;
            this.numericUpDown7.Value = this.map._8d;
            this.numericUpDown8.Value = this.map._8e;
            this.comboBox3.SelectedIndex = this.map.updergroundmap;
            if (this.map.mapnpcshare == 0)
            {
                this.checkBox4.Checked = true;
            }
            else
            {
                this.checkBox4.Checked = false;
            }
            this.numericUpDown18.Maximum = this.editmapid;
            byte[] enterdata = this.map.enterdata;
            string[] strArray = new string[4];
            int num2 = 0;
            foreach (byte num3 in enterdata)
            {
                string[] strArray2;
                (strArray2 = strArray)[0] = strArray2[0] + num3.ToString("X2") + "  ";
                num2++;
                if (num2 == 0x10)
                {
                    string[] strArray3;
                    (strArray3 = strArray)[0] = strArray3[0] + "\r\n";
                    num2 = 0;
                }
            }
            this.textBox2.Text = strArray[0];
            byte[] outjump = this.map.outjump;
            if (outjump == null)
            {
                this.radioButton1.Checked = true;
            }
            else if (outjump.Length == 3)
            {
                this.radioButton2.Checked = true;
                this.nu_map1.Value = outjump[0];
                this.nu_x1.Value = outjump[1];
                this.nu_y1.Value = outjump[2];
                this.panel2.Visible = false;
            }
            else
            {
                this.radioButton3.Checked = true;
                this.panel2.Visible = true;
                this.nu_map1.Value = outjump[0];
                this.nu_x1.Value = outjump[1];
                this.nu_y1.Value = outjump[2];
                this.nu_map2.Value = outjump[3];
                this.nu_x2.Value = outjump[4];
                this.nu_y2.Value = outjump[5];
                this.nu_map3.Value = outjump[6];
                this.nu_x3.Value = outjump[7];
                this.nu_y3.Value = outjump[8];
                this.nu_map4.Value = outjump[9];
                this.nu_x4.Value = outjump[10];
                this.nu_y4.Value = outjump[11];
            }
            for (int j = 0; j < this.map.enters.Count; j++)
            {
                this.comboBox1.Items.Add(j.ToString("d2"));
            }
            if (this.comboBox1.Items.Count > 0)
            {
                this.comboBox1.SelectedIndex = 0;
            }
            for (int k = 0; k < this.map.npc_datas.Count; k++)
            {
                this.comboBox2.Items.Add(k.ToString("d2"));
            }
            if (this.comboBox2.Items.Count > 0)
            {
                this.comboBox2.SelectedIndex = 0;
            }
            for (int m = 0; m < this.map.specP.Count; m++)
            {
                this.comboBox5.Items.Add(m.ToString("d2"));
            }
            if (this.comboBox5.Items.Count > 0)
            {
                this.comboBox5.SelectedIndex = 0;
            }
            for (int n = 0; n < this.map.hideItem.Count; n++)
            {
                this.comboBox4.Items.Add(n.ToString("d2"));
            }
            if (this.comboBox4.Items.Count > 0)
            {
                this.comboBox4.SelectedIndex = 0;
            }
            if (this.map.dynamicdata != null)
            {
                int num9;
                byte[] dynamicdata = this.map.dynamicdata;
                byte num1 = dynamicdata[0];
                for (int num8 = 0; num8 < dynamicdata.Length; num8 += (num9 * 3) + 2)
                {
                    num9 = dynamicdata[num8 + 1];
                    for (int num10 = 0; num10 < num9; num10++)
                    {
                        ListViewItem item = new ListViewItem();
                        item.SubItems[0].Text = dynamicdata[num8].ToString("X2");
                        item.SubItems.Add(dynamicdata[((num10 * 3) + num8) + 2].ToString("X2"));
                        item.SubItems.Add(dynamicdata[((num10 * 3) + num8) + 3].ToString("X2"));
                        item.SubItems.Add(dynamicdata[((num10 * 3) + num8) + 4].ToString("X2"));
                        this.listView2.Items.Add(item);
                    }
                }
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.panelPaintRect = this.panel1.ClientRectangle;
            this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.panel1 = new Panel();
            this.hScrollBar1 = new HScrollBar();
            this.vScrollBar1 = new VScrollBar();
            this.groupBox1 = new GroupBox();
            this.checkBox3 = new CheckBox();
            this.checkBox2 = new CheckBox();
            this.checkBox1 = new CheckBox();
            this.button2 = new Button();
            this.button1 = new Button();
            this.groupBox5 = new GroupBox();
            this.listView1 = new ListView();
            this.imageList1 = new ImageList(this.components);
            this.label11 = new Label();
            this.textBox2 = new TextBox();
            this.button3 = new Button();
            this.label4 = new Label();
            this.label2 = new Label();
            this.numericUpDown4 = new NumericUpDown();
            this.numericUpDown2 = new NumericUpDown();
            this.label3 = new Label();
            this.numericUpDown3 = new NumericUpDown();
            this.label1 = new Label();
            this.numericUpDown1 = new NumericUpDown();
            this.textBox1 = new TextBox();
            this.groupBox2 = new GroupBox();
            this.label18 = new Label();
            this.comboBox1 = new ComboBox();
            this.button6 = new Button();
            this.button7 = new Button();
            this.button5 = new Button();
            this.button4 = new Button();
            this.panel2 = new Panel();
            this.nu_map2 = new NumericUpDown();
            this.nu_map4 = new NumericUpDown();
            this.nu_y2 = new NumericUpDown();
            this.nu_x4 = new NumericUpDown();
            this.nu_y3 = new NumericUpDown();
            this.nu_map3 = new NumericUpDown();
            this.label8 = new Label();
            this.nu_x3 = new NumericUpDown();
            this.nu_y4 = new NumericUpDown();
            this.label12 = new Label();
            this.label17 = new Label();
            this.label9 = new Label();
            this.nu_x2 = new NumericUpDown();
            this.label15 = new Label();
            this.label14 = new Label();
            this.label13 = new Label();
            this.label10 = new Label();
            this.label16 = new Label();
            this.un_point_map = new NumericUpDown();
            this.nu_map1 = new NumericUpDown();
            this.un_point_x1 = new NumericUpDown();
            this.nu_point_x = new NumericUpDown();
            this.nu_x1 = new NumericUpDown();
            this.label24 = new Label();
            this.label20 = new Label();
            this.label5 = new Label();
            this.label7 = new Label();
            this.label22 = new Label();
            this.label21 = new Label();
            this.label23 = new Label();
            this.label19 = new Label();
            this.un_point_y1 = new NumericUpDown();
            this.un_point_y = new NumericUpDown();
            this.label6 = new Label();
            this.nu_y1 = new NumericUpDown();
            this.radioButton3 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.radioButton1 = new RadioButton();
            this.groupBox3 = new GroupBox();
            this.groupBox4 = new GroupBox();
            this.button22 = new Button();
            this.checkBox4 = new CheckBox();
            this.button20 = new Button();
            this.button21 = new Button();
            this.numericUpDown18 = new NumericUpDown();
            this.button8 = new Button();
            this.nu_npc_y = new NumericUpDown();
            this.label50 = new Label();
            this.nu_npc_x = new NumericUpDown();
            this.nu_npc_6 = new NumericUpDown();
            this.nu_npc_5 = new NumericUpDown();
            this.nu_npc_4 = new NumericUpDown();
            this.nu_npc_3 = new NumericUpDown();
            this.nu_npc_1 = new NumericUpDown();
            this.cb_npc_2 = new ComboBox();
            this.label31 = new Label();
            this.comboBox2 = new ComboBox();
            this.label33 = new Label();
            this.label32 = new Label();
            this.label25 = new Label();
            this.label26 = new Label();
            this.label27 = new Label();
            this.label28 = new Label();
            this.label29 = new Label();
            this.label30 = new Label();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.button23 = new Button();
            this.numericUpDown22 = new NumericUpDown();
            this.label54 = new Label();
            this.groupBox8 = new GroupBox();
            this.button17 = new Button();
            this.button18 = new Button();
            this.button19 = new Button();
            this.numericUpDown19 = new NumericUpDown();
            this.numericUpDown20 = new NumericUpDown();
            this.numericUpDown21 = new NumericUpDown();
            this.label49 = new Label();
            this.comboBox4 = new ComboBox();
            this.label51 = new Label();
            this.label52 = new Label();
            this.label53 = new Label();
            this.groupBox6 = new GroupBox();
            this.button11 = new Button();
            this.button12 = new Button();
            this.button10 = new Button();
            this.numericUpDown10 = new NumericUpDown();
            this.numericUpDown11 = new NumericUpDown();
            this.numericUpDown15 = new NumericUpDown();
            this.numericUpDown16 = new NumericUpDown();
            this.label40 = new Label();
            this.comboBox5 = new ComboBox();
            this.label41 = new Label();
            this.label42 = new Label();
            this.label46 = new Label();
            this.label48 = new Label();
            this.tabPage3 = new TabPage();
            this.comboBox3 = new ComboBox();
            this.button9 = new Button();
            this.numericUpDown9 = new NumericUpDown();
            this.numericUpDown8 = new NumericUpDown();
            this.numericUpDown7 = new NumericUpDown();
            this.numericUpDown6 = new NumericUpDown();
            this.numericUpDown5 = new NumericUpDown();
            this.label39 = new Label();
            this.label38 = new Label();
            this.label37 = new Label();
            this.label36 = new Label();
            this.label35 = new Label();
            this.label34 = new Label();
            this.groupBox7 = new GroupBox();
            this.button15 = new Button();
            this.button16 = new Button();
            this.button14 = new Button();
            this.listView2 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.button13 = new Button();
            this.label47 = new Label();
            this.numericUpDown17 = new NumericUpDown();
            this.numericUpDown14 = new NumericUpDown();
            this.label43 = new Label();
            this.numericUpDown12 = new NumericUpDown();
            this.label44 = new Label();
            this.label45 = new Label();
            this.numericUpDown13 = new NumericUpDown();
            this.label55 = new Label();
            this.numericUpDown23 = new NumericUpDown();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.numericUpDown4.BeginInit();
            this.numericUpDown2.BeginInit();
            this.numericUpDown3.BeginInit();
            this.numericUpDown1.BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.nu_map2.BeginInit();
            this.nu_map4.BeginInit();
            this.nu_y2.BeginInit();
            this.nu_x4.BeginInit();
            this.nu_y3.BeginInit();
            this.nu_map3.BeginInit();
            this.nu_x3.BeginInit();
            this.nu_y4.BeginInit();
            this.nu_x2.BeginInit();
            this.un_point_map.BeginInit();
            this.nu_map1.BeginInit();
            this.un_point_x1.BeginInit();
            this.nu_point_x.BeginInit();
            this.nu_x1.BeginInit();
            this.un_point_y1.BeginInit();
            this.un_point_y.BeginInit();
            this.nu_y1.BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.numericUpDown18.BeginInit();
            this.nu_npc_y.BeginInit();
            this.nu_npc_x.BeginInit();
            this.nu_npc_6.BeginInit();
            this.nu_npc_5.BeginInit();
            this.nu_npc_4.BeginInit();
            this.nu_npc_3.BeginInit();
            this.nu_npc_1.BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.numericUpDown22.BeginInit();
            this.groupBox8.SuspendLayout();
            this.numericUpDown19.BeginInit();
            this.numericUpDown20.BeginInit();
            this.numericUpDown21.BeginInit();
            this.groupBox6.SuspendLayout();
            this.numericUpDown10.BeginInit();
            this.numericUpDown11.BeginInit();
            this.numericUpDown15.BeginInit();
            this.numericUpDown16.BeginInit();
            this.tabPage3.SuspendLayout();
            this.numericUpDown9.BeginInit();
            this.numericUpDown8.BeginInit();
            this.numericUpDown7.BeginInit();
            this.numericUpDown6.BeginInit();
            this.numericUpDown5.BeginInit();
            this.groupBox7.SuspendLayout();
            this.numericUpDown17.BeginInit();
            this.numericUpDown14.BeginInit();
            this.numericUpDown12.BeginInit();
            this.numericUpDown13.BeginInit();
            this.numericUpDown23.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.hScrollBar1);
            this.panel1.Controls.Add(this.vScrollBar1);
            this.panel1.Location = new Point(12, 0x36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x207, 0x21f);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseMove += new MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseDown += new MouseEventHandler(this.panel1_MouseDown);
            this.hScrollBar1.Dock = DockStyle.Bottom;
            this.hScrollBar1.Location = new Point(0, 0x20e);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new Size(0x1f6, 0x11);
            this.hScrollBar1.TabIndex = 1;
            this.hScrollBar1.Scroll += new ScrollEventHandler(this.hScrollBar1_Scroll);
            this.vScrollBar1.Dock = DockStyle.Right;
            this.vScrollBar1.Location = new Point(0x1f6, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new Size(0x11, 0x21f);
            this.vScrollBar1.TabIndex = 0;
            this.vScrollBar1.Scroll += new ScrollEventHandler(this.vScrollBar1_Scroll);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1b1, 0x2b);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new Point(0x10b, 0x10);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new Size(0x48, 0x10);
            this.checkBox3.TabIndex = 1;
            this.checkBox3.Text = "显示箱子";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new EventHandler(this.checkBox3_CheckedChanged);
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new Point(0xa5, 0x10);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new Size(0x60, 0x10);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "显示动态图像";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(0x57, 0x10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x48, 0x10);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "显示入口";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.button2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.button2.Location = new Point(0x160, 0x10);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 0;
            this.button2.Text = "保存此地图";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button1.Location = new Point(6, 0x10);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "添加到图块库";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.groupBox5.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.groupBox5.Controls.Add(this.listView1);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Location = new Point(0x2f0, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0xc0, 0x150);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "图块";
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new Point(10, 20);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0xb0, 0x134);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            this.imageList1.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new Size(0x10, 0x10);
            this.imageList1.TransparentColor = Color.Transparent;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(15, 0x1b);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0, 12);
            this.label11.TabIndex = 0;
            this.textBox2.Location = new Point(0x29f, 5);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0x18, 0x19);
            this.textBox2.TabIndex = 5;
            this.textBox2.Visible = false;
            this.button3.Location = new Point(7, 0x74);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x26, 0x17);
            this.button3.TabIndex = 4;
            this.button3.Text = "保存";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(1, 0x5b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "下";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(1, 0x42);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "右";
            this.numericUpDown4.Location = new Point(20, 0x59);
            int[] bits = new int[4];
            bits[0] = 0xff;
            this.numericUpDown4.Maximum = new decimal(bits);
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new Size(0x24, 0x15);
            this.numericUpDown4.TabIndex = 2;
            this.numericUpDown2.Location = new Point(20, 0x40);
            int[] numArray2 = new int[4];
            numArray2[0] = 0xff;
            this.numericUpDown2.Maximum = new decimal(numArray2);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new Size(0x24, 0x15);
            this.numericUpDown2.TabIndex = 2;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(2, 0x2b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "上";
            this.numericUpDown3.Location = new Point(20, 0x29);
            int[] numArray3 = new int[4];
            numArray3[0] = 0xff;
            this.numericUpDown3.Maximum = new decimal(numArray3);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new Size(0x24, 0x15);
            this.numericUpDown3.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(2, 0x12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "左";
            this.numericUpDown1.Location = new Point(20, 0x10);
            int[] numArray4 = new int[4];
            numArray4[0] = 0xff;
            this.numericUpDown1.Maximum = new decimal(numArray4);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new Size(0x24, 0x15);
            this.numericUpDown1.TabIndex = 2;
            this.textBox1.Location = new Point(0x1c3, 0x17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x99, 0x15);
            this.textBox1.TabIndex = 0;
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.un_point_map);
            this.groupBox2.Controls.Add(this.nu_map1);
            this.groupBox2.Controls.Add(this.un_point_x1);
            this.groupBox2.Controls.Add(this.nu_point_x);
            this.groupBox2.Controls.Add(this.nu_x1);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.un_point_y1);
            this.groupBox2.Controls.Add(this.un_point_y);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.nu_y1);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new Point(5, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x13c, 0xfb);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "超过边界后";
            this.label18.AutoSize = true;
            this.label18.Location = new Point(14, 0xb3);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x47, 12);
            this.label18.TabIndex = 10;
            this.label18.Text = "地图切换点:";
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0x55, 0xb0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x36, 20);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.button6.Location = new Point(0x113, 0xae);
            this.button6.Name = "button6";
            this.button6.Size = new Size(0x26, 0x17);
            this.button6.TabIndex = 5;
            this.button6.Text = "删除";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new EventHandler(this.button6_Click);
            this.button7.Location = new Point(0xec, 0xc6);
            this.button7.Name = "button7";
            this.button7.Size = new Size(0x49, 0x17);
            this.button7.TabIndex = 5;
            this.button7.Text = "修改此入口";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new EventHandler(this.button7_Click);
            this.button5.Location = new Point(0xec, 0xad);
            this.button5.Name = "button5";
            this.button5.Size = new Size(0x26, 0x17);
            this.button5.TabIndex = 5;
            this.button5.Text = "添加";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new EventHandler(this.button5_Click);
            this.button4.Location = new Point(0x10f, 0x11);
            this.button4.Name = "button4";
            this.button4.Size = new Size(0x26, 0x17);
            this.button4.TabIndex = 5;
            this.button4.Text = "保存";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new EventHandler(this.button4_Click);
            this.panel2.Controls.Add(this.nu_map2);
            this.panel2.Controls.Add(this.nu_map4);
            this.panel2.Controls.Add(this.nu_y2);
            this.panel2.Controls.Add(this.nu_x4);
            this.panel2.Controls.Add(this.nu_y3);
            this.panel2.Controls.Add(this.nu_map3);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.nu_x3);
            this.panel2.Controls.Add(this.nu_y4);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.nu_x2);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Location = new Point(0x53, 0x56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0xe9, 0x54);
            this.panel2.TabIndex = 8;
            this.panel2.Visible = false;
            this.nu_map2.Hexadecimal = true;
            this.nu_map2.Location = new Point(0x19, 3);
            int[] numArray5 = new int[4];
            numArray5[0] = 0xef;
            this.nu_map2.Maximum = new decimal(numArray5);
            this.nu_map2.Name = "nu_map2";
            this.nu_map2.Size = new Size(0x33, 0x15);
            this.nu_map2.TabIndex = 5;
            this.nu_map4.Hexadecimal = true;
            this.nu_map4.Location = new Point(0xb5, 5);
            int[] numArray6 = new int[4];
            numArray6[0] = 0xef;
            this.nu_map4.Maximum = new decimal(numArray6);
            this.nu_map4.Name = "nu_map4";
            this.nu_map4.Size = new Size(0x33, 0x15);
            this.nu_map4.TabIndex = 5;
            this.nu_y2.Hexadecimal = true;
            this.nu_y2.Location = new Point(0x19, 0x3a);
            int[] numArray7 = new int[4];
            numArray7[0] = 0xff;
            this.nu_y2.Maximum = new decimal(numArray7);
            this.nu_y2.Name = "nu_y2";
            this.nu_y2.Size = new Size(0x33, 0x15);
            this.nu_y2.TabIndex = 4;
            this.nu_x4.Hexadecimal = true;
            this.nu_x4.Location = new Point(0xb5, 0x20);
            int[] numArray8 = new int[4];
            numArray8[0] = 0xff;
            this.nu_x4.Maximum = new decimal(numArray8);
            this.nu_x4.Name = "nu_x4";
            this.nu_x4.Size = new Size(0x33, 0x15);
            this.nu_x4.TabIndex = 5;
            this.nu_y3.Hexadecimal = true;
            this.nu_y3.Location = new Point(0x67, 0x3a);
            int[] numArray9 = new int[4];
            numArray9[0] = 0xff;
            this.nu_y3.Maximum = new decimal(numArray9);
            this.nu_y3.Name = "nu_y3";
            this.nu_y3.Size = new Size(0x33, 0x15);
            this.nu_y3.TabIndex = 4;
            this.nu_map3.Hexadecimal = true;
            this.nu_map3.Location = new Point(0x67, 3);
            int[] numArray10 = new int[4];
            numArray10[0] = 0xef;
            this.nu_map3.Maximum = new decimal(numArray10);
            this.nu_map3.Name = "nu_map3";
            this.nu_map3.Size = new Size(0x33, 0x15);
            this.nu_map3.TabIndex = 5;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(10, 0x20);
            this.label8.Name = "label8";
            this.label8.Size = new Size(11, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "X";
            this.nu_x3.Hexadecimal = true;
            this.nu_x3.Location = new Point(0x67, 30);
            int[] numArray11 = new int[4];
            numArray11[0] = 0xff;
            this.nu_x3.Maximum = new decimal(numArray11);
            this.nu_x3.Name = "nu_x3";
            this.nu_x3.Size = new Size(0x33, 0x15);
            this.nu_x3.TabIndex = 5;
            this.nu_y4.Hexadecimal = true;
            this.nu_y4.Location = new Point(0xb5, 60);
            int[] numArray12 = new int[4];
            numArray12[0] = 0xff;
            this.nu_y4.Maximum = new decimal(numArray12);
            this.nu_y4.Name = "nu_y4";
            this.nu_y4.Size = new Size(0x33, 0x15);
            this.nu_y4.TabIndex = 4;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x58, 0x20);
            this.label12.Name = "label12";
            this.label12.Size = new Size(11, 12);
            this.label12.TabIndex = 7;
            this.label12.Text = "X";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(0xa8, 0x3e);
            this.label17.Name = "label17";
            this.label17.Size = new Size(11, 12);
            this.label17.TabIndex = 6;
            this.label17.Text = "Y";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(-1, 5);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x17, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "map";
            this.nu_x2.Hexadecimal = true;
            this.nu_x2.Location = new Point(0x19, 30);
            int[] numArray13 = new int[4];
            numArray13[0] = 0xff;
            this.nu_x2.Maximum = new decimal(numArray13);
            this.nu_x2.Name = "nu_x2";
            this.nu_x2.Size = new Size(0x33, 0x15);
            this.nu_x2.TabIndex = 5;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0xa8, 0x22);
            this.label15.Name = "label15";
            this.label15.Size = new Size(11, 12);
            this.label15.TabIndex = 7;
            this.label15.Text = "X";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0x58, 60);
            this.label14.Name = "label14";
            this.label14.Size = new Size(11, 12);
            this.label14.TabIndex = 6;
            this.label14.Text = "Y";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x4e, 5);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x17, 12);
            this.label13.TabIndex = 7;
            this.label13.Text = "map";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(10, 60);
            this.label10.Name = "label10";
            this.label10.Size = new Size(11, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "Y";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(0x9d, 7);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x17, 12);
            this.label16.TabIndex = 7;
            this.label16.Text = "map";
            this.un_point_map.Hexadecimal = true;
            this.un_point_map.Location = new Point(0xa3, 0xc5);
            int[] numArray14 = new int[4];
            numArray14[0] = 0xef;
            this.un_point_map.Maximum = new decimal(numArray14);
            this.un_point_map.Name = "un_point_map";
            this.un_point_map.Size = new Size(0x33, 0x15);
            this.un_point_map.TabIndex = 5;
            this.nu_map1.Hexadecimal = true;
            this.nu_map1.Location = new Point(0x1d, 0x58);
            int[] numArray15 = new int[4];
            numArray15[0] = 0xef;
            this.nu_map1.Maximum = new decimal(numArray15);
            this.nu_map1.Name = "nu_map1";
            this.nu_map1.Size = new Size(0x33, 0x15);
            this.nu_map1.TabIndex = 5;
            this.un_point_x1.Hexadecimal = true;
            this.un_point_x1.Location = new Point(0xa2, 0xe0);
            int[] numArray16 = new int[4];
            numArray16[0] = 0xff;
            this.un_point_x1.Maximum = new decimal(numArray16);
            this.un_point_x1.Name = "un_point_x1";
            this.un_point_x1.Size = new Size(0x33, 0x15);
            this.un_point_x1.TabIndex = 5;
            this.nu_point_x.Hexadecimal = true;
            this.nu_point_x.Location = new Point(0x22, 200);
            int[] numArray17 = new int[4];
            numArray17[0] = 0xff;
            this.nu_point_x.Maximum = new decimal(numArray17);
            this.nu_point_x.Name = "nu_point_x";
            this.nu_point_x.Size = new Size(0x33, 0x15);
            this.nu_point_x.TabIndex = 5;
            this.nu_x1.Hexadecimal = true;
            this.nu_x1.Location = new Point(0x1d, 0x73);
            int[] numArray18 = new int[4];
            numArray18[0] = 0xff;
            this.nu_x1.Maximum = new decimal(numArray18);
            this.nu_x1.Name = "nu_x1";
            this.nu_x1.Size = new Size(0x33, 0x15);
            this.nu_x1.TabIndex = 5;
            this.label24.AutoSize = true;
            this.label24.Location = new Point(0xdf, 0xe0);
            this.label24.Name = "label24";
            this.label24.Size = new Size(11, 12);
            this.label24.TabIndex = 6;
            this.label24.Text = "Y";
            this.label20.AutoSize = true;
            this.label20.Location = new Point(15, 0xe4);
            this.label20.Name = "label20";
            this.label20.Size = new Size(11, 12);
            this.label20.TabIndex = 6;
            this.label20.Text = "Y";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(14, 0x91);
            this.label5.Name = "label5";
            this.label5.Size = new Size(11, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "Y";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(3, 90);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x17, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "map";
            this.label22.AutoSize = true;
            this.label22.Location = new Point(0x62, 0xe4);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x29, 12);
            this.label22.TabIndex = 7;
            this.label22.Text = "切换后";
            this.label21.AutoSize = true;
            this.label21.Location = new Point(0x5e, 0xc7);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x41, 12);
            this.label21.TabIndex = 7;
            this.label21.Text = "切换到地图";
            this.label23.AutoSize = true;
            this.label23.Location = new Point(0x91, 0xe4);
            this.label23.Name = "label23";
            this.label23.Size = new Size(11, 12);
            this.label23.TabIndex = 7;
            this.label23.Text = "X";
            this.label19.AutoSize = true;
            this.label19.Location = new Point(15, 200);
            this.label19.Name = "label19";
            this.label19.Size = new Size(11, 12);
            this.label19.TabIndex = 7;
            this.label19.Text = "X";
            this.un_point_y1.Hexadecimal = true;
            this.un_point_y1.Location = new Point(0xf2, 0xe0);
            int[] numArray19 = new int[4];
            numArray19[0] = 0xff;
            this.un_point_y1.Maximum = new decimal(numArray19);
            this.un_point_y1.Name = "un_point_y1";
            this.un_point_y1.Size = new Size(0x33, 0x15);
            this.un_point_y1.TabIndex = 4;
            this.un_point_y.Hexadecimal = true;
            this.un_point_y.Location = new Point(0x22, 0xe4);
            int[] numArray20 = new int[4];
            numArray20[0] = 0xff;
            this.un_point_y.Maximum = new decimal(numArray20);
            this.un_point_y.Name = "un_point_y";
            this.un_point_y.Size = new Size(0x33, 0x15);
            this.un_point_y.TabIndex = 4;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(14, 0x75);
            this.label6.Name = "label6";
            this.label6.Size = new Size(11, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "X";
            this.nu_y1.Hexadecimal = true;
            this.nu_y1.Location = new Point(0x1d, 0x8f);
            int[] numArray21 = new int[4];
            numArray21[0] = 0xff;
            this.nu_y1.Maximum = new decimal(numArray21);
            this.nu_y1.Name = "nu_y1";
            this.nu_y1.Size = new Size(0x33, 0x15);
            this.nu_y1.TabIndex = 4;
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new Point(6, 0x40);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new Size(0xd1, 0x10);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "分别指定从4个方向超过边界的跳转";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new EventHandler(this.radioButton3_CheckedChanged);
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(6, 0x2a);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0xb3, 0x10);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "跳转到指定地图位置，在下面";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new Point(6, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x8f, 0x10);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "返回进入地图时的位置";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.numericUpDown3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.numericUpDown2);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.numericUpDown4);
            this.groupBox3.Location = new Point(0x147, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x3e, 0x95);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "指定边界";
            this.groupBox4.Controls.Add(this.button22);
            this.groupBox4.Controls.Add(this.checkBox4);
            this.groupBox4.Controls.Add(this.button20);
            this.groupBox4.Controls.Add(this.button21);
            this.groupBox4.Controls.Add(this.numericUpDown18);
            this.groupBox4.Controls.Add(this.button8);
            this.groupBox4.Controls.Add(this.nu_npc_y);
            this.groupBox4.Controls.Add(this.label50);
            this.groupBox4.Controls.Add(this.nu_npc_x);
            this.groupBox4.Controls.Add(this.nu_npc_6);
            this.groupBox4.Controls.Add(this.nu_npc_5);
            this.groupBox4.Controls.Add(this.nu_npc_4);
            this.groupBox4.Controls.Add(this.nu_npc_3);
            this.groupBox4.Controls.Add(this.nu_npc_1);
            this.groupBox4.Controls.Add(this.cb_npc_2);
            this.groupBox4.Controls.Add(this.label31);
            this.groupBox4.Controls.Add(this.comboBox2);
            this.groupBox4.Controls.Add(this.label33);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.label29);
            this.groupBox4.Controls.Add(this.label30);
            this.groupBox4.Location = new Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x91, 0x107);
            this.groupBox4.TabIndex = 0x10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "NPC";
            this.button22.Location = new Point(0x6b, 0xe2);
            this.button22.Name = "button22";
            this.button22.Size = new Size(0x26, 0x17);
            this.button22.TabIndex = 0x17;
            this.button22.Text = "设置";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new EventHandler(this.button22_Click);
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new Point(6, 0xf7);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new Size(60, 0x10);
            this.checkBox4.TabIndex = 0x16;
            this.checkBox4.Text = "不共享";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new EventHandler(this.checkBox4_CheckedChanged);
            this.button20.Location = new Point(0x3a, 0xc9);
            this.button20.Name = "button20";
            this.button20.Size = new Size(0x26, 0x17);
            this.button20.TabIndex = 0x13;
            this.button20.Text = "删除";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new EventHandler(this.button20_Click);
            this.button21.Location = new Point(14, 0xc9);
            this.button21.Name = "button21";
            this.button21.Size = new Size(0x26, 0x17);
            this.button21.TabIndex = 0x12;
            this.button21.Text = "添加";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new EventHandler(this.button21_Click);
            this.numericUpDown18.Hexadecimal = true;
            this.numericUpDown18.Location = new Point(0x33, 0xe0);
            int[] numArray22 = new int[4];
            numArray22[0] = 0xef;
            this.numericUpDown18.Maximum = new decimal(numArray22);
            int[] numArray23 = new int[4];
            numArray23[0] = 1;
            this.numericUpDown18.Minimum = new decimal(numArray23);
            this.numericUpDown18.Name = "numericUpDown18";
            this.numericUpDown18.Size = new Size(0x37, 0x15);
            this.numericUpDown18.TabIndex = 0x15;
            int[] numArray24 = new int[4];
            numArray24[0] = 1;
            this.numericUpDown18.Value = new decimal(numArray24);
            this.button8.Location = new Point(0x5b, 14);
            this.button8.Name = "button8";
            this.button8.Size = new Size(0x26, 0x17);
            this.button8.TabIndex = 15;
            this.button8.Text = "保存";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new EventHandler(this.button8_Click);
            this.nu_npc_y.Hexadecimal = true;
            this.nu_npc_y.Location = new Point(0x5d, 0xae);
            int[] numArray25 = new int[4];
            numArray25[0] = 0xff;
            this.nu_npc_y.Maximum = new decimal(numArray25);
            this.nu_npc_y.Name = "nu_npc_y";
            this.nu_npc_y.Size = new Size(0x24, 0x15);
            this.nu_npc_y.TabIndex = 14;
            this.label50.AutoSize = true;
            this.label50.Location = new Point(4, 0xe2);
            this.label50.Name = "label50";
            this.label50.Size = new Size(0x35, 12);
            this.label50.TabIndex = 20;
            this.label50.Text = "共享npc:";
            this.nu_npc_x.Hexadecimal = true;
            this.nu_npc_x.Location = new Point(0x1c, 0xae);
            int[] numArray26 = new int[4];
            numArray26[0] = 0xff;
            this.nu_npc_x.Maximum = new decimal(numArray26);
            this.nu_npc_x.Name = "nu_npc_x";
            this.nu_npc_x.Size = new Size(0x26, 0x15);
            this.nu_npc_x.TabIndex = 14;
            this.nu_npc_6.Hexadecimal = true;
            this.nu_npc_6.Location = new Point(0x4e, 0x93);
            int[] numArray27 = new int[4];
            numArray27[0] = 0xff;
            this.nu_npc_6.Maximum = new decimal(numArray27);
            this.nu_npc_6.Name = "nu_npc_6";
            this.nu_npc_6.Size = new Size(0x36, 0x15);
            this.nu_npc_6.TabIndex = 14;
            this.nu_npc_5.Hexadecimal = true;
            this.nu_npc_5.Location = new Point(0x4e, 0x7e);
            int[] numArray28 = new int[4];
            numArray28[0] = 0xff;
            this.nu_npc_5.Maximum = new decimal(numArray28);
            this.nu_npc_5.Name = "nu_npc_5";
            this.nu_npc_5.Size = new Size(0x36, 0x15);
            this.nu_npc_5.TabIndex = 14;
            this.nu_npc_4.Hexadecimal = true;
            this.nu_npc_4.Location = new Point(0x4e, 0x69);
            int[] numArray29 = new int[4];
            numArray29[0] = 0xff;
            this.nu_npc_4.Maximum = new decimal(numArray29);
            this.nu_npc_4.Name = "nu_npc_4";
            this.nu_npc_4.Size = new Size(0x36, 0x15);
            this.nu_npc_4.TabIndex = 14;
            this.nu_npc_3.Hexadecimal = true;
            int[] numArray30 = new int[4];
            numArray30[0] = 0x10;
            this.nu_npc_3.Increment = new decimal(numArray30);
            this.nu_npc_3.Location = new Point(0x4e, 0x54);
            int[] numArray31 = new int[4];
            numArray31[0] = 240;
            this.nu_npc_3.Maximum = new decimal(numArray31);
            this.nu_npc_3.Name = "nu_npc_3";
            this.nu_npc_3.Size = new Size(0x36, 0x15);
            this.nu_npc_3.TabIndex = 14;
            this.nu_npc_1.Hexadecimal = true;
            this.nu_npc_1.Location = new Point(0x4e, 0x2b);
            int[] numArray32 = new int[4];
            numArray32[0] = 0x3f;
            this.nu_npc_1.Maximum = new decimal(numArray32);
            this.nu_npc_1.Name = "nu_npc_1";
            this.nu_npc_1.Size = new Size(0x36, 0x15);
            this.nu_npc_1.TabIndex = 14;
            this.cb_npc_2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cb_npc_2.FormattingEnabled = true;
            this.cb_npc_2.Items.AddRange(new object[] { "上", "下", "左", "右" });
            this.cb_npc_2.Location = new Point(0x4e, 0x40);
            this.cb_npc_2.Name = "cb_npc_2";
            this.cb_npc_2.Size = new Size(0x36, 20);
            this.cb_npc_2.TabIndex = 13;
            this.label31.AutoSize = true;
            this.label31.Location = new Point(6, 20);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0x17, 12);
            this.label31.TabIndex = 12;
            this.label31.Text = "NPC";
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new Point(30, 0x11);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(0x2c, 20);
            this.comboBox2.TabIndex = 11;
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.label33.AutoSize = true;
            this.label33.Location = new Point(0x49, 0xb2);
            this.label33.Name = "label33";
            this.label33.Size = new Size(11, 12);
            this.label33.TabIndex = 4;
            this.label33.Text = "Y";
            this.label32.AutoSize = true;
            this.label32.Location = new Point(11, 0xb0);
            this.label32.Name = "label32";
            this.label32.Size = new Size(11, 12);
            this.label32.TabIndex = 4;
            this.label32.Text = "X";
            this.label25.AutoSize = true;
            this.label25.Location = new Point(10, 0x94);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x2f, 12);
            this.label25.TabIndex = 4;
            this.label25.Text = "对话2：";
            this.label26.AutoSize = true;
            this.label26.Location = new Point(11, 0x80);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x2f, 12);
            this.label26.TabIndex = 5;
            this.label26.Text = "对话1：";
            this.label27.AutoSize = true;
            this.label27.Location = new Point(11, 0x6c);
            this.label27.Name = "label27";
            this.label27.Size = new Size(0x29, 12);
            this.label27.TabIndex = 6;
            this.label27.Text = "走动：";
            this.label28.AutoSize = true;
            this.label28.Location = new Point(11, 0x58);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x29, 12);
            this.label28.TabIndex = 1;
            this.label28.Text = "速度：";
            this.label29.AutoSize = true;
            this.label29.Location = new Point(11, 0x44);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x29, 12);
            this.label29.TabIndex = 2;
            this.label29.Text = "朝向：";
            this.label30.AutoSize = true;
            this.label30.Location = new Point(11, 0x30);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0x29, 12);
            this.label30.TabIndex = 3;
            this.label30.Text = "图像：";
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new Point(0x219, 0x15a);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x197, 300);
            this.tabControl1.TabIndex = 0x11;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x18f, 0x113);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "边界入口数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage2.Controls.Add(this.button23);
            this.tabPage2.Controls.Add(this.numericUpDown22);
            this.tabPage2.Controls.Add(this.label54);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x18f, 0x113);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "NPC电脑箱子售货机";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.button23.Location = new Point(260, 0xe8);
            this.button23.Name = "button23";
            this.button23.Size = new Size(0x26, 0x17);
            this.button23.TabIndex = 0x18;
            this.button23.Text = "设置";
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new EventHandler(this.button23_Click);
            this.numericUpDown22.Hexadecimal = true;
            this.numericUpDown22.Location = new Point(0xf3, 0xcd);
            int[] numArray33 = new int[4];
            numArray33[0] = 0xef;
            this.numericUpDown22.Maximum = new decimal(numArray33);
            int[] numArray34 = new int[4];
            numArray34[0] = 1;
            this.numericUpDown22.Minimum = new decimal(numArray34);
            this.numericUpDown22.Name = "numericUpDown22";
            this.numericUpDown22.Size = new Size(0x37, 0x15);
            this.numericUpDown22.TabIndex = 0x17;
            int[] numArray35 = new int[4];
            numArray35[0] = 1;
            this.numericUpDown22.Value = new decimal(numArray35);
            this.label54.AutoSize = true;
            this.label54.Location = new Point(0x9a, 0xcf);
            this.label54.Name = "label54";
            this.label54.Size = new Size(0x59, 12);
            this.label54.TabIndex = 0x16;
            this.label54.Text = "共享npc图像组:";
            this.groupBox8.Controls.Add(this.button17);
            this.groupBox8.Controls.Add(this.button18);
            this.groupBox8.Controls.Add(this.button19);
            this.groupBox8.Controls.Add(this.numericUpDown19);
            this.groupBox8.Controls.Add(this.numericUpDown20);
            this.groupBox8.Controls.Add(this.numericUpDown21);
            this.groupBox8.Controls.Add(this.label49);
            this.groupBox8.Controls.Add(this.comboBox4);
            this.groupBox8.Controls.Add(this.label51);
            this.groupBox8.Controls.Add(this.label52);
            this.groupBox8.Controls.Add(this.label53);
            this.groupBox8.Location = new Point(0x9b, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new Size(120, 0xb6);
            this.groupBox8.TabIndex = 0x11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "箱子，埋藏品";
            this.button17.Location = new Point(0x44, 0x7b);
            this.button17.Name = "button17";
            this.button17.Size = new Size(0x26, 0x17);
            this.button17.TabIndex = 0x11;
            this.button17.Text = "删除";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new EventHandler(this.button17_Click);
            this.button18.Location = new Point(0x12, 0x7b);
            this.button18.Name = "button18";
            this.button18.Size = new Size(0x26, 0x17);
            this.button18.TabIndex = 0x10;
            this.button18.Text = "添加";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new EventHandler(this.button18_Click);
            this.button19.Location = new Point(70, 0x98);
            this.button19.Name = "button19";
            this.button19.Size = new Size(0x26, 0x17);
            this.button19.TabIndex = 15;
            this.button19.Text = "保存";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new EventHandler(this.button19_Click);
            this.numericUpDown19.Hexadecimal = true;
            this.numericUpDown19.Location = new Point(0x3f, 0x5d);
            int[] numArray36 = new int[4];
            numArray36[0] = 0xff;
            this.numericUpDown19.Maximum = new decimal(numArray36);
            this.numericUpDown19.Name = "numericUpDown19";
            this.numericUpDown19.Size = new Size(0x36, 0x15);
            this.numericUpDown19.TabIndex = 14;
            this.numericUpDown20.Hexadecimal = true;
            this.numericUpDown20.InterceptArrowKeys = false;
            this.numericUpDown20.Location = new Point(0x3f, 0x44);
            int[] numArray37 = new int[4];
            numArray37[0] = 0xff;
            this.numericUpDown20.Maximum = new decimal(numArray37);
            this.numericUpDown20.Name = "numericUpDown20";
            this.numericUpDown20.Size = new Size(0x36, 0x15);
            this.numericUpDown20.TabIndex = 14;
            this.numericUpDown21.Hexadecimal = true;
            this.numericUpDown21.Location = new Point(0x3f, 0x2b);
            int[] numArray38 = new int[4];
            numArray38[0] = 0xff;
            this.numericUpDown21.Maximum = new decimal(numArray38);
            this.numericUpDown21.Name = "numericUpDown21";
            this.numericUpDown21.Size = new Size(0x36, 0x15);
            this.numericUpDown21.TabIndex = 14;
            this.label49.AutoSize = true;
            this.label49.Location = new Point(2, 20);
            this.label49.Name = "label49";
            this.label49.Size = new Size(0x1d, 12);
            this.label49.TabIndex = 12;
            this.label49.Text = "序号";
            this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new Point(30, 0x11);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new Size(0x2c, 20);
            this.comboBox4.TabIndex = 11;
            this.comboBox4.SelectedIndexChanged += new EventHandler(this.comboBox4_SelectedIndexChanged);
            this.label51.AutoSize = true;
            this.label51.Location = new Point(2, 0x61);
            this.label51.Name = "label51";
            this.label51.Size = new Size(0x3b, 12);
            this.label51.TabIndex = 4;
            this.label51.Text = "物品代码:";
            this.label52.AutoSize = true;
            this.label52.Location = new Point(0x27, 0x48);
            this.label52.Name = "label52";
            this.label52.Size = new Size(0x11, 12);
            this.label52.TabIndex = 1;
            this.label52.Text = "Y:";
            this.label53.AutoSize = true;
            this.label53.Location = new Point(0x27, 0x30);
            this.label53.Name = "label53";
            this.label53.Size = new Size(0x11, 12);
            this.label53.TabIndex = 3;
            this.label53.Text = "X:";
            this.groupBox6.Controls.Add(this.button11);
            this.groupBox6.Controls.Add(this.button12);
            this.groupBox6.Controls.Add(this.button10);
            this.groupBox6.Controls.Add(this.numericUpDown10);
            this.groupBox6.Controls.Add(this.numericUpDown11);
            this.groupBox6.Controls.Add(this.numericUpDown15);
            this.groupBox6.Controls.Add(this.numericUpDown16);
            this.groupBox6.Controls.Add(this.label40);
            this.groupBox6.Controls.Add(this.comboBox5);
            this.groupBox6.Controls.Add(this.label41);
            this.groupBox6.Controls.Add(this.label42);
            this.groupBox6.Controls.Add(this.label46);
            this.groupBox6.Controls.Add(this.label48);
            this.groupBox6.Location = new Point(0x119, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x70, 0xb6);
            this.groupBox6.TabIndex = 0x11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "电脑";
            this.button11.Location = new Point(0x44, 0x7b);
            this.button11.Name = "button11";
            this.button11.Size = new Size(0x26, 0x17);
            this.button11.TabIndex = 0x11;
            this.button11.Text = "删除";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new EventHandler(this.button11_Click);
            this.button12.Location = new Point(0x12, 0x7b);
            this.button12.Name = "button12";
            this.button12.Size = new Size(0x26, 0x17);
            this.button12.TabIndex = 0x10;
            this.button12.Text = "添加";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new EventHandler(this.button12_Click);
            this.button10.Location = new Point(70, 0x98);
            this.button10.Name = "button10";
            this.button10.Size = new Size(0x26, 0x17);
            this.button10.TabIndex = 15;
            this.button10.Text = "保存";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new EventHandler(this.button10_Click);
            this.numericUpDown10.Hexadecimal = true;
            this.numericUpDown10.Location = new Point(70, 0x5d);
            int[] numArray39 = new int[4];
            numArray39[0] = 0xff;
            this.numericUpDown10.Maximum = new decimal(numArray39);
            this.numericUpDown10.Name = "numericUpDown10";
            this.numericUpDown10.Size = new Size(0x24, 0x15);
            this.numericUpDown10.TabIndex = 14;
            this.numericUpDown11.Hexadecimal = true;
            this.numericUpDown11.Location = new Point(0x12, 0x5d);
            int[] numArray40 = new int[4];
            numArray40[0] = 0xff;
            this.numericUpDown11.Maximum = new decimal(numArray40);
            this.numericUpDown11.Name = "numericUpDown11";
            this.numericUpDown11.Size = new Size(0x26, 0x15);
            this.numericUpDown11.TabIndex = 14;
            this.numericUpDown15.Hexadecimal = true;
            this.numericUpDown15.InterceptArrowKeys = false;
            this.numericUpDown15.Location = new Point(0x34, 0x44);
            int[] numArray41 = new int[4];
            numArray41[0] = 10;
            this.numericUpDown15.Maximum = new decimal(numArray41);
            this.numericUpDown15.Name = "numericUpDown15";
            this.numericUpDown15.Size = new Size(0x36, 0x15);
            this.numericUpDown15.TabIndex = 14;
            this.numericUpDown16.Hexadecimal = true;
            this.numericUpDown16.Location = new Point(0x34, 0x2b);
            int[] numArray42 = new int[4];
            numArray42[0] = 15;
            this.numericUpDown16.Maximum = new decimal(numArray42);
            this.numericUpDown16.Name = "numericUpDown16";
            this.numericUpDown16.Size = new Size(0x36, 0x15);
            this.numericUpDown16.TabIndex = 14;
            this.label40.AutoSize = true;
            this.label40.Location = new Point(2, 20);
            this.label40.Name = "label40";
            this.label40.Size = new Size(0x1d, 12);
            this.label40.TabIndex = 12;
            this.label40.Text = "序号";
            this.comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new Point(30, 0x11);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new Size(0x2c, 20);
            this.comboBox5.TabIndex = 11;
            this.comboBox5.SelectedIndexChanged += new EventHandler(this.comboBox5_SelectedIndexChanged);
            this.label41.AutoSize = true;
            this.label41.Location = new Point(0x3a, 0x61);
            this.label41.Name = "label41";
            this.label41.Size = new Size(11, 12);
            this.label41.TabIndex = 4;
            this.label41.Text = "Y";
            this.label42.AutoSize = true;
            this.label42.Location = new Point(6, 0x61);
            this.label42.Name = "label42";
            this.label42.Size = new Size(11, 12);
            this.label42.TabIndex = 4;
            this.label42.Text = "X";
            this.label46.AutoSize = true;
            this.label46.Location = new Point(11, 0x48);
            this.label46.Name = "label46";
            this.label46.Size = new Size(0x17, 12);
            this.label46.TabIndex = 1;
            this.label46.Text = "cd:";
            this.label48.AutoSize = true;
            this.label48.Location = new Point(11, 0x30);
            this.label48.Name = "label48";
            this.label48.Size = new Size(0x17, 12);
            this.label48.TabIndex = 3;
            this.label48.Text = "cc:";
            this.tabPage3.Controls.Add(this.comboBox3);
            this.tabPage3.Controls.Add(this.button9);
            this.tabPage3.Controls.Add(this.numericUpDown23);
            this.tabPage3.Controls.Add(this.numericUpDown9);
            this.tabPage3.Controls.Add(this.numericUpDown8);
            this.tabPage3.Controls.Add(this.numericUpDown7);
            this.tabPage3.Controls.Add(this.numericUpDown6);
            this.tabPage3.Controls.Add(this.numericUpDown5);
            this.tabPage3.Controls.Add(this.label55);
            this.tabPage3.Controls.Add(this.label39);
            this.tabPage3.Controls.Add(this.label38);
            this.tabPage3.Controls.Add(this.label37);
            this.tabPage3.Controls.Add(this.label36);
            this.tabPage3.Controls.Add(this.label35);
            this.tabPage3.Controls.Add(this.label34);
            this.tabPage3.Location = new Point(4, 0x15);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(0x18f, 0x113);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "杂项";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.comboBox3.AccessibleDescription = "在地下地图使用传真，会返回地面";
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] { "普通地图", "地下地图" });
            this.comboBox3.Location = new Point(0x5f, 0x7a);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new Size(0x36, 20);
            this.comboBox3.TabIndex = 0x12;
            this.button9.Location = new Point(0x6f, 0x94);
            this.button9.Name = "button9";
            this.button9.Size = new Size(0x26, 0x17);
            this.button9.TabIndex = 0x11;
            this.button9.Text = "保存";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new EventHandler(this.button9_Click);
            this.numericUpDown9.Hexadecimal = true;
            this.numericUpDown9.Location = new Point(0x5f, 0x5f);
            int[] numArray43 = new int[4];
            numArray43[0] = 1;
            this.numericUpDown9.Maximum = new decimal(numArray43);
            this.numericUpDown9.Name = "numericUpDown9";
            this.numericUpDown9.Size = new Size(0x36, 0x15);
            this.numericUpDown9.TabIndex = 0x10;
            this.numericUpDown8.Hexadecimal = true;
            this.numericUpDown8.Location = new Point(0x5f, 0x49);
            int[] numArray44 = new int[4];
            numArray44[0] = 0xff;
            this.numericUpDown8.Maximum = new decimal(numArray44);
            this.numericUpDown8.Name = "numericUpDown8";
            this.numericUpDown8.Size = new Size(0x36, 0x15);
            this.numericUpDown8.TabIndex = 0x10;
            this.numericUpDown7.Hexadecimal = true;
            this.numericUpDown7.Location = new Point(0x5f, 0x34);
            int[] numArray45 = new int[4];
            numArray45[0] = 0xff;
            this.numericUpDown7.Maximum = new decimal(numArray45);
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new Size(0x36, 0x15);
            this.numericUpDown7.TabIndex = 0x10;
            this.numericUpDown6.Hexadecimal = true;
            this.numericUpDown6.Location = new Point(0x5f, 0x1f);
            int[] numArray46 = new int[4];
            numArray46[0] = 0xff;
            this.numericUpDown6.Maximum = new decimal(numArray46);
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new Size(0x36, 0x15);
            this.numericUpDown6.TabIndex = 0x10;
            this.numericUpDown5.Hexadecimal = true;
            this.numericUpDown5.Location = new Point(0x5f, 10);
            int[] numArray47 = new int[4];
            numArray47[0] = 0xff;
            this.numericUpDown5.Maximum = new decimal(numArray47);
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new Size(0x36, 0x15);
            this.numericUpDown5.TabIndex = 0x10;
            this.label39.AutoSize = true;
            this.label39.Location = new Point(10, 0x7d);
            this.label39.Name = "label39";
            this.label39.Size = new Size(0x41, 12);
            this.label39.TabIndex = 15;
            this.label39.Text = "地下地图：";
            this.label38.AutoSize = true;
            this.label38.Location = new Point(10, 0x62);
            this.label38.Name = "label38";
            this.label38.Size = new Size(0x2f, 12);
            this.label38.TabIndex = 15;
            this.label38.Text = "未知2：";
            this.label37.AutoSize = true;
            this.label37.Location = new Point(10, 0x4c);
            this.label37.Name = "label37";
            this.label37.Size = new Size(0x29, 12);
            this.label37.TabIndex = 15;
            this.label37.Text = "歌曲：";
            this.label36.AutoSize = true;
            this.label36.Location = new Point(10, 0x37);
            this.label36.Name = "label36";
            this.label36.Size = new Size(0x4d, 12);
            this.label36.TabIndex = 15;
            this.label36.Text = "边界外地形：";
            this.label35.AutoSize = true;
            this.label35.Location = new Point(10, 0x22);
            this.label35.Name = "label35";
            this.label35.Size = new Size(0x2f, 12);
            this.label35.TabIndex = 15;
            this.label35.Text = "未知1：";
            this.label34.AutoSize = true;
            this.label34.Location = new Point(10, 13);
            this.label34.Name = "label34";
            this.label34.Size = new Size(0x59, 12);
            this.label34.TabIndex = 15;
            this.label34.Text = "门下地图块号：";
            this.groupBox7.Controls.Add(this.button15);
            this.groupBox7.Controls.Add(this.button16);
            this.groupBox7.Controls.Add(this.button14);
            this.groupBox7.Controls.Add(this.listView2);
            this.groupBox7.Controls.Add(this.button13);
            this.groupBox7.Controls.Add(this.label47);
            this.groupBox7.Controls.Add(this.numericUpDown17);
            this.groupBox7.Controls.Add(this.numericUpDown14);
            this.groupBox7.Controls.Add(this.label43);
            this.groupBox7.Controls.Add(this.numericUpDown12);
            this.groupBox7.Controls.Add(this.label44);
            this.groupBox7.Controls.Add(this.label45);
            this.groupBox7.Controls.Add(this.numericUpDown13);
            this.groupBox7.Location = new Point(0x21d, 0x36);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new Size(200, 0x11e);
            this.groupBox7.TabIndex = 0x12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "动态地形";
            this.button15.Location = new Point(0x9c, 0xf6);
            this.button15.Name = "button15";
            this.button15.Size = new Size(0x26, 0x17);
            this.button15.TabIndex = 0x17;
            this.button15.Text = "保存";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new EventHandler(this.button15_Click);
            this.button16.Location = new Point(0x9c, 0x67);
            this.button16.Name = "button16";
            this.button16.Size = new Size(0x26, 0x17);
            this.button16.TabIndex = 0x16;
            this.button16.Text = "修改";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new EventHandler(this.button16_Click);
            this.button14.Location = new Point(0x39, 0x69);
            this.button14.Name = "button14";
            this.button14.Size = new Size(0x26, 0x17);
            this.button14.TabIndex = 0x16;
            this.button14.Text = "删除";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new EventHandler(this.button14_Click);
            this.listView2.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4 });
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView2.Location = new Point(5, 0x86);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new Size(0xbd, 0x61);
            this.listView2.TabIndex = 0x15;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = View.Details;
            this.listView2.SelectedIndexChanged += new EventHandler(this.listView2_SelectedIndexChanged);
            this.columnHeader1.Text = "事件";
            this.columnHeader2.Text = "X坐标";
            this.columnHeader3.Text = "Y坐标";
            this.columnHeader4.Text = "图块";
            this.button13.Location = new Point(13, 0x69);
            this.button13.Name = "button13";
            this.button13.Size = new Size(0x26, 0x17);
            this.button13.TabIndex = 20;
            this.button13.Text = "添加";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new EventHandler(this.button13_Click);
            this.label47.AutoSize = true;
            this.label47.Location = new Point(0x67, 0x4e);
            this.label47.Name = "label47";
            this.label47.Size = new Size(0x23, 12);
            this.label47.TabIndex = 0x13;
            this.label47.Text = "图块:";
            this.numericUpDown17.Hexadecimal = true;
            this.numericUpDown17.Location = new Point(0x90, 0x4c);
            int[] numArray48 = new int[4];
            numArray48[0] = 0xff;
            this.numericUpDown17.Maximum = new decimal(numArray48);
            this.numericUpDown17.Name = "numericUpDown17";
            this.numericUpDown17.Size = new Size(0x33, 0x15);
            this.numericUpDown17.TabIndex = 0x12;
            this.numericUpDown14.Hexadecimal = true;
            this.numericUpDown14.Location = new Point(0x30, 15);
            int[] numArray49 = new int[4];
            numArray49[0] = 0xff;
            this.numericUpDown14.Maximum = new decimal(numArray49);
            this.numericUpDown14.Name = "numericUpDown14";
            this.numericUpDown14.Size = new Size(0x33, 0x15);
            this.numericUpDown14.TabIndex = 0x11;
            this.label43.AutoSize = true;
            this.label43.Location = new Point(7, 0x11);
            this.label43.Name = "label43";
            this.label43.Size = new Size(0x23, 12);
            this.label43.TabIndex = 0x10;
            this.label43.Text = "事件:";
            this.numericUpDown12.Hexadecimal = true;
            this.numericUpDown12.Location = new Point(0x8f, 0x11);
            int[] numArray50 = new int[4];
            numArray50[0] = 0xff;
            this.numericUpDown12.Maximum = new decimal(numArray50);
            this.numericUpDown12.Name = "numericUpDown12";
            this.numericUpDown12.Size = new Size(0x33, 0x15);
            this.numericUpDown12.TabIndex = 12;
            this.label44.AutoSize = true;
            this.label44.Location = new Point(0x7c, 0x2d);
            this.label44.Name = "label44";
            this.label44.Size = new Size(11, 12);
            this.label44.TabIndex = 13;
            this.label44.Text = "Y";
            this.label45.AutoSize = true;
            this.label45.Location = new Point(0x7c, 0x11);
            this.label45.Name = "label45";
            this.label45.Size = new Size(11, 12);
            this.label45.TabIndex = 14;
            this.label45.Text = "X";
            this.numericUpDown13.Hexadecimal = true;
            this.numericUpDown13.Location = new Point(0x8f, 0x2d);
            int[] numArray51 = new int[4];
            numArray51[0] = 0xff;
            this.numericUpDown13.Maximum = new decimal(numArray51);
            this.numericUpDown13.Name = "numericUpDown13";
            this.numericUpDown13.Size = new Size(0x33, 0x15);
            this.numericUpDown13.TabIndex = 11;
            this.label55.AutoSize = true;
            this.label55.Location = new Point(11, 0xc5);
            this.label55.Name = "label55";
            this.label55.Size = new Size(0x41, 12);
            this.label55.TabIndex = 15;
            this.label55.Text = "遇敌代码：";
            this.numericUpDown23.Hexadecimal = true;
            this.numericUpDown23.Location = new Point(0x5f, 0xc3);
            int[] numArray52 = new int[4];
            numArray52[0] = 0x5d;
            this.numericUpDown23.Maximum = new decimal(numArray52);
            this.numericUpDown23.Name = "numericUpDown23";
            this.numericUpDown23.Size = new Size(0x36, 0x15);
            this.numericUpDown23.TabIndex = 0x10;
            this.numericUpDown23.ValueChanged += new EventHandler(this.numericUpDown23_ValueChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3b5, 0x289);
            base.Controls.Add(this.groupBox7);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.groupBox5);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.panel1);
            base.Name = "editform";
            this.Text = "地图编辑窗体";
            base.Load += new EventHandler(this.editform_Load);
            base.FormClosing += new FormClosingEventHandler(this.editform_FormClosing);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.numericUpDown4.EndInit();
            this.numericUpDown2.EndInit();
            this.numericUpDown3.EndInit();
            this.numericUpDown1.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.nu_map2.EndInit();
            this.nu_map4.EndInit();
            this.nu_y2.EndInit();
            this.nu_x4.EndInit();
            this.nu_y3.EndInit();
            this.nu_map3.EndInit();
            this.nu_x3.EndInit();
            this.nu_y4.EndInit();
            this.nu_x2.EndInit();
            this.un_point_map.EndInit();
            this.nu_map1.EndInit();
            this.un_point_x1.EndInit();
            this.nu_point_x.EndInit();
            this.nu_x1.EndInit();
            this.un_point_y1.EndInit();
            this.un_point_y.EndInit();
            this.nu_y1.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.numericUpDown18.EndInit();
            this.nu_npc_y.EndInit();
            this.nu_npc_x.EndInit();
            this.nu_npc_6.EndInit();
            this.nu_npc_5.EndInit();
            this.nu_npc_4.EndInit();
            this.nu_npc_3.EndInit();
            this.nu_npc_1.EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.numericUpDown22.EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.numericUpDown19.EndInit();
            this.numericUpDown20.EndInit();
            this.numericUpDown21.EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.numericUpDown10.EndInit();
            this.numericUpDown11.EndInit();
            this.numericUpDown15.EndInit();
            this.numericUpDown16.EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.numericUpDown9.EndInit();
            this.numericUpDown8.EndInit();
            this.numericUpDown7.EndInit();
            this.numericUpDown6.EndInit();
            this.numericUpDown5.EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.numericUpDown17.EndInit();
            this.numericUpDown14.EndInit();
            this.numericUpDown12.EndInit();
            this.numericUpDown13.EndInit();
            this.numericUpDown23.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 1)
            {
                int index = this.listView1.SelectedItems[0].Index;
                this.f.currentitemdata = new MapItemData(1, 1, index);
                this.f.currentbigitemid = -1;
                this.f.currentitemdata.ItemImage = (Bitmap)this.map.map_item_image[index].Clone();
                this.f.currentitemdata.name = "";
                this.f.currentitemdata.subItems = new int[,] { { index } };
                this.f.currentitemdata.ItemImage = Form1.VitrificationImage(this.f.currentitemdata.ItemImage, 0.7f);
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count > 0)
            {
                int index = this.listView2.SelectedItems[0].Index;
                this.numericUpDown14.Value = int.Parse(this.listView2.Items[index].SubItems[0].Text, NumberStyles.HexNumber);
                this.numericUpDown12.Value = int.Parse(this.listView2.Items[index].SubItems[1].Text, NumberStyles.HexNumber);
                this.numericUpDown13.Value = int.Parse(this.listView2.Items[index].SubItems[2].Text, NumberStyles.HexNumber);
                this.numericUpDown17.Value = int.Parse(this.listView2.Items[index].SubItems[3].Text, NumberStyles.HexNumber);
            }
        }

        private void modifybitmappart(int imageX, int imageY, Bitmap image)
        {
            for (int i = 0; i < Math.Min(image.Height, (this.map.MapHeight - imageY) * 0x10); i++)
            {
                for (int j = 0; j < Math.Min(image.Width, (this.map.MapWidth - imageX) * 0x10); j++)
                {
                    this.map.cityBmp.SetPixel((imageX * 0x10) + j, (imageY * 0x10) + i, image.GetPixel(j, i));
                }
            }
        }

        private void modifymappart(int imageX, int imageY, MapItemData data)
        {
            for (int i = 0; i < Math.Min(data.width, this.map.MapWidth - imageX); i++)
            {
                for (int j = 0; j < Math.Min(data.height, this.map.MapHeight - imageY); j++)
                {
                    this.map.maptitles[(((imageY + j) * this.map.MapWidth) + imageX) + i] = (byte)data.subItems[i, j];
                }
            }
        }

        private void numericUpDown23_ValueChanged(object sender, EventArgs e)
        {
            this.f.mondatas.setcity_monareadata(this.editmapid + 1, (byte)this.numericUpDown23.Value);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.f.currentitemdata != null)
                {
                    Rectangle rectangle = new Rectangle(this.curorP, new Size(this.f.currentitemdata.width, this.f.currentitemdata.height));
                    if (((e.X + this.hScrollBar1.Value) < (this.map.MapWidth * 0x10)) && ((e.Y + this.vScrollBar1.Value) < (this.map.MapHeight * 0x10)))
                    {
                        this.modifymappart(rectangle.X, rectangle.Y, this.f.currentitemdata);
                        if (this.f.currentbigitemid >= 0)
                        {
                            this.modifybitmappart(rectangle.X, rectangle.Y, this.f.clipmapitems[this.f.currentbigitemid].ItemImage);
                        }
                        else
                        {
                            this.modifybitmappart(rectangle.X, rectangle.Y, this.map.map_item_image[this.f.currentitemdata.mapid]);
                        }
                        if (this.f.currentitemdata != null)
                        {
                            this.panelPaintRect.X = (this.curorP.X * 0x10) - this.hScrollBar1.Value;
                            this.panelPaintRect.Y = (this.curorP.Y * 0x10) - this.vScrollBar1.Value;
                            this.panelPaintRect.Width = this.f.currentitemdata.width * 0x10;
                            this.panelPaintRect.Height = this.f.currentitemdata.height * 0x10;
                            this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
                        }
                        this.currentDrawRect = rectangle;
                    }
                }
                else
                {
                    for (int i = 0; i < this.map.enters.Count; i++)
                    {
                        if ((this.curorP.X == this.map.enters[i].X) && (this.curorP.Y == this.map.enters[i].Y))
                        {
                            this.comboBox1.SelectedIndex = i;
                            this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
                            break;
                        }
                    }
                    for (int j = 0; j < this.map.npc_datas.Count; j++)
                    {
                        if ((this.curorP.X == this.map.npc_datas[j].X) && (this.curorP.Y == this.map.npc_datas[j].Y))
                        {
                            this.comboBox2.SelectedIndex = j;
                            this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
                            break;
                        }
                    }
                    for (int k = 0; k < this.map.hideItem.Count; k++)
                    {
                        if ((this.curorP.X == this.map.hideItem[k].x) && (this.curorP.Y == this.map.hideItem[k].y))
                        {
                            this.comboBox4.SelectedIndex = k;
                            this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
                            break;
                        }
                    }
                    for (int m = 0; m < this.map.specP.Count; m++)
                    {
                        if ((this.curorP.X == this.map.specP[m].X) && (this.curorP.Y == this.map.specP[m].Y))
                        {
                            this.comboBox5.SelectedIndex = m;
                            this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
                            break;
                        }
                    }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (this.SelectedRect != Rectangle.Empty)
                {
                    this.panelPaintRect.X = (this.SelectedRect.X * 0x10) - this.hScrollBar1.Value;
                    this.panelPaintRect.Y = (this.SelectedRect.Y * 0x10) - this.vScrollBar1.Value;
                    this.panelPaintRect.Width = (this.SelectedRect.Width * 0x10) + 1;
                    this.panelPaintRect.Height = (this.SelectedRect.Height * 0x10) + 1;
                    this.SelectedRect = Rectangle.Empty;
                    this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
                }
                this.SelectedRect.X = this.curorP.X;
                this.SelectedRect.Y = this.curorP.Y;
                this.SelectedRect.Width = 1;
                this.SelectedRect.Height = 1;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (((((e.X + this.hScrollBar1.Value) - (this.curorP.X * 0x10)) < 0) || (((e.X + this.hScrollBar1.Value) - (this.curorP.X * 0x10)) >= 0x10)) || ((((e.Y + this.vScrollBar1.Value) - (this.curorP.Y * 0x10)) < 0) || (((e.Y + this.vScrollBar1.Value) - (this.curorP.Y * 0x10)) >= 0x10)))
                {
                    this.curorP.X = (e.X + this.hScrollBar1.Value) / 0x10;
                    this.curorP.Y = (e.Y + this.vScrollBar1.Value) / 0x10;
                    this.panelPaintRect.X = (this.SelectedRect.X * 0x10) - this.hScrollBar1.Value;
                    this.panelPaintRect.Y = (this.SelectedRect.Y * 0x10) - this.vScrollBar1.Value;
                    this.panelPaintRect.Width = (this.SelectedRect.Width * 0x10) + 1;
                    this.panelPaintRect.Height = (this.SelectedRect.Height * 0x10) + 1;
                    this.SelectedRect.Width = this.curorP.X - this.SelectedRect.X;
                    this.SelectedRect.Height = this.curorP.Y - this.SelectedRect.Y;
                    this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
                }
            }
            else if (((((e.X + this.hScrollBar1.Value) - (this.curorP.X * 0x10)) < 0) || (((e.X + this.hScrollBar1.Value) - (this.curorP.X * 0x10)) >= 0x10)) || ((((e.Y + this.vScrollBar1.Value) - (this.curorP.Y * 0x10)) < 0) || (((e.Y + this.vScrollBar1.Value) - (this.curorP.Y * 0x10)) >= 0x10)))
            {
                if (this.f.currentitemdata != null)
                {
                    if (((e.X + this.hScrollBar1.Value) >= (this.map.MapWidth * 0x10)) || ((e.Y + this.vScrollBar1.Value) >= (this.map.MapHeight * 0x10)))
                    {
                        this.panelPaintRect = Rectangle.Empty;
                    }
                    else
                    {
                        this.panelPaintRect.X = (this.curorP.X * 0x10) - this.hScrollBar1.Value;
                        this.panelPaintRect.Y = (this.curorP.Y * 0x10) - this.vScrollBar1.Value;
                        this.panelPaintRect.Width = this.f.currentitemdata.width * 0x10;
                        this.panelPaintRect.Height = this.f.currentitemdata.height * 0x10;
                        this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
                    }
                }
                this.curorP.X = (e.X + this.hScrollBar1.Value) / 0x10;
                this.curorP.Y = (e.Y + this.vScrollBar1.Value) / 0x10;
            }
            if (((e.Button == MouseButtons.Left) && (this.f.currentitemdata != null)) && !this.currentDrawRect.Contains(this.curorP))
            {
                Rectangle rectangle = new Rectangle(this.curorP, new Size(this.f.currentitemdata.width, this.f.currentitemdata.height));
                this.modifymappart(rectangle.X, rectangle.Y, this.f.currentitemdata);
                if (this.f.currentbigitemid >= 0)
                {
                    this.modifybitmappart(rectangle.X, rectangle.Y, this.f.clipmapitems[this.f.currentbigitemid].ItemImage);
                }
                else
                {
                    this.modifybitmappart(rectangle.X, rectangle.Y, this.map.map_item_image[this.f.currentitemdata.mapid]);
                }
                if (this.f.currentitemdata != null)
                {
                    this.panelPaintRect.X = (this.curorP.X * 0x10) - this.hScrollBar1.Value;
                    this.panelPaintRect.Y = (this.curorP.Y * 0x10) - this.vScrollBar1.Value;
                    this.panelPaintRect.Width = this.f.currentitemdata.width * 0x10;
                    this.panelPaintRect.Height = this.f.currentitemdata.height * 0x10;
                    this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
                }
                this.currentDrawRect = rectangle;
            }
            if ((this.curorP.X < this.map.MapWidth) && (this.curorP.Y < this.map.MapHeight))
            {
                this.textBox1.Text = string.Format("X:{0},Y:{1};itemid:{2}", this.curorP.X.ToString("X"), this.curorP.Y.ToString("X"), this.map.maptitles[(this.curorP.Y * this.map.MapWidth) + this.curorP.X].ToString("X2"));
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = this.panel1.CreateGraphics();
            Rectangle panelPaintRect = this.panelPaintRect;
            panelPaintRect.Offset(this.hScrollBar1.Value, this.vScrollBar1.Value);
            Bitmap cityBmp = this.map.cityBmp;
            graphics.DrawImage(cityBmp, this.panelPaintRect, panelPaintRect, GraphicsUnit.Pixel);
            if (this.showhideItem && (this.map.hideItem.Count >= 0))
            {
                panelPaintRect = new Rectangle(0, 0, 0x10, 0x10);
                panelPaintRect.Offset(-this.hScrollBar1.Value, -this.vScrollBar1.Value);
                Rectangle rectangle2 = panelPaintRect;
                for (int i = 0; i < this.map.hideItem.Count; i++)
                {
                    panelPaintRect = rectangle2;
                    panelPaintRect.Offset(this.map.hideItem[i].x * 0x10, this.map.hideItem[i].y * 0x10);
                    graphics.DrawImage(this.map.map_item_image[1], panelPaintRect, new Rectangle(0, 0, 0x10, 0x10), GraphicsUnit.Pixel);
                }
            }
            if (this.showdynmic)
            {
                int num3;
                panelPaintRect = new Rectangle(0, 0, 0x10, 0x10);
                panelPaintRect.Offset(-this.hScrollBar1.Value, -this.vScrollBar1.Value);
                Rectangle rectangle3 = panelPaintRect;
                for (int j = 0; j < this.map.dynamicdata.Length; j += (num3 * 3) + 2)
                {
                    num3 = this.map.dynamicdata[j + 1];
                    for (int k = 0; k < num3; k++)
                    {
                        panelPaintRect = rectangle3;
                        panelPaintRect.Offset(this.map.dynamicdata[((k * 3) + j) + 2] * 0x10, this.map.dynamicdata[((k * 3) + j) + 3] * 0x10);
                        graphics.DrawImage(this.map.map_item_image[this.map.dynamicdata[((k * 3) + j) + 4]], panelPaintRect, new Rectangle(0, 0, 0x10, 0x10), GraphicsUnit.Pixel);
                    }
                }
            }
            if (this.displayAreaLine && (this.map.enters.Count > 0))
            {
                panelPaintRect = new Rectangle(0, 0, 0x10, 0x10);
                panelPaintRect.Offset(-this.hScrollBar1.Value, -this.vScrollBar1.Value);
                Rectangle rectangle4 = panelPaintRect;
                for (int m = 0; m < this.map.enters.Count; m++)
                {
                    panelPaintRect = rectangle4;
                    panelPaintRect.Offset(this.map.enters[m].X * 0x10, this.map.enters[m].Y * 0x10);
                    graphics.DrawRectangle(new Pen(Color.Red, 1f), panelPaintRect);
                }
            }
            if (this.map.specP.Count > 0)
            {
                panelPaintRect = new Rectangle(0, 0, 0x10, 0x10);
                panelPaintRect.Offset(-this.hScrollBar1.Value, -this.vScrollBar1.Value);
                Rectangle rectangle5 = panelPaintRect;
                for (int n = 0; n < this.map.specP.Count; n++)
                {
                    panelPaintRect = rectangle5;
                    panelPaintRect.Offset(this.map.specP[n].X * 0x10, this.map.specP[n].Y * 0x10);
                    graphics.DrawRectangle(new Pen(Color.Blue, 1f), panelPaintRect);
                }
            }
            graphics.DrawRectangle(new Pen(Color.Red, 1f), new Rectangle((0x10 * this.SelectedRect.X) - this.hScrollBar1.Value, (0x10 * this.SelectedRect.Y) - this.vScrollBar1.Value, this.SelectedRect.Width * 0x10, this.SelectedRect.Height * 0x10));
            if (this.f.currentitemdata != null)
            {
                graphics.DrawImage(this.f.currentitemdata.ItemImage, new Rectangle(new Point((this.curorP.X * 0x10) - this.hScrollBar1.Value, (this.curorP.Y * 0x10) - this.vScrollBar1.Value), new Size(this.f.currentitemdata.width * 0x10, this.f.currentitemdata.height * 0x10)), new Rectangle(0, 0, this.f.currentitemdata.width * 0x10, this.f.currentitemdata.height * 0x10), GraphicsUnit.Pixel);
            }
            panelPaintRect = new Rectangle(-this.hScrollBar1.Value, -this.vScrollBar1.Value, (this.map.rightline - this.map.leftline) * 0x10, (this.map.bottomline - this.map.topline) * 0x10);
            panelPaintRect.Offset(this.map.leftline * 0x10, this.map.topline * 0x10);
            graphics.DrawRectangle(new Pen(Color.Black, 1f), panelPaintRect);
            panelPaintRect = e.ClipRectangle;
            panelPaintRect.Intersect(new Rectangle(0, 0, this.map.MapWidth * 0x10, this.map.MapHeight * 0x10));
            this.panelPaintRect = panelPaintRect;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked)
            {
                this.panel2.Visible = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton3.Checked)
            {
                this.panel2.Visible = true;
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.panelPaintRect = this.panel1.ClientRectangle;
            this.panel1.Invalidate(new Rectangle(0, 0, 1, 1));
        }
    }




}
