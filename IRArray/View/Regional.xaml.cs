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
    /// Regional.xaml 的互動邏輯
    /// </summary>
    public partial class Regional : UserControl
    {
        #region Parameter
        private string Flag = "Regional";
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
        public Regional()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
            Button1.Click += Button_Click;
            Button2.Click += Button_Click;
            Button3.Click += Button_Click;
            Button4.Click += Button_Click;
            Button5.Click += Button_Click;
            Button6.Click += Button_Click;
            Button7.Click += Button_Click;
            Button8.Click += Button_Click;
            Button9.Click += Button_Click;
            Button10.Click += Button_Click;
            Button11.Click += Button_Click;
            Button12.Click += Button_Click;
            Button13.Click += Button_Click;
            Button14.Click += Button_Click;
            Button15.Click += Button_Click;
            Button16.Click += Button_Click;
        }
        public void Import_Value(List<RegionalStruct> List)
        {
            try
            {
                if (List[0].Text != null) Button1.Content = List[0].Text; Button1.Tag = (List[0].Switch) ? "1" : null;
                if (List[1].Text != null) Button2.Content = List[1].Text; Button2.Tag = (List[1].Switch) ? "1" : null;
                if (List[2].Text != null) Button3.Content = List[2].Text; Button3.Tag = (List[2].Switch) ? "1" : null;
                if (List[3].Text != null) Button4.Content = List[3].Text; Button4.Tag = (List[3].Switch) ? "1" : null;
                if (List[4].Text != null) Button5.Content = List[4].Text; Button5.Tag = (List[4].Switch) ? "1" : null;
                if (List[5].Text != null) Button6.Content = List[5].Text; Button6.Tag = (List[5].Switch) ? "1" : null;
                if (List[6].Text != null) Button7.Content = List[6].Text; Button7.Tag = (List[6].Switch) ? "1" : null;
                if (List[7].Text != null) Button8.Content = List[7].Text; Button8.Tag = (List[7].Switch) ? "1" : null;
                if (List[8].Text != null) Button9.Content = List[8].Text; Button9.Tag = (List[8].Switch) ? "1" : null;
                if (List[9].Text != null) Button10.Content = List[9].Text; Button10.Tag = (List[9].Switch) ? "1" : null;
                if (List[10].Text != null) Button11.Content = List[10].Text; Button11.Tag = (List[10].Switch) ? "1" : null;
                if (List[11].Text != null) Button12.Content = List[11].Text; Button12.Tag = (List[11].Switch) ? "1" : null;
                if (List[12].Text != null) Button13.Content = List[12].Text; Button13.Tag = (List[12].Switch) ? "1" : null;
                if (List[13].Text != null) Button14.Content = List[13].Text; Button14.Tag = (List[13].Switch) ? "1" : null;
                if (List[14].Text != null) Button15.Content = List[14].Text; Button15.Tag = (List[14].Switch) ? "1" : null;
                if (List[15].Text != null) Button16.Content = List[15].Text; Button16.Tag = (List[15].Switch) ? "1" : null;
            }
            catch (Exception ex) { OnEvent("Error", Flag, "Import_Value", ex.Message); }
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
