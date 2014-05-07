using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Enums;
using SA.Repository.Domain;

namespace SA.Repository.Extensions
{
    public static class PedidoExtension
    {
        public static void Atualizar(this Pedido pedido)
        {
            pedido.Valor = 0;
            pedido.ValorDescontoTotal = pedido.ValorDesconto;    

            foreach (var item in pedido.Itens)
            {
                pedido.ValorDescontoTotal += item.ValorDesconto;
                pedido.Valor += item.Valor; // item net value - after deducting discount
            }

            pedido.Valor -= pedido.ValorDesconto; // order net value - dedicting order discount
        }
    }
}
