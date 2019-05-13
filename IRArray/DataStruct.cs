using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IRArray
{
    class DataStruct
    {
    }
    // ========== 系統 ==========
    public class EventInfo
    {
        #region Property
        public EventInfo(string Module, string Method, string Option, params object[] Value)
        {
            this.Module = Module;
            this.Method = Method;
            this.Option = Option;
            this.Value = Value;
        }
        public EventInfo(string Option, params object[] Value)
        {
            this.Option = Option;
            this.Value = Value;
        }
        public string Module { get; set; }
        public string Method { get; set; }
        public string Option { get; set; }
        public object[] Value { get; set; }
        #endregion
    }
    public class PairStruct
    {
        #region Property
        public string Text { get; set; }        //名稱
        public object Value { get; set; }       //數值
        #endregion
    }
    public class DeviceStruct
    {
        #region Property
        private string _Text = "";
        public string Text { get { return _Text; } set { _Text = value; } }        //名稱
        public int Switch { get; set; }         //0:Wi-fi 1:UART
        private string _IP = "";
        public string IP { get { return _IP; } set { _IP = value; } }          //IP Address
        public int Port { get; set; }           //Port
        private string _COM = "";
        public string COM { get { return _COM; } set { _COM = value; } }         //COM Port
        public int BaudrRate { get; set; }      //Baudr Rate
        private string _UARTConfig = "";
        public string UARTConfig { get { return _UARTConfig; } set { _UARTConfig = value; } }  //UART Config
        private string _COM2 = "";
        public string COM2 { get { return _COM2; } set { _COM2 = value; } }         //COM Port
        public int BaudrRate2 { get; set; }      //Baudr Rate
        private string _UARTConfig2 = "";
        public string UARTConfig2 { get { return _UARTConfig2; } set { _UARTConfig2 = value; } }  //UART Config
        private int _Regional = 16;
        private int _MaxValue = 38;
        public int MaxValue { get { return _MaxValue; } set { _MaxValue = value; } }         //MaxValue
        private int _MinValue = 27;
        public int MinValue { get { return _MinValue; } set { _MinValue = value; } }         //MaxValue
        public int Regional { get { return _Regional; } set { _Regional = value; } }       //Regional 數量
        #endregion
    }
    public class ScreenStruct
    {
        #region Property
        public int Mode { get; set; }                   //模式
        private string _Colormap = "Jet";            //Color Map
        public string Colormap { get { return _Colormap; } set { _Colormap = value; } }            //Color Map
        #endregion
    }
    public class RegionalStruct
    {
        #region Property
        public string Text { get; set; }                //名稱
        public bool Switch { get; set; }                //Switch
        public bool MoveEnable { get; set; }            //事件 On/Off
        private int _Move = 1;
        public int Move
        {
            get { return _Move; }
            set
            {
                if (value < 1) { _Move = 1; }
                else if (value > 10) { _Move = 10; }
                else { _Move = value; }
            }
        }                              //事件
        public bool NumberEnable { get; set; }          //人數 On/Off
        public string Number { get; set; }              //人數
        public bool TemperatureEnable { get; set; }     //溫度 On/Off
        public string High { get; set; }                //最高溫度
        public string Low { get; set; }                 //最低溫度
        public string Time { get; set; }                //持續時間
        public string Email { get; set; }               //E-mail
        public string Point1 { get; set; }              //偵測區域點1
        public string Point2 { get; set; }              //偵測區域點2
        public List<PeriodStruct> List { get; set; }    //區域時間
        #endregion
    }
    public class PreferencesStruct
    {
        #region Property
        public string Depth { get; set; }               //高
        public int Pixels { get; set; }                 //像素
        public bool TopEnable { get; set; }             //頂裝 On/Off
        public string TopAngle { get; set; }            //頂裝角度
        public bool HangEnable { get; set; }            //壁掛 On/Off
        public string HangAngle { get; set; }           //壁掛角度
        public bool NFEnable { get; set; }              //噪聲濾除
        private int _NF = 1;
        public int NF
        {
            get { return _NF; }
            set
            {
                if (value < 1) { _NF = 1; }
                else if (value > 5) { _NF = 5; }
                else { _NF = value; }
            }
        }                                //噪聲濾除值
        public bool ShieldEnable { get; set; }          //屏蔽圈數               
        private int _Shield = 1;
        public int Shield
        {
            get { return _Shield; }
            set
            {
                if (value < 1) { _Shield = 1; }
                else if (value > 3) { _Shield = 3; }
                else { _Shield = value; }
            }
        }                            //屏蔽圈數值
        public bool FPSEnable { get; set; }             //物件顯示條件              
        private int _FPS = 1;
        public int FPS
        {
            get { return _FPS; }
            set
            {
                if (value < 1) { _FPS = 1; }
                else if (value > 3) { _FPS = 3; }
                else { _FPS = value; }
            }
        }                               //物件顯示條件值
        public bool DifferEnable { get; set; }          //物件溫度
        private int _Differ = 0;
        public int Differ
        {
            get { return _Differ; }
            set
            {
                if (value < 5) { _Differ = 5; }
                else if (value > 50) { _Differ = 50; }
                else { _Differ = value; }
            }
        }                         //物件溫度值
        #endregion
    }
    public class PeriodStruct
    {
        #region Property
        private int _Week1 = 0;
        public int Week1
        {
            get { return _Week1; }
            set
            {
                if (value < 0) { _Week1 = 0; }
                else if (value > 6) { _Week1 = 6; }
                else { _Week1 = value; }
            }
        }        //星期
        private int _Value1 = 0;
        public int Value1
        {
            get { return _Value1; }
            set
            {
                if (value < 0) { _Value1 = 0; }
                else if (value > 84399) { _Value1 = 84399; }
                else { _Value1 = value; }
            }
        }       //時間
        private int _Week2 = 0;
        public int Week2
        {
            get { return _Week2; }
            set
            {
                if (value < 0) { _Week2 = 0; }
                else if (value > 6) { _Week2 = 6; }
                else { _Week2 = value; }
            }
        }        //星期
        private int _Value2 = 0;
        public int Value2
        {
            get { return _Value2; }
            set
            {
                if (value < 0) { _Value2 = 0; }
                else if (value > 84399) { _Value2 = 84399; }
                else { _Value2 = value; }
            }
        }       //時間
        #endregion
    }
    public class Refer
    {
        public static bool IsIP(string IPAddress)
        {
            Regex NumberPattern = new Regex(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$");
            if (IPAddress != null && NumberPattern.IsMatch(IPAddress)) return true;
            return false;
        }
    }
}
