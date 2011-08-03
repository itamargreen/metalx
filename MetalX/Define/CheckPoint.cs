using System;
using System.Collections.Generic;
using System.Text;

namespace MetalX.Define
{
    [Serializable]
    public class CheckPoint
    {
        public string SceneName;
        public DateTime SaveTime = DateTime.Now;
        public PC ME;
        public Microsoft.DirectX.Vector3 SceneRealLocationPixel;
    }
}
