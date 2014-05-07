using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace SA.Infrastructure.Event
{
    public interface IEntityChangedEventArgs
    {
        CrudType CrudType { get; }
        object Entity { get; }
    }
}
