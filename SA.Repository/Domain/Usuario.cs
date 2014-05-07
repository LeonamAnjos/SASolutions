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
    public class Usuario : Entity
    {
        public virtual string Login { get; set; }
        public virtual string Senha { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
//        public virtual string SituacaoID { get; set; }
        public virtual UsuarioGrupo Grupo { get; set; }

        public virtual ActiveInactiveType Situacao { get; set; }

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<Usuario>(this);
        }

        [SelfValidation]
        public virtual void ValidarLogin(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Login))
                results.AddResult(new ValidationResult("Login do usuário não pode ser vazio!", this, "Login", null, null));

            if ((this.Login != null) && (this.Login.Length > 20))
                results.AddResult(new ValidationResult("Login do usuário deve ter no máximo 20 caracteres!", this, "Login", null, null));
        }

        [SelfValidation]
        public virtual void ValidarNome(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Nome))
                results.AddResult(new ValidationResult("Nome do usuário não pode ser vazio!", this, "Nome", null, null));

            if ((this.Nome != null) && (this.Nome.Length > 60))
                results.AddResult(new ValidationResult("Nome do usuário deve ter no máximo 60 caracteres!", this, "Nome", null, null));
        }

        [SelfValidation]
        public virtual void ValidarSituacao(ValidationResults results)
        {
            if (this.Situacao == null)
                results.AddResult(new ValidationResult("Situação do usuário não pode ser vazio!", this, "Situacao", null, null));
        }

        [SelfValidation]
        public virtual void ValidarGrupo(ValidationResults results)
        {
            if (this.Grupo == null)
                results.AddResult(new ValidationResult("Grupo de usuários não pode ser vazio!", this, "Grupo", null, null));
        }

        #endregion

    }
}
