using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace IRArray
{
    /// <summary>
    /// PHTextBox.xaml 的互動邏輯
    /// </summary>
    public partial class PHTextBox : TextBox
    {
        #region Parameter
        //private string Flag = "PHTextBox";
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
            typeof(PHTextBox),
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
            typeof(PHTextBox),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FF57B56C"))
        );
        public Brush ObjBackground
        {
            get { return (Brush)GetValue(ObjBackgroundProperty); }
            set { SetValue(ObjBackgroundProperty, value); }
        }
        public static readonly DependencyProperty ObjBackgroundProperty = DependencyProperty.Register(
            "ObjBackground",
            typeof(Brush),
            typeof(PHTextBox),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FF828CB8"))
        );
        public int ObjToggleSize
        {
            get { return (int)GetValue(ObjToggleSizeProperty); }
            set { SetValue(ObjToggleSizeProperty, value); }
        }
        public static readonly DependencyProperty ObjToggleSizeProperty = DependencyProperty.Register(
            "ObjToggleSize",
            typeof(int),
            typeof(PHTextBox),
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
            typeof(PHTextBox),
            new PropertyMetadata(14)
        );
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
                  "Source",
                  typeof(ImageSource),
                  typeof(PHTextBox),
                  new PropertyMetadata(null)
        );
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            "Placeholder",
            typeof(string),
            typeof(PHTextBox),
            new PropertyMetadata(null)
        );
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
        public PHTextBox()
        {
            InitializeComponent();
        }
        //public void Initialize()
        //{
        //}
        private void Clear_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Text = "";
        }
        #endregion
        #region Command
        #endregion
    }
}
