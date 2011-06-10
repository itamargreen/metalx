using System;
using System.Collections.ObjectModel;
using System.Text;

namespace MetalX
{
    [Serializable]
    public class MetalXAudio : IDisposable
    {
        //public int Index;
        public string Name;
        public string Path;
        public DateTime CreateTime;
        public string Version;

        public byte[] AudioData;

        public MetalXAudio()
        {
            //Index = -1;
            //Version = Path = Name = null;
            CreateTime = DateTime.Now;
        }
        public void Dispose()
        {
        }
    }

}
