using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace IRArray_test
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        //==== System ====
        private string Flag = "Main";
        private bool IsClose = false;
        //==== Thread ====
        private System.Threading.Thread Thread1 = null;     //Log資料
        private System.Threading.Thread Thread3 = null;     //連線Server
        private System.Threading.Thread Thread4 = null;     //計時器
        //==== Thread Parameter ====
        private System.Text.Encoding Encoding = System.Text.Encoding.GetEncoding(950);//System.Text.Encoding.GetEncoding(950);
        private List<LogInfo> List1 = new List<LogInfo>();  //Log資料
        private List<ThreadInfo> List3 = new List<ThreadInfo>();//記錄Server基本資料
        private List<SocketInfo> List4 = new List<SocketInfo>();//記錄Server回傳資料
        private List<SocketInfo> _List = new List<SocketInfo>();//記錄Server回傳資料
        //==== Parameter ====
        private int Int = 0;
        private double MaxValue = 0;
        private double MinValue = 0;
        private int ImgWidth;
        private int ImgHeight;
        //private List<byte[]> List5 = new List<byte[]>();
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            this.Closing += MainWindow_Closing;
            //RangeSlider.DragCompletedEvent += RangeSlider_DragCompletedEvent;
            ButtonClose.Click += Button_Click;
            MaxValue = 1; MinValue = 0;

            ImgWidth = 512; ImgHeight = 256;
            Thread3 = new System.Threading.Thread(Thread3_Run); Thread3.Start(); System.Threading.Thread.Sleep(100);
            //Thread4 = new System.Threading.Thread(Thread4_Run); Thread4.Start(); System.Threading.Thread.Sleep(100);
            this.List3.Add(new ThreadInfo() { Text = "COM", COM = "COM4", IsConnect = true });
            SendData(0, "Module", "Option", "ATGS");
            SendData(0, "Module", "Option", "ATGR");
            SendData(0, "Module", "Option", "ATGO");
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !IsClose;
        }
        private void Trace(LogInfo LogStruct)
        {
            try { List1.Add(LogStruct); }
            catch { }
        }
        private void Trace(SocketInfo Info, bool Bool, int Int)
        {
            try
            {
                List1.Add(new LogInfo() { File = "Error", Module = Info.Module, Category = Info.Category, Method = Info.Option, Message = string.Format("{0}:{1} Key:{2}/Value:{3} {4}", Info.IP, Info.Port, Info.Key, Info.Value, Info.Message) });
                if (Int < 0) { return; }
                List3[Int].IsError = Bool;
                //Dispatcher.BeginInvoke(new Action(() => { Menu.Import_Server(Int, !Bool); }));
                //bool IsConnect = _List[Int].IsAlarmConnect; if (IsConnect != _List[Int].IsAlarmConnect) { _List[Int].IsAlarmConnect = IsConnect; }
            }
            catch { }
        }

        private void RangeSlider_DragCompletedEvent(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            try
            {
                RangeSlider RangeSlider = sender as RangeSlider; if (RangeSlider == null) { return; }
                MaxValue = RangeSlider.Start;
                MinValue = RangeSlider.End;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsClose = true; this.Close();
        }
        //private void Thread4_Run()
        //{
        //    byte[] Bytes = new byte[ImgWidth * ImgHeight];
        //    //while (Thread4 != null && Thread4.IsAlive)
        //    //{
        //    //    try
        //    //    {
        //    //        //if (IsClose) { break; }
        //    //        //for (int i = 0; i < ImgHeight; i++)
        //    //        //{
        //    //        //    for (int j = 0; j < ImgWidth; j++) { Bytes[i * ImgHeight + j] = (byte)(i % 256); }
        //    //        //}
        //    //        //List4.Add(Bytes);
        //    //    }
        //    //    catch (Exception ex) { Console.WriteLine(ex.Message); }
        //    //    finally { System.Threading.Thread.Sleep(1000); }
        //    //}
        //}       //處理資料  
        private System.Windows.Media.Imaging.BitmapImage HSLToImage(byte[] srcBytes)
        {
            System.Drawing.Bitmap Bitmap = new System.Drawing.Bitmap(ImgWidth, ImgHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //將Bitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的Bitmap
            System.Drawing.Imaging.BitmapData BitmapData = Bitmap.LockBits(new System.Drawing.Rectangle(0, 0, ImgWidth, ImgHeight), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            //將Bitmap對象的訊息存放到Buffer中
            int BufferLength = BitmapData.Stride * ImgHeight;
            byte[] Buffer = new byte[BufferLength];

            double Temp = (MaxValue - MinValue) / 255f;
            for (int i = 0; i < ImgHeight; i++)
            {
                for (int j = 0; j < ImgWidth; j++)
                {
                    //只處理每行中圖像像素數據,捨棄未用空間
                    //注意位圖結構中RGB按BGR的順序存儲
                    //blue = srcP[0];
                    //green = srcP[1];
                    //red = srcP[2];
                    //byte Byte = srcBytes[i * ImgHeight + j];
                    ////double Double = 1 - ((Byte / 255f) * (MaxValue - MinValue) + MinValue);
                    //double Double = (Byte * Temp + MinValue);
                    //byte[] Bytes = HSLToRGB(Double);
                    //Buffer[i * BitmapData.Stride + j * 3] = Bytes[2];
                    //Buffer[i * BitmapData.Stride + j * 3 + 1] = Bytes[1];
                    //Buffer[i * BitmapData.Stride + j * 3 + 2] = Bytes[0];
                    ///
                    //Buffer[i * BitmapData.Stride + j * 3] = (byte)(255 * 0.5);
                    //Buffer[i * BitmapData.Stride + j * 3 + 1] = (byte)(255 * 0.15);
                    //Buffer[i * BitmapData.Stride + j * 3 + 2] = (byte)(255 * 0.15);
                    //Buffer[i * BitmapData.Stride + j * 3] = (byte)(255 * 0.75);
                    //Buffer[i * BitmapData.Stride + j * 3 + 1] = (byte)(255 * 0.15);
                    //Buffer[i * BitmapData.Stride + j * 3 + 2] =(byte)(255 * 0.3);
                    //Buffer[i * BitmapData.Stride + j * 3] = (byte)(255 * 0.5);
                    //Buffer[i * BitmapData.Stride + j * 3 + 1] = (byte)(255 * 0.2);
                    //Buffer[i * BitmapData.Stride + j * 3 + 2] = (byte)(255 * 0.6);
                    //Buffer[i * BitmapData.Stride + j * 3] = (byte)(255 * 0.15);
                    //Buffer[i * BitmapData.Stride + j * 3 + 1] = (byte)(255 * 0.25);
                    //Buffer[i * BitmapData.Stride + j * 3 + 2] = (byte)(255 * 1);
                    //Buffer[i * BitmapData.Stride + j * 3] = (byte)(255 * 0);
                    //Buffer[i * BitmapData.Stride + j * 3 + 1] = (byte)(255 * 0.5);
                    //Buffer[i * BitmapData.Stride + j * 3 + 2] = (byte)(255 * 0.9);
                    //Buffer[i * BitmapData.Stride + j * 3] = (byte)(255 * 0.1);
                    //Buffer[i * BitmapData.Stride + j * 3 + 1] = (byte)(255 * 0.75);
                    //Buffer[i * BitmapData.Stride + j * 3 + 2] = (byte)(255 * 0.9);
                    Buffer[i * BitmapData.Stride + j * 3] = (byte)(255 * 0.5);
                    Buffer[i * BitmapData.Stride + j * 3 + 1] = (byte)(255 * 0.9);
                    Buffer[i * BitmapData.Stride + j * 3 + 2] = (byte)(255 * 0.9);
                }
            }
            //位元圖中第一個像素數據的地址。它也可以看成是位圖中的第一個掃描行
            //目的是設兩個起始旗標IntPtr，為BitmapData的掃描行的開始位置
            //System.IntPtr IntPtr = BitmapData.Scan0;
            //複製GRB信息到Buffer中
            System.Runtime.InteropServices.Marshal.Copy(Buffer, 0, BitmapData.Scan0, BufferLength);
            //解鎖位圖
            Bitmap.UnlockBits(BitmapData);
            return ToBitmapImage(Bitmap);
        }
        private byte[] HSLToRGB(double H, double S = 1, double L = 0.5)
        {
            byte[] Byte = new byte[3];
            double R = 0, G = 0, B = 0;
            if (L == 0) { return Byte; }
            if (S == 0) { R = G = B = L; }
            else
            {
                double temp2;
                if (L < 0.5) { temp2 = L * (1.0 + S); } else { temp2 = L + S - (L * S); }
                double temp1 = 2.0 * L - temp2;
                R = ColorComponent(temp1, temp2, H + 1.0 / 3.0);
                G = ColorComponent(temp1, temp2, H);
                B = ColorComponent(temp1, temp2, H - 1.0 / 3.0);
            }
            Byte[0] = (byte)(R * 255);
            Byte[1] = (byte)(G * 255);
            Byte[2] = (byte)(B * 255);
            return Byte;
        }
        private double ColorComponent(double temp1, double temp2, double temp3)
        {
            if (temp3 < 0.0) { temp3 += 1.0; }
            else if (temp3 > 1.0) { temp3 -= 1.0; }

            if (temp3 < 1.0 / 6.0) { return temp1 + (temp2 - temp1) * 6.0 * temp3; }
            else if (temp3 < 0.5) { return temp2; }
            else if (temp3 < 2.0 / 3.0) { return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0); }
            else { return temp1; }
        }
        private System.Windows.Media.Imaging.BitmapImage ToBitmapImage(System.Drawing.Bitmap Bitmap)
        {
            System.Windows.Media.Imaging.BitmapImage BitmapImage = new System.Windows.Media.Imaging.BitmapImage();
            using (System.IO.MemoryStream MemoryStream = new System.IO.MemoryStream())
            {
                Bitmap.Save(MemoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                BitmapImage.BeginInit();
                BitmapImage.StreamSource = MemoryStream;
                BitmapImage.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                BitmapImage.EndInit();
                BitmapImage.Freeze();
            }
            return BitmapImage;
        }
        #region Thread
        private class LogInfo
        {
            #region Property
            public string File { get; set; }            //檔案
            public string Module { get; set; }          //模組
            public string Category { get; set; }        //分類
            public string Method { get; set; }          //方法/控制
            public string Message { get; set; }         //訊息
            #endregion
        }
        private class LogFile
        {
            public string File { get; set; }            //檔案
            public System.IO.StreamWriter StreamWriter { get; set; }
        }
        private class ThreadInfo
        {
            public string Text { get; set; }
            public string IP { get; set; }
            public int Port { get; set; }
            public string COM { get; set; }
            public bool IsConnect { get; set; } //連線狀態
            public bool IsHide { get; set; }    //顯示狀態
            public bool IsError { get; set; }   //連線狀態
        }
        private class SocketInfo
        {
            public string Module { get; set; }
            public string Category { get; set; }
            public string Option { get; set; }  //即是 Option
            public string Message { get; set; }

            public string IP { get; set; }
            public int Port { get; set; }
            public string COM { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
            public bool IsError { get; set; }
        }
        private void Thread1_Run()
        {
            Trace(new LogInfo() { Module = Flag, Category = "Thread", Method = "Thread_Run", Message = "Log" });

            bool IsExist = false;
            int Day = DateTime.Now.Day; string DateStr = DateTime.Now.ToString("yyyyMMdd");
            List<LogFile> _List = new List<LogFile>();
            while (Thread1 != null && Thread1.IsAlive)
            {
                try
                {
                    if (!IsClose && List1.Count == 0) { System.Threading.Thread.Sleep(500); continue; }
                    else if ((Day != DateTime.Now.Day) || (IsClose && List1.Count == 0))
                    {
                        foreach (LogFile Item in _List) { Item.File = null; if (Item.StreamWriter != null) { Item.StreamWriter.Close(); Item.StreamWriter.Dispose(); Item.StreamWriter = null; } }
                        _List.Clear();
                        if (IsClose) { break; }
                        Day = DateTime.Now.Day; DateStr = DateTime.Now.ToString("yyyyMMdd");
                        Trace(new LogInfo() { Module = Flag, Method = "Thread_Run", Category = "Create", Message = "New File" });
                    }

                    LogInfo Struct = List1[0]; List1.RemoveAt(0); if (Struct == null) { continue; }
                    string File = (Struct.File == null) ? "Log" : Struct.File;
                    string Module = (Struct.Module == null) ? "" : Struct.Module;
                    string Method = (Struct.Method == null) ? "" : Struct.Method;
                    string Category = (Struct.Category == null) ? "" : Struct.Category;
                    string Message = (Struct.Message == null) ? "" : Struct.Message;

                    IsExist = false;
                    int Index = -1;
                    int Count = (_List == null) ? 0 : _List.Count;
                    for (int i = 0; i < Count; i++) { if (_List[i].File == File) { Index = i; IsExist = true; break; } }
                    if (!IsExist)
                    {
                        string FolderPath = @".\Log\";
                        string FilePath = string.Format(@"{0}{1}{2}.txt", FolderPath, File, DateStr);
                        if (!System.IO.Directory.Exists(FolderPath)) System.IO.Directory.CreateDirectory(FolderPath);
                        else { IsExist = System.IO.File.Exists(FilePath); }
                        _List.Add(new LogFile() { File = File, StreamWriter = System.IO.File.AppendText(FilePath) }); Index = Count;
                    };

                    System.Text.StringBuilder Content = new System.Text.StringBuilder();
                    if (!IsExist)
                    {
                        Content.Append("#Timestamp".PadRight(20)); Content.Append('\t', 1);
                        Content.Append("#Module".PadRight(20)); Content.Append('\t', 1);
                        Content.Append("#Category".PadRight(20)); Content.Append('\t', 1);
                        Content.Append("#Method".PadRight(20)); Content.Append('\t', 1);
                        Content.Append("#Message"); //Content.Append('\t', 1);
                        _List[Index].StreamWriter.WriteLine(Content.ToString()); Content.Clear();
                    }

                    Content.Append(DateTime.Now.ToString("HH:mm:ss.ffffff").PadRight(20)); Content.Append('\t', 1);
                    Content.Append(Module.PadRight(20)); Content.Append('\t', 1);
                    Content.Append(Category.PadRight(20)); Content.Append('\t', 1);
                    Content.Append(Method.PadRight(20)); Content.Append('\t', 1);
                    Content.Append(Message); //Content.Append('\t', 2);
                    _List[Index].StreamWriter.WriteLine(Content.ToString()); Content.Clear();
                    _List[Index].StreamWriter.Flush();
                }
                catch { if (List1.Count > 128) List1.Clear(); }
            }
        }       //Log 寫Log
        private string Thread3_Run1(int Int, bool IsError, SocketInfo Info)
        {
            System.IO.Ports.SerialPort SerialPort = null;
            try
            {
                Info.Category = "Initial";
                byte[] buffer = Encoding.GetBytes(Info.Key); if (buffer == null) { return null; }
                SerialPort = new System.IO.Ports.SerialPort(Info.COM, 230400, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                SerialPort.WriteTimeout = 1000; SerialPort.ReadTimeout = 1000;
                Info.Category = "Connect"; SerialPort.Open();
                Info.Category = "Send"; SerialPort.Write(buffer, 0, buffer.Length); int send = SerialPort.BytesToWrite;
                buffer = new byte[SerialPort.ReadBufferSize];
                Info.Category = "Receive"; SerialPort.Read(buffer, 0, buffer.Length); int read = SerialPort.BytesToRead;
                Info.Category = "Encode"; Info.Value = Encoding.GetString(buffer, 0, read);
                Info.Category = "Category";
                //if (Info.Option == "Info") { List4.Add(Info); }
                //else if (Info.Option == "DB") { List4.Add(Info); }
                //else if (Info.Option == "Alarm") { List4.Add(Info); }
                //else if (Info.Option == "RELAY") { List4.Add(Info); }
                Info.Category = "Finish";
                //if (IsError) { IsError = false; Trace(Info, IsError, Int); }
                Console.WriteLine(Info.Value);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); if (!IsError) { IsError = true; Info.Message = ex.Message; Trace(Info, IsError, Int); } }
            finally { if (SerialPort != null) { SerialPort.Close(); SerialPort.Dispose(); SerialPort = null; } }
            return Info.Value;
        }
        private void Thread3_Run()
        {
            Trace(new LogInfo() { Module = Flag, Category = "Thread", Method = "Thread3_Run", Message = "Data" });
            System.IO.Ports.SerialPort SerialPort = null;
            SocketInfo Info = new SocketInfo();
            while (Thread3 != null && Thread3.IsAlive)
            {
                try
                {
                    if (_List.Count == 0) { if (IsClose) { break; } else { if (SerialPort != null) { SerialPort.Close(); SerialPort.Dispose(); SerialPort = null; } continue; } }
                    Info = _List[0]; _List.RemoveAt(0); if (Info == null) { continue; }
                    Info.Category = "Initial";
                    byte[] buffer = Encoding.GetBytes(Info.Key); if (buffer == null) { continue; }
                    if (SerialPort == null)
                    {
                        SerialPort = new System.IO.Ports.SerialPort(Info.COM, 230400, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                        SerialPort.WriteTimeout = 1000; SerialPort.ReadTimeout = 1000;
                    }
                    Info.Category = "Connect"; if (!SerialPort.IsOpen) { SerialPort.Open(); }
                    Info.Category = "Send"; SerialPort.Write(buffer, 0, buffer.Length); int send = SerialPort.BytesToWrite;
                    buffer = new byte[SerialPort.ReadBufferSize];
                    Info.Category = "Receive"; SerialPort.Read(buffer, 0, buffer.Length); int read = SerialPort.BytesToRead;
                    Info.Category = "Encode"; Info.Value = Encoding.GetString(buffer, 0, read);
                    Console.WriteLine("send: {0} Read: {1} Value: {2}", send, read, Info.Value);
                    Info.Category = "Category";
                    //if (Info.Option == "Info") { List4.Add(Info); }
                    //else if (Info.Option == "DB") { List4.Add(Info); }
                    //else if (Info.Option == "Alarm") { List4.Add(Info); }
                    //else if (Info.Option == "RELAY") { List4.Add(Info); }
                    Info.Category = "Finish";
                    if (Info.IsError) { Info.IsError = false; Trace(Info, Info.IsError, Int); }
                    //System.Windows.Media.Imaging.BitmapImage BitmapImage = HSLToImage(Info.Value);
                    //Dispatcher.BeginInvoke(new Action(() => { Image.Source = BitmapImage; }));
                }
                catch (Exception ex) { Info.Message = ex.Message; Trace(Info, false, -1); }
                finally { System.Threading.Thread.Sleep(100); }
            }
            if (SerialPort != null) { SerialPort.Close(); SerialPort.Dispose(); SerialPort = null; }
        }       //處理資料  
        private void Thread4_Run()
        {
            Trace(new LogInfo() { Module = Flag, Category = "Thread", Method = "Thread4_Run", Message = "Timer" });
            bool IsError = false;
            while (Thread4 != null && Thread4.IsAlive)
            {
                try
                {
                    if (IsClose) { break; }
                    if (IsError) { IsError = false; Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Thread", Method = "Thread4_Run", Message = "Troubleshoot" }); }
                }
                catch (Exception ex) { if (!IsError) { IsError = true; Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Thread", Method = "Thread4_Run", Message = ex.Message }); } }
                finally { System.Threading.Thread.Sleep(1000); }
            }
        }
        private void SendData(int Int, string Module, string Option, string Command)
        {
            try
            {
                ThreadInfo Info = List3[Int]; if (Info == null) { return; }
                SocketInfo SubInfo = new SocketInfo();
                SubInfo.Module = Module;
                SubInfo.IP = Info.IP;
                SubInfo.Port = Info.Port;
                SubInfo.COM = Info.COM;
                SubInfo.Option = Option;
                SubInfo.Key = Command;
                SubInfo.IsError = Info.IsError;
                _List.Add(SubInfo);
                //System.Threading.Thread Thread = new System.Threading.Thread(() => Thread3_Run1(Int, Info.IsError, SubInfo)); Thread.Start();
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "SendData", Message = ex.Message }); }
        }
        //private void VL2000(string Option, string Command)
        //{
        //    try { SendData(3 + Int, "VL2000", Option, Command); }
        //    catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "VL2000", Message = ex.Message }); }
        //}
        //private void VLAdm(string Option, string Command)
        //{
        //    try { SendData(2, "VLAdm", Option, Command); }
        //    catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "VLAdm", Message = ex.Message }); }
        //}
        //private void Monitor(int Int, bool IsStop)
        //{
        //    try
        //    {
        //        if (IsServer)
        //        {
        //            if (!IsStop) { SendData(1, "Monitor", "Monitor", string.Format("M{0:000}", Int)); }
        //            else { SendData(1, "Monitor", "Monitor", string.Format("R{0:000}", Int)); }
        //        }
        //        else
        //        {
        //            ThreadInfo Info = List3[1 + Int];
        //            if (!IsStop) { SendData(1, "Monitor", "Monitor", string.Format("LS{0}F{1:000}", Info.IP, Int)); }
        //            else { SendData(1, "Monitor", "Monitor", "S"); }
        //        }
        //    }
        //    catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "Monitor", Message = ex.Message }); }
        //}
        #endregion
    }
}
