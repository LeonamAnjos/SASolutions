using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SA.Repository.Enums
{
    public enum FinancialAccountType
    {
        [Description("Cliente")]
        Costumer = 'C',
        [Description("Fornecedor")]
        Supplier = 'F',
        [Description("Banco")]
        Bank = 'B'
    }
}
