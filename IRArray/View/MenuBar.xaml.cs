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
    /// MenuBar.xaml 的互動邏輯
    /// </summary>
    public partial class MenuBar : UserControl
    {
        #region Parameter
        //private string Flag = "MenuBar";
        #endregion
        #region Property
        public Brush ObjBackgroud
        {
            get { return (Brush)GetValue(ObjBackgroudProperty); }
            set { SetValue(ObjBackgroudProperty, value); }
        }
        public static readonly DependencyProperty ObjBackgroudProperty = DependencyProperty.Register(
            "ObjBackgroud",
            typeof(Brush),
            typeof(MenuBar),
            new PropertyMetadata(new System.Windows.Media.BrushConverter().ConvertFromString("#FF2B3763"))
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
        public MenuBar()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
        }
        private void PicRadio_MouseDown(object sender, RoutedEventArgs e)
        {
            PicRadio PicRadio = sender as PicRadio; if (PicRadio == null) { return; }
            Screen.IsChecked = Regional.IsChecked = Preferences.IsChecked = false;
            PicRadio.IsChecked = true;
            OnEvent(PicRadio.Name);
        }
        #endregion
        #region Command
        #endregion
    }
}
