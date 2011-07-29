using System;
using System.Collections.Generic;
using System.Text;

namespace MetalX.File
{
    [Serializable]
    public class MetalXAudio : IDisposable
    {
        public string Name;
        public string Path;
        public DateTime CreateTime;
        public string Version;

        public byte[] AudioData;

        public MetalXAudio()
        {
            CreateTime = DateTime.Now;
        }
        public void Dispose()
        {
        }
    }
}
