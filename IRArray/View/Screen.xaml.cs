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
    /// Screen.xaml 的互動邏輯
    /// </summary>
    public partial class Screen : UserControl
    {
        #region Parameter
        private string Flag = "Screen";
        #endregion
        #region Property
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
        public Screen()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
            Cancel.Click += Button_Click;
            Yes.Click += Button_Click;
            RadioButton1.Checked += RadioButton_Checked;
            RadioButton2.Checked += RadioButton_Checked;
            RadioButton3.Checked += RadioButton_Checked;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton RadioButton = sender as RadioButton; if (RadioButton == null) { return; }
            OnEvent(RadioButton.Name);
        }

        public void Import_Ini(List<PairStruct> List1)
        {
            try { ComboBox1.ItemsSource = List1; }
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Ini", ex.Message); }
        }
        public void Import_Value(ScreenStruct Struct)
        {
            try
            {
                RadioButton1.IsChecked = RadioButton2.IsChecked = RadioButton3.IsChecked = false;
                switch (Struct.Mode)
                {
                    case 0: { RadioButton1.IsChecked = true; } break;
                    case 1: { RadioButton2.IsChecked = true; } break;
                    case 3: { RadioButton3.IsChecked = true; } break;
                }
                ComboBox1.SelectedValue = Struct.Colormap;
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Value", ex.Message); }
        }
        public ScreenStruct Export()
        {
            ScreenStruct Struct = new ScreenStruct();
            try
            {
                if ((bool)RadioButton1.IsChecked) { Struct.Mode = 0; }
                else if ((bool)RadioButton2.IsChecked) { Struct.Mode = 1; }
                else if ((bool)RadioButton3.IsChecked) { Struct.Mode = 3; }
                Struct.Colormap = (string)ComboBox1.SelectedValue;
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Export", ex.Message); }
            return Struct;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button Button = sender as Button; if (Button == null) { return; }
            OnEvent(Button.Name);
        }
        #endregion
        #region Command
        #endregion
    }
}
