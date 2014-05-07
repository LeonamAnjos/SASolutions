using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace SA.Repository.Domain
{
    [HasSelfValidation]
    public class Vendedor : Entity
    {
        public virtual string Nome { get; set; }

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<Vendedor>(this);
        }

        [SelfValidation]
        public virtual void ValidarNome(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Nome))
                results.AddResult(new ValidationResult("Nome do vendedor não pode ser vazio!", this, "Nome", null, null));

            if ((this.Nome != null) && (this.Nome.Length > 40))
                results.AddResult(new ValidationResult("Nome do vendedor deve ter no máximo 40 caracteres!", this, "Nome", null, null));
        }

        #endregion
    }
}
