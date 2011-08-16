using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.DirectX;
using MetalX.File;
namespace MetalX.Component
{
    public class MovieManager : GameCom
    {
        List<MetalXMovie> movies = new List<MetalXMovie>();
        //List<MetalXMovie> bmovies = new List<MetalXMovie>();
        public MovieManager(Game g)
            : base(g)
        {
            //for (int i = 0; i < 16; i++)
            //{
            //    bmovies.Add(new MetalXMovie());
            //}
        }

        public override void Code()
        {
            for (int i = 0; i < movies.Count; i++)
            {
                if (movies[i].NextFrame() == false)
                {
                    movies.RemoveAt(i);
                    i--;
                }
            }
            //foreach (MetalXMovie m in bmovies)
            //{
            //    m.NextFrame();
            //}
        }
        public override void Draw()
        {
            foreach (MetalXMovie m in movies)
            {
                Vector3 loc = m.DrawLocation;
                Color color = Util.MixColor(ColorFilter, m.ColorFilter);
                game.DrawMetalXTexture(m.MXT, m.DrawZone, loc, m.TileSize2X, 0, color);
            }
            //foreach (MetalXMovie m in bmovies)
            //{
            //    Vector3 loc = m.DrawLocation;
            //    Color color = Util.MixColor(ColorFilter, m.ColorFilter);
            //    game.DrawMetalXTexture(m.MXT, m.DrawZone, loc, m.TileSize2X, 0, color);
            //}
        }

        //public void BattlePlayMovie(int i, MetalXMovie movie, Vector3 fromLoc, Vector3 toLoc, double timespan)
        //{
        //    i += 12;
        //    movie.BeginLocation = fromLoc;
        //    movie.EndLocation = toLoc;
        //    movie.PlayTime = timespan;

        //    movie.Reset();

        //    bmovies[i] = movie;
        //}
        //public void BattlePlayMovie(int i, MetalXMovie movie, Vector3 fromLoc, double timespan)
        //{
        //    BattlePlayMovie(i, movie, fromLoc, fromLoc, timespan);
        //}
        public void PlayMovie(MetalXMovie movie, Vector3 fromLoc, Vector3 toLoc, double timespan)
        {
            fromLoc.X -= movie.TileSize2X.Width / 2;
            fromLoc.Y -= movie.TileSize2X.Height;
            toLoc.X -= movie.TileSize2X.Width / 2;
            toLoc.Y -= movie.TileSize2X.Height;

            movie.BeginLocation = fromLoc;
            movie.EndLocation = toLoc;
            movie.PlayTime = timespan;

            movie.Reset();

            movies.Add(movie);
        }
        public void PlayMovie(MetalXMovie movie, Vector3 fromLoc)
        {
            PlayMovie(movie, fromLoc, fromLoc, 1);
        }
        public void PlayMovie(string name, Vector3 loc)
        {
            int i = GetIndex(name);
            if (i > -1)
            {
                movies[i].BeginLocation = loc;
                movies[i].EndLocation = loc;
                movies[i].Reset();
            }
        }
        public int GetIndex(string name)
        {
            int i = -1;
            for (int j = 0; j < movies.Count; j++)
            {
                if (movies[j].Name == name)
                {
                    i = j;
                    break;
                }
            }
            return i;
        }
        public void RemoveMovie(string name)
        {
            if (movies.Count == 0)
            {
                return;
            }
            int i = 0;
            foreach (MetalXMovie m in movies)
            {
                if (m.Name != null)
                {
                    if (m.Name == name)
                    {
                        break;
                    }
                }
                i++;
            }
            movies.RemoveAt(i);
        }
    }
}
