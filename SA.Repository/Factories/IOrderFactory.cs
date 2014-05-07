using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using SA.Repository.Enums;

namespace SA.Repository.Factories
{
    public interface IOrderFactory
    {
        Pedido CreateOrder(OrderType type);
    }
}
