using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SA.Infrastructure
{
    public enum CrudType    {
        [Description("Inclusão")]
        Create,
        [Description("Consulta")]
        Read,
        [Description("Alteração")]
        Update,
        [Description("Exclusão")]
        Delete
    }
}
