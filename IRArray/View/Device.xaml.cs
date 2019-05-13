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
    /// Device.xaml 的互動邏輯
    /// </summary>
    public partial class Device : UserControl
    {
        #region Parameter
        private string Flag = "Device";
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
        public Device()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
            Insert.Click += Button_Click;
            Delete.Click += Button_Click;
            Save.Click += Button_Click;
            ComboBox1.SelectionChanged += ComboBox1_SelectionChanged;
        }
        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnEvent("Changed", ComboBox1.SelectedIndex);
        }
        public void Import_Ini(List<PairStruct> List1, List<PairStruct> List2, List<PairStruct> List3, List<PairStruct> List4)
        {
            try
            {
                ComboBox1.ItemsSource = List1;
                ComboBox2.ItemsSource = List2;
                ComboBox3.ItemsSource = List3;
                ComboBox4.ItemsSource = List4;
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Ini", ex.Message); }
        }
        public void Import_Ini(List<PairStruct> List1)
        {
            try { ComboBox1.ItemsSource = List1; ComboBox1.Items.Refresh(); }
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Ini", ex.Message); }
        }
        public void Import_Value(DeviceStruct Struct)
        {
            try
            {
                RadioButton1.IsChecked = (Struct.Switch == 0);
                RadioButton2.IsChecked = (Struct.Switch == 1);
                PHTextBox1.Text = Struct.IP;
                PHTextBox2.Text = Struct.Port.ToString();
                ComboBox2.SelectedValue = Struct.COM;
                ComboBox3.SelectedValue = Struct.BaudrRate;
                ComboBox4.SelectedValue = Struct.UARTConfig;
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Value", ex.Message); }
        }
        public void Import_Value(int Int)
        {
            try { ComboBox1.SelectedIndex = Int; }
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Value", ex.Message); }
        }
        public DeviceStruct Export()
        {
            DeviceStruct Struct = new DeviceStruct();
            try
            {
                Struct.Switch = ((bool)RadioButton1.IsChecked) ? 0 : 1;
                Struct.IP = PHTextBox1.Text;
                int Temp;
                int.TryParse(PHTextBox2.Text, out Temp); Struct.Port = Temp;
                Struct.COM = (string)ComboBox2.SelectedValue;
                Struct.BaudrRate = (int)ComboBox3.SelectedValue;
                Struct.UARTConfig = (string)ComboBox4.SelectedValue;
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
