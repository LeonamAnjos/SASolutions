using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.General.Application
{
    public sealed class Application
    {
        #region Properties
        private static Application corrente = new Application();
        private Usuario usuario;
        #endregion

        #region Constructor
        private Application()
        {
            
        }
        #endregion

        #region Getters and Setters
        public static Application Corrente
        {
            get
            {
                return Corrente;
            }
        }
        public Usuario Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
            }
        }

        #endregion
    }
}
