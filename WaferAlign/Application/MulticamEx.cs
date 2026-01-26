using Euresys.MultiCam;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;

namespace eMotion
{
    class MulticamEx
    {
        public enum eState {IDLE=0, ACTIVE, READY }
        public delegate void OnCallbackHandler();// object sender);
        public event OnCallbackHandler OnCallback;

        private const int BUFFER_COUNT = 10;
        private UInt32 currentSurface;
        private MC.CALLBACK multiCamCallback;
        private UInt32 channel;
        private bool isActived;
        private bool isOpened;
        private bool isGrabDone;
        private byte[] imageBuffer = null;
        Bitmap bitmap = null;

        private EventWaitHandle grabDone = new EventWaitHandle(false, EventResetMode.AutoReset);

        public bool Opened { get { return isOpened; } }
        public bool Actived { get { return isActived; } }
        public bool GrabDone { get { return isGrabDone; } }

        public MulticamEx()
        {
            OpenDriver();

            channel = 0;
            isActived = false;
            isOpened = false;
            isGrabDone = false;
        }

        ~MulticamEx()
        {
            DeleteChannel();

            CloseDriver();
        }

        private bool OpenDriver()
        {
            try
            {
                MC.OpenDriver();
                return true;
            }
            catch (Euresys.MultiCamException exc)
            {
                Debug.WriteLine(exc.Message);
                return false;
            }
        }
        private bool CloseDriver()
        {
            try
            {
                MC.CloseDriver();
                return true;
            }
            catch (Euresys.MultiCamException exc)
            {
                Debug.WriteLine(exc.Message);
                return false;
            }
        }
        public Bitmap GetImage()
        {
            if (bitmap == null) return null;
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            IntPtr ptrBmp = bmpData.Scan0;

            // 2. 비트맵의 메모리 영역 잠금
            //BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),ImageLockMode.WriteOnly, bmp.PixelFormat);

            // 3. byte[] 데이터를 비트맵 메모리로 복사
            Marshal.Copy(GetImagePointer(), 0, ptrBmp, bitmap.Width * bitmap.Height);

            // 4. 메모리 잠금 해제
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }


        private bool DeleteChannel()
        {
            try
            {
                if (channel != 0)
                {
                    MC.Delete(channel);
                    channel = 0;
                }

                return true;
            }
            catch (Euresys.MultiCamException exc)
            {
                Debug.WriteLine(exc.Message);
                return false;
            }
        }

        public void OpenBoard(uint driverIndex, string connector, string topology, string camfilePath)
        {
            try
            {
                MC.SetParam(MC.CONFIGURATION, "ErrorHandling", "MSGBOX");
                MC.SetParam(MC.CONFIGURATION, "ErrorLog", "error.log");

                MC.SetParam(MC.BOARD + driverIndex, "BoardTopology", topology);

                MC.Create("CHANNEL", out channel);
                MC.SetParam(channel, "DriverIndex", driverIndex);

                MC.SetParam(channel, "Connector", connector);

                MC.SetParam(channel, "CamFile", camfilePath);


                //Set the acquisition mode to Snapshot
                MC.SetParam(channel, "AcquisitionMode", "SNAPSHOT");
                // Choose the way the first acquisition is triggered
                MC.SetParam(channel, "TrigMode", "COMBINED");
                // Choose the triggering mode for subsequent acquisitions
                MC.SetParam(channel, "NextTrigMode", "COMBINED");

                // Configure triggering line
                // A rising edge on the triggering line generates a trigger.
                // See the TrigLine Parameter and the board documentation for more details.
                MC.SetParam(channel, "TrigLine", "NOM");
                MC.SetParam(channel, "TrigEdge", "GOHIGH");
                MC.SetParam(channel, "TrigFilter", "ON");
                MC.SetParam(channel, "TrigCtl", "ISO");

                Int32 size = 0;
                MC.GetParam(channel, "BufferSize", out size);
                imageBuffer = new byte[size];

                MC.SetParam(channel, "SurfaceCount", BUFFER_COUNT);

                multiCamCallback = new MC.CALLBACK(MultiCamCallback);
                MC.RegisterCallback(channel, multiCamCallback, channel);

                // Enable the signals corresponding to the callback functions
                MC.SetParam(channel, MC.SignalEnable + MC.SIG_START_ACQUISITION_SEQUENCE, "ON");
                MC.SetParam(channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "ON");
                MC.SetParam(channel, MC.SignalEnable + MC.SIG_END_CHANNEL_ACTIVITY, "ON");
                MC.SetParam(channel, MC.SignalEnable + MC.SIG_ACQUISITION_FAILURE, "ON");

                // Prepare the channel in order to minimize the acquisition sequence startup latency
                MC.SetParam(channel, "ChannelState", "READY");

                InitBitmat();
                isOpened = true;
            }
            catch (Euresys.MultiCamException exc)
            {
                throw exc;
            }
        }

        public void CloseBoard()
        {
            try
            {
                if (isActived == true)
                    SetAcquisition(eState.IDLE);

                DeleteChannel();

                isOpened = false;
            }
            catch (Euresys.MultiCamException exc)
            {
                throw exc;
            }
        }

        private void InitBitmat()
        {
            int width, height;
            GetWidth(out width);
            GetHeight(out height);

            // 1. 빈 비트맵 생성 (포맷은 데이터에 맞게 설정, 예: 32bpp RGB)
            bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            if (bitmap.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                ColorPalette colorPalette = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    colorPalette.Entries[i] = Color.FromArgb(i, i, i);
                }

                bitmap.Palette = colorPalette;
            }
        }

        public void SetAcquisition(eState index)
        {
            try
            {
                switch (index)
                {
                    case eState.IDLE:
                        MC.SetParam(channel, "ChannelState", "IDLE");
                        isActived = false;
                        break;
                    case eState.ACTIVE:
                        MC.SetParam(channel, "ChannelState", "ACTIVE");
                        isActived = true;
                        break;
                    case eState.READY:
                        MC.SetParam(channel, "ChannelState", "READY");
                        isActived = false;
                        break;
                }
            }
            catch (Euresys.MultiCamException exc)
            {
                throw exc;
            }
        }

        public void OnGrab()
        {
            try
            {
                MC.SetParam(channel, "ForceTrig", "TRIG");
                isGrabDone = false;
            }
            catch (Euresys.MultiCamException exc)
            {
                throw exc;
            }
        }

        private void MultiCamCallback(ref MC.SIGNALINFO signalInfo)
        {
            switch (signalInfo.Signal)
            {
                case MC.SIG_START_ACQUISITION_SEQUENCE:
                    Debug.WriteLine("SIG_START_ACQUISITION_SEQUENCE");
                    break;
                case MC.SIG_SURFACE_PROCESSING:
                    ProcessingCallback(signalInfo);

                    isGrabDone = true;
                    if (OnCallback != null)
                        OnCallback();
                    break;
                case MC.SIG_END_CHANNEL_ACTIVITY:
                    Debug.WriteLine("SIG_END_CHANNEL_ACTIVITY");
                    break;
                case MC.SIG_ACQUISITION_FAILURE:
                    Debug.WriteLine("SIG_ACQUISITION_FAILURE");
                    break;
                default:
                    throw new Euresys.MultiCamException("Unknown signal");
            }
        }

        private void ProcessingCallback(MC.SIGNALINFO signalInfo)
        {
            UInt32 currentChannel = (UInt32)signalInfo.Context;

            currentSurface = signalInfo.SignalInfo;
            try
            {
                Int32 width, height, bufferPitch, bufferSize;
                IntPtr bufferAddress;
                MC.GetParam(currentChannel, "ImageSizeX", out width);
                MC.GetParam(currentChannel, "ImageSizeY", out height);
                MC.GetParam(currentChannel, "BufferPitch", out bufferPitch);
                MC.GetParam(currentChannel, "BufferSize", out bufferSize);
                MC.GetParam(currentSurface, "SurfaceAddr", out bufferAddress);

                Marshal.Copy(bufferAddress, imageBuffer, 0, bufferSize);

                grabDone.Set();
            }
            catch (Euresys.MultiCamException exc)
            {
                Debug.WriteLine(exc.Message);
            }
            catch (System.Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        public EventWaitHandle GetHandleGrabDone()
        {
            return grabDone;
        }

        public void OnResetEventGrabDone()
        {
            grabDone.Reset();
        }

        public byte[] GetImagePointer()
        {
            return imageBuffer;
        }

        public void GetWidth(out int value)
        {
            try
            {
                MC.GetParam(channel, "ImageSizeX", out value);
            }
            catch (Euresys.MultiCamException exc)
            {
                throw exc;
            }
        }

        public void GetHeight(out int value)
        {
            try
            {
                MC.GetParam(channel, "ImageSizeY", out value);
            }
            catch (Euresys.MultiCamException exc)
            {
                throw exc;
            }
        }

        public void GetFrameRate(out double value)
        {
            try
            {
                MC.GetParam(channel, "PerSecond_Fr", out value);
            }
            catch (Euresys.MultiCamException exc)
            {
                throw exc;
            }
        }

        public void SetExposureTime(int time) //unit :us
        {
            try
            {
                MC.SetParam(channel, "Expose_us", time);

                isActived = false;
            }
            catch (Euresys.MultiCamException exc)
            {
                throw exc;
            }
        }

        public void SaveImage(string name)
        {
            /*
            int width, height;
            GetWidth(out width);
            GetHeight(out height);

            // 1. 빈 비트맵 생성 (포맷은 데이터에 맞게 설정, 예: 32bpp RGB)
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            if (bitmap.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                ColorPalette colorPalette = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    colorPalette.Entries[i] = Color.FromArgb(i, i, i);
                }

                bitmap.Palette = colorPalette;
            }
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            IntPtr ptrBmp = bmpData.Scan0;

            // 2. 비트맵의 메모리 영역 잠금
            //BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
            //                                  ImageLockMode.WriteOnly, bmp.PixelFormat);

            // 3. byte[] 데이터를 비트맵 메모리로 복사
            Marshal.Copy(GetImagePointer(), 0, ptrBmp, bitmap.Width * bitmap.Height);

            // 4. 메모리 잠금 해제
            bitmap.UnlockBits(bmpData);
            */
            bitmap.Save(name, ImageFormat.Jpeg);
        }
    }
}
