using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace SA.General.Service
{
    public sealed class SharedObjectContext<T> where T : ObjectContext, new()
    {
        #region Constructors
        private SharedObjectContext() { }
        #endregion

        #region Singleton Pattern
        private static readonly T t = new T();
        public static T Context
        {
            get
            {
                return t;
            }
        }
        #endregion

    }
}
