using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace SA.Repository.Domain
{
    [HasSelfValidation]
    public class Cidade : Entity
    {
        public virtual string Nome { get; set; }
        public virtual string DDD { get; set; }
        public virtual Estado Estado { get; set; }

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<Cidade>(this);
        }

        [SelfValidation]
        public virtual void ValidarNome(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Nome))
                results.AddResult(new ValidationResult("Nome da cidade não pode ser vazio!", this, "Nome", null, null));

            if ((this.Nome != null) && (this.Nome.Length > 40))
                results.AddResult(new ValidationResult("Nome da cidade deve ter no máximo 40 caracteres!", this, "Nome", null, null));
        }

        [SelfValidation]
        public virtual void ValidarDDD(ValidationResults results)
        {
            if (!String.IsNullOrWhiteSpace(this.DDD))  
            {
                int i;
                if (!Int32.TryParse(this.DDD, out i))
                    results.AddResult(new ValidationResult("DDD deve possuir apenas dígitos!", this, "DDD", null, null));

                if (this.DDD.Length < 2)
                    results.AddResult(new ValidationResult("DDD deve possuir 2 dígitos!", this, "DDD", null, null));
            }
        }


        [SelfValidation]
        public virtual void ValidarEstado(ValidationResults results)
        {
            if (this.Estado == null)
                results.AddResult(new ValidationResult("Estado da cidade não pode ser vazio!", this, "EstadoID", null, null));
        }

        #endregion

    }
}
