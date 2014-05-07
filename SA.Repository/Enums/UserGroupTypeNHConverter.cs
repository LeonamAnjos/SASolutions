using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Type;

namespace SA.Repository.Enums
{
    public class UserGroupTypeNHConverter : EnumStringType 
    {

        public UserGroupTypeNHConverter() : base(typeof(UserGroupType), 1)
        {
        }

        public override object GetValue(object value)
        {
            if (null == value)
                return String.Empty;

            return ((char)(UserGroupType)value).ToString();
        }

        public override object GetInstance(object code)
        {
            return (UserGroupType)((string)code)[0];
        }  
    }
}
