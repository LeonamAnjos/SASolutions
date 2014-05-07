using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NHibernate.Type;

namespace SA.Repository.Enums
{
    public class OrderTypeNHConverter : EnumStringType 
    {

        public OrderTypeNHConverter()
            : base(typeof(OrderType), 1)
        {
        }

        public override object GetValue(object value)
        {
            if (null == value)
                return String.Empty;

            return ((char)(OrderType)value).ToString();
        }

        public override object GetInstance(object value)
        {
            if (null == value)
                return OrderType.PurchaseOrder;


            return (OrderType)((string)value)[0];
        }  
    }
}
