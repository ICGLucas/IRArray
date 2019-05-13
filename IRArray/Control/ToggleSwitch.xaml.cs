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
    /// ToggleSwitch.xaml 的互動邏輯
    /// </summary>
    public partial class ToggleSwitch : UserControl
    {
        #region Parameter
        //private string Flag = "ToggleSwitch";
        #endregion
        #region Property
        public bool Toggled
        {
            get { return (bool)GetValue(ToggledProperty); }
            set { SetValue(ToggledProperty, value); }
        }
        public static readonly DependencyProperty ToggledProperty = DependencyProperty.Register(
            "Toggled",
            typeof(bool),
            typeof(ToggleSwitch),
            new PropertyMetadata(false)
        );
        public Brush OffColor
        {
            get { return (Brush)GetValue(OffColorProperty); }
            set { SetValue(OffColorProperty, value); }
        }
        public static readonly DependencyProperty OffColorProperty = DependencyProperty.Register(
            "OffColor",
            typeof(Brush),
            typeof(ToggleSwitch),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FFA0A0A0"))
        );
        public Brush OnColor
        {
            get { return (Brush)GetValue(OnColorProperty); }
            set { SetValue(OnColorProperty, value); }
        }
        public static readonly DependencyProperty OnColorProperty = DependencyProperty.Register(
            "OnColor",
            typeof(Brush),
            typeof(ToggleSwitch),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FF82BE7D"))
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
        public ToggleSwitch()
        {
            InitializeComponent();
        }
        private void ToggleSwitch_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Toggled = !Toggled;
        }
        //public void Initialize()
        //{
        //}
        #endregion
        #region Command
        #endregion
    }
}
