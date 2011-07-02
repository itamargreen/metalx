using System;
using System.Collections.Generic;
using System.Text;

namespace MetalX.Component
{
    public class ScriptManager : GameCom
    {
        public ScriptManager(Game g)
            : base(g)
        {
        }

        DateTime delayStartTime = DateTime.Now;
        TimeSpan delayEclipseTimeSpan
        {
            get
            {
                return DateTime.Now - delayStartTime;
            }
        }
        public override void Code()
        {
            if (delayLeftTime > 0)
            {
                delayLeftTime = delayTime - delayEclipseTimeSpan.TotalMilliseconds;
            }
            else
            {
                game.DrawText("testing", new System.Drawing.Point(), ColorFilter);
            }
        }
        double delayTime = 0;
        double delayLeftTime = 0;
        public void Execute(string[] cmds)
        {
            foreach (string cmd in cmds)
            {
                Execute(cmd);
            }
        }
        public void Execute(string cmd)
        {
            cmd = cmd.ToLower();
            string[] keywords = cmd.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (keywords.Length > 1)
            {
                if (keywords[0] == "delay")
                {
                    double ms = double.Parse(keywords[1]);
                    delayLeftTime = delayTime = ms;
                }
                //else if (keywords[0] == "size")
                //{
                //    for (int i = 1; i < keywords.Length; i++)
                //    {
                //        string[] parm = keywords[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                //    }
                //}
            }
        }
    }
}
