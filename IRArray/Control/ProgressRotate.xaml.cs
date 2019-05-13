using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IRArray
{
    /// <summary>
    /// ProgressRotate.xaml 的互動邏輯
    /// </summary>
    public partial class ProgressRotate : UserControl
    {
        #region Parameter
        //private string Flag = "ProgressRotate";
        public int Int;
        #endregion
        #region Property
        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius",
            typeof(int),
            typeof(ProgressRotate),
            new PropertyMetadata(50, new PropertyChangedCallback(OnValueChanged))
        );
        public int StrokeThickness
        {
            get { return (int)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness",
            typeof(int),
            typeof(ProgressRotate),
            new PropertyMetadata(10)
        );
        #region Ellispe
        private int EX
        {
            get { return (int)GetValue(EXProperty); }
            set { SetValue(EXProperty, value); }
        }
        private static readonly DependencyProperty EXProperty = DependencyProperty.Register(
            "EX",
            typeof(int),
            typeof(ProgressRotate),
            new PropertyMetadata(-50)
        );
        private int EY
        {
            get { return (int)GetValue(EYProperty); }
            set { SetValue(EYProperty, value); }
        }
        private static readonly DependencyProperty EYProperty = DependencyProperty.Register(
            "EY",
            typeof(int),
            typeof(ProgressRotate),
            new PropertyMetadata(-50)
        );
        private int EHeight
        {
            get { return (int)GetValue(EHeightProperty); }
            set { SetValue(EHeightProperty, value); }
        }
        private static readonly DependencyProperty EHeightProperty = DependencyProperty.Register(
            "EHeight",
            typeof(int),
            typeof(ProgressRotate),
            new PropertyMetadata(100)
        );
        private int EWidth
        {
            get { return (int)GetValue(EWidthProperty); }
            set { SetValue(EWidthProperty, value); }
        }
        private static readonly DependencyProperty EWidthProperty = DependencyProperty.Register(
            "EWidth",
            typeof(int),
            typeof(ProgressRotate),
            new PropertyMetadata(100)
        );
        public Brush EStroke
        {
            get { return (Brush)GetValue(EStrokeProperty); }
            set { SetValue(EStrokeProperty, value); }
        }
        public static readonly DependencyProperty EStrokeProperty = DependencyProperty.Register(
            "EStroke",
            typeof(Brush),
            typeof(ProgressRotate),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FFEEEEEE"))
        );
        #endregion

        #region ArcSegment
        private Point ArcStartPoint
        {
            get { return (Point)GetValue(ArcStartPointProperty); }
            set { SetValue(ArcStartPointProperty, value); }
        }
        private static readonly DependencyProperty ArcStartPointProperty = DependencyProperty.Register(
            "ArcStartPoint",
            typeof(Point),
            typeof(ProgressRotate),
            new PropertyMetadata(new Point(0, -50))
        );
        private Point ArcEndPoint
        {
            get { return (Point)GetValue(ArcEndPointProperty); }
            set { SetValue(ArcEndPointProperty, value); }
        }
        private static readonly DependencyProperty ArcEndPointProperty = DependencyProperty.Register(
            "ArcEndPoint",
            typeof(Point),
            typeof(ProgressRotate),
            new PropertyMetadata(new Point(0, 50))
        );
        private Size ArcSize
        {
            get { return (Size)GetValue(ArcSizeProperty); }
            set { SetValue(ArcSizeProperty, value); }
        }
        private static readonly DependencyProperty ArcSizeProperty = DependencyProperty.Register(
            "ArcSize",
            typeof(Size),
            typeof(ProgressRotate),
            new PropertyMetadata(new Size(50, 50))
        );
        public System.Windows.Media.Color ArcColor
        {
            get { return (System.Windows.Media.Color)GetValue(ArcColorProperty); }
            set { SetValue(ArcColorProperty, value); }
        }
        public static readonly DependencyProperty ArcColorProperty = DependencyProperty.Register(
            "ArcColor",
            typeof(System.Windows.Media.Color),
            typeof(ProgressRotate),
            new PropertyMetadata(System.Windows.Media.ColorConverter.ConvertFromString("#FF3399FF"))
        );
        #endregion
        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ProgressRotate ProgressRotate = sender as ProgressRotate; if (ProgressRotate == null) { return; }
            ProgressRotate.EX = ProgressRotate.EY = -ProgressRotate.Radius;
            ProgressRotate.EHeight = ProgressRotate.EWidth = ProgressRotate.Radius * 2;
            ProgressRotate.ArcStartPoint = new Point(0, -ProgressRotate.Radius);
            ProgressRotate.ArcEndPoint = new Point(0, ProgressRotate.Radius);
            ProgressRotate.ArcSize = new Size(ProgressRotate.Radius, ProgressRotate.Radius);
        }
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
        public ProgressRotate()
        {
            InitializeComponent();
        }
        #endregion
        #region Command
        #endregion
    }
}
