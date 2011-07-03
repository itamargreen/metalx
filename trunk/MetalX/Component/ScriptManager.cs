using System;
using System.Collections.Generic;
using System.Text;

namespace MetalX.Component
{
    public class ScriptManager : GameCom
    {
        Queue<string> commands = new Queue<string>();
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
                if (commands.Count > 0)
                {
                    execute(commands.Dequeue());
                }
            }
        }
        public override void Draw()
        {
            game.DrawText("fps: " + game.FPS + "\ndelay left time: " + delayLeftTime, new System.Drawing.Point(), ColorFilter);
        }
        double delayTime = 0;
        double delayLeftTime = 0;

        void execute(string cmd)
        {
            //cmd = cmd.ToLower();
            string[] kw = cmd.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (kw.Length == 1)
            {
                if (kw[0] == "exit")
                {
                    game.Exit();
                }
            }
            else if (kw.Length == 2)
            {
                if (kw[0] == "delay")
                {
                    double ms = double.Parse(kw[1]);
                    delayLeftTime = delayTime = ms;
                    delayStartTime = DateTime.Now;
                }
                else if (kw[0] == "appear")
                {
                    game.FormBoxManager.Appear(kw[1]);
                }
                else if (kw[0] == "mp3")
                {
                    game.PlayMP3(kw[1]);
                }
            }
            else if (kw.Length == 3)
            {
                if (kw[0] == "shock")
                {
                    double ms = double.Parse(kw[2]);
                    if (kw[1] == "scene")
                    {
                        game.SceneManager.ShockScreen(ms);
                    }
                    else if (kw[1] == "ui")
                    {
                        game.FormBoxManager.ShockScreen(ms);
                    }
                }
                else if (kw[0] == "fallout")
                {
                    double ms = double.Parse(kw[2]);
                    if (kw[1] == "scene")
                    {
                        game.SceneManager.FallOutSceen(ms);
                    }
                    else if (kw[1] == "ui")
                    {
                        game.FormBoxManager.FallOutSceen(ms);
                    }
                }
                else if (kw[0] == "fallin")
                {
                    double ms = double.Parse(kw[2]);
                    if (kw[1] == "scene")
                    {
                        game.SceneManager.FallInSceen(ms);
                    }
                    else if (kw[1] == "ui")
                    {
                        game.FormBoxManager.FallInSceen(ms);
                    }
                }

            }
            else if (kw.Length == 4)
            {
                if (kw[0] == "shock")
                {
                    double ms = double.Parse(kw[2]);
                    int range = int.Parse(kw[3]);
                    if (kw[1] == "scene")
                    {
                        game.SceneManager.ShockScreen(ms, range);
                    }
                    else if (kw[1] == "ui")
                    {
                        game.FormBoxManager.ShockScreen(ms, range);
                    }
                }
            }
        }
        public void Execute(string cmd)
        {
            string[] cmds = cmd.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string c in cmds)
            {
                commands.Enqueue(c);
            }
        }
        public void ExecuteDotMXScript(string fileName)
        {
            Execute(System.IO.File.ReadAllText(fileName + ".mxscript"));
        }
    }
}
