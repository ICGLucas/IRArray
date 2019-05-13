using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace IRArray
{
    /// <summary>
    /// PromptBox.xaml 的互動邏輯
    /// </summary>
    public partial class PromptBox : UserControl
    {
        #region Parameter
        private string Flag = "PromptBox";
        private System.Windows.Threading.DispatcherTimer DispatcherTimer = null;
        private string Option = null;
        private int Second = 0;
        #endregion
        #region Property
        public int ImageSize
        {
            get { return (int)GetValue(ImageSizeProperty); }
            set { SetValue(ImageSizeProperty, value); }
        }
        public static readonly DependencyProperty ImageSizeProperty = DependencyProperty.Register(
            "ImageSize",
            typeof(int),
            typeof(PromptBox),
            new PropertyMetadata(64)
        );
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
                  "Source",
                  typeof(ImageSource),
                  typeof(PromptBox),
                  new PropertyMetadata(null)
        );
        public Brush ObjForeground
        {
            get { return (Brush)GetValue(ObjForegroundProperty); }
            set { SetValue(ObjForegroundProperty, value); }
        }
        public static readonly DependencyProperty ObjForegroundProperty = DependencyProperty.Register(
            "ObjForeground",
            typeof(Brush),
            typeof(PromptBox),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#2B3763"))
        );
        public string ObjText
        {
            get { return (string)GetValue(ObjTextProperty); }
            set { SetValue(ObjTextProperty, value); }
        }
        public static readonly DependencyProperty ObjTextProperty = DependencyProperty.Register(
            "ObjText",
            typeof(string),
            typeof(PromptBox),
            new PropertyMetadata("通知")
        );
        public Brush BtnForeground1
        {
            get { return (Brush)GetValue(BtnForeground1Property); }
            set { SetValue(BtnForeground1Property, value); }
        }
        public static readonly DependencyProperty BtnForeground1Property = DependencyProperty.Register(
            "BtnForeground1",
            typeof(Brush),
            typeof(PromptBox),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#57B56C"))
        );
        public Brush BtnForeground2
        {
            get { return (Brush)GetValue(BtnForeground2Property); }
            set { SetValue(BtnForeground2Property, value); }
        }
        public static readonly DependencyProperty BtnForeground2Property = DependencyProperty.Register(
            "BtnForeground2",
            typeof(Brush),
            typeof(PromptBox),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#2B3763"))
        );
        public string BtnText1
        {
            get { return (string)GetValue(BtnText1Property); }
            set { SetValue(BtnText1Property, value); }
        }
        public static readonly DependencyProperty BtnText1Property = DependencyProperty.Register(
            "BtnText1",
            typeof(string),
            typeof(PromptBox),
            new PropertyMetadata("确定")
        );
        public string BtnText2
        {
            get { return (string)GetValue(BtnText2Property); }
            set { SetValue(BtnText2Property, value); }
        }
        public static readonly DependencyProperty BtnText2Property = DependencyProperty.Register(
            "BtnText2",
            typeof(string),
            typeof(PromptBox),
            new PropertyMetadata("取消")
        );
        public bool IsConfirm1
        {
            get { return (bool)GetValue(IsConfirm1Property); }
            set { SetValue(IsConfirm1Property, value); }
        }
        public static readonly DependencyProperty IsConfirm1Property = DependencyProperty.Register(
            "IsConfirm1",
            typeof(bool),
            typeof(PromptBox),
            new PropertyMetadata(false)
        );
        public bool IsConfirm2
        {
            get { return (bool)GetValue(IsConfirm2Property); }
            set { SetValue(IsConfirm2Property, value); }
        }
        public static readonly DependencyProperty IsConfirm2Property = DependencyProperty.Register(
            "IsConfirm2",
            typeof(bool),
            typeof(PromptBox),
            new PropertyMetadata(false)
        );
        public bool IsSource
        {
            get { return (bool)GetValue(IsSourceProperty); }
            set { SetValue(IsSourceProperty, value); }
        }
        public static readonly DependencyProperty IsSourceProperty = DependencyProperty.Register(
            "IsSource",
            typeof(bool),
            typeof(PromptBox),
            new PropertyMetadata(true)
        );
        #endregion
        #region Presentation
        #endregion
        #region Contents
        #endregion
        #region Event
        public event System.EventHandler<EventInfo> EventHandler;
        protected virtual void OnEvent(string Option, params object[] Values)
        {
            EventHandler?.Invoke(this, new EventInfo(Option, Values));
        }
        #endregion
        #region Method
        public PromptBox()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
        }
        public void Import(string Text, bool IsSource = true, bool IsConfirm1 = false, bool IsConfirm2 = false, string Option = "None", int Second = 2)
        {
            try
            {
                this.ObjText = Text;
                this.IsSource = IsSource;
                this.IsConfirm1 = IsConfirm1;
                this.IsConfirm2 = IsConfirm2;
                this.Option = Option;
                if (!IsConfirm1 && !IsConfirm2)
                {
                    this.Second = Second;
                    DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    DispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
                    DispatcherTimer.Tick += DispatcherTimer_Tick;
                    DispatcherTimer.Start();
                }
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Import", ex.Message); }
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Second <= 0)
                {
                    if (DispatcherTimer != null) { DispatcherTimer.Stop(); DispatcherTimer = null; }
                    OnEvent("Timer_" + Option);
                }
                if (!IsConfirm1 || !IsConfirm2) { Second--; }
            }
            catch (Exception ex) { OnEvent("Error", Flag, "DispatcherTimer_Tick", ex.Message); }
        }
        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OnEvent("Border_" + Option);
        }
        private void Confirm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DispatcherTimer != null) { DispatcherTimer.Stop(); DispatcherTimer = null; }
            Second = 0;
            OnEvent("Confirm_" + Option);
        }
        private void Yes_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DispatcherTimer != null) { DispatcherTimer.Stop(); DispatcherTimer = null; }
            Second = 0;
            OnEvent("Yes_" + Option);
        }
        private void No_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DispatcherTimer != null) { DispatcherTimer.Stop(); DispatcherTimer = null; }
            Second = 0;
            OnEvent("No_" + Option);
        }
        #endregion
        #region Command
        #endregion
    }
}
