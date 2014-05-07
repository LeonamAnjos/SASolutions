using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using SA.Repository.Enums;

namespace SA.Repository.Domain
{
    [HasSelfValidation]
    public class ContaFinanceira : Entity
    {
        public virtual FinancialAccountType Tipo { get; set; }
        public virtual ActiveInactiveType Situacao { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        
        public virtual Cep CobrancaCep { get; set; }
        public virtual int CobrancaNumero { get; set; }
        public virtual string CobrancaComplemento { get; set; }

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<ContaFinanceira>(this);
        }

        [SelfValidation]
        public virtual void ValidarNome(ValidationResults results)
        {
            if (this.Cadastro == null)
                results.AddResult(new ValidationResult("Conta financeira deve ser vinculada a um cadastro!", this, "Cadastro", null, null));
        }
        #endregion

    }
}
