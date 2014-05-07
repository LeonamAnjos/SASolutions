using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Enums;
using SA.Repository.Domain;

namespace SA.Repository.Extensions
{
    public static class CadastroExtension
    {
        public static bool EhCliente(this Cadastro cadastro)
        {
            var c = from cf in cadastro.ContasFinanceiras where cf.Tipo == FinancialAccountType.Costumer && cf.Situacao == ActiveInactiveType.Active select cf.Id;
            return (c.Count() > 0);
        }

        public static bool EhFornecedor(this Cadastro cadastro)
        {
            var c = from cf in cadastro.ContasFinanceiras where cf.Tipo == FinancialAccountType.Supplier && cf.Situacao == ActiveInactiveType.Active select cf.Id;
            return (c.Count() > 0);
        }
    }
}
