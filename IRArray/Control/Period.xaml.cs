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
    /// Period.xaml 的互動邏輯
    /// </summary>
    public partial class Period : UserControl
    {
        #region Parameter
        private string Flag = "Period";
        #endregion
        #region Property
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
            "IsChecked",
            typeof(bool),
            typeof(Period),
            new PropertyMetadata(false)
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
        public Period()
        {
            InitializeComponent();
        }
        //public void Initialize()
        //{
        //}
        public void Import_Init(List<PairStruct> List)
        {
            try
            {
                ComboBox1.ItemsSource = List;
                ComboBox2.ItemsSource = List;
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Init", ex.Message); }
        }
        public void Import_Value(PeriodStruct Struct)
        {
            try
            {
                ComboBox1.SelectedValue = Struct.Week1; TimeTextBox1.Value = Struct.Value1;
                ComboBox2.SelectedValue = Struct.Week2; TimeTextBox2.Value = Struct.Value2;
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Value", ex.Message); }
        }
        public PeriodStruct Export()
        {
            PeriodStruct Struct = new PeriodStruct();
            Struct.Week1 = (int)ComboBox1.SelectedValue;
            Struct.Week2 = (int)ComboBox2.SelectedValue;
            Struct.Value1 = TimeTextBox1.Value;
            Struct.Value2 = TimeTextBox2.Value;
            return Struct;
        }
        #endregion
        #region Command
        #endregion
    }
}
