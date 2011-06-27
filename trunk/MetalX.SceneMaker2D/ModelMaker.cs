using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using MetalX;
using MetalX.Component;
namespace MetalX.SceneMaker2D
{
    class ModelMaker : GameCom
    {
        public Vector3 xyz;
        public MetalXModel model;
        //public MetalXTexture t;
        public ModelMaker(Game g)
            : base(g)
        {
        }
        public override void Code()
        {
            base.Code();
        }
        public override void Draw()
        {
            base.Draw();
            if(model!=null)
                game.DrawMetalXModel(model, new Vector3(), xyz, new Vector3());

            game.DrawText("FPS:" + game.AverageFPS.ToString("f1"), new Point(), Color.White);
            //game.DrawText("Camera Z:"+game.Devices.D3DDev.Transform.View.
            //if(t!=null)
                //game.DrawMetalXTexture(t, new Vector3(),, Color.White);
        }
    }
}
