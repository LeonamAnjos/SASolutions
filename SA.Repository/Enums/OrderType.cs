using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SA.Repository.Enums
{
    public enum OrderType
    {
        [Description("Venda")]
        SalesOrder = 'V',
        [Description("Compra")]
        PurchaseOrder = 'C'
    }
}
