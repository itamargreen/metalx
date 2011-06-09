using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.DirectX.DirectSound;
using Mp3Sharp;

namespace MetalX.Component
{
    public class SoundManager : GameCom
    {
        SecondaryBuffer secondaryBuffer;
        WaveFormat waveFormat;
        BufferDescription bufferDescription;
        Mp3Stream mp3Stream;
        int wholeSize;
        int halfSize
        {
            get
            {
                return wholeSize / 2;
            }
        }
        byte[] buff = new byte[512];

        public bool Loop = false;
        public bool Playing = false;

        System.Threading.AutoResetEvent autoResetEvent;
        Notify notify;
        BufferPositionNotify[] bufferPositionNotify;

        public double Progress
        {
            get
            {
                if (mp3Stream == null)
                {
                    return 0;
                }
                else
                {
                    return (double)(mp3Stream.Position) / (double)(mp3Stream.Length);
                }
            }
        }

        public SoundManager(Game g)
            : base(g)
        { }

        public override void Code()
        {
            if (Playing)
            {
                //autoResetEvent.Reset();
                //autoResetEvent.WaitOne();

                if (mp3Stream.Position < mp3Stream.Length)
                {
                    //mp3Stream.Read(buff, 0, halfSize);
                    //if (secondaryBuffer.PlayPosition > halfSize)
                    //{
                    //    secondaryBuffer.Write(0, new System.IO.MemoryStream(buff), halfSize, LockFlag.None);
                    //}
                    //else
                    //{
                    //    secondaryBuffer.Write(halfSize, new System.IO.MemoryStream(buff), halfSize, LockFlag.None);
                    //}
                }
                else
                {
                    if (Loop)
                    {
                        mp3Stream.Position = 0;
                    }
                    else
                    {
                        secondaryBuffer.Stop();
                        Playing = false;
                    }
                }
            }
        }

        public void Load(int i)
        {
            TimeSpan ts = TimeSpan.FromSeconds(0.2);

            mp3Stream = new Mp3Stream(new System.IO.MemoryStream(game.Audios[i].AudioData));
            mp3Stream.Read(buff, 0, 512);
            mp3Stream.Position = 0;

            waveFormat = new WaveFormat();
            waveFormat.BitsPerSample = 16;
            waveFormat.Channels = mp3Stream.ChannelCount;
            waveFormat.SamplesPerSecond = mp3Stream.Frequency;
            waveFormat.FormatTag = WaveFormatTag.Pcm;
            waveFormat.BlockAlign = (short)(waveFormat.Channels * (waveFormat.BitsPerSample / 8));
            waveFormat.AverageBytesPerSecond = waveFormat.SamplesPerSecond * waveFormat.BlockAlign;

            bufferDescription = new BufferDescription(waveFormat);
            bufferDescription.BufferBytes = wholeSize = (int)(waveFormat.AverageBytesPerSecond * ts.TotalSeconds);
            bufferDescription.ControlPositionNotify = true;

            secondaryBuffer = new SecondaryBuffer(bufferDescription, game.Devices.DSoundDev);

            #region useless code area
            notify = new Notify(secondaryBuffer);

            //System.Reflection.MethodInfo methodInfo;

            bufferPositionNotify = new BufferPositionNotify[2];
            bufferPositionNotify[0] = new BufferPositionNotify();
            bufferPositionNotify[0].Offset = 0;
            bufferPositionNotify[0].EventNotifyHandle = this.GetType().GetMethod("fillBack").MethodHandle.Value;
            bufferPositionNotify[1] = new BufferPositionNotify();
            bufferPositionNotify[1].Offset = halfSize;
            bufferPositionNotify[1].EventNotifyHandle = this.GetType().GetMethod("fillFore").MethodHandle.Value;

            notify.SetNotificationPositions(bufferPositionNotify);
            autoResetEvent = new System.Threading.AutoResetEvent(false);
            #endregion
        }
        bool playFinish()
        {
            if (mp3Stream.Position < mp3Stream.Length)
            {
                return false;
            }
            else
            {
                if (Loop)
                {
                    mp3Stream.Position = 0;
                }
                else
                {
                    secondaryBuffer.Stop();
                    Playing = false;
                }
                return true;
            }
        }
        void fillFore()
        {
            mp3Stream.Read(buff, 0, halfSize);
            secondaryBuffer.Write(0, new System.IO.MemoryStream(buff), halfSize, LockFlag.None);
        }
        void fillBack()
        {
            mp3Stream.Read(buff, 0, halfSize);
            secondaryBuffer.Write(halfSize, new System.IO.MemoryStream(buff), halfSize, LockFlag.None);
        }
        public void Play()
        {
            if (mp3Stream == null)
            {
                return;
            } 

            mp3Stream.Read(buff, 0, halfSize);
            secondaryBuffer.Write(0, new System.IO.MemoryStream(buff), halfSize, LockFlag.None);

            secondaryBuffer.Play(0, BufferPlayFlags.Looping);
            
            Playing = true;
        }
        public void Stop()
        {
            if (secondaryBuffer == null)
            {
                return;
            }

            secondaryBuffer.Stop();
            Playing = false;

            mp3Stream.Dispose();
        }
    }
}
