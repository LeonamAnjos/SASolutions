using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NHibernate.Type;

namespace SA.Repository.Enums
{
    public class FinancialAccountTypeNHConverter : EnumStringType 
    {

        public FinancialAccountTypeNHConverter()
            : base(typeof(FinancialAccountType), 1)
        {
        }

        public override object GetValue(object value)
        {
            if (null == value)
                return String.Empty;

            return ((char)(FinancialAccountType)value).ToString();
        }

        public override object GetInstance(object value)
        {
            if (null == value)
                return FinancialAccountType.Costumer;


            return (FinancialAccountType)((string)value)[0];
        }  
    }
}
