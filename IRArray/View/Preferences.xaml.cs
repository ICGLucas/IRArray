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
    /// Preferences.xaml 的互動邏輯
    /// </summary>
    public partial class Preferences : UserControl
    {
        #region Parameter
        private string Flag = "Preferences";
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
        public Preferences()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
            Cancel.Click += Button_Click;
            Yes.Click += Button_Click;
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
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Value", ex.Message); }
        }
        public void Import_Value(PreferencesStruct Struct)
        {
            try
            {
                //PHTextBox1.Text = Struct.Depth;
                if (Struct.Depth == null)
                {
                    PHTextBox1.Text = PHTextBox2.Text = PHTextBox3.Text = PHTextBox4.Text = null;
                }
                else
                {
                    double Temp = 0; double.TryParse(Struct.Depth, out Temp);
                    if (Temp < 1) { Temp = 1.0f; }
                    else if (Temp > 5) { Temp = 5.0f; }
                    else { Temp = Math.Round(Temp, 1); }
                    PHTextBox1.Text = Temp.ToString();
                    if (Struct.Pixels == 0)
                    {
                        if (Temp >= 4.5) { PHTextBox2.Text = "1"; }
                        else if (Temp >= 4) { PHTextBox2.Text = "3"; }
                        else if (Temp >= 3.5) { PHTextBox2.Text = "4"; }
                        else if (Temp >= 3) { PHTextBox2.Text = "5"; }
                        else if (Temp >= 2.5) { PHTextBox2.Text = "6"; }
                        else if (Temp >= 2) { PHTextBox2.Text = "8"; }
                        else if (Temp >= 1) { PHTextBox2.Text = "8"; }
                    }
                    else { PHTextBox2.Text = Struct.Pixels.ToString(); }
                    PHTextBox3.Text = (Temp * 2.85).ToString();
                    PHTextBox4.Text = (Temp * 1.53).ToString();
                }
                PHTextBox5.Text = Struct.TopAngle;
                PHTextBox6.Text = Struct.HangAngle;
                RadioButton1.IsChecked = Struct.TopEnable;
                RadioButton2.IsChecked = Struct.HangEnable;
                CheckBox3.IsChecked = Struct.NFEnable;
                CheckBox4.IsChecked = Struct.ShieldEnable;
                CheckBox5.IsChecked = Struct.FPSEnable;
                CheckBox6.IsChecked = Struct.DifferEnable;
                ComboBox1.SelectedValue = Struct.NF;
                ComboBox2.SelectedValue = Struct.Shield;
                ComboBox3.SelectedValue = Struct.FPS;
                ComboBox4.SelectedValue = Struct.Differ;
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Value", ex.Message); }
        }
        public PreferencesStruct Export()
        {
            PreferencesStruct Struct = new PreferencesStruct();
            try
            {
                Struct.Depth = PHTextBox1.Text;
                int Temp = 0; int.TryParse(PHTextBox2.Text, out Temp);
                Struct.Pixels = Temp;
                Struct.TopAngle = PHTextBox5.Text;
                Struct.HangAngle = PHTextBox6.Text;

                Struct.TopEnable = (bool)RadioButton1.IsChecked;
                Struct.HangEnable = (bool)RadioButton2.IsChecked;
                Struct.NFEnable = (bool)CheckBox3.IsChecked;
                Struct.ShieldEnable = (bool)CheckBox4.IsChecked;
                Struct.FPSEnable = (bool)CheckBox5.IsChecked;
                Struct.DifferEnable = (bool)CheckBox6.IsChecked;

                Struct.NF = (int)ComboBox1.SelectedValue;
                Struct.Shield = (int)ComboBox2.SelectedValue;
                Struct.FPS = (int)ComboBox3.SelectedValue;
                Struct.Differ = (int)ComboBox4.SelectedValue;
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Export", ex.Message); }
            return Struct;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button Button = sender as Button; if (Button == null) { return; }
                OnEvent(Button.Name);
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Button_Click", ex.Message); }
        }
        private void PHTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Key != Key.Enter) { return; }
                PHTextBox PHTextBox = sender as PHTextBox; if (PHTextBox == null) { return; }
                switch (PHTextBox.Name)
                {
                    case "PHTextBox1":
                        {
                            double Temp = 0; double.TryParse(PHTextBox.Text, out Temp);
                            if (Temp < 1) { Temp = 1.0f; }
                            else if (Temp > 5) { Temp = 5.0f; }
                            else { Temp = Math.Round(Temp, 1); }
                            PHTextBox1.Text = Temp.ToString();
                            if (Temp >= 4.5) { PHTextBox2.Text = "1"; }
                            else if (Temp >= 4) { PHTextBox2.Text = "3"; }
                            else if (Temp >= 3.5) { PHTextBox2.Text = "4"; }
                            else if (Temp >= 3) { PHTextBox2.Text = "5"; }
                            else if (Temp >= 2.5) { PHTextBox2.Text = "6"; }
                            else if (Temp >= 2) { PHTextBox2.Text = "8"; }
                            else if (Temp >= 1) { PHTextBox2.Text = "8"; }
                            PHTextBox3.Text = (Temp > 0) ? (Temp * 2.85).ToString() : null;
                            PHTextBox4.Text = (Temp > 0) ? (Temp * 1.53).ToString() : null;
                        }
                        break;
                    default: { int Temp = 0; if (!int.TryParse(PHTextBox.Text, out Temp)) { OnEvent("InputError"); } } break;
                }
            }
            catch (Exception ex) { OnEvent("Error", Flag, "PHTextBox_KeyUp", ex.Message); }
        }
        #endregion
        #region Command
        #endregion
    }
}
