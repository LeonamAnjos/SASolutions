using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace SA.Repository.Domain
{
    [HasSelfValidation]
    public class Fabricante : Entity
    {
        public virtual string Nome { get; set; }

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<Fabricante>(this);
        }

        [SelfValidation]
        public virtual void ValidarNome(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Nome))
                results.AddResult(new ValidationResult("Nome do fabricante não pode ser vazio!", this, "Nome", null, null));

            if ((this.Nome != null) && (this.Nome.Length > 40))
                results.AddResult(new ValidationResult("Nome do fabricante deve ter no máximo 40 caracteres!", this, "Nome", null, null));
        }

        #endregion
    }
}
