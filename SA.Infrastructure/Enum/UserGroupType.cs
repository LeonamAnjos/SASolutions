using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SA.Infrastructure
{
    public enum UserGroupType_DEPRECATED
    {
        [Description("Administrador")]
        Administrator = 'A',
        [Description("Funcionário")]
        Employee = 'E',
        [Description("Consumidor")]
        Visitor = 'V'
    }
    
}
