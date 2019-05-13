using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IRArray_test
{
    /// <summary>
    /// CircleRadioButton.xaml 的互動邏輯
    /// </summary>
    public partial class CircleRadioButton : UserControl
    {
        #region Parameter
        //private string Flag = "CircleRadioButton";
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
            new PropertyMetadata(10)
        );
        #region Border
        public int BThickness
        {
            get { return (int)GetValue(BThicknessProperty); }
            set { SetValue(BThicknessProperty, value); }
        }
        public static readonly DependencyProperty BThicknessProperty = DependencyProperty.Register(
            "BThickness",
            typeof(int),
            typeof(ProgressRotate),
            new PropertyMetadata(2)
        );
        public Brush BColor
        {
            get { return (Brush)GetValue(BColorProperty); }
            set { SetValue(BColorProperty, value); }
        }
        public static readonly DependencyProperty BColorProperty = DependencyProperty.Register(
            "BColor",
            typeof(Brush),
            typeof(ProgressRotate),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FFCCCCCC"))
        );
        #endregion

        #region Ellipse
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill",
            typeof(Brush),
            typeof(ProgressRotate),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FF666666"))
        );
        #endregion
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
        public CircleRadioButton()
        {
            InitializeComponent();
        }
        #endregion
        #region Command
        #endregion
    }
}
