using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace SA.Repository.Domain
{
    [HasSelfValidation]
    public class Grupo : Entity
    {
        public virtual string Descricao { get; set; }

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<Grupo>(this);
        }

        [SelfValidation]
        public virtual void ValidarDescricao(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Descricao))
                results.AddResult(new ValidationResult("Descrição do grupo não pode ser vazio!", this, "Descricao", null, null));

            if ((this.Descricao != null) && (this.Descricao.Length > 40))
                results.AddResult(new ValidationResult("Descrição do grupo deve ter no máximo 40 caracteres!", this, "Descricao", null, null));
        }

        #endregion
    }
}
