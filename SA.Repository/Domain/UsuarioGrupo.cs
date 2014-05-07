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
    public class UsuarioGrupo : Entity
    {
        public virtual string Descricao { get; set; }
        public virtual UserGroupType Tipo { get; set; }

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<UsuarioGrupo>(this);
        }

        [SelfValidation]
        public virtual void ValidarDescricao(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Descricao))
                results.AddResult(new ValidationResult("Descrição do grupo de usuários não pode ser vazio!", this, "Descricao", null, null));

            if ((this.Descricao != null) && (this.Descricao.Length > 30))
                results.AddResult(new ValidationResult("Descrição do grupo de usuários deve ter no máximo 30 caracteres!", this, "Descricao", null, null));
        }

        [SelfValidation]
        public virtual void ValidarTipo(ValidationResults results)
        {
            if (this.Tipo == null)
                results.AddResult(new ValidationResult("Tipo do grupo de usuários não pode ser vazio!", this, "Tipo", null, null));
        }
        #endregion


    }
}
