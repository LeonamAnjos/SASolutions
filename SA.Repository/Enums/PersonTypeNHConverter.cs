using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NHibernate.Type;

namespace SA.Repository.Enums
{
    public class PersonTypeNHConverter : EnumStringType 
    {

        public PersonTypeNHConverter()
            : base(typeof(PersonType), 1)
        {
        }

        public override object GetValue(object value)
        {
            if (null == value)
                return String.Empty;

            return ((char)(PersonType)value).ToString();
        }

        public override object GetInstance(object value)
        {
            if (null == value)
                return PersonType.Fisica;


            return (PersonType)((string)value)[0];
        }  
    }
}
