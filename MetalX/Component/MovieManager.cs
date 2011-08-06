using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.DirectX;
using MetalX.File;
namespace MetalX.Component
{
    public class MovieManager : GameCom
    {
        MetalXMovie moviefile;

        public MovieManager(Game g)
            : base(g)
        { }

        public override void Code()
        {
            if (moviefile == null)
            {
                return;
            }

            moviefile.NextFrame();
        }
        public override void Draw()
        {
            if (moviefile == null)
            {
                return;
            }
            Vector3 loc = moviefile.DrawLocation;
            Color color = Util.MixColor(ColorFilter, moviefile.ColorFilter);
            game.DrawMetalXTexture(moviefile.MXT, moviefile.DrawZone, loc, moviefile.TileSize, 0, color);
        }

        public void PlayMovie(MetalXMovie movie, Vector3 fromLoc, Vector3 toLoc, double timespan)
        {
            moviefile = movie;

            moviefile.BeginLocation = fromLoc;
            moviefile.EndLocation = toLoc;
            moviefile.PlayTime = timespan;

            moviefile.Reset();
        }

        public void PlayMovie(MetalXMovie movie, Vector3 Loc)
        {
            moviefile = movie;

            moviefile.BeginLocation = Loc;
            moviefile.EndLocation = Loc;
            moviefile.PlayTime = 0;

            moviefile.Reset();
        }
    }
}
