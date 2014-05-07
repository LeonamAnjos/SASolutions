using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace SA.Repository.Domain
{
    [HasSelfValidation]
    public class Produto : Entity
    {
        public virtual string Referencia { get; set; }
        public virtual string Nome { get; set; }
        public virtual Unidade Unidade { get; set; }
        public virtual Fabricante Fabricante { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual SubGrupo SubGrupo { get; set; }

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<Produto>(this);
        }

        [SelfValidation]
        public virtual void ValidarReferencia(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Referencia))
                results.AddResult(new ValidationResult("Referência do produto não pode ser vazio!", this, "Referencia", null, null));

            if ((this.Referencia != null) && (this.Referencia.Length > 30))
                results.AddResult(new ValidationResult("Referência do produto deve ter no máximo 40 caracteres!", this, "Referencia", null, null));
        }

        [SelfValidation]
        public virtual void ValidarNome(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Nome))
                results.AddResult(new ValidationResult("Nome do produto não pode ser vazio!", this, "Nome", null, null));

            if ((this.Nome != null) && (this.Nome.Length > 40))
                results.AddResult(new ValidationResult("Nome do produto deve ter no máximo 40 caracteres!", this, "Nome", null, null));
        }

        #endregion
    }
}
