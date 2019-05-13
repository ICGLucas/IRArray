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
    /// RegionalDetail.xaml 的互動邏輯
    /// </summary>
    public partial class RegionalDetail : UserControl
    {
        #region Parameter
        private string Flag = "RegionalDetail";
        private List<PairStruct> Period_List = null;
        #endregion
        #region Property
        private int Index { get; set; }
        private string Point1 { get; set; }
        private string Point2 { get; set; }
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
        public RegionalDetail()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
            Cancel.Click += Button_Click;
            Yes.Click += Button_Click;
            PeriodAdd.MouseDown += PeriodAdd_MouseDown;
            PeriodRemove.Click += PeriodRemove_Click;
        }
        public void Import_Init(List<PairStruct> List1, List<PairStruct> List2)
        {
            try
            {
                this.Period_List = List1;
                ComboBox1.ItemsSource = List2;
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Init", ex.Message); }
        }
        public void Import_Value(RegionalStruct Struct, int Index)
        {
            try
            {
                this.Index = Index;
                PHTextBox1.Text = Struct.Text;
                PHTextBox2.Text = Struct.Number;
                PHTextBox3.Text = Struct.High;
                PHTextBox4.Text = Struct.Low;
                PHTextBox5.Text = Struct.Time;
                PHTextBox6.Text = Struct.Email;
                CheckBox1.IsChecked = Struct.Switch;
                CheckBox2.IsChecked = Struct.MoveEnable;
                CheckBox3.IsChecked = Struct.NumberEnable;
                CheckBox4.IsChecked = Struct.TemperatureEnable;
                ComboBox1.SelectedValue = Struct.Move;
                Point1 = Struct.Point1;
                Point2 = Struct.Point2;
                PeriodRemove.Visibility = Visibility.Hidden;
                Period_Clear();
                foreach (PeriodStruct Item in Struct.List) { Period_Add(Item); }
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Value", ex.Message); }
        }
        public RegionalStruct Export()
        {
            RegionalStruct Struct = new RegionalStruct();
            try
            {
                Struct.Text = PHTextBox1.Text;
                Struct.Number = PHTextBox2.Text;
                Struct.High = PHTextBox3.Text;
                Struct.Low = PHTextBox4.Text;
                Struct.Time = PHTextBox5.Text;
                Struct.Email = PHTextBox6.Text;

                Struct.Switch = (bool)CheckBox1.IsChecked;
                Struct.MoveEnable = (bool)CheckBox2.IsChecked;
                Struct.NumberEnable = (bool)CheckBox3.IsChecked;
                Struct.TemperatureEnable = (bool)CheckBox4.IsChecked;
                Struct.Move = (int)ComboBox1.SelectedValue;
                Struct.Point1 = Point1;
                Struct.Point2 = Point2;
                Struct.List = new List<PeriodStruct>();
                foreach (Period Item in PeriodContainer.Children) { Struct.List.Add(Item.Export()); }
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Export", ex.Message); }
            return Struct;
        }
        public int GetIndex()
        {
            return Index;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button Button = sender as Button; if (Button == null) { return; }
            OnEvent(Button.Name);
        }
        #region Period
        private void PeriodRemove_Click(object sender, RoutedEventArgs e)
        {
            OnEvent("PeriodRemove");
        }
        private void PeriodAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Period_Add();
        }
        private void Period_Clear()
        {
            try
            {
                PeriodContainer.Children.Clear();
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Period_Clear", ex.Message); }
        }
        private void Period_Add()
        {
            try
            {
                if (PeriodContainer.Children.Count > 4) { OnEvent("PeriodCountOver"); return; }
                Period Period = new Period() { Margin = new Thickness(0, 5, 0, 5) }; Period.Import_Init(Period_List);
                PeriodContainer.Children.Add(Period); PeriodRemove.Visibility = Visibility.Visible;
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Period_Add", ex.Message); }
        }
        private void Period_Add(PeriodStruct Struct)
        {
            try
            {
                if (PeriodContainer.Children.Count > 4) { OnEvent("PeriodCountOver"); return; }
                Period Period = new Period() { Margin = new Thickness(0, 5, 0, 5) }; Period.Import_Init(Period_List);
                Period.Import_Value(Struct);
                PeriodContainer.Children.Add(Period); PeriodRemove.Visibility = Visibility.Visible;
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Period_Add", ex.Message); }
        }
        public void Period_Remove()
        {
            try
            {
                int Count = PeriodContainer.Children.Count - 1;
                for (int i = Count; i >= 0; i--)
                {
                    Period Period = PeriodContainer.Children[i] as Period; if (Period == null) { continue; }
                    if (Period.IsChecked) { PeriodContainer.Children.Remove(Period); }
                }
                if (PeriodContainer.Children.Count == 0) { PeriodRemove.Visibility = Visibility.Hidden; }
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Period_Remove", ex.Message); }
        }
        #endregion
        private void PHTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Key != Key.Enter) { return; }
                PHTextBox PHTextBox = sender as PHTextBox; if (PHTextBox == null) { return; }
                int Temp = 0; if (!int.TryParse(PHTextBox.Text, out Temp)) { OnEvent("InputError"); }
                switch (PHTextBox.Name)
                {
                    case "PHTextBox2": { if (Temp > 200) { PHTextBox.Text = "200"; } else if (Temp <= 0) { PHTextBox.Text = null; } } break;
                    case "PHTextBox3": { if (Temp > 400) { PHTextBox.Text = "400"; } else if (Temp < -30) { PHTextBox.Text = "-30"; } } break;
                    case "PHTextBox4": { if (Temp > 400) { PHTextBox.Text = "400"; } else if (Temp < -30) { PHTextBox.Text = "-30"; } } break;
                    case "PHTextBox5": { if (Temp > 300) { PHTextBox.Text = "300"; } else if (Temp <= 0) { PHTextBox.Text = null; } } break;
                }
            }
            catch (Exception ex) { OnEvent("Error", Flag, "PHTextBox_KeyUp", ex.Message); }
        }
        private void PHTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                PHTextBox PHTextBox = sender as PHTextBox; if (PHTextBox == null) { return; }
                //PHTextBox.ScrollToEnd();
                //PHTextBox.Select(PHTextBox.Text.Length, 0);
            }
            catch (Exception ex) { OnEvent("Error", Flag, "PHTextBox_KeyDown", ex.Message); }
        }
        #endregion
        #region Command
        #endregion
    }
}
