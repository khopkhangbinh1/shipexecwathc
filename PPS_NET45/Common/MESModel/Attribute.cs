using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESModel
{
    public class TableNameAttribute : Attribute
    {
        private string _value;

        public TableNameAttribute(string Value)
        {
            _value = Value;
        }

        public string Value
        {
            get
            {
                return _value;
            }
        }
    }

    public class DbLlobAttribute : Attribute
    {
        private string _value;

        public DbLlobAttribute(string Value)
        {
            _value = Value;
        }

        public string Value
        {
            get
            {
                return _value;
            }
        }
    }
}
