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
public class MapItemData
{
    // Fields
    public int height;
    public Bitmap ItemImage;
    public int mapid;
    public string name;
    public int[,] subItems;
    public int width;

    // Methods
    public MapItemData(int widt, int heigh, int mapid)
    {
        this.width = widt;
        this.height = heigh;
        this.subItems = new int[widt, heigh];
        this.mapid = mapid;
        this.name = "";
        this.ItemImage = new Bitmap(this.width * 0x10, this.height * 0x10);
    }
}

 
 

}
