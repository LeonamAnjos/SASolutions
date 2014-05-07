using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SA.Repository.Enums
{
    public enum ActiveInactiveType
    {
        [Description("Ativo")]
        Active = 'A',
        [Description("Inativo")]
        Inactive = 'I'
    }
}
