using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface IOrderRepository
    {
        #region CRUD
        void Add(Pedido item);
        void Update(Pedido item);
        void Remove(Pedido item);
        #endregion

        Pedido GetById(int id);
        ICollection<Pedido> GetByDate(DateTime value);
        ICollection<Pedido> GetAll();
    }
}
