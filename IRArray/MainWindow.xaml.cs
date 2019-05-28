using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IRArray
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Parameter
        //private string Flag = "Main";
        #endregion
        #region Property
        #endregion
        #region Presentation
        #endregion
        #region Contents
        #endregion
        #region Event
        //public event System.EventHandler<EventInfo> EventHandler;
        //protected virtual void OnEvent(string Option, params object[] Values)
        //{
        //    EventHandler?.Invoke(this, new EventInfo(Option, Values));
        //}
        #endregion
        #region Method
        //public void Initialize()
        //{
        //}
        #endregion
        #region Command
        #endregion

        #region Parameter
        //==== System ====
        private string Flag = "Main";
        private bool IsClose = false;
        //==== Thread ====
        private System.Threading.Thread Thread1 = null;     //Log資料
        private System.Threading.Thread Thread3 = null;     //連線Server
        private System.Threading.Thread Thread4 = null;     //連線Server
        private System.Threading.Thread Thread5 = null;     //處理
        private System.Threading.Thread Thread6 = null;     //處理
        private System.Threading.Thread Thread7 = null;     //處理
        //==== Thread Parameter ====
        private System.Text.Encoding Encoding = System.Text.Encoding.GetEncoding(950);
        private List<LogInfo> List1 = new List<LogInfo>();  //Log資料
        private List<ThreadInfo> List2 = new List<ThreadInfo>();//記錄Server基本資料
        private List<ThreadInfo> List3 = new List<ThreadInfo>();//記錄Server基本資料
        private List<ThreadInfo> List4 = new List<ThreadInfo>();//記錄Server基本資料
        private List<byte[]> List5 = new List<byte[]>();        //記錄回傳資料
        private List<byte[]> List6 = new List<byte[]>();        //記錄回傳資料
        private List<byte[]> List7 = new List<byte[]>();        //記錄回傳資料
        //==== Parameter ====
        private DeviceStruct Struct1 = new DeviceStruct();
        private ScreenStruct Struct2 = new ScreenStruct();
        private List<RegionalStruct> List_RegionalStruct = new List<RegionalStruct>();
        private PreferencesStruct Struct4 = new PreferencesStruct();
        private List<PairStruct> DeviceList = new List<PairStruct>();
        private int Int = 0;
        private int ImgWidth = 32;
        private int ImgHeight = 24;
        private ColorMap ColorMap = new ColorMap();
        private int[,] CMap = new int[255, 4];
        private int Mode = 3;
        //==== SerialPort Data ====
        private System.IO.Ports.SerialPort SerialPort = null;
        private System.IO.Ports.SerialPort SerialPort1 = null;
        private byte s;
        private byte S;
        private byte e;
        private byte E;
        #endregion
        #region Property
        private DataInfo _DataInfo = new DataInfo();
        public class DataInfo
        {
            public double Avg { get; set; }
            public double High { get; set; }
            public double Low { get; set; }
            public double Diff { get { return (High - Low); } }
            public int Person { get; set; }
            public ImageSource Source { get; set; }
            public DataInfo GetDataInfo(double _Avg, double _High, double _Low, int _Person)
            {
                Avg = _Avg;
                High = _High;
                Low = _Low;
                Person = _Person;
                return this;
            }
            public DataInfo GetDataInfo(ImageSource _Source)
            {
                Source = _Source;
                return this;
            }
        }
        #endregion
        #region Presentation
        #region 視窗效果
        //隱藏視窗
        //WindowStyle="None" AllowsTransparency="True" ResizeMode="NoResize"
        //視窗陰影
        //<Window.Effect>
        //  <DropShadowEffect BlurRadius = "10" Color="Gray" ShadowDepth="2" Direction="-90"/>
        //</Window.Effect>
        //縮放視窗
        //<Style TargetType="{x:Type Thumb}">
        //    <EventSetter Event="PreviewMouseLeftButtonDown" Handler="thumb_PreviewMouseLeftButtonDown"/>
        //    <Setter Property = "Background" Value="Red"/>
        //    <Setter Property = "Opacity" Value="0"/>
        //</Style>
        //<Thumb x:Name="thumbHeader" Grid.Row="0" Margin="15,5,15,0" Cursor="SizeAll"/>
        //<Thumb x:Name="thumbTop" Grid.RowSpan="2" Margin="15,0" VerticalAlignment="Top" Height="5" Cursor="SizeNS"/>
        //<Thumb x:Name="thumbTopRight" Grid.RowSpan="2" Margin="0" VerticalAlignment="Top" HorizontalAlignment="Right" Height="15" Width="15" Cursor="SizeNESW"/>
        //<Thumb x:Name="thumbRight" Grid.RowSpan="2" Margin="0,15" HorizontalAlignment="Right" Width="5" Cursor="SizeWE"/>
        //<Thumb x:Name="thumbBottomRight" Grid.RowSpan="2" Margin="0" VerticalAlignment="Bottom" HorizontalAlignment="Right" Height="15" Width="15" Cursor="SizeNWSE"/>
        //<Thumb x:Name="thumbBottom" Grid.RowSpan="2" Margin="15,0" VerticalAlignment="Bottom" Height="5" Cursor="SizeNS"/>
        //<Thumb x:Name="thumbBottomLeft" Grid.RowSpan="2" Margin="0" VerticalAlignment="Bottom" HorizontalAlignment="Left" Height="15" Width="15" Cursor="SizeNESW"/>
        //<Thumb x:Name="thumbLeft" Grid.RowSpan="2" Margin="0,15" HorizontalAlignment="Left" Width="5" Cursor="SizeWE"/>
        //<Thumb x:Name="thumbTopLeft" Grid.RowSpan="2" Margin="0" VerticalAlignment="Top" HorizontalAlignment="Left" Height="15" Width="15" Cursor="SizeNWSE"/>
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_CLOSE = 0xF060;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
            Caption = 18,
            Close = 96,
            Minimize = 32,
            Maximize = 48
        }
        private HwndSource HwndSource;
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            IntPtr IntPtr = new WindowInteropHelper(this).Handle;
            HwndSource = HwndSource.FromHwnd(IntPtr);
            HwndSource.AddHook(WndProc);
        }
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            return IntPtr.Zero;
        }
        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(HwndSource.Handle, WM_SYSCOMMAND, (IntPtr)(61440 + direction), IntPtr.Zero);
        }
        private void thumb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Thumb thumb = sender as Thumb; if (thumb == null) { return; }
            //這邊要配合Thumb的命名來取處理，如果Thumb的命名和我的不同，請自行修改下面的程式內容。
            switch (thumb.Name)
            {
                case "thumbHeader": { ResizeWindow(ResizeDirection.Caption); } break;
                case "thumbTop": { ResizeWindow(ResizeDirection.Top); } break;
                case "thumbBottom": { ResizeWindow(ResizeDirection.Bottom); } break;
                case "thumbLeft": { ResizeWindow(ResizeDirection.Left); } break;
                case "thumbRight": { ResizeWindow(ResizeDirection.Right); } break;
                case "thumbTopLeft": { ResizeWindow(ResizeDirection.TopLeft); } break;
                case "thumbTopRight": { ResizeWindow(ResizeDirection.TopRight); } break;
                case "thumbBottomLeft": { ResizeWindow(ResizeDirection.BottomLeft); } break;
                case "thumbBottomRight": { ResizeWindow(ResizeDirection.BottomRight); } break;
                default: break;
            };
        }
        private void ThumbHeader_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Thumb thumb = sender as Thumb; if (thumb == null) { return; }
            //ResizeWindow(ResizeDirection.Maximize);
            //this.MaxHeight = System.Windows.SystemParameters.MaximizedPrimaryScreenHeight;
            //this.MaxWidth = System.Windows.SystemParameters.MaximizedPrimaryScreenWidth;
            //this.WindowState = WindowState.Maximized;
            //this.WindowState = (this.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }
        #endregion
        #endregion
        #region Event
        public event System.EventHandler<EventInfo> EventHandler;
        protected virtual void OnEvent(string Option, params object[] Values)
        {
            EventHandler?.Invoke(this, new EventInfo(Option, Values));
        }
        #endregion
        #region Method
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
            //DataContext = DataInfo.GetDataInfo(10f, 20f, 30f, 5);
        }
        private void Initialize()
        {
            try
            {
                // ========== 系統變數 與 Struct ==========
                this.FontSize = 18;
                DeviceList = GetConfig();   //取得Config目錄下的 Config檔案
                //沒有 Config狀態
                //  有 Config狀態 
                if (DeviceList.Count > 0) { ConfigToStruct((string)DeviceList[Int].Value); }
                else
                {
                    for (int i = 0; i < Struct1.Regional; i++) { List_RegionalStruct.Add(new RegionalStruct()); }
                }
                GetColorMap();              //取得Color Map
                //IRArray 資料流關鍵字 sS:開始 eE:結束
                s = Encoding.GetBytes("s")[0]; S = Encoding.GetBytes("S")[0];
                e = Encoding.GetBytes("e")[0]; E = Encoding.GetBytes("E")[0];
                // ========== 系統事件 ==========
                this.Closing += MainWindow_Closing;
                // ========== Local系統事件 ==========
                WinNarrow.MouseDown += Narrow_MouseDown;
                WinClose.MouseDown += Close_MouseDown;
                Canvas.MouseDown += Canvas_MouseDown;
                Canvas.MouseMove += Canvas_MouseMove;
                Canvas.MouseUp += Canvas_MouseUp;
                // ========== View 初始化 事件 資料 ==========
                Device.Initialize(); Device.EventHandler += Device_EventHandler; Device.Import_Ini(DeviceList, LocalCom(), LocalBaudrRate(), LocalUARTConfig());
                MenuBar.Initialize(); MenuBar.EventHandler += MenuBar_EventHandler;
                Screen.Initialize(); Screen.EventHandler += Screen_EventHandler; Screen.Import_Ini(LocalColorMap());
                Regional.Initialize(); Regional.EventHandler += Regional_EventHandler;
                RegionalDetail.Initialize(); RegionalDetail.EventHandler += RegionalDetail_EventHandler; RegionalDetail.Import_Init(LocalWeek(), LocalMove());
                Preferences.Initialize(); Preferences.EventHandler += Preferences_EventHandler; Preferences.Import_Ini(LocalNF(), LocalShield(), LocalFPS(), LocalDiffer());
                //告警
                PromptBox1.Initialize(); PromptBox1.EventHandler += PromptBox_EventHandler;
                PromptBox2.Initialize(); PromptBox2.EventHandler += PromptBox_EventHandler;
                PromptBoxStackPanel.MouseDown += PromptBoxStackPanel_MouseDown;
                // ========== View 輸入資料 ==========
                this.List2.Add(new ThreadInfo() { COM = Struct1.COM, BaudrRate = Struct1.BaudrRate, DataBits = (Struct1.UARTConfig == "8N1") ? 8 : 7, IsNet = (Struct1.Switch == 0), IsRest = true });
                Import_Value();
                // ========== Thread ==========
                Thread1 = new System.Threading.Thread(Thread1_Run); Thread1.Start(); System.Threading.Thread.Sleep(100);
                //Thread3 = new System.Threading.Thread(Thread3_Run); Thread3.Start(); System.Threading.Thread.Sleep(100);
                Thread4 = new System.Threading.Thread(Thread4_Run); Thread4.Start(); System.Threading.Thread.Sleep(100);
                Thread5 = new System.Threading.Thread(Thread5_Run); Thread5.Start(); System.Threading.Thread.Sleep(100);
                Thread6 = new System.Threading.Thread(Thread6_Run); Thread6.Start(); System.Threading.Thread.Sleep(100);
                Thread7 = new System.Threading.Thread(Thread7_Run); Thread7.Start(); System.Threading.Thread.Sleep(100);
                SerialPort_Ini();

                //this.MinHeight = this.MaxHeight = System.Windows.SystemParameters.MaximizedPrimaryScreenHeight;
                //this.MinWidth = this.MaxWidth = System.Windows.SystemParameters.MaximizedPrimaryScreenWidth;
                //this.WindowState = WindowState.Maximized;
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "Initialize", Category = "Initialize", Message = ex.Message }); }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsClose = true;
        }

        private void Narrow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsClose = true;
            ResizeWindow(ResizeDirection.Close);
        }
        private void Trace(LogInfo LogStruct)
        {
            try { List1.Add(LogStruct); }
            catch { }
        }
        private void Trace(ThreadInfo Info)
        {
            try
            {
                if (Info.IsNet)
                {
                    List1.Add(new LogInfo() { File = "Error", Module = Info.Module, Category = Info.Category, Method = Info.Option, Message = string.Format("{0}:{1} Key:{2}/Value:{3} {4}", Info.IP, Info.Port, Info.Key, Info.Value, Info.Message) });
                }
                else
                {
                    List1.Add(new LogInfo() { File = "Error", Module = Info.Module, Category = Info.Category, Method = Info.Option, Message = string.Format("{0}:{1} Key:{2}/Value:{3} {4}", Info.COM, Info.BaudrRate, Info.Key, Info.Value, Info.Message) });
                }
                if (Int < 0) { return; }
                List2[Info.Int].IsError = Info.IsError;
            }
            catch { }
        }
        #region SerialPort
        private void SerialPort_Ini()
        {
            try
            {
                ThreadInfo Info = List2[0] as ThreadInfo; if (Info == null) { return; }
                if (SerialPort != null)
                {
                    SerialPort.DataReceived -= SerialPort_DataReceived;
                    SerialPort.Dispose(); SerialPort.Close(); SerialPort = null;
                }
                if (Info.IsNet) { return; }
                SerialPort = new System.IO.Ports.SerialPort(Info.COM, Info.BaudrRate, System.IO.Ports.Parity.None, Info.DataBits, System.IO.Ports.StopBits.One);
                SerialPort.ReceivedBytesThreshold = 768;
                SerialPort.DataReceived += SerialPort_DataReceived;
                if (!SerialPort.IsOpen) { SerialPort.Open(); PromptBox1.Import("连线成功"); PromptBox1.Visibility = Visibility.Visible; }
                if (SerialPort != null && SerialPort.IsOpen)
                {
                    SerialPort.WriteLine("ATSM=1"); //SerialPort.WriteLine("ATSS=1");
                    SerialPort.WriteLine(string.Format("ATSS={0}", Mode));
                    //Console.WriteLine(string.Format("ATSR={0:00},{1:000},{2:000},{3:00},{4:00}", Struct4.Shield, Struct4.Differ, Struct4.Pixels, Struct4.FPS, Struct4.FPS));
                    SerialPort.WriteLine(string.Format("ATSR={0:00},{1:000},{2:000},{3:00},{4:00}", Struct4.Shield, Struct4.Differ, Struct4.Pixels, Struct4.FPS, Struct4.FPS));

                    int Count = List_RegionalStruct.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        RegionalStruct Struct = List_RegionalStruct[i];
                        byte[] bytes = GetAreaInt((byte)(i + 1), Struct.Point1, Struct.Point2);
                        SerialPort.Write(bytes, 0, bytes.Length); System.Threading.Thread.Sleep(100);
                        SerialPort.WriteLine("ATSZ=W"); System.Threading.Thread.Sleep(100);
                    }
                }
                //SerialPort.WriteLine("ATGS");
            }
            catch (Exception ex) { PromptBox2.Import("连线失败"); PromptBox2.Visibility = Visibility.Visible; Trace(new LogInfo() { File = "Error", Module = Flag, Method = "SerialPort_Ini", Category = "SerialPort", Message = ex.Message }); }
        }
        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                int BytesToRead = SerialPort.BytesToRead;
                byte[] Buffer = new byte[BytesToRead];
                int Read = SerialPort.Read(Buffer, 0, BytesToRead);
                //Console.WriteLine("=> {0}", Encoding.GetString(Buffer));
                List5.Add(Buffer);
                System.Threading.Thread.Sleep(200);
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "SerialPort_DataReceived", Category = "SerialPort", Message = ex.Message }); }
        }
        #endregion
        #region LocalData
        private List<PairStruct> LocalCom()
        {
            List<PairStruct> List = new List<PairStruct>();
            for (int i = 1; i < 41; i++)
            {
                string Temp = string.Format("COM{0}", i);
                List.Add(new PairStruct() { Text = Temp, Value = Temp });
            }
            return List;
        }
        private List<PairStruct> LocalBaudrRate()
        {
            List<PairStruct> List = new List<PairStruct>();
            List.Add(new PairStruct() { Text = "9600", Value = 9600 });
            List.Add(new PairStruct() { Text = "19200", Value = 19200 });
            List.Add(new PairStruct() { Text = "38400", Value = 38400 });
            List.Add(new PairStruct() { Text = "115200", Value = 115200 });
            List.Add(new PairStruct() { Text = "230400", Value = 230400 });
            return List;
        }
        private List<PairStruct> LocalUARTConfig()
        {
            List<PairStruct> List = new List<PairStruct>();
            List.Add(new PairStruct() { Text = "8N1", Value = "8N1" });
            List.Add(new PairStruct() { Text = "7N1", Value = "7N1" });
            return List;
        }
        private List<PairStruct> LocalColorMap()
        {
            List<PairStruct> List = new List<PairStruct>();
            //List.Add(new PairStruct() { Text = "请选择", Value = null });
            List.Add(new PairStruct() { Text = "Jet", Value = "Jet" });
            List.Add(new PairStruct() { Text = "CMRmap", Value = "CMRmap" });
            return List;
        }
        private List<PairStruct> LocalNF()
        {
            List<PairStruct> List = new List<PairStruct>();
            for (int i = 1; i < 5; i++)
            {
                List.Add(new PairStruct() { Text = i.ToString(), Value = i });
            }
            return List;
        }
        private List<PairStruct> LocalShield()
        {
            List<PairStruct> List = new List<PairStruct>();
            for (int i = 1; i < 4; i++)
            {
                List.Add(new PairStruct() { Text = i.ToString(), Value = i });
            }
            return List;
        }
        private List<PairStruct> LocalFPS()
        {
            List<PairStruct> List = new List<PairStruct>();
            for (int i = 1; i < 4; i++)
            {
                List.Add(new PairStruct() { Text = i.ToString(), Value = i });
            }
            return List;
        }
        private List<PairStruct> LocalDiffer()
        {
            List<PairStruct> List = new List<PairStruct>();
            for (int i = 5; i < 55; i += 5)
            {
                List.Add(new PairStruct() { Text = string.Format("{0:0.0}", (double)i / 10), Value = i });
            }
            return List;
        }
        private List<PairStruct> LocalMove()
        {
            List<PairStruct> List = new List<PairStruct>();
            for (int i = 1; i < 11; i++)
            {
                List.Add(new PairStruct() { Text = i.ToString(), Value = i });
            }
            return List;
        }
        private List<PairStruct> LocalWeek()
        {
            List<PairStruct> List = new List<PairStruct>();
            List.Add(new PairStruct() { Text = "周一", Value = 0 });
            List.Add(new PairStruct() { Text = "周二", Value = 1 });
            List.Add(new PairStruct() { Text = "周三", Value = 2 });
            List.Add(new PairStruct() { Text = "周四", Value = 3 });
            List.Add(new PairStruct() { Text = "周五", Value = 4 });
            List.Add(new PairStruct() { Text = "周六", Value = 5 });
            List.Add(new PairStruct() { Text = "周日", Value = 6 });
            return List;
        }
        #endregion
        #region EventHandler
        private void MenuBar_EventHandler(object sender, EventInfo e)
        {
            try
            {
                Screen.Visibility = Regional.Visibility = RegionalDetail.Visibility = Preferences.Visibility = Visibility.Collapsed;
                switch (e.Option)
                {
                    case "Error": { Trace(new LogInfo() { File = "Error", Module = e.Value[0] as string, Method = e.Value[1] as string, Category = "EventHandler", Message = e.Value[2] as string }); } return;
                    case "Screen": { Screen.Import_Value(Struct2); Screen.Visibility = Visibility.Visible; } break;
                    case "Regional": { Regional.Visibility = Visibility.Visible; } break;
                    case "Preferences": { Preferences.Import_Value(Struct4); Preferences.Visibility = Visibility.Visible; } break;
                }
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "MenuBar_EventHandler", Category = "EventHandler", Message = ex.Message }); }
        }
        private void Device_EventHandler(object sender, EventInfo e)
        {
            try
            {
                switch (e.Option)
                {
                    case "Error": { Trace(new LogInfo() { File = "Error", Module = e.Value[0] as string, Method = e.Value[1] as string, Category = "EventHandler", Message = e.Value[2] as string }); } return;
                    case "Insert":
                        {
                            PairStruct PairStruct = CreateConfig();
                            DeviceList.Add(PairStruct);
                            //檢查參數是否正常
                            if (Device_Check()) { return; }
                            Int = DeviceList.Count - 1;
                            //寫入 Config
                            string Text = (string)DeviceList[Int].Text;
                            string Path = (string)DeviceList[Int].Value;
                            StructToConfig(Path, "Device");
                            PromptBox1.Import("已保存"); PromptBox1.Visibility = Visibility.Visible;
                            //更新選單
                            Device.Import_Ini(DeviceList);
                            Device.Import_Value(DeviceList.Count - 1);
                            Trace(new LogInfo() { Module = Flag, Method = "Device_EventHandler", Category = "EventHandler", Message = string.Format("新增连线装置:{0}", Text) });
                        }
                        break;
                    case "Save":
                        { //更新 Device 資料
                          //檢查參數是否正常
                            if (Device_Check()) { return; }
                            //寫入Config
                            string Text = (string)DeviceList[Int].Text;
                            string Path = (string)DeviceList[Int].Value;
                            StructToConfig(Path, "Device");
                            PromptBox1.Import("已保存", true, false, false, "SerialPort_Save"); PromptBox1.Visibility = Visibility.Visible;
                            List2[0] = new ThreadInfo() { COM = Struct1.COM, BaudrRate = Struct1.BaudrRate, DataBits = (Struct1.UARTConfig == "8N1") ? 8 : 7, IsNet = (Struct1.Switch == 0), IsRest = true };
                            Trace(new LogInfo() { Module = Flag, Method = "Device_EventHandler", Category = "EventHandler", Message = string.Format("儲存连线装置:{0} COM:{1} BaudrRate:{2} UARTConfig:{3}", Text, Struct1.COM, Struct1.BaudrRate, Struct1.UARTConfig) });
                        }
                        break;
                    case "Delete":
                        {
                            string Text = DeviceList[Int].Text;
                            PromptBox1.Import(string.Format("是否删除{0}", Text), false, true, false, "SerialPort_Delete"); PromptBox1.Visibility = Visibility.Visible;
                            Trace(new LogInfo() { Module = Flag, Method = "Device_EventHandler", Category = "EventHandler", Message = string.Format("刪除连线装置:{0}", Text) });
                        }
                        break;
                    case "Changed":
                        {
                            int Temp = (int)e.Value[0]; if (Temp >= 0) { Int = Temp; }
                            ConfigToStruct((string)DeviceList[Int].Value, "Device"); Device.Import_Value(Struct1);
                        }
                        return;
                }
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "Device_EventHandler", Category = "EventHandler", Message = ex.Message }); }
        }
        private void Screen_EventHandler(object sender, EventInfo e)
        {
            try
            {
                //Console.WriteLine("Screen {0}", e.Option);
                switch (e.Option)
                {
                    case "Error": { Trace(new LogInfo() { File = "Error", Module = e.Value[0] as string, Method = e.Value[1] as string, Category = "EventHandler", Message = e.Value[2] as string }); } return;
                    case "Cancel": { Mode = Struct2.Mode; } break;
                    case "Yes":
                        {
                            Struct2 = Screen.Export();
                            StructToConfig((string)DeviceList[Int].Value, "Screen");
                            Mode = Struct2.Mode; GetColorMap();
                            PromptBox1.Import("已保存");
                            PromptBox1.Visibility = Visibility.Visible;
                            Trace(new LogInfo() { Module = Flag, Method = "Screen_EventHandler", Category = "EventHandler", Message = string.Format("画面设定 Mode:{0} Colormap:{1}", Mode, Struct2.Colormap) });
                        }
                        break;
                    //case "RadioButton1": { Mode = 0; } break;
                    //case "RadioButton2": { Mode = 1; } break;
                    //case "RadioButton3": { Mode = 3; } break;
                    default: return;
                }
                if (SerialPort != null && SerialPort.IsOpen) { SerialPort.WriteLine(string.Format("ATSS={0}", Mode)); }
                //SendData("Mode", "Option", string.Format("ATSS={0}", Mode));
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "Screen_EventHandler", Category = "EventHandler", Message = ex.Message }); }
        }
        private void Regional_EventHandler(object sender, EventInfo e)
        {
            try
            {
                int Int = -1;
                switch (e.Option)
                {
                    case "Error": { Trace(new LogInfo() { File = "Error", Module = e.Value[0] as string, Method = e.Value[1] as string, Category = "EventHandler", Message = e.Value[2] as string }); } return;
                    case "Button1": { Int = 0; } break;
                    case "Button2": { Int = 1; } break;
                    case "Button3": { Int = 2; } break;
                    case "Button4": { Int = 3; } break;
                    case "Button5": { Int = 4; } break;
                    case "Button6": { Int = 5; } break;
                    case "Button7": { Int = 6; } break;
                    case "Button8": { Int = 7; } break;
                    case "Button9": { Int = 8; } break;
                    case "Button10": { Int = 9; } break;
                    case "Button11": { Int = 10; } break;
                    case "Button12": { Int = 11; } break;
                    case "Button13": { Int = 12; } break;
                    case "Button14": { Int = 13; } break;
                    case "Button15": { Int = 14; } break;
                    case "Button16": { Int = 15; } break;
                }
                if (Int < 0) { return; }
                RegionalDetail.Import_Value(List_RegionalStruct[Int], Int);
                Regional.Visibility = Visibility.Collapsed;
                RegionalDetail.Visibility = Visibility.Visible;
                SetPoint(List_RegionalStruct[Int].Point1, List_RegionalStruct[Int].Point2);
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "Regional_EventHandler", Category = "EventHandler", Message = ex.Message }); }
        }
        private void RegionalDetail_EventHandler(object sender, EventInfo e)
        {
            try
            {
                //Console.WriteLine("Screen {0}", e.Option);
                switch (e.Option)
                {
                    case "Error": { Trace(new LogInfo() { File = "Error", Module = e.Value[0] as string, Method = e.Value[1] as string, Category = "EventHandler", Message = e.Value[2] as string }); } return;
                    case "Cancel":
                        {
                            Regional.Visibility = Visibility.Visible;
                            RegionalDetail.Visibility = Visibility.Collapsed;
                        }
                        break;
                    case "Yes":
                        {
                            Regional.Visibility = Visibility.Visible;
                            RegionalDetail.Visibility = Visibility.Collapsed;
                            RegionalStruct Struct = RegionalDetail.Export();
                            int Index = RegionalDetail.GetIndex();
                            List_RegionalStruct[Index] = Struct;
                            if (IsDraw)
                            {
                                string Point1 = List_RegionalStruct[Index].Point1 = GetPoint(SPoint);
                                string Point2 = List_RegionalStruct[Index].Point2 = GetPoint(EPoint);

                                byte[] bytes = GetAreaInt((byte)(Index + 1), Point1, Point2);
                                SerialPort.Write(bytes, 0, bytes.Length); System.Threading.Thread.Sleep(100);
                                SerialPort.WriteLine("ATSZ=W"); System.Threading.Thread.Sleep(100);
                            }
                            Regional.Import_Value(List_RegionalStruct);
                            StructToConfig((string)DeviceList[Int].Value, "Regional");

                            Regional.Visibility = Visibility.Visible;
                            RegionalDetail.Visibility = Visibility.Collapsed;
                            Trace(new LogInfo()
                            {
                                Module = Flag,
                                Method = "RegionalDetail_EventHandler",
                                Category = "EventHandler",
                                Message = string.Format("区域设定:{0} 检测功能:{1} 移动事件侦测:{2}/{3} 人数设定:{4}/{5} 温度警报设定:{6}/High:{7}/Low:{8} 持续时间设定:{9} Email:{10}",
                                Struct.Text, Struct.Switch,
                                Struct.MoveEnable, Struct.Move,
                                Struct.NumberEnable, Struct.Number,
                                Struct.TemperatureEnable, Struct.High, Struct.Low,
                                Struct.Time,
                                Struct.Email)
                            });
                        }
                        break;
                    case "PeriodRemove": { PromptBox1.Import("是否删除此区域时间？", false, false, true, "PeriodRemove", -1); PromptBox1.Visibility = Visibility.Visible; } break;
                    case "PeriodCountOver": { } break;
                }
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "RegionalDetail_EventHandler", Category = "EventHandler", Message = ex.Message }); }
        }
        private void Preferences_EventHandler(object sender, EventInfo e)
        {
            try
            {
                switch (e.Option)
                {
                    case "Error": { Trace(new LogInfo() { File = "Error", Module = e.Value[0] as string, Method = e.Value[1] as string, Category = "EventHandler", Message = e.Value[2] as string }); } return;
                    case "Cancel": { } break;
                    case "Yes":
                        {
                            Struct4 = Preferences.Export(); StructToConfig((string)DeviceList[Int].Value, "Preferences");
                            if (SerialPort != null && SerialPort.IsOpen)
                            {
                                SerialPort.WriteLine(string.Format("ATSR={0:00},{1:000},{2:000},{3:00},{4:00}", Struct4.Shield, Struct4.Differ, Struct4.Pixels, Struct4.FPS, Struct4.FPS));
                            }
                            PromptBox1.Import("已保存"); PromptBox1.Visibility = Visibility.Visible;
                            Trace(new LogInfo()
                            {
                                Module = Flag,
                                Method = "Preferences_EventHandler",
                                Category = "EventHandler",
                                Message = string.Format("参数设定 高:{0} 像素:{1} 顶装:{2}/{3} 壁挂:{4}/{5} 噪声滤除:{6}/{7} 屏蔽圈数:{8}/{9} 物件显示条件:{10}/{11} 物件温差:{12}/{13}",
                              Struct4.Depth, Struct4.Pixels,
                              Struct4.TopEnable, Struct4.TopAngle,
                              Struct4.HangEnable, Struct4.HangAngle,
                              Struct4.NFEnable, Struct4.NF,
                              Struct4.ShieldEnable, Struct4.Shield,
                              Struct4.FPSEnable, Struct4.FPS,
                              Struct4.DifferEnable, Struct4.Differ)
                            });
                        }
                        break;
                }
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "Preferences_EventHandler", Category = "EventHandler", Message = ex.Message }); }
        }
        private void PromptBox_EventHandler(object sender, EventInfo e)
        {
            try
            {
                PromptBox PromptBox = sender as PromptBox; if (PromptBox == null) { return; }
                switch (e.Option)
                {
                    case "Error": { Trace(new LogInfo() { File = "Error", Module = e.Value[0] as string, Method = e.Value[1] as string, Category = "EventHandler", Message = e.Value[2] as string }); } return;
                    case "Confirm_PeriodRemove": { RegionalDetail.Period_Remove(); } break;
                    case "Confirm_None": { } break;
                    case "Yes_None": { } break;
                    case "No_None": { } break;
                    case "Timer_None": { } break;
                    //case "Border_SerialPort_Save":
                    case "Timer_SerialPort_Save": { SerialPort_Ini(); } return;
                    case "Yes_SerialPort_Delete":
                        {
                            DeleteConfig(DeviceList[Int]);
                            Struct1 = new DeviceStruct();
                            DeviceList.RemoveAt(Int); Device.Import_Ini(DeviceList); Device.Import_Value(Struct1);

                            List2[0] = new ThreadInfo() { };
                            SerialPort_Ini();
                        }
                        break;
                    case "No_SerialPort_Delete": { } break;
                    default: { return; }
                }
                PromptBox.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "PromptBox_EventHandler", Category = "EventHandler", Message = ex.Message }); }
        }

        private bool Device_Check()
        {
            Struct1 = Device.Export();
            if (Struct1.Switch == 0)
            {
                if (!Refer.IsIP(Struct1.IP)) { PromptBox2.Import("Wi-Fi IP Address格式错误！", true, false, true); PromptBox2.Visibility = Visibility.Visible; return true; }
                if (Struct1.Port == 0) { PromptBox2.Import("Wi-Fi Port 格式错误！", true, false, true); PromptBox2.Visibility = Visibility.Visible; return true; }
                this.List2[0].IP = Struct1.IP;
                this.List2[0].Port = Struct1.Port;
                this.List2[0].IsNet = true;
                this.List2[0].IsRest = true;
            }
            else if (Struct1.Switch == 1)
            {
                this.List2[0].COM = Struct1.COM;
                this.List2[0].BaudrRate = Struct1.BaudrRate;
                this.List2[0].DataBits = (Struct1.UARTConfig == "8N1") ? 8 : 7;
                this.List2[0].IsNet = false;
                this.List2[0].IsRest = true;
                //SerialPort_Ini();
                //SerialPort1_Ini();
                SendData(0);
            }
            return false;
        }
        private void PromptBoxStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try { PromptBox1.Visibility = PromptBox2.Visibility = Visibility.Collapsed; }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "PromptBoxStackPanel_MouseDown", Message = ex.Message }); }
        }
        #endregion
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton RadioButton = sender as RadioButton; if (RadioButton == null) { return; }
                //Monitor.Visibility = MenuBar.Visibility = ScrollViewer1.Visibility = ScrollViewer2.Visibility = Visibility.Collapsed;
                Monitor.Visibility = MenuBar.Visibility = Grid2.Visibility = Border1.Visibility = Visibility.Collapsed;
                switch (RadioButton.Name)
                {
                    case "Main": { Monitor.Visibility = Visibility.Visible; } break;
                    //case "Setting": { Monitor.Visibility = MenuBar.Visibility = ScrollViewer1.Visibility = Visibility.Visible; } break;
                    case "Setting": { Monitor.Visibility = MenuBar.Visibility = Grid2.Visibility = Visibility.Visible; } break;
                    //case "Devices": { ScrollViewer2.Visibility = Visibility.Visible; } break;
                    case "Devices": { Border1.Visibility = Visibility.Visible; } break;
                }
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "RadioButton_Click", Message = ex.Message }); }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsClose = true; this.Close();
        }
        private void Import_Value()
        {
            try
            {
                Device.Import_Value(Struct1); Device.Import_Value(Int);
                Screen.Import_Value(Struct2);
                Regional.Import_Value(List_RegionalStruct);
                //Preferences.Import_Value(Struct4);
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "Import_Value", Message = ex.Message }); }
        }
        private void GetColorMap()
        {
            try
            {
                switch (Struct2.Colormap)
                {
                    case "Jet": { CMap = ColorMap.Jet(); } break;
                    case "CMRmap": { CMap = ColorMap.CMRmap(); } break;
                }
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "GetColorMap", Message = ex.Message }); }
        }
        #region Config & Struct
        private PairStruct CreateConfig()
        {
            try
            {
                PairStruct Struct = new PairStruct();
                string Folder = @".\Config\";
                if (!System.IO.Directory.Exists(Folder)) { System.IO.Directory.CreateDirectory(Folder); }
                string Path = null;
                int Int = 1;
                while (true)
                {
                    Path = string.Format("{0}Config{1}.ini", Folder, Int);
                    if (System.IO.File.Exists(Path)) { Int++; continue; }
                    //else { System.IO.FileStream FileStream = System.IO.File.Create(Path); FileStream.Close(); FileStream.Dispose(); FileStream = null; }
                    Struct.Text = string.Format("连线设备{0}", Int); Struct.Value = Path;
                    break;
                }
                return Struct;
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "CreateConfig", Category = "Config", Message = ex.Message }); }
            return null;
        }
        private List<PairStruct> GetConfig()
        {
            List<PairStruct> List = new List<PairStruct>();
            try
            {
                string Folder = @".\Config\";
                if (!System.IO.Directory.Exists(Folder)) { System.IO.Directory.GetDirectories(Folder); return List; }
                System.IO.DirectoryInfo DirectoryInfo = new System.IO.DirectoryInfo(Folder);
                System.IO.FileInfo[] FileInfos = DirectoryInfo.GetFiles();
                foreach (System.IO.FileInfo Item in FileInfos)
                {
                    string Name = Item.Name;
                    string Extension = Item.Extension;
                    if (Extension.ToLower() == ".ini" && Name.StartsWith("Config"))
                    {
                        string Temp = Name.Replace("Config", "").Replace(Extension, "");
                        int Value = 0; int.TryParse(Temp, out Value); if (Value == 0) { continue; }
                        string Text = string.Format("连线设备{0}", Value);
                        string Path = Folder + Name;
                        List.Add(new PairStruct() { Text = Text, Value = Path });
                    }
                }
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "GetConfig", Category = "Config", Message = ex.Message }); }
            return List;
        }
        private void DeleteConfig(PairStruct PairStruct)
        {
            try
            {
                System.IO.FileInfo FileInfo = new System.IO.FileInfo((string)PairStruct.Value);
                FileInfo.Delete();
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "DeleteConfig", Category = "Config", Message = ex.Message }); }
        }
        private void ConfigToStruct(string Path, string Type = null)
        {
            try
            {
                Setup.Config(Path);
                if (Type == "Device" || Type == null)
                {
                    Struct1.Text = Setup.Device.Text;
                    Struct1.Switch = Setup.Device.Switch;
                    Struct1.IP = Setup.Device.IP;
                    Struct1.Port = Setup.Device.Port;
                    Struct1.COM = Setup.Device.COM;
                    Struct1.BaudrRate = Setup.Device.BaudrRate;
                    Struct1.UARTConfig = Setup.Device.UARTConfig;
                    Struct1.COM2 = Setup.Device.COM2;
                    Struct1.BaudrRate2 = Setup.Device.BaudrRate2;
                    Struct1.UARTConfig2 = Setup.Device.UARTConfig2;
                    Struct1.MaxValue = Setup.Device.MaxValue;
                    Struct1.MinValue = Setup.Device.MinValue;
                    Struct1.Regional = Setup.Device.Regional;
                }
                if (Type == "Screen" || Type == null)
                {
                    Struct2.Mode = Setup.Screen.Mode;
                    Struct2.Colormap = Setup.Screen.ColorMaps;
                }
                if (Type == "Regional" || Type == null)
                {
                    List_RegionalStruct = new List<RegionalStruct>();
                    int Int = Struct1.Regional;
                    for (int i = 0; i < Int; i++)
                    {
                        Setup.Regional.SetRegional(i + 1);
                        RegionalStruct Struc3 = new RegionalStruct();
                        Struc3.Text = Setup.Regional.Text;
                        Struc3.Switch = Setup.Regional.Switch;

                        Struc3.MoveEnable = Setup.Regional.MoveEnable;
                        Struc3.Move = Setup.Regional.Move;

                        Struc3.NumberEnable = Setup.Regional.NumberEnable;
                        Struc3.Number = Setup.Regional.Number;
                        Struc3.TemperatureEnable = Setup.Regional.TemperatureEnable;
                        Struc3.High = Setup.Regional.High;
                        Struc3.Low = Setup.Regional.Low;
                        Struc3.Time = Setup.Regional.Time;
                        Struc3.Email = Setup.Regional.Email;
                        Struc3.Point1 = Setup.Regional.Point1;
                        Struc3.Point2 = Setup.Regional.Point2;
                        string Str_Period = Setup.Regional.Period;
                        Struc3.List = new List<PeriodStruct>();
                        if (Str_Period != null)
                        {
                            string[] Array = Str_Period.Split(';');
                            foreach (string Item in Array)
                            {
                                string[] ItemArray = Item.Split(',');
                                if (ItemArray.Length < 4) { continue; }
                                PeriodStruct PeriodStruct = new PeriodStruct();
                                int Temp = 0;
                                int.TryParse(ItemArray[0], out Temp); PeriodStruct.Week1 = Temp;
                                int.TryParse(ItemArray[1], out Temp); PeriodStruct.Value1 = Temp;
                                int.TryParse(ItemArray[2], out Temp); PeriodStruct.Week2 = Temp;
                                int.TryParse(ItemArray[3], out Temp); PeriodStruct.Value2 = Temp;
                                Struc3.List.Add(PeriodStruct);
                            }
                        }
                        List_RegionalStruct.Add(Struc3);
                    }
                }
                if (Type == "Preferences" || Type == null)
                {
                    Struct4.Depth = Setup.Preferences.Depth;
                    Struct4.Pixels = Setup.Preferences.Pixels;
                    Struct4.TopEnable = Setup.Preferences.TopEnable;
                    Struct4.TopAngle = Setup.Preferences.TopAngle;
                    Struct4.HangEnable = Setup.Preferences.HangEnable;
                    Struct4.HangAngle = Setup.Preferences.HangAngle;
                    Struct4.NFEnable = Setup.Preferences.NFEnable;
                    Struct4.NF = Setup.Preferences.NF;
                    Struct4.ShieldEnable = Setup.Preferences.ShieldEnable;
                    Struct4.Shield = Setup.Preferences.Shield;
                    Struct4.FPSEnable = Setup.Preferences.FPSEnable;
                    Struct4.FPS = Setup.Preferences.FPS;
                    Struct4.DifferEnable = Setup.Preferences.DifferEnable;
                    Struct4.Differ = Setup.Preferences.Differ;
                }
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "GetConfig", Category = "Config", Message = ex.Message }); }
        }
        private void StructToConfig(string Path, string Type = null)
        {
            try
            {
                Setup.Config(Path);
                if (Type == "Device" || Type == null)
                {
                    Setup.Device.Text = (Struct1.Text == null) ? "" : Struct1.Text;
                    Setup.Device.Switch = Struct1.Switch;
                    Setup.Device.IP = Struct1.IP;
                    Setup.Device.Port = Struct1.Port;
                    Setup.Device.COM = (Struct1.COM == null) ? "" : Struct1.COM;
                    Setup.Device.BaudrRate = Struct1.BaudrRate;
                    Setup.Device.UARTConfig = (Struct1.UARTConfig == null) ? "" : Struct1.UARTConfig;
                }
                if (Type == "Screen" || Type == null)
                {
                    Setup.Screen.Mode = Struct2.Mode;
                    Setup.Screen.ColorMaps = (Struct2.Colormap == null) ? "" : Struct2.Colormap;
                }
                if (Type == "Regional" || Type == null)
                {
                    int Int = Struct1.Regional;
                    for (int i = 0; i < Int; i++)
                    {
                        Setup.Regional.SetRegional(i + 1);
                        Setup.Regional.Text = (List_RegionalStruct[i].Text == null) ? "" : List_RegionalStruct[i].Text;
                        Setup.Regional.Switch = List_RegionalStruct[i].Switch;
                        Setup.Regional.MoveEnable = List_RegionalStruct[i].MoveEnable;
                        Setup.Regional.Move = List_RegionalStruct[i].Move;
                        Setup.Regional.NumberEnable = List_RegionalStruct[i].NumberEnable;
                        Setup.Regional.Number = (List_RegionalStruct[i].Number == null) ? "" : List_RegionalStruct[i].Number;
                        Setup.Regional.TemperatureEnable = List_RegionalStruct[i].TemperatureEnable;
                        Setup.Regional.High = (List_RegionalStruct[i].High == null) ? "" : List_RegionalStruct[i].High;
                        Setup.Regional.Low = (List_RegionalStruct[i].Low == null) ? "" : List_RegionalStruct[i].Low;
                        Setup.Regional.Time = (List_RegionalStruct[i].Time == null) ? "" : List_RegionalStruct[i].Time;
                        Setup.Regional.Email = (List_RegionalStruct[i].Email == null) ? "" : List_RegionalStruct[i].Email;
                        Setup.Regional.Point1 = (List_RegionalStruct[i].Point1 == null) ? "" : List_RegionalStruct[i].Point1;
                        Setup.Regional.Point2 = (List_RegionalStruct[i].Point2 == null) ? "" : List_RegionalStruct[i].Point2;
                        string Str_Period = null;
                        foreach (PeriodStruct Item in List_RegionalStruct[i].List)
                        {
                            if (Str_Period == null) { Str_Period = string.Format("{0},{1},{2},{3}", Item.Week1, Item.Value1, Item.Week2, Item.Value2); }
                            else { Str_Period += string.Format(";{0},{1},{2},{3}", Item.Week1, Item.Value1, Item.Week2, Item.Value2); }
                        }
                        Setup.Regional.Period = (Str_Period == null) ? "" : Str_Period;
                    }
                }
                if (Type == "Preferences" || Type == null)
                {
                    Setup.Preferences.Depth = (Struct4.Depth == null) ? "" : Struct4.Depth;
                    Setup.Preferences.Pixels = Struct4.Pixels;
                    Setup.Preferences.TopEnable = Struct4.TopEnable;
                    Setup.Preferences.TopAngle = (Struct4.TopAngle == null) ? "" : Struct4.TopAngle;
                    Setup.Preferences.HangEnable = Struct4.HangEnable;
                    Setup.Preferences.HangAngle = (Struct4.HangAngle == null) ? "" : Struct4.HangAngle;
                    Setup.Preferences.NFEnable = Struct4.NFEnable;
                    Setup.Preferences.NF = Struct4.NF;
                    Setup.Preferences.ShieldEnable = Struct4.ShieldEnable;
                    Setup.Preferences.Shield = Struct4.Shield;
                    Setup.Preferences.FPSEnable = Struct4.FPSEnable;
                    Setup.Preferences.FPS = Struct4.FPS;
                    Setup.Preferences.DifferEnable = Struct4.DifferEnable;
                    Setup.Preferences.Differ = Struct4.Differ;
                }
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "SetConfig", Category = "Config", Message = ex.Message }); }
        }
        #endregion
        #region 圈選畫面
        protected class CanvasPoint
        {
            public int Index { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public string Text { get; set; }
            public double degree { get; set; }
        }
        private bool IsCanvas = false;
        private bool IsDraw = false;
        private Point SPoint = new Point();
        private Point EPoint = new Point();
        private double LineWitdh = 0.1;
        private GeometryGroup GeometryGroup = new GeometryGroup();
        private void SetPoint(string Point1, string Point2)
        {
            try
            {
                IsDraw = false;
                GeometryGroup.Children.Clear();
                if (Point1 == null || Point2 == null) { PathRect.Data = null; return; }
                string[] Array = Point1.Split(',');
                if (Array.Length < 2) { PathRect.Data = null; return; }
                double X1 = 0; double Y1 = 0; double X2 = 0; double Y2 = 0;
                double.TryParse(Array[0], out X1);
                double.TryParse(Array[1], out Y1);
                Array = Point2.Split(',');
                if (Array.Length < 2) { PathRect.Data = null; return; }
                double.TryParse(Array[0], out X2);
                double.TryParse(Array[1], out Y2);
                SPoint.X = X1; SPoint.Y = Y1;
                EPoint.X = X2; EPoint.Y = Y2;
                GeometryGroup.Children.Add(new EllipseGeometry(new Point() { X = X1, Y = Y1 }, LineWitdh, LineWitdh));
                GeometryGroup.Children.Add(new EllipseGeometry(new Point() { X = X2, Y = Y1 }, LineWitdh, LineWitdh));
                GeometryGroup.Children.Add(new EllipseGeometry(new Point() { X = X2, Y = Y2 }, LineWitdh, LineWitdh));
                GeometryGroup.Children.Add(new EllipseGeometry(new Point() { X = X1, Y = Y2 }, LineWitdh, LineWitdh));
                PathPoint.Data = GeometryGroup;
                //畫線條
                PathRect.Data = new RectangleGeometry(new Rect(new Point() { X = X1, Y = Y1 }, new Point() { X = X2, Y = Y2 }), 0.1, 0.1);
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Canvas", Method = "SetPoint", Message = ex.Message }); }
            finally { }
        }
        private string GetPoint(Point Point1)
        {
            return string.Format("{0},{1}", Point1.X, Point1.Y);
        }
        private byte[] GetAreaInt(byte Byte, string Point1, string Point2)
        {
            int Length = (ImgWidth * ImgHeight) / 8;
            byte[] Bytes = new byte[Length + 6];
            Bytes[0] = 65; Bytes[1] = 84; Bytes[2] = 83; Bytes[3] = 90; Bytes[4] = 61;       //"ATSZ=";
            Bytes[5] = Byte;
            try
            {
                if (Point1 == null || Point2 == null) { return Bytes; }
                string[] Array = Point1.Split(',');
                if (Array.Length < 2) { return Bytes; }
                double X1 = 0; double Y1 = 0; double X2 = 0; double Y2 = 0;
                double.TryParse(Array[0], out X1);
                double.TryParse(Array[1], out Y1);
                Array = Point2.Split(',');
                if (Array.Length < 2) { return Bytes; }
                double.TryParse(Array[0], out X2);
                double.TryParse(Array[1], out Y2);
                //先高 再寬
                for (int i = 0; i < ImgHeight; i++)
                {
                    for (int j = 0; j < ImgWidth; j++)
                    {
                        int Temp = (i * ImgWidth + j) / 8;
                        int Pow = 7 - ((i * ImgWidth + j) % 8);
                        if (i >= Y1 && i <= Y2 && j >= X1 && j <= X2)
                        {
                            Bytes[Temp + 6] += (byte)(1 * Math.Pow(2, Pow));
                        }
                    }
                }
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Canvas", Method = "GetAreaInt", Message = ex.Message }); }
            finally { }
            return Bytes;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                IsCanvas = true;
                SPoint = e.GetPosition(Canvas);
                GeometryGroup.Children.Clear();
                GeometryGroup.Children.Add(new EllipseGeometry(SPoint, LineWitdh, LineWitdh));
                PathPoint.Data = GeometryGroup;
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Canvas", Method = "Canvas_MouseDown", Message = ex.Message }); }
            finally { }
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!IsCanvas) { return; }
                //畫點
                Point Point = e.GetPosition(Canvas);
                GeometryGroup.Children.Clear();
                GeometryGroup.Children.Add(new EllipseGeometry(new Point() { X = SPoint.X, Y = SPoint.Y }, LineWitdh, LineWitdh));
                GeometryGroup.Children.Add(new EllipseGeometry(new Point() { X = Point.X, Y = SPoint.Y }, LineWitdh, LineWitdh));
                GeometryGroup.Children.Add(new EllipseGeometry(new Point() { X = Point.X, Y = Point.Y }, LineWitdh, LineWitdh));
                GeometryGroup.Children.Add(new EllipseGeometry(new Point() { X = SPoint.X, Y = Point.Y }, LineWitdh, LineWitdh));
                PathPoint.Data = GeometryGroup;

                //畫線條
                PathRect.Data = new RectangleGeometry(new Rect(SPoint, Point), 0.1, 0.1);
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Canvas", Method = "Canvas_MouseMove", Message = ex.Message }); }
            finally { }
        }
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!IsCanvas) { return; }
                IsCanvas = false; IsDraw = true;
                EPoint = e.GetPosition(Canvas);
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Canvas", Method = "Canvas_MouseUp", Message = ex.Message }); }
            finally { }
        }
        private void DrawPoint(double X, double Y, string Text)
        {
            try
            {
                StackPanel StackPanel = new StackPanel(); StackPanel.Orientation = Orientation.Horizontal;
                Image Image = new Image(); Image.Style = this.FindResource("ImageStyle") as Style;
                TextBlock TextBlock = new TextBlock(); TextBlock.Style = this.FindResource("TextBlockStyle") as Style; TextBlock.Text = Text;
                StackPanel.Children.Add(Image); StackPanel.Children.Add(TextBlock);
                Canvas.SetLeft(StackPanel, X);
                Canvas.SetTop(StackPanel, Y);
                Canvas.Children.Add(StackPanel);
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Canvas", Method = "DrawPoint", Message = ex.Message }); }
            finally { }
        }
        private void DrawPoint_Clear()
        {
            try
            {
                int Count = Canvas.Children.Count;
                for (int i = (Count - 1); i > 0; i--)
                {
                    StackPanel StackPanel = Canvas.Children[i] as StackPanel; if (StackPanel == null) { continue; }
                    Canvas.Children.RemoveAt(i);
                    StackPanel = null;
                }
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Canvas", Method = "DrawPoint_Clear", Message = ex.Message }); }
            finally { }
        }
        #endregion
        private System.Windows.Media.Imaging.BitmapImage ToBitmapImage(System.Drawing.Bitmap Bitmap)
        {
            System.Windows.Media.Imaging.BitmapImage BitmapImage = new System.Windows.Media.Imaging.BitmapImage();
            using (System.IO.MemoryStream MemoryStream = new System.IO.MemoryStream())
            {
                Bitmap.Save(MemoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                BitmapImage.BeginInit();
                BitmapImage.StreamSource = MemoryStream;
                //BitmapImage.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.Default;
                BitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                //BitmapImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                BitmapImage.EndInit();
                //BitmapImage.Freeze();
            }
            return BitmapImage;
        }
        public System.Drawing.Bitmap AdjustTobBlur(System.Drawing.Bitmap bitmap, int effectRradius)
        {
            // 整體圖片跑 Pixel 迴圈
            for (int heightOffset = 0; heightOffset < bitmap.Height; heightOffset++)
            {
                for (int widthOffset = 0; widthOffset < bitmap.Width; widthOffset++)
                {
                    // 負責計算平均值
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    // 計算傳入影響範圍內 的 RGB 平均
                    for (int x = widthOffset; (x < widthOffset + effectRradius && x < bitmap.Width); x++)
                    {
                        for (int y = heightOffset; (y < heightOffset + effectRradius && y < bitmap.Height); y++)
                        {
                            System.Drawing.Color pixel = bitmap.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    // 計算個別平均
                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;


                    // 寫回入新圖片
                    for (int x = widthOffset; (x < widthOffset + effectRradius && x < bitmap.Width); x++)
                    {
                        for (int y = heightOffset; (y < heightOffset + effectRradius && y < bitmap.Height); y++)
                        {
                            System.Drawing.Color newColor = System.Drawing.Color.FromArgb(avgR, avgG, avgB);
                            bitmap.SetPixel(x, y, newColor);
                        }
                    }

                }
            }
            return bitmap;
        }
        private System.Windows.Media.Imaging.BitmapImage HexToImage_Mode3(string[] hexValuesSplit, int MaxValue, int MinValue)
        {
            int Height = ImgHeight;
            int Width = ImgWidth;
            System.Drawing.Bitmap Bitmap = new System.Drawing.Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //將Bitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的Bitmap
            System.Drawing.Imaging.BitmapData BitmapData = Bitmap.LockBits(new System.Drawing.Rectangle(0, 0, Width, Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            int BufferLength = BitmapData.Stride * Height;
            byte[] Buffer = new byte[BufferLength];
            int m = 255 / ((MaxValue - MinValue) == 0 ? 1 : (MaxValue - MinValue));
            int Length = Height * Width;
            for (int i = 0; i < Length; i++)
            {
                int Int = Convert.ToInt32(hexValuesSplit[i], 16);
                int colorIndex = (Int - MinValue) * m;
                colorIndex = (colorIndex > 255) ? 255 : ((colorIndex < 0) ? 0 : colorIndex);
                byte R = (byte)CMap[colorIndex, 1];
                byte G = (byte)CMap[colorIndex, 2];
                byte B = (byte)CMap[colorIndex, 3];
                Buffer[i * 3] = B;
                Buffer[i * 3 + 1] = G;
                Buffer[i * 3 + 2] = R;
            }
            System.Runtime.InteropServices.Marshal.Copy(Buffer, 0, BitmapData.Scan0, BufferLength);
            //解鎖位圖
            Bitmap.UnlockBits(BitmapData);
            return ToBitmapImage(Bitmap);
        }
        private System.Windows.Media.Imaging.BitmapImage HexToImage_Mode0(string[] hexValuesSplit, int MaxValue, int MinValue)
        {
            int Height = ImgHeight;
            int Width = ImgWidth;
            System.Drawing.Bitmap Bitmap = new System.Drawing.Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //將Bitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的Bitmap
            System.Drawing.Imaging.BitmapData BitmapData = Bitmap.LockBits(new System.Drawing.Rectangle(0, 0, Width, Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            int BufferLength = BitmapData.Stride * Height;
            byte[] Buffer = new byte[BufferLength];
            int Length = Height * Width;
            for (int i = 0; i < Length; i++)
            {
                byte R = 201;
                byte G = 201;
                byte B = 201;
                int Int = Convert.ToInt32(hexValuesSplit[i], 16);
                if (Int != 255) { R = 102; G = 205; B = 170; }
                Buffer[i * 3] = B;
                Buffer[i * 3 + 1] = G;
                Buffer[i * 3 + 2] = R;
            }
            System.Runtime.InteropServices.Marshal.Copy(Buffer, 0, BitmapData.Scan0, BufferLength);
            //解鎖位圖
            Bitmap.UnlockBits(BitmapData);
            return ToBitmapImage(Bitmap);
        }
        private System.Windows.Media.Imaging.BitmapImage HexToImage_Mode1(string[] hexValuesSplit, int MaxValue, int MinValue)
        {
            int Height = ImgHeight;
            int Width = ImgWidth;
            System.Drawing.Bitmap Bitmap = new System.Drawing.Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //將Bitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的Bitmap
            System.Drawing.Imaging.BitmapData BitmapData = Bitmap.LockBits(new System.Drawing.Rectangle(0, 0, Width, Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            int BufferLength = BitmapData.Stride * Height;
            byte[] Buffer = new byte[BufferLength];
            int m = 255 / ((MaxValue - MinValue) == 0 ? 1 : (MaxValue - MinValue));
            int Length = Height * Width;
            for (int i = 0; i < Length; i++)
            {
                byte R = 201;
                byte G = 201;
                byte B = 201;
                int Int = 0;
                int Temp = 0;
                Temp = Convert.ToInt32(hexValuesSplit[i * 2], 16); if (Temp != 255) { Int += Temp * 255; }
                Temp = Convert.ToInt32(hexValuesSplit[i * 2 + 1], 16); if (Temp != 255) { Int += Temp; }
                if (Int > 0)
                {
                    Int /= 10;
                    int colorIndex = (Int - MinValue) * m;
                    colorIndex = (colorIndex > 255) ? 255 : ((colorIndex < 0) ? 0 : colorIndex);
                    R = (byte)CMap[colorIndex, 1];
                    G = (byte)CMap[colorIndex, 2];
                    B = (byte)CMap[colorIndex, 3];
                }
                Buffer[i * 3] = B;
                Buffer[i * 3 + 1] = G;
                Buffer[i * 3 + 2] = R;
            }
            System.Runtime.InteropServices.Marshal.Copy(Buffer, 0, BitmapData.Scan0, BufferLength);
            //解鎖位圖
            Bitmap.UnlockBits(BitmapData);
            return ToBitmapImage(Bitmap);
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
            public int Int { get; set; }
            public string Module { get; set; }
            public string Category { get; set; }
            public string Option { get; set; }  //即是 Option
            public string Message { get; set; }

            public string Text { get; set; }
            public string IP { get; set; }
            public int Port { get; set; }
            public string COM { get; set; }
            public int BaudrRate { get; set; }
            public int DataBits { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
            public bool IsNet { get; set; }
            public bool IsRest { get; set; }
            public bool IsConnect { get; set; } //連線狀態
            public bool IsHide { get; set; }    //顯示狀態
            public bool IsError { get; set; }   //連線狀態
        }
        private void SendData(string Command)
        {
            try
            {
                if (SerialPort1 == null) { return; }
                SerialPort1.WriteLine(Command);
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "SendData", Message = ex.Message }); }
        }
        private void SendData(int Int)
        {
            try
            {
                ThreadInfo _Info = List2[Int]; if (_Info == null) { return; }
                ThreadInfo Info = new ThreadInfo();
                Info.Int = Int;
                Info.IP = _Info.IP;
                Info.Port = _Info.Port;
                Info.COM = _Info.COM;
                Info.BaudrRate = _Info.BaudrRate;
                Info.DataBits = _Info.DataBits;
                Info.IsNet = _Info.IsNet;
                Info.IsRest = _Info.IsRest;
                Info.IsConnect = _Info.IsConnect;
                Info.IsHide = _Info.IsHide;
                Info.IsError = _Info.IsError;
                Info.Option = "Data";
                if (List3.Count == 0) { List3.Add(Info); }
                else { List3[0] = Info; }
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "SendData", Message = ex.Message }); }
        }
        private void SendData(string Module, string Option, string Command)
        {
            try
            {
                List3[0].Module = Module;
                List3[0].Option = Option;
                List3[0].Key = Command;
            }
            catch (Exception ex) { Trace(new LogInfo() { File = "Error", Module = Flag, Method = "SendData", Message = ex.Message }); }
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
        private void Thread3_Run()
        {
            Trace(new LogInfo() { Module = Flag, Category = "Thread", Method = "Thread3_Run", Message = "Timer" });
            bool IsError = false;
            System.IO.Ports.SerialPort SerialPort = null;
            ThreadInfo Info = new ThreadInfo();
            while (Thread3 != null && Thread3.IsAlive)
            {
                try
                {
                    if (IsClose) { break; }
                    if (List3.Count == 0) { System.Threading.Thread.Sleep(3000); continue; }
                    Info = List3[0]; if (Info == null || Info.IsNet) { System.Threading.Thread.Sleep(3000); continue; }
                    Info.Category = "Initial";
                    if (Info.IsRest)
                    {
                        if (SerialPort != null) { SerialPort.Close(); SerialPort.Dispose(); SerialPort = null; }
                        if (SerialPort == null)
                        {
                            SerialPort = new System.IO.Ports.SerialPort(Info.COM, Info.BaudrRate, System.IO.Ports.Parity.None, Info.DataBits, System.IO.Ports.StopBits.One);
                            SerialPort.ReadTimeout = 1000;
                        }
                        Info.IsRest = false;
                    }
                    Info.Category = "Connect"; if (!SerialPort.IsOpen) { SerialPort.Open(); }
                    int BytesToRead = SerialPort.BytesToRead; if (BytesToRead <= 0) { System.Threading.Thread.Sleep(100); continue; } //if (BytesToRead < 768) { System.Threading.Thread.Sleep(100); continue; }

                    if (Info.Key != null) { SerialPort.Write(Info.Key); Info.Key = null; };
                    byte[] Buffer = new byte[BytesToRead];
                    Info.Category = "Receive"; SerialPort.Read(Buffer, 0, BytesToRead);
                    //if (SerialPort != null) { SerialPort.Close(); }
                    //Console.WriteLine("{0}/{1}", BytesToRead, Encoding.GetString(Buffer));
                    Info.Category = "Category"; List5.Add(Buffer);
                    Info.Category = "Finish";
                }
                catch (Exception ex) { if (!IsError) { IsError = true; Info.Message = ex.Message; Trace(Info); } }
                finally { System.Threading.Thread.Sleep(100); }
            }
            if (SerialPort != null) { SerialPort.Close(); SerialPort.Dispose(); SerialPort = null; }
        }
        private void Thread4_Run()
        {
            Trace(new LogInfo() { Module = Flag, Category = "Thread", Method = "Thread4_Run", Message = "Timer" });
            bool IsError = false;
            System.IO.Ports.SerialPort SerialPort = null;
            ThreadInfo Info = new ThreadInfo();
            while (Thread4 != null && Thread4.IsAlive)
            {
                try
                {
                    if (List4.Count == 0)
                    {
                        if (IsClose) { break; }
                        else if (SerialPort != null) { SerialPort.Close(); SerialPort.Dispose(); SerialPort = null; }
                        System.Threading.Thread.Sleep(1000); continue;
                    }
                    Info = List4[0]; List4.RemoveAt(0); if (Info == null) { continue; }
                    Info.Category = "Initial";
                    //byte[] buffer = Encoding.GetBytes(Info.Key); if (buffer == null) { continue; }
                    if (SerialPort == null)
                    {
                        SerialPort = new System.IO.Ports.SerialPort(Info.COM, Info.BaudrRate, System.IO.Ports.Parity.None, Info.DataBits, System.IO.Ports.StopBits.One);
                        SerialPort.WriteTimeout = 1000; SerialPort.ReadTimeout = 5000;
                    }
                    Info.Category = "Connect"; if (!SerialPort.IsOpen) { SerialPort.Open(); }
                    Info.Category = "Send"; SerialPort.Write(Info.Key); System.Threading.Thread.Sleep(1000);
                    int BytesToRead = SerialPort.BytesToRead;
                    byte[] buffer = new byte[BytesToRead];
                    Info.Category = "Receive"; SerialPort.Read(buffer, 0, BytesToRead);
                    Info.Category = "Encode"; Info.Value = Encoding.GetString(buffer, 0, BytesToRead);
                    Info.Category = "Category";
                    Info.Category = "Finish";
                    //ATGS=3
                    //ATGR=02,040,006,03,03
                    //ATGF=243,1,1,13,11,27,5,1
                    //ATGO=000.0,000.0,0,000.0,000.0,0,000.0,000.0,0,010.0
                    //Console.WriteLine("{0} {1}", Info.Key, Info.Value);
                }
                catch (Exception ex) { if (!IsError) { IsError = true; Info.Message = ex.Message; Trace(Info); } }
                finally { System.Threading.Thread.Sleep(1000); }
            }
            if (SerialPort != null) { SerialPort.Close(); SerialPort.Dispose(); SerialPort = null; }
        }
        private void Thread5_Run()
        {
            Trace(new LogInfo() { Module = Flag, Category = "Thread", Method = "Thread5_Run", Message = "Timer" });
            bool IsError = false;
            int StartInt = 0;
            int EndInt = 0;
            byte[] ByteRemain = new byte[0];
            while (Thread5 != null && Thread5.IsAlive)
            {
                try
                {
                    if (List5.Count == 0) { if (IsClose) { break; } System.Threading.Thread.Sleep(200); continue; }
                    byte[] Buffer = List5[0]; List5.RemoveAt(0); if (Buffer == null) { continue; }
                    if (ByteRemain.Length > 10240) { ByteRemain = new byte[0]; }
                    byte[] ByteArray = new byte[Buffer.Length + ByteRemain.Length];
                    Array.Copy(ByteRemain, 0, ByteArray, 0, ByteRemain.Length);
                    Array.Copy(Buffer, 0, ByteArray, ByteRemain.Length, Buffer.Length);
                    int Index = 0; StartInt = -1; EndInt = -1;
                    foreach (byte Item in ByteArray)
                    {
                        if (Item == s && EndInt > -1)
                        {
                            StartInt = Index;                            //Console.WriteLine("IsObj"); 
                            Buffer = new byte[StartInt - EndInt - 1];                           //去除sS
                            Array.Copy(ByteArray, EndInt + 1, Buffer, 0, Buffer.Length);        //去除eE
                            //Console.WriteLine("Buffer.Length {0}/{1}", Buffer.Length, Encoding.GetString(Buffer));
                            List7.Add(Buffer);
                            EndInt = -1;
                        }       //已經發現 E項 發現s項 obj  結束
                        else if (Item == s && EndInt == -1) { StartInt = Index; }               //尚未發現 E項 發現s項 Data 開始
                        else if (Item == E && StartInt > -1)
                        {
                            EndInt = Index;                            //Console.WriteLine("IsData");
                            Buffer = new byte[EndInt - StartInt - 3];                           //去除eE
                            Array.Copy(ByteArray, StartInt + 2, Buffer, 0, Buffer.Length);      //去除sS
                            if (Buffer.Length >= 768) { List6.Add(Buffer); }
                            StartInt = -1;
                        } //已經發現 s項 發現E項 Data 結束
                        else if (Item == E && StartInt == -1) { EndInt = Index; }               //尚未發現 s項 發現E項 obj  開始
                        //else if (Item == 0) { Array.Clear(ByteArray, Index, 1); }
                        Index++;
                    }
                    int Int = Math.Max(StartInt, EndInt); //if (Int == -1) { Int = 0; }
                    ByteRemain = new byte[ByteArray.Length - Int];
                    Array.Copy(ByteArray, Int, ByteRemain, 0, ByteArray.Length - Int);
                    if (IsError) { IsError = false; Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Thread", Method = "Thread5_Run", Message = "Troubleshoot" }); }
                }
                catch (Exception ex) { if (!IsError) { IsError = true; Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Thread", Method = "Thread5_Run", Message = ex.Message }); } }
                finally { }
            }
        }
        private void Thread6_Run()
        {
            Trace(new LogInfo() { Module = Flag, Category = "Thread", Method = "Thread6_Run", Message = "Timer" });
            bool IsError = false;
            while (Thread6 != null && Thread6.IsAlive)
            {
                try
                {
                    if (List6.Count == 0) { if (IsClose) { break; } System.Threading.Thread.Sleep(200); continue; }
                    byte[] Buffer = List6[0]; List6.RemoveAt(0); if (Buffer == null) { continue; }
                    //string Value = Encoding.GetString(Buffer); Console.WriteLine(Value);
                    string hexValue = BitConverter.ToString(Buffer); //Console.WriteLine("{0}/{1}", Buffer.Length, hexValue);
                    Trace(new LogInfo() { File = "Data", Message = hexValue });
                    string[] hexValuesSplit = hexValue.Split('-'); if (hexValuesSplit.Length < 768) { continue; }
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        if (Mode == 0 && hexValuesSplit.Length == 768) { this.DataContext = null; this.DataContext = _DataInfo.GetDataInfo(HexToImage_Mode0(hexValuesSplit, Struct1.MaxValue, Struct1.MinValue)); }
                        else if (Mode == 1 && hexValuesSplit.Length == 1536) { this.DataContext = null; this.DataContext = _DataInfo.GetDataInfo(HexToImage_Mode1(hexValuesSplit, Struct1.MaxValue, Struct1.MinValue)); }
                        else if (Mode == 3 && hexValuesSplit.Length == 768) { this.DataContext = null; this.DataContext = _DataInfo.GetDataInfo(HexToImage_Mode3(hexValuesSplit, Struct1.MaxValue, Struct1.MinValue)); }
                    }));
                    if (IsError) { IsError = false; Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Thread", Method = "Thread6_Run", Message = "Troubleshoot" }); }
                }
                catch (Exception ex) { if (!IsError) { IsError = true; Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Thread", Method = "Thread6_Run", Message = ex.Message }); } }
                finally { }
            }
        }
        private void Thread7_Run()
        {
            Trace(new LogInfo() { Module = Flag, Category = "Thread", Method = "Thread7_Run", Message = "Timer" });
            bool IsError = false;
            while (Thread7 != null && Thread7.IsAlive)
            {
                try
                {
                    if (List7.Count == 0) { if (IsClose) { break; } System.Threading.Thread.Sleep(200); continue; }
                    byte[] Buffer = List7[0]; List7.RemoveAt(0); if (Buffer == null) { continue; }
                    string Value = Encoding.GetString(Buffer); //Console.WriteLine(Value);
                    Trace(new LogInfo() { File = "Data", Message = Value });
                    int objInt = 0; int degreeInt = 0;
                    double DoubleTemp = 0; double Avg = 0; double High = 0; double Low = 0;
                    int Person = 0;
                    List<CanvasPoint> List = new List<CanvasPoint>();
                    while (objInt > -1)
                    {
                        objInt = Value.IndexOf("objNo. ", degreeInt); if (objInt == -1) { break; }
                        degreeInt = Value.IndexOf("degree_h", objInt); if (degreeInt == -1) { break; }

                        string Temp = Value.Substring(objInt + 7, degreeInt - objInt + 1); //Console.WriteLine(Temp);
                        int Colon = Temp.IndexOf(':'); if (Colon == -1) { break; }
                        int Brackets_L = Temp.IndexOf('('); if (Brackets_L == -1) { break; }
                        int Comma = Temp.IndexOf(","); if (Comma == -1) { break; }
                        int Brackets_R = Temp.IndexOf(')'); if (Brackets_R == -1) { break; }
                        int Equal = Temp.IndexOf('='); if (Equal == -1) { break; }
                        int Degree = Temp.IndexOf("degree_h"); if (Degree == -1) { break; }
                        //CanvasPoint
                        CanvasPoint CanvasPoint = new CanvasPoint();
                        int Int = 0;
                        int.TryParse(Temp.Substring(0, Colon), out Int); CanvasPoint.Index = Person = Int;
                        int.TryParse(Temp.Substring(Brackets_L + 1, Comma - Brackets_L - 1), out Int); CanvasPoint.X = Int;
                        int.TryParse(Temp.Substring(Comma + 1, Brackets_R - Comma - 1), out Int); CanvasPoint.Y = Int;
                        double Double = 0;
                        double.TryParse(Temp.Substring(Equal + 1, Degree - Equal - 1), out Double); Double = Double / 10.0f; CanvasPoint.degree = Double;
                        CanvasPoint.Text = string.Format("h,{0:00.0}", Double);
                        List.Add(CanvasPoint);

                        if (High == 0 && Low == 0) { High = Low = Double; }
                        else if (High < Double) { High = Double; }
                        else if (Low > Double) { Low = Double; }
                        DoubleTemp += Double;
                    }
                    if (Person > 0) { Avg = Math.Round(DoubleTemp / Person, 1); }
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        //DataContext = null;       
                        //DataContext = _DataInfo.GetDataInfo(Avg, High, Low, Person);
                        _DataInfo.GetDataInfo(Avg, High, Low, Person);
                        DrawPoint_Clear();
                        foreach (CanvasPoint Item in List) { DrawPoint(Item.X, Item.Y, Item.Text); }
                    }));
                    if (IsError) { IsError = false; Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Thread", Method = "Thread7_Run", Message = "Troubleshoot" }); }
                }
                catch (Exception ex) { if (!IsError) { IsError = true; Trace(new LogInfo() { File = "Error", Module = Flag, Category = "Thread", Method = "Thread7_Run", Message = ex.Message }); } }
                finally { }
            }
        }
        #endregion
        #endregion
        #region Command
        #endregion
    }
}
