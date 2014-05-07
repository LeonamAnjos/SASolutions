using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using SA.Repository.Extensions;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        #region Properties
        private ISession _session
        {
            get
            {
                return NHibernateHelper.OpenSession();
            }
        }
        #endregion

        #region Constructors
        public OrderRepository()
        {

        }
        #endregion

        #region CRUD
        public void Add(Pedido item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.Atualizar();
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(Pedido item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.Atualizar();
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(Pedido item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.RemoveBySession(session);
                transaction.Commit();
            }
        }
        #endregion

        #region Gets

        public Pedido GetById(int id)
        {
            return _session.Get<Pedido>(id);
        }

        public ICollection<Pedido> GetByDate(DateTime value)
        {
            return _session
                .CreateCriteria(typeof(Pedido))
                .Add(Restrictions.Eq("Data", value))
                .List<Pedido>();
        }

        public ICollection<Pedido> GetAll()
        {
            var items = _session.CreateQuery("from Pedido").List<Pedido>();
            return items;
        }

        #endregion


    }
}
