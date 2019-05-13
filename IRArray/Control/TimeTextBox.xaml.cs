using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IRArray
{
    /// <summary>
    /// TimeTextBox.xaml 的互動邏輯
    /// </summary>
    public partial class TimeTextBox : UserControl
    {
        #region Parameter
        //private string Flag = "TimeTextBox";
        #endregion
        #region Property
        public Brush ObjBorderBrush
        {
            get { return (Brush)GetValue(ObjBorderBrushProperty); }
            set { SetValue(ObjBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty ObjBorderBrushProperty = DependencyProperty.Register(
            "ObjBorderBrush",
            typeof(Brush),
            typeof(TimeTextBox),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FF828CB8"))
        );
        public Brush FocusBrush
        {
            get { return (Brush)GetValue(FocusBrushProperty); }
            set { SetValue(FocusBrushProperty, value); }
        }
        public static readonly DependencyProperty FocusBrushProperty = DependencyProperty.Register(
            "FocusBrush",
            typeof(Brush),
            typeof(TimeTextBox),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FF57B56C"))
        );
        public int ObjToggleSize
        {
            get { return (int)GetValue(ObjToggleSizeProperty); }
            set { SetValue(ObjToggleSizeProperty, value); }
        }
        public static readonly DependencyProperty ObjToggleSizeProperty = DependencyProperty.Register(
            "ObjToggleSize",
            typeof(int),
            typeof(TimeTextBox),
            new PropertyMetadata(28)
        );
        public int ImageSize
        {
            get { return (int)GetValue(ImageSizeProperty); }
            set { SetValue(ImageSizeProperty, value); }
        }
        public static readonly DependencyProperty ImageSizeProperty = DependencyProperty.Register(
            "ImageSize",
            typeof(int),
            typeof(TimeTextBox),
            new PropertyMetadata(14)
        );
        public ImageSource SourceUp
        {
            get { return (ImageSource)GetValue(SourceUpProperty); }
            set { SetValue(SourceUpProperty, value); }
        }
        public static readonly DependencyProperty SourceUpProperty = DependencyProperty.Register(
                  "SourceUp",
                  typeof(ImageSource),
                  typeof(TimeTextBox),
                  new PropertyMetadata(null)
        );
        public ImageSource SourceDown
        {
            get { return (ImageSource)GetValue(SourceDownProperty); }
            set { SetValue(SourceDownProperty, value); }
        }
        public static readonly DependencyProperty SourceDownProperty = DependencyProperty.Register(
                  "SourceDown",
                  typeof(ImageSource),
                  typeof(TimeTextBox),
                  new PropertyMetadata(null)
        );
        public int Hour
        {
            get { return (int)GetValue(HourProperty); }
            set { SetValue(HourProperty, value); }
        }
        public static readonly DependencyProperty HourProperty = DependencyProperty.Register(
            "Hour",
            typeof(int),
            typeof(TimeTextBox),
            new PropertyMetadata(0)
        );
        public int Minute
        {
            get { return (int)GetValue(MinuteProperty); }
            set { SetValue(MinuteProperty, value); }
        }
        public static readonly DependencyProperty MinuteProperty = DependencyProperty.Register(
            "Minute",
            typeof(int),
            typeof(TimeTextBox),
            new PropertyMetadata(0)
        );
        public int Second
        {
            get { return (int)GetValue(SecondProperty); }
            set { SetValue(SecondProperty, value); }
        }
        public static readonly DependencyProperty SecondProperty = DependencyProperty.Register(
            "Second",
            typeof(int),
            typeof(TimeTextBox),
            new PropertyMetadata(0)
        );
        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
            "MaxValue",
            typeof(int),
            typeof(TimeTextBox),
            new PropertyMetadata(86399)
        );
        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
            "MinValue",
            typeof(int),
            typeof(TimeTextBox),
            new PropertyMetadata(0)
        );
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(int),
            typeof(TimeTextBox),
            new PropertyMetadata(0, new PropertyChangedCallback(OnValueChanged))
        );
        public int Increment
        {
            get { return (int)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }
        public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register(
            "Increment",
            typeof(int),
            typeof(TimeTextBox),
            new PropertyMetadata(1)
        );
        public int LargeIncrement
        {
            get { return (int)GetValue(LargeIncrementProperty); }
            set { SetValue(LargeIncrementProperty, value); }
        }
        public static readonly DependencyProperty LargeIncrementProperty = DependencyProperty.Register(
            "LargeIncrement",
            typeof(int),
            typeof(TimeTextBox),
            new PropertyMetadata(5)
        );
        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            TimeTextBox TimeTextBox = sender as TimeTextBox; if (TimeTextBox == null) { return; }
            TimeTextBox.Hour = TimeTextBox.Value / 3600;
            TimeTextBox.Minute = (TimeTextBox.Value / 60) % 60;
            TimeTextBox.Second = TimeTextBox.Value % 60;
        }
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
        public TimeTextBox()
        {
            InitializeComponent();
        }
        //public void Initialize()
        //{
        //}
        private System.Threading.Thread Thread = null;
        private bool IsIncrement = false;
        private bool IsDecrement = false;
        private void Increment_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IncrementValue(Increment); IsIncrement = true;
            if (Thread != null) { Thread.Abort(); }
            Thread = new System.Threading.Thread(Thread_Run); Thread.Start();
        }
        private void Increment_MouseLeave(object sender, MouseEventArgs e)
        {
            IsIncrement = false;
        }
        private void Increment_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsIncrement = false;
        }
        private void Decrement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DecrementValue(Increment); IsDecrement = true;
            if (Thread != null) { Thread.Abort(); }
            Thread = new System.Threading.Thread(Thread_Run); Thread.Start();
        }
        private void Decrement_MouseLeave(object sender, MouseEventArgs e)
        {
            IsDecrement = false;
        }
        private void Decrement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsDecrement = false;
        }
        private void Thread_Run()
        {
            System.Threading.Thread.Sleep(1000);
            while (IsIncrement || IsDecrement)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (IsIncrement) { IncrementValue(LargeIncrement); }
                    else if (IsDecrement) { DecrementValue(LargeIncrement); }
                }));
                System.Threading.Thread.Sleep(100);
            }
        }
        private void IncrementValue(int Increment)
        {
            Value = Math.Min(Value + Increment, MaxValue);
        }
        private void DecrementValue(int Increment)
        {
            Value = Math.Max(Value - Increment, MinValue);
        }
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox TextBox = sender as TextBox; if (TextBox == null) { return; }
            int Temp = 0; int.TryParse(TextBox.Text, out Temp);
            switch (TextBox.Name)
            {
                case "TextBox1": if (Temp < 0) { Hour = 0; } if (Temp > 23) { Hour = 23; } else { Hour = Temp; } break;
                case "TextBox2": if (Temp < 0) { Minute = 0; } if (Temp > 59) { Minute = 59; } else { Minute = Temp; } break;
                case "TextBox3": if (Temp < 0) { Second = 0; } if (Temp > 59) { Second = 59; } else { Second = Temp; } break;
            }
            Temp = Hour * 3600 + Minute * 60 + Second;
            if (Temp < MinValue) { Value = 0; }
            else if (Temp > MaxValue) { Value = MaxValue; }
            else { Value = Temp; }
        }
        #endregion
        #region Command
        #endregion
    }
}
