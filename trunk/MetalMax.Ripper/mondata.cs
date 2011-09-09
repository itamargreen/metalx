using System;
using System.Collections.Generic;
using System.Text;

namespace MetalMax.Ripper
{
    public class mondata
    {
        // Fields
        public byte[] data_store;

        // Methods
        public mondata(byte[] data)
        {
            this.data_store = data;
        }

        public byte city_monareadata(int cityno)
        {
            if ((cityno >= 0x80) && (cityno <= 0xef))
            {
                return this.data_store[cityno - 0x80];
            }
            return 0;
        }

        public void setcity_monareadata(int cityno, byte value)
        {
            if ((cityno >= 0x80) && (cityno <= 0xef))
            {
                this.data_store[cityno - 0x80] = value;
            }
        }
    }

 

}
