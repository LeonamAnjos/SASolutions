using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SA.Repository.Enums
{
    public enum PhaseType
    {
        [Description("Orçamento")]
        Tender = 'O',
        [Description("Pedido")]
        Order = 'P',
        [Description("Pedido Faturado")]
        BilledOrder = 'F'

    }
}
