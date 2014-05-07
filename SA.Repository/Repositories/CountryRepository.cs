using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class CountryRepository : ICountryRepository
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
        public CountryRepository()
        {

        }
        #endregion

        #region CRUD
        public void Add(Pais item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(Pais item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(Pais item)
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
        public Pais GetById(int id)
        {
            return _session.Get<Pais>(id);
        }

        public Pais GetByName(string name)
        {
            Pais item = _session
                .CreateCriteria(typeof(Pais))
                .Add(Restrictions.Eq("Nome", name))
                .UniqueResult<Pais>();
            return item;
        }

        public ICollection<Pais> GetAll()
        {
            var items = _session
                .CreateCriteria(typeof(Pais))
                .List<Pais>();

            return items;
        }
        #endregion
    }
}
