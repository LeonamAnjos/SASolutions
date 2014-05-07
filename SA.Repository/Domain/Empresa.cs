using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace SA.Repository.Domain
{
    [HasSelfValidation]
    public class Empresa : Entity
    {
        public virtual string Nome { get; set; }
        public virtual string CNPJ { get; set; }
        public virtual string InscricaoEstadual { get; set; }
        public virtual Cep Cep { get; set; }
        public virtual int Numero { get; set; }
        public virtual string Complemento { get; set; }

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<Empresa>(this);
        }

        [SelfValidation]
        public virtual void ValidarNome(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Nome))
                results.AddResult(new ValidationResult("Nome da empresa não pode ser vazio!", this, "Nome", null, null));

            if ((this.Nome != null) && (this.Nome.Length > 40))
                results.AddResult(new ValidationResult("Nome da empresa deve ter no máximo 40 caracteres!", this, "Nome", null, null));
        }

        [SelfValidation]
        public virtual void ValidarCNPJ(ValidationResults results)
        {
            if ((this.CNPJ != null) && (this.CNPJ.Length != 14))
                results.AddResult(new ValidationResult("CNPJ da empresa deve ter 14 dígitos!", this, "CNPJ", null, null));
        }

        [SelfValidation]
        public virtual void ValidarComplemento(ValidationResults results)
        {
            if ((this.Complemento != null) && (this.Complemento.Length > 30))
                results.AddResult(new ValidationResult("Complemento deve ter no máximo 30 caracteres!", this, "Complemento", null, null));
        }

        #endregion

    }
}
