using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NHibernate.Type;

namespace SA.Repository.Enums
{
    public class ActiveInactiveTypeNHConverter : EnumStringType 
    {

        public ActiveInactiveTypeNHConverter()
            : base(typeof(ActiveInactiveType), 1)
        {
        }

        public override object GetValue(object value)
        {
            if (null == value)
                return String.Empty;

            return ((char)(ActiveInactiveType)value).ToString();
        }

        public override object GetInstance(object value)
        {
            if (null == value)
                return ActiveInactiveType.Active;


            return (ActiveInactiveType)((string)value)[0];
        }  
    }
}
