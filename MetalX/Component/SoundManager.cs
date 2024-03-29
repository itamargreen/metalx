﻿using System;
using System.Collections.Generic;
using System.Threading;

using Microsoft.DirectX.DirectSound;
using Mp3Sharp;
using MetalX.File;

namespace MetalX.Component
{
    public class SoundManager : GameCom
    {
        SecondaryBuffer secondaryBuffer;
        WaveFormat waveFormat = new WaveFormat();
        BufferDescription bufferDescription;
        Mp3Stream mp3Stream;
        MetalXAudio mxa;
        Thread fillControlthd;
        int wholeSize;
        int halfSize
        {
            get
            {
                return wholeSize / 2;
            }
        }
        byte[] buff = new byte[20000];

        public bool Loop = false;
        public bool Playing = false;
        int volum = 0;

        public int Volume
        {
            set
            {                
                int v = value;
                if (v >= 100)
                {
                    volum = (int)Microsoft.DirectX.DirectSound.Volume.Max;
                }
                else if (v <= 0)
                {
                    volum = (int)Microsoft.DirectX.DirectSound.Volume.Min;
                }
                else
                {
                    volum = v * 50 - 5000;
                }
            }
        }

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
            set
            {
                if (mp3Stream == null)
                {
                    return;
                }
                mp3Stream.Position = (long)(value * (double)mp3Stream.Length);
            }
        }

        public SoundManager(Game g)
            : base(g)
        {
            DisableAll();

            //Enable = true;

            fillControlthd = new Thread(fillControl);
            fillControlthd.IsBackground = true;
            fillControlthd.Start();
        }

        bool pos, posb;
        //true: in fore | false: in back
        bool foreFilled, backFilled;

        public override void Code()
        {
            if (secondaryBuffer == null || secondaryBuffer.Disposed || mp3Stream == null)
            {

            }
            else
            {
                if (Playing)
                {
                    if (secondaryBuffer.PlayPosition < halfSize)
                    {
                        pos = true;
                    }
                    else
                    {
                        pos = false;
                    }
                    if (pos != posb)
                    {
                        if (pos)
                        {
                            backFilled = false;
                        }
                        else
                        {
                            foreFilled = false;
                        }
                    }
                    posb = pos;
                    if (mp3Stream.Position < mp3Stream.Length)
                    {
                        if (pos && !backFilled)
                        {
                            fillBack();
                            backFilled = true;
                        }
                        else if (!pos && !foreFilled)
                        {
                            fillFore();
                            foreFilled = true;
                        }
                    }
                    else
                    {
                        //mp3Stream.Position = 0;
                        secondaryBuffer.Stop();
                        if (Loop)
                        {
                            playMP3();
                        }
                        else
                        {
                            
                            Playing = false;
                        }
                    }
                }
            }
        }

        void Load(System.IO.Stream stream)
        {
            //if (secondaryBuffer == null)
            //{
            //    return;
            //}
            //if (secondaryBuffer.Disposed)
            //{
            //    return;
            //}
            if (Playing)
            {
                secondaryBuffer.Stop();
                Playing = false;
            } 
            mp3Stream = new Mp3Stream(stream);

            mp3Stream.Read(buff, 0, 512);
            mp3Stream.Position = 0;

            waveFormat.BitsPerSample = 16;
            waveFormat.Channels = mp3Stream.ChannelCount;
            waveFormat.SamplesPerSecond = mp3Stream.Frequency;
            waveFormat.FormatTag = WaveFormatTag.Pcm;
            waveFormat.BlockAlign = (short)(waveFormat.Channels * (waveFormat.BitsPerSample / 8));
            waveFormat.AverageBytesPerSecond = waveFormat.SamplesPerSecond * waveFormat.BlockAlign;

            wholeSize = (int)(waveFormat.AverageBytesPerSecond * TimeSpan.FromSeconds(0.2).TotalSeconds);

            bufferDescription = new BufferDescription(waveFormat);
            bufferDescription.BufferBytes = wholeSize;
            bufferDescription.GlobalFocus = true;
            bufferDescription.ControlVolume = true;

            secondaryBuffer = new SecondaryBuffer(bufferDescription, game.Devices.DSoundDev);
            secondaryBuffer.Volume = volum;

            #region useless code area
            //autoResetEvent = new System.Threading.AutoResetEvent(false);

            //notify = new Notify(secondaryBuffer);

            //System.Reflection.MethodInfo methodInfo;
            //methodInfo = typeof(SoundManager).GetMethod("fillBack");

            //bufferPositionNotify = new BufferPositionNotify[2];
            //bufferPositionNotify[0] = new BufferPositionNotify();
            //bufferPositionNotify[0].Offset = 0;
            //bufferPositionNotify[0].EventNotifyHandle = this.GetType().GetMethod("fillBack", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).MethodHandle.Value;
            //bufferPositionNotify[1] = new BufferPositionNotify();
            //bufferPositionNotify[1].Offset = halfSize;
            //bufferPositionNotify[1].EventNotifyHandle = this.GetType().GetMethod("fillFore", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).MethodHandle.Value;

            //bufferPositionNotify = new BufferPositionNotify[2];
            //bufferPositionNotify[0] = new BufferPositionNotify();
            //bufferPositionNotify[0].Offset = 0;
            //bufferPositionNotify[0].EventNotifyHandle = autoResetEvent.Handle;
            //bufferPositionNotify[1] = new BufferPositionNotify();
            //bufferPositionNotify[1].Offset = halfSize;
            //bufferPositionNotify[1].EventNotifyHandle = autoResetEvent.Handle;

            //notify.SetNotificationPositions(bufferPositionNotify);
            #endregion
        }

        void fillControl()
        {
            while (true)
            {
                if (secondaryBuffer == null || secondaryBuffer.Disposed || mp3Stream == null)
                {
                }
                else
                {
                    if (Playing)
                    {

                        if (secondaryBuffer.PlayPosition < halfSize)
                        {
                            pos = true;
                        }
                        else
                        {
                            pos = false;
                        }
                        if (pos != posb)
                        {
                            if (pos)
                            {
                                backFilled = false;
                            }
                            else
                            {
                                foreFilled = false;
                            }
                        }
                        posb = pos;
                        if (mp3Stream.Position < mp3Stream.Length)
                        {
                            if (pos && !backFilled)
                            {
                                fillBack();
                                backFilled = true;
                            }
                            else if (!pos && !foreFilled)
                            {
                                fillFore();
                                foreFilled = true;
                            }
                        }
                        else
                        {
                            //mp3Stream.Position = 0;
                            secondaryBuffer.Stop();
                            if (Loop)
                            {
                                playMP3();
                            }
                            else
                            {                                
                                
                                Playing = false;
                                //for (int i = 0; i < buff.Length; i++)
                                //{
                                //    buff[i] = 0;
                                //}
                            }
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }
        void fillFore()
        {
            if (mp3Stream == null)
            {
                return;
            } 
            if (secondaryBuffer == null || secondaryBuffer.Disposed || mp3Stream == null)
            {
                return;
            }
            try
            {
                mp3Stream.Read(buff, 0, halfSize);
                secondaryBuffer.Write(0, new System.IO.MemoryStream(buff), halfSize, LockFlag.None);
            }
            catch { }
        }
        void fillBack()
        {
            if (mp3Stream == null)
            {
                return;
            }
            if (secondaryBuffer == null || secondaryBuffer.Disposed || mp3Stream == null)
            {
                return;
            }
            try
            {
                mp3Stream.Read(buff, 0, halfSize);
                secondaryBuffer.Write(halfSize, new System.IO.MemoryStream(buff), halfSize, LockFlag.None);
            }
            catch { }
        }
        //public void PlayMetalXAudio(string musicName) { PlayMetalXAudio(game.Audios.GetIndex(musicName)); }
        //public void PlayMetalXAudio(int i) { PlayMetalXAudio(new System.IO.MemoryStream(game.Audios[i].AudioData)); }
        public void PlayMP3(string fileName)
        {
            if (fileName == null)
            {
                return;
            }
            if (fileName == string.Empty)
            {
                return;
            }
            if (playingFileName == fileName && mp3Stream != null)
            {
                playMP3();
            }
            else
            {
                playingFileName = fileName;
                playMP3(new System.IO.MemoryStream(System.IO.File.ReadAllBytes(fileName)));
            }
        }
        string playingFileName = "";
        public void PlayMetalXAudio(string fileName)
        {
            if (playingFileName == fileName)
            {
                playMP3();
            }
            else
            {
                playingFileName = fileName;
                mxa = (MetalXAudio)Util.LoadObject(fileName);
                playMP3(new System.IO.MemoryStream(mxa.AudioData));
            }
        }
        void playMP3(System.IO.Stream stream)
        {
            Load(stream);
            playMP3();
        }
        void playMP3()
        {
            //if (mp3Stream == null)
            //{
            //    return;
            //}
            mp3Stream.Position = 0;
            try
            {
                mp3Stream.Read(buff, 0, halfSize);
            }
            catch { }
            pos = posb = true;
            foreFilled = true;
            backFilled = false;
            secondaryBuffer.Volume = volum;
            secondaryBuffer.Write(0, new System.IO.MemoryStream(buff), halfSize, LockFlag.None);
            secondaryBuffer.Play(0, BufferPlayFlags.Looping);
            Playing = true;
        }
        public void Stop()
        {
            if (mp3Stream == null)
            {
                return;
            }
            if (secondaryBuffer == null)
            {
                return;
            }
            Playing = false;
            bufferDescription.Dispose();
            secondaryBuffer.Dispose();
            mp3Stream.Dispose();
            mp3Stream = null;
        }
        public void Pause()
        {
            //secondaryBuffer.
            Enable = false;
        }
        public void GoOn()
        {
            Enable = true;
        }
    }
}
