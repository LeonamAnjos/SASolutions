using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace SA.Repository.Domain
{
    [HasSelfValidation]
    public class Pais : Entity
    {
        public virtual string Nome { get; set; }
        public virtual string Sigla { get; set; }

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<Pais>(this);
        }

        [SelfValidation]
        public virtual void ValidarNome(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Nome))
                results.AddResult(new ValidationResult("Nome do País não pode ser vazio!", this, "Nome", null, null));

            if ((this.Nome != null) && (this.Nome.Length > 40))
                results.AddResult(new ValidationResult("Nome do País deve ter no máximo 40 caracteres!", this, "Nome", null, null));
        }

        [SelfValidation]
        public virtual void ValidarSigla(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Sigla))
                results.AddResult(new ValidationResult("Sigla do País não pode ser vazio!", this, "Sigla", null, null));

            if ((this.Sigla != null) && (this.Sigla.Length > 2))
                results.AddResult(new ValidationResult("Sigla do País deve ter no máximo 2 caracteres!", this, "Sigla", null, null));
        }

        #endregion
    }
}
