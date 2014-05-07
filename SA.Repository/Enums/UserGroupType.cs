using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SA.Repository.Enums
{
    public enum UserGroupType
    {
        [Description("Administrador")]
        Administrator = 'A',
        [Description("Funcionário")]
        Employee = 'E',
        [Description("Consumidor")]
        Visitor = 'V'
    }
    
}
