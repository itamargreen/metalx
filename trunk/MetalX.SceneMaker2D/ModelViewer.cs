﻿using System;
using System.Collections.Generic;
using System.Drawing;
using MetalX;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
namespace MetalX.SceneMaker2D
{
    class ModelViewer : MetalXGameCom
    {
        public Vector3 xyz;
        public MetalXModel model;
        public MetalXTexture t;
        public ModelViewer(MetalXGame g)
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
                metalXGame.DrawMetalXModel(model, new Vector3(), xyz, new Vector3());

            metalXGame.DrawText("FPS:" + metalXGame.AverageFPS.ToString("f1"), new Point(), Color.White);
            //metalXGame.DrawText("Camera Z:"+metalXGame.Devices.D3DDev.Transform.View.
            //if(t!=null)
                //metalXGame.DrawMetalXTexture(t, new Vector3(),, Color.White);
        }
    }
}