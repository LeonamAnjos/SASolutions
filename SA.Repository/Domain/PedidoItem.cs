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
    public class PedidoItem : Entity
    {
        public virtual Pedido  Pedido { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Double  Quantidade { get; set; }
        public virtual Double  ValorUnitario { get; set; }
        public virtual Double  Valor { get; set; }
        public virtual Double ValorDesconto { get; set; }        

        #region Validations
        public override ValidationResults Validar()
        {
            return Validation.Validate<PedidoItem>(this);
        }
        
        [SelfValidation]
        public virtual void ValidarPedido(ValidationResults results)
        {
            if (Pedido == null)
                results.AddResult(new ValidationResult("Pedido não pode ser vazio!", this, "Pedido", null, null));
        }

        [SelfValidation]
        public virtual void ValidarProduto(ValidationResults results)
        {
            if (Produto == null)
                results.AddResult(new ValidationResult("Produto não pode ser vazio!", this, "Produto", null, null));
        }

        [SelfValidation]
        public virtual void ValidarQuantidade(ValidationResults results)
        {
            if (Quantidade <= 0)
                results.AddResult(new ValidationResult("Quantidade informada deve ser maior que ZERO!", this, "Quantidade", null, null));
        }

        [SelfValidation]
        public virtual void ValidarValorUnitario(ValidationResults results)
        {
            if (ValorUnitario < 0)
                results.AddResult(new ValidationResult("Valor Unitário não pode ser negativo!", this, "ValorUnitario", null, null));
        }

        [SelfValidation]
        public virtual void ValidarValor(ValidationResults results)
        {
            if (Valor < 0)
                results.AddResult(new ValidationResult("Valor não pode ser negativo!", this, "Valor", null, null));
        }

        [SelfValidation]
        public virtual void ValidarValorDesconto(ValidationResults results)
        {
            if (Valor < 0)
                results.AddResult(new ValidationResult("Valor Desconto não pode ser negativo!", this, "ValorDesconto", null, null));
        }

        #endregion

    }
}
