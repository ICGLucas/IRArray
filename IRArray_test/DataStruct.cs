using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRArray_test
{
    class DataStruct
    {
    }
    // ========== 系統 ==========
    public class EventInfo
    {
        #region Property
        public EventInfo(string Module, string Method, string Option, params object[] Value)
        {
            this.Module = Module;
            this.Method = Method;
            this.Option = Option;
            this.Value = Value;
        }
        public EventInfo(string Option, params object[] Value)
        {
            this.Option = Option;
            this.Value = Value;
        }
        public string Module { get; set; }
        public string Method { get; set; }
        public string Option { get; set; }
        public object[] Value { get; set; }
        #endregion
    }
}
