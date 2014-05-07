using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Repository.Domain
{
    public class CadastroJuridico
    {
        private readonly Cadastro _cadastro;

        public static CadastroJuridico Create(Cadastro cadastro) 
        {
            if (cadastro == null)
                throw new ArgumentNullException("cadastro");

            if (cadastro.Tipo != Enums.PersonType.Juridica)
                throw new ArgumentException("cadastro.tipo");

            return new CadastroJuridico(cadastro);
        }

        private CadastroJuridico(Cadastro cadastro)
        {
            this._cadastro = cadastro;
        }
    }
}
