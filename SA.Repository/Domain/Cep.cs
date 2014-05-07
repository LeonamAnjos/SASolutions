using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace SA.Repository.Domain
{
    [HasSelfValidation]
    public class Cep : Entity
    {
        public virtual string CEP { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Bairro { get; set; }
        public virtual Cidade Cidade { get; set; }

        public override ValidationResults Validar()
        {
            return Validation.Validate<Cep>(this);
        }

        #region Validations

        [SelfValidation]
        public virtual void ValidarCEP(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.CEP))
                results.AddResult(new ValidationResult("CEP não pode ser vazio!", this, "CEP", null, null));

            if ((this.CEP != null) && (this.CEP.Length != 8))
                results.AddResult(new ValidationResult("CEP deve ter 8 dígitos!", this, "CEP", null, null));

            if (this.CEP != null)
            {
                try
                {
                    Int32.Parse(this.CEP);
                }
                catch (Exception)
                {
                    results.AddResult(new ValidationResult("CEP deve possuir apenas dígitos!", this, "CEP", null, null));
                }
            }
        }

        [SelfValidation]
        public virtual void ValidarLogradouro(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Logradouro))
                results.AddResult(new ValidationResult("Logradouro não pode ser vazio!", this, "Logradouro", null, null));

            if ((this.Logradouro != null) && (this.Logradouro.Length > 60))
                results.AddResult(new ValidationResult("Logradouro deve ter no máximo 60 caracteres!", this, "Logradouro", null, null));
        }

        [SelfValidation]
        public virtual void ValidarBairro(ValidationResults results)
        {
            if ((this.Bairro != null) && (this.Bairro.Length > 40))
                results.AddResult(new ValidationResult("Bairro deve ter no máximo 40 caracteres!", this, "Bairro", null, null));
        }

        [SelfValidation]
        public virtual void ValidarCidade(ValidationResults results)
        {
            if (this.Cidade == null)
                results.AddResult(new ValidationResult("Cidade do CEP não pode ser vazio!", this, "CidadeID", null, null));

        }

        #endregion
    }
}
