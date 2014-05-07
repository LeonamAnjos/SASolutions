using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SA.Repository.Enums
{
    public enum PersonType
    {
        [Description("Física")]
        Fisica = 'F',
        [Description("Jurídica")]
        Juridica = 'J'
    }
}
