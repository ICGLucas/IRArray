using System;
using System.Runtime.InteropServices;

namespace IRArray
{
    public class IniConfig
    {
        /*
         * Win32 API
         * 因為是Win32 API 所以必須使用靜態宣告DLL
         * 並將所需用到的Function進行宣告
         */
        [DllImport("Kernel32.dll")]
        public static extern bool WritePrivateProfileString(byte[] Section, byte[] Key, byte[] Value, string FilePath);
        [DllImport("Kernel32.dll")]
        public static extern int GetPrivateProfileString(byte[] Section, byte[] Key, byte[] DefaultValue, byte[] RetVal, int Size, string FilePath);
        [DllImport("Kernel32.dll")]
        private static extern int GetPrivateProfileInt(string Section, string Key, int DefaultValue, string FilePath);

        /*
         * 讀取檔案位置
         * 預設 同目錄的Config.ini
         */
        public static void Config(string FilePath)
        {
            IniConfig.FilePath = FilePath;
        }
        private static string FilePath = @".\Config.ini";
        private static System.Text.Encoding Encoding = System.Text.Encoding.GetEncoding(950);
        public static string GetString(string Section, string Key, string DefaultValue)
        {
            return ReadString(Section, Key, DefaultValue, FilePath);
        }
        public static void WriteString(string Section, string Key, string Value)
        {
            WriteString(Section, Key, Value, FilePath);
        }
        public static bool GetBoolean(string Section, string Key, bool DefaultValue)
        {
            return (GetPrivateProfileInt(Section, Key, (DefaultValue ? 1 : 0), FilePath) == 1);
        }
        public static void WriteBoolean(string Section, string Key, bool Value)
        {
            WriteString(Section, Key, (Value ? "1" : "0"), FilePath);
        }
        public static int GetInteger(string Section, string Key, int DefaultValue)
        {
            return GetPrivateProfileInt(Section, Key, DefaultValue, FilePath);
        }
        public static void WriteInteger(string Section, string Key, int Value)
        {
            WriteString(Section, Key, Value.ToString(), FilePath);
        }
        public static double GetDouble(string Section, string Key, double DefaultValue)
        {
            double Double;
            string Temp = ReadString(Section, Key, null, FilePath);
            return (Temp == null || !double.TryParse(Temp, out Double)) ? DefaultValue : Double;
        }
        public static void WriteDouble(string Section, string Key, double Value)
        {
            WriteString(Section, Key, Value.ToString(), FilePath);
        }
        public static string ReadString(string SectionName, string KeyName, string Default, string FilePath, int Size = 1024)
        {
            byte[] Buffer = new byte[Size];
            int Read = GetPrivateProfileString(Encoding.GetBytes(SectionName), Encoding.GetBytes(KeyName), (Default != null) ? Encoding.GetBytes(Default) : null, Buffer, Size, FilePath);
            return (Read != 0) ? Encoding.GetString(Buffer, 0, Read).Trim() : Default;
        }
        public static bool WriteString(string SectionName, string KeyName, string Value, string FilePath)
        {
            return WritePrivateProfileString(Encoding.GetBytes(SectionName), Encoding.GetBytes(KeyName), Encoding.GetBytes(Value), FilePath);
        }
    }
    public class Setup
    {

        #region Config
        public static void Config(string ConfigPath)
        {
            IniConfig.Config(ConfigPath);
        }
        #endregion
        #region Device
        public class Device
        {
            public static string Text
            {
                get { return IniConfig.GetString("Device", "Text", "连线设备"); }
                set { IniConfig.WriteString("Device", "Text", value); }
            }
            public static int Switch
            {
                get { return IniConfig.GetInteger("Device", "Switch", 1); }
                set { IniConfig.WriteInteger("Device", "Switch", value); }
            }
            public static string IP
            {
                get { return IniConfig.GetString("Device", "IP", "127.0.0.1"); }
                set { IniConfig.WriteString("Device", "IP", value); }
            }
            public static int Port
            {
                get { return IniConfig.GetInteger("Device", "Port", 0); }
                set { IniConfig.WriteInteger("Device", "Port", value); }
            }
            public static string COM
            {
                get { return IniConfig.GetString("Device", "COM", "COM"); }
                set { IniConfig.WriteString("Device", "COM", value); }
            }
            public static int BaudrRate
            {
                get { return IniConfig.GetInteger("Device", "BaudrRate", 230400); }
                set { IniConfig.WriteInteger("Device", "BaudrRate", value); }
            }
            public static string UARTConfig
            {
                get { return IniConfig.GetString("Device", "UARTConfig", "8N1"); }
                set { IniConfig.WriteString("Device", "UARTConfig", value); }
            }
            public static string COM2
            {
                get { return IniConfig.GetString("Device", "COM2", "COM"); }
                set { IniConfig.WriteString("Device", "COM2", value); }
            }
            public static int BaudrRate2
            {
                get { return IniConfig.GetInteger("Device", "BaudrRate2", 115200); }
                set { IniConfig.WriteInteger("Device", "BaudrRate2", value); }
            }
            public static string UARTConfig2
            {
                get { return IniConfig.GetString("Device", "UARTConfig2", "8N1"); }
                set { IniConfig.WriteString("Device", "UARTConfig", value); }
            }
            public static int MaxValue
            {
                get { return IniConfig.GetInteger("Device", "MaxValue", 38); }
                set { IniConfig.WriteInteger("Device", "MaxValue", value); }
            }
            public static int MinValue
            {
                get { return IniConfig.GetInteger("Device", "MinValue", 27); }
                set { IniConfig.WriteInteger("Device", "MinValue", value); }
            }
            public static int Regional
            {
                get { return IniConfig.GetInteger("Device", "Regional", 16); }
            }
        }
        #endregion
        #region Screen
        public class Screen
        {
            public static int Mode
            {
                get { return IniConfig.GetInteger("Screen", "Mode", 3); }
                set { IniConfig.WriteInteger("Screen", "Mode", value); }
            }
            public static string ColorMaps
            {
                get { return IniConfig.GetString("Screen", "ColorMaps", "Jet"); }
                set { IniConfig.WriteString("Screen", "ColorMaps", value); }
            }
        }
        #endregion
        #region Regional
        public class Regional
        {
            private static int Int = 1;
            private static string Section = "Regional";
            public static void SetRegional(int Int)
            {
                Regional.Int = Int;
                Section = string.Format("Regional{0}", Int);
            }
            public static string Text
            {
                get { return IniConfig.GetString(Section, "Text", null); }
                set { IniConfig.WriteString(Section, "Text", value); }
            }
            public static bool Switch
            {
                get { return IniConfig.GetBoolean(Section, "Switch", false); }
                set { IniConfig.WriteBoolean(Section, "Switch", value); }
            }
            public static string Period
            {
                get { return IniConfig.GetString(Section, "Period", null); }
                set { IniConfig.WriteString(Section, "Period", value); }
            }
            public static bool MoveEnable
            {
                get { return IniConfig.GetBoolean(Section, "MoveEnable", false); }
                set { IniConfig.WriteBoolean(Section, "MoveEnable", value); }
            }
            public static int Move
            {
                get { return IniConfig.GetInteger(Section, "Move", 0); }
                set { IniConfig.WriteInteger(Section, "Move", value); }
            }
            public static bool NumberEnable
            {
                get { return IniConfig.GetBoolean(Section, "NumberEnable", false); }
                set { IniConfig.WriteBoolean(Section, "NumberEnable", value); }
            }
            public static string Number
            {
                get { return IniConfig.GetString(Section, "Number", null); }
                set { IniConfig.WriteString(Section, "Number", value); }
            }
            public static bool TemperatureEnable
            {
                get { return IniConfig.GetBoolean(Section, "TemperatureEnable", false); }
                set { IniConfig.WriteBoolean(Section, "TemperatureEnable", value); }
            }
            public static string High
            {
                get { return IniConfig.GetString(Section, "High", null); }
                set { IniConfig.WriteString(Section, "High", value); }
            }
            public static string Low
            {
                get { return IniConfig.GetString(Section, "Low", null); }
                set { IniConfig.WriteString(Section, "Low", value); }
            }
            public static string Time
            {
                get { return IniConfig.GetString(Section, "Time", null); }
                set { IniConfig.WriteString(Section, "Time", value); }
            }
            public static string Email
            {
                get { return IniConfig.GetString(Section, "Email", null); }
                set { IniConfig.WriteString(Section, "Email", value); }
            }
            public static string Point1
            {
                get { return IniConfig.GetString(Section, "Point1", null); }
                set { IniConfig.WriteString(Section, "Point1", value); }
            }
            public static string Point2
            {
                get { return IniConfig.GetString(Section, "Point2", null); }
                set { IniConfig.WriteString(Section, "Point2", value); }
            }
        }
        #endregion
        #region Preferences
        public class Preferences
        {
            public static string Depth
            {
                get { return IniConfig.GetString("Preferences", "Depth", null); }
                set { IniConfig.WriteString("Preferences", "Depth", value); }
            }
            public static int Pixels
            {
                get { return IniConfig.GetInteger("Preferences", "Pixels", 5); }
                set { IniConfig.WriteInteger("Preferences", "Pixels", value); }
            }
            public static bool TopEnable
            {
                get { return IniConfig.GetBoolean("Preferences", "TopEnable", false); }
                set { IniConfig.WriteBoolean("Preferences", "TopEnable", value); }
            }
            public static string TopAngle
            {
                get { return IniConfig.GetString("Preferences", "TopAngle", null); }
                set { IniConfig.WriteString("Preferences", "TopAngle", value); }
            }
            public static bool HangEnable
            {
                get { return IniConfig.GetBoolean("Preferences", "HangEnable", false); }
                set { IniConfig.WriteBoolean("Preferences", "HangEnable", value); }
            }
            public static string HangAngle
            {
                get { return IniConfig.GetString("Preferences", "HangAngle", null); }
                set { IniConfig.WriteString("Preferences", "HangAngle", value); }
            }
            public static bool NFEnable
            {
                get { return IniConfig.GetBoolean("Preferences", "NFEnable", false); }
                set { IniConfig.WriteBoolean("Preferences", "NFEnable", value); }
            }
            public static int NF
            {
                get { return IniConfig.GetInteger("Preferences", "NF", 1); }
                set { IniConfig.WriteInteger("Preferences", "NF", value); }
            }
            public static bool ShieldEnable
            {
                get { return IniConfig.GetBoolean("Preferences", "ShieldEnable", false); }
                set { IniConfig.WriteBoolean("Preferences", "ShieldEnable", value); }
            }
            public static int Shield
            {
                get { return IniConfig.GetInteger("Preferences", "Shield", 1); }
                set { IniConfig.WriteInteger("Preferences", "Shield", value); }
            }
            public static bool FPSEnable
            {
                get { return IniConfig.GetBoolean("Preferences", "FPSEnable", false); }
                set { IniConfig.WriteBoolean("Preferences", "FPSEnable", value); }
            }
            public static int FPS
            {
                get { return IniConfig.GetInteger("Preferences", "FPS", 1); }
                set { IniConfig.WriteInteger("Preferences", "FPS", value); }
            }
            public static bool DifferEnable
            {
                get { return IniConfig.GetBoolean("Preferences", "DifferEnable", false); }
                set { IniConfig.WriteBoolean("Preferences", "DifferEnable", value); }
            }
            public static int Differ
            {
                get { return IniConfig.GetInteger("Preferences", "Differ", 0); }
                set { IniConfig.WriteInteger("Preferences", "Differ", value); }
            }
        }
        #endregion
    }
}
