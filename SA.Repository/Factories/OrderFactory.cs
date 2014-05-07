using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using SA.Repository.Enums;
using Microsoft.Practices.Unity;

namespace SA.Repository.Factories
{
    public class OrderFactory : IOrderFactory
    {
        private IUnityContainer _container;
        private int _diasValidadePedido = 30;

        public OrderFactory(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this._container = container;
        }

        public Pedido CreateOrder(OrderType type)
        {
            Pedido pedido = new Pedido();
            pedido.Tipo = type;
            pedido.Data = DateTime.Today;
            pedido.Hora = DateTime.Now;
            pedido.DataValidade = (DateTime.Today.AddDays(_diasValidadePedido));
            pedido.Fase = PhaseType.Tender;

            switch (type)
            {
                case OrderType.PurchaseOrder:
                    if (_container.IsRegistered(typeof(Cadastro), "Fornecedor"))
                        pedido.Cadastro = (Cadastro)_container.Resolve(typeof(Cadastro), "Fornecedor");
                    break;
                case OrderType.SalesOrder:
                    if (_container.IsRegistered(typeof(Cadastro), "Cliente"))
                        pedido.Cadastro = (Cadastro)_container.Resolve(typeof(Cadastro), "Cliente");

                    if (_container.IsRegistered(typeof(Vendedor), "Vendedor"))
                        pedido.Vendedor = (Vendedor)_container.Resolve(typeof(Vendedor), "Vendedor");

                    break;
                default:
                    throw new ArgumentException("Tipo de pedido inválido! (" + type.ToString() + ")");
            }

            return pedido;
        }
    }
}
