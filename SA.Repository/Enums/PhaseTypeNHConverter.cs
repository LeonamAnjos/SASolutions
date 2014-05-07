using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NHibernate.Type;

namespace SA.Repository.Enums
{
    public class PhaseTypeNHConverter : EnumStringType 
    {

        public PhaseTypeNHConverter()
            : base(typeof(PhaseType), 1)
        {
        }

        public override object GetValue(object value)
        {
            if (null == value)
                return String.Empty;

            return ((char)(PhaseType)value).ToString();
        }

        public override object GetInstance(object value)
        {
            if (null == value)
                return PhaseType.Tender;


            return (PhaseType)((string)value)[0];
        }  
    }
}
