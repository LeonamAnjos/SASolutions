using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Infrastructure
{
    public interface ISACloneable : ICloneable
    {
        object CopyTo(object instance);
    }
}
