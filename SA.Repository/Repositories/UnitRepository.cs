using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class UnitRepository : IUnitRepository
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
        public UnitRepository()
        {
        }
        #endregion

        #region CRUD
        public void Add(Unidade item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(Unidade item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(Unidade item)
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
        public Unidade GetById(int id)
        {
            return _session.Get<Unidade>(id);
        }

        public Unidade GetBySymbol(string value)
        {
            var item = _session
                .CreateCriteria(typeof(Unidade))
                .Add(Restrictions.Eq("Simbolo", value))
                .UniqueResult<Unidade>();
            return item;
        }

        public ICollection<Unidade> GetAll()
        {
            var items = _session
                .CreateCriteria(typeof(Unidade))
                .List<Unidade>();

            return items;
        }
        #endregion
    }
}
