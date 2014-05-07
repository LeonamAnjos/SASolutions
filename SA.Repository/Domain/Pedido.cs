using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using SA.Repository.Enums;
using NHibernate;
using SA.Repository.Extensions;

namespace SA.Repository.Domain
{
    [HasSelfValidation]
    public class Pedido : Entity
    {
        public virtual Cadastro Cadastro { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime Hora { get; set; }

        public virtual OrderType Tipo { get; set; }
        public virtual Vendedor Vendedor { get; set; }
        public virtual DateTime DataValidade { get; set; }

        public virtual PhaseType Fase { get; set; }

        public virtual Double Valor { get; set; }
        public virtual Double ValorDesconto { get; set; }
        public virtual Double ValorDescontoTotal { get; set; }

        // Property variable
        private IList<PedidoItem> _itens = new List<PedidoItem>();
        public virtual IList<PedidoItem> Itens
        {
            get { return _itens; }
            set { _itens = value; }
        }

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<Pedido>(this);
        }
        
        [SelfValidation]
        public virtual void ValidarCadastro(ValidationResults results)
        {
            if (Cadastro == null)
            {
                if (Tipo == OrderType.SalesOrder)
                    results.AddResult(new ValidationResult("Cliente não pode ser vazio!", this, "CadastroID", null, null));
                else if (Tipo == OrderType.PurchaseOrder)
                    results.AddResult(new ValidationResult("Fornecedor não pode ser vazio!", this, "CadastroID", null, null));
                else
                    results.AddResult(new ValidationResult("Cadastro não pode ser vazio!", this, "CadastroID", null, null));
            }
            else
            {
                if ((Tipo == OrderType.SalesOrder) && (!Cadastro.EhCliente()))
                    results.AddResult(new ValidationResult("Cadastro tem que ser de um cliente!", this, "CadastroID", null, null));

                if ((Tipo == OrderType.PurchaseOrder) && (!Cadastro.EhFornecedor()))
                    results.AddResult(new ValidationResult("Cadastro tem que ser de um fornecedor!", this, "CadastroID", null, null));

            }
        }

        [SelfValidation]
        public virtual void ValidarData(ValidationResults results)
        {
            if (DateTime.MinValue.Equals(this.Data))
                results.AddResult(new ValidationResult("Data de emissão do pedido/orçamento não pode ser vazio!", this, "Data", null, null));
        }

        [SelfValidation]
        public virtual void ValidarHora(ValidationResults results)
        {
            if (DateTime.MinValue.Equals(this.Hora))
                results.AddResult(new ValidationResult("Hora de emissão do pedido/orçamento não pode ser vazio!", this, "Hora", null, null));
        }

        [SelfValidation]
        public virtual void ValidarTipo(ValidationResults results)
        {
            if (Tipo == null)
                results.AddResult(new ValidationResult("Tipo do pedido não pode ser vazio!", this, "Tipo", null, null));
        }

        [SelfValidation]
        public virtual void ValidarFase(ValidationResults results)
        {
            if (Fase == null)
                results.AddResult(new ValidationResult("Fase do pedido/orçamento não pode ser vazio!", this, "Fase", null, null));
        }

        [SelfValidation]
        public virtual void ValidarDataValidade(ValidationResults results)
        {
            if (Fase == PhaseType.Tender)
                if (DateTime.MinValue.Equals(DataValidade))
                    results.AddResult(new ValidationResult("Data de validade do orçamento não pode ser vazio!", this, "DataValidade", null, null));
        }

        [SelfValidation]
        public virtual void ValidarVendedor(ValidationResults results)
        {
            if (Tipo == OrderType.SalesOrder)
                if (Vendedor == null)
                    results.AddResult(new ValidationResult("Vendedor não pode ser vazio!", this, "VendedorID", null, null));
        }

        [SelfValidation]
        public virtual void ValidarValores(ValidationResults results)
        {
            Double valor = 0;
            Double valorDesconto = 0;

            foreach (var item in Itens)
            {
                valor += item.Valor;
                valorDesconto += item.ValorDesconto;
            }

            if(this.ValorDescontoTotal != (this.ValorDesconto + valorDesconto))
                results.AddResult(new ValidationResult(String.Format("Valor total de desconto no pedido está incorreto! Esperado: {0:0.00} Encontrado: {1:0.00}", this.ValorDesconto + valorDesconto, this.ValorDescontoTotal), this, "ValorDescontoTotal", null, null));

            if (this.Valor != (valor - this.ValorDesconto))
                results.AddResult(new ValidationResult(String.Format("Valor do pedido está incorreto! Esperado: {0:0.00} Encontrado: {1:0.00}", valor - this.ValorDesconto, this.Valor), this, "Valor", null, null));
        }

        #endregion

    }
}
