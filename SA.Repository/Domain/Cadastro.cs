using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using SA.Repository.Enums;
using NHibernate;

namespace SA.Repository.Domain
{
    [HasSelfValidation]
    public class Cadastro : Entity
    {
        public virtual PersonType Tipo { get; set; }
        public virtual ActiveInactiveType Situacao { get; set; }
        public virtual string Nome { get; set; }
        public virtual string RazaoSocial { get; set; }
        public virtual string Contato { get; set; }
        public virtual string CPF { get; set; }
        public virtual string RG { get; set; }
        
        public virtual Cep CorrespCep { get; set; }
        public virtual int CorrespNumero { get; set; }
        public virtual string CorrespComplemento { get; set; }

        public virtual string Telefone { get; set; }
        public virtual string Celular { get; set; }
        public virtual string Fax { get; set; }
        public virtual string EMail { get; set; }

        public virtual DateTime DataNascimento { get; set; }
        public virtual DateTime DataInclusao { get; set; }
        public virtual DateTime DataAlteracao { get; set; }

        // Property variable
        private IList<ContaFinanceira> _contasFinanceiras = new List<ContaFinanceira>();
        public virtual IList<ContaFinanceira> ContasFinanceiras
        {
            get { return _contasFinanceiras; }
            set { _contasFinanceiras = value; }
        }

        #region CRUD
        public override void AddBySession(ISession session)
        {
            this.DataInclusao = DateTime.Today;
            this.DataAlteracao = DateTime.Today;
            base.AddBySession(session);
        }

        public override void UpdateBySession(ISession session)
        {
            this.DataAlteracao = DateTime.Today;
            base.UpdateBySession(session);
        }        
        #endregion

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<Cadastro>(this);
        }

        [SelfValidation]
        public virtual void ValidarNome(ValidationResults results)
        {
            if (String.IsNullOrWhiteSpace(this.Nome))
                results.AddResult(new ValidationResult("Nome não pode ser vazio!", this, "Nome", null, null));

            if ((this.Nome != null) && (this.Nome.Length > 60))
                results.AddResult(new ValidationResult("Nome deve ter no máximo 60 caracteres!", this, "Nome", null, null));
        }

        [SelfValidation]
        public virtual void ValidarCPF(ValidationResults results)
        {
            if (this.Tipo == PersonType.Juridica)
            {
                if ((!(String.IsNullOrEmpty(this.CPF))) && (this.CPF.Length != 14))
                    results.AddResult(new ValidationResult("CNPJ deve ter 14 dígitos!", this, "CPF", null, null));
            }
            else
            {
                if ((!(String.IsNullOrEmpty(this.CPF))) && (this.CPF.Length != 11))
                    results.AddResult(new ValidationResult("CPF deve ter 11 dígitos!", this, "CPF", null, null));
            }
        }

        [SelfValidation]
        public virtual void ValidarRG(ValidationResults results)
        {
            if ((!(String.IsNullOrEmpty(this.RG))) && (this.RG.Length > 8))
            {
                if (this.Tipo == PersonType.Fisica)
                    results.AddResult(new ValidationResult("RG deve ter no máximo 8 dígitos!", this, "RG", null, null));
                else
                    results.AddResult(new ValidationResult("Inscrição Estadual deve ter no máximo 8 dígitos!", this, "RG", null, null));
            }
        }

        #endregion

    }
}
