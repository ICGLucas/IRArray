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
    /// PicRadio.xaml 的互動邏輯
    /// </summary>
    public partial class PicRadio : UserControl
    {
        #region Parameter
        //private string Flag = "PicRadio";
        #endregion
        #region Property
        #region Normal
        public Brush ObjBackgroud
        {
            get { return (Brush)GetValue(ObjBackgroudProperty); }
            set { SetValue(ObjBackgroudProperty, value); }
        }
        public static readonly DependencyProperty ObjBackgroudProperty = DependencyProperty.Register(
            "ObjBackgroud",
            typeof(Brush),
            typeof(PicRadio),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FF2B3763"))
        );
        public Brush ObjForeground
        {
            get { return (Brush)GetValue(ObjForegroundProperty); }
            set { SetValue(ObjForegroundProperty, value); }
        }
        public static readonly DependencyProperty ObjForegroundProperty = DependencyProperty.Register(
            "ObjForeground",
            typeof(Brush),
            typeof(PicRadio),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FFB8BEE8"))
        );
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
                  "Source",
                  typeof(ImageSource),
                  typeof(PicRadio),
                  new PropertyMetadata(null)
        );
        #endregion
        #region Focus
        public Brush ObjFocusBackgroud
        {
            get { return (Brush)GetValue(ObjFocusBackgroudProperty); }
            set { SetValue(ObjFocusBackgroudProperty, value); }
        }
        public static readonly DependencyProperty ObjFocusBackgroudProperty = DependencyProperty.Register(
            "ObjFocusBackgroud",
            typeof(Brush),
            typeof(PicRadio),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FF17224D"))
        );
        public Brush ObjFocusForeground
        {
            get { return (Brush)GetValue(ObjFocusForegroundProperty); }
            set { SetValue(ObjFocusForegroundProperty, value); }
        }
        public static readonly DependencyProperty ObjFocusForegroundProperty = DependencyProperty.Register(
            "ObjFocusForeground",
            typeof(Brush),
            typeof(PicRadio),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FFFFFFFF"))
        );
        public ImageSource FocusSource
        {
            get { return (ImageSource)GetValue(FocusSourceProperty); }
            set { SetValue(FocusSourceProperty, value); }
        }
        public static readonly DependencyProperty FocusSourceProperty = DependencyProperty.Register(
                  "FocusSource",
                  typeof(ImageSource),
                  typeof(PicRadio),
                  new PropertyMetadata(null)
        );
        #endregion
        
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
            "IsChecked",
            typeof(bool),
            typeof(PicRadio),
            new PropertyMetadata(false)
        );
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(PicRadio),
            new PropertyMetadata(null)
        );
        public int ImageSize
        {
            get { return (int)GetValue(ImageSizeProperty); }
            set { SetValue(ImageSizeProperty, value); }
        }
        public static readonly DependencyProperty ImageSizeProperty = DependencyProperty.Register(
            "ImageSize",
            typeof(int),
            typeof(PicRadio),
            new PropertyMetadata(24)
        );
        public int Imagepace
        {
            get { return (int)GetValue(ImagepaceProperty); }
            set { SetValue(ImagepaceProperty, value); }
        }
        public static readonly DependencyProperty ImagepaceProperty = DependencyProperty.Register(
            "Imagepace",
            typeof(int),
            typeof(PicRadio),
            new PropertyMetadata(24)
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
        public PicRadio()
        {
            InitializeComponent();
        }
        //public void Initialize()
        //{
        //}
        #endregion
        #region Command
        #endregion
    }
}
