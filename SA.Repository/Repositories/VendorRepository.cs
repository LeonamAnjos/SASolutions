using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class VendorRepository : IVendorRepository
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
        public VendorRepository()
        {
        }
        #endregion

        #region CRUD
        public void Add(Vendedor item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(Vendedor item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(Vendedor item)
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

        public Vendedor GetById(int id)
        {
            return _session.Get<Vendedor>(id);
        }

        public Vendedor GetByName(string name)
        {
            Vendedor item = _session
                .CreateCriteria(typeof(Vendedor))
                .Add(Restrictions.Eq("Nome", name))
                .UniqueResult<Vendedor>();
            return item;
        }

        public ICollection<Vendedor> GetAll()
        {
            var items = _session.CreateQuery("from Vendedor").List<Vendedor>();
            return items;
        }
        #endregion

    }
}
