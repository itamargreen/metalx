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
public class Form2 : Form
{
    // Fields
    private IContainer components;
    private DataGridView dataGridView1;

    // Methods
    public Form2()
    {
        this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void Form2_Load(object sender, EventArgs e)
    {
        Form1 owner = (Form1) base.Owner;
        DataTable table = new DataTable();
        table.Columns.Add("代码", Type.GetType("System.String"));
        table.Columns.Add("一级指针", Type.GetType("System.String"));
        table.Columns.Add("Z49", Type.GetType("System.String"));
        table.Columns.Add("宽", Type.GetType("System.String"));
        table.Columns.Add("长", Type.GetType("System.String"));
        table.Columns.Add("边界（左上右下）", Type.GetType("System.String"));
        table.Columns.Add("地形数据地址", Type.GetType("System.String"));
        table.Columns.Add("地形数据长度", Type.GetType("System.String"));
        table.Columns.Add("小块数据1", Type.GetType("System.String"));
        table.Columns.Add("小块数据2", Type.GetType("System.String"));
        table.Columns.Add("模式表块号1", Type.GetType("System.String"));
        table.Columns.Add("2", Type.GetType("System.String"));
        table.Columns.Add("3", Type.GetType("System.String"));
        table.Columns.Add("4", Type.GetType("System.String"));
        table.Columns.Add("npc模式表块号", Type.GetType("System.String"));
        table.Columns.Add("调色板数据", Type.GetType("System.String"));
        table.Columns.Add("入口数据", Type.GetType("System.String"));
        table.Columns.Add("npc地址", Type.GetType("System.String"));
        table.Columns.Add("npc数量", Type.GetType("System.String"));
        table.Columns.Add("未知数据(a2,a3)", Type.GetType("System.String"));
        table.Columns.Add("动态图块数据", Type.GetType("System.String"));
        for (int i = 0; i < owner.allmapdata.Count; i++)
        {
            singleMap map = owner.allmapdata[i];
            string[] values = new string[] { 
                (i + 1).ToString("X2"), map.map_addr.ToString("X4"), map.map_store_data[0].ToString("X2"), map.map_store_data[1].ToString("X2"), map.map_store_data[2].ToString("X2"), map.map_store_data[3].ToString("X2") + "," + map.map_store_data[4].ToString("X2") + "," + map.map_store_data[5].ToString("X2") + "," + map.map_store_data[6].ToString("X2"), map.mapTitleAddr.ToString("X4"), map.maptitlestore_len.ToString("X4"), map.map_store_data[9].ToString("X2"), map.map_store_data[10].ToString("X2"), map.PattenTable[0].ToString("X2"), map.PattenTable[1].ToString("X2"), map.PattenTable[2].ToString("X2"), map.PattenTable[3].ToString("X2"), map.PattenTable1.ToString("X2"), map.palleteaddr.ToString("X4"), 
                map.enterAddr.ToString("X4"), map.npc_addr.ToString("X4"), map.npc_datas.Count.ToString(), ((map.map_store_data[0] & 1) == 1) ? (((map.map_store_data[0x19] * 0x100) + map.map_store_data[0x19])).ToString("X4") : "无", ((map.map_store_data[0] & 4) != 0) ? map.dynamicMapaddr.ToString("X4") : "无"
             };
            table.Rows.Add(values);
        }
        this.dataGridView1.DataSource = table;
    }

    private void InitializeComponent()
    {
        this.dataGridView1 = new DataGridView();
        ((ISupportInitialize) this.dataGridView1).BeginInit();
        base.SuspendLayout();
        this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Dock = DockStyle.Fill;
        this.dataGridView1.Location = new Point(0, 0);
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.RowTemplate.Height = 0x17;
        this.dataGridView1.Size = new Size(0x2c8, 0x196);
        this.dataGridView1.TabIndex = 0;
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.ClientSize = new Size(0x2c8, 0x196);
        base.Controls.Add(this.dataGridView1);
        base.Name = "Form2";
        this.Text = "Form2";
        base.Load += new EventHandler(this.Form2_Load);
        ((ISupportInitialize) this.dataGridView1).EndInit();
        base.ResumeLayout(false);
    }
}

 
 

}
