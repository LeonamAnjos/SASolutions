using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace SA.Repository.Domain
{
    [HasSelfValidation]
    public class Unidade : Entity
    {
        public virtual string Descricao { get; set; }
        public virtual string Simbolo { get; set; }

        #region Validations

        public override ValidationResults Validar()
        {
            return Validation.Validate<Unidade>(this);
        }

        [SelfValidation]
        public virtual void ValidarDescricao(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Descricao))
                results.AddResult(new ValidationResult("Descrição da unidade não pode ser vazio!", this, "Descricao", null, null));

            if ((this.Descricao != null) && (this.Descricao.Length > 40))
                results.AddResult(new ValidationResult("Descrição da unidade deve ter no máximo 40 caracteres!", this, "Descricao", null, null));
        }

        [SelfValidation]
        public virtual void ValidarSimbolo(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Simbolo))
                results.AddResult(new ValidationResult("Símbolo da unidade não pode ser vazio!", this, "Simbolo", null, null));

            if ((this.Simbolo != null) && (this.Simbolo.Length > 5))
                results.AddResult(new ValidationResult("Símbolo da unidade deve ter no máximo 5 caracteres!", this, "Simbolo", null, null));
        }

        #endregion
    }
}
