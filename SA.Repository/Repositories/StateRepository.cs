using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class StateRepository : IStateRepository
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
        public StateRepository()
        {
        }
        #endregion

        #region CRUD
        public void Add(Estado item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }
        public void Update(Estado item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(Estado item)
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
        public Estado GetById(int id)
        {
            return _session.Get<Estado>(id);
        }

        public Estado GetByName(string name)
        {
            Estado item = _session
                .CreateCriteria(typeof(Estado))
                .Add(Restrictions.Eq("Nome", name))
                .UniqueResult<Estado>();
            return item;
        }

        public ICollection<Estado> GetAll()
        {
            var items = _session
                .CreateCriteria(typeof(Estado))
                .List<Estado>();

            return items;
        }
        #endregion
    }
}
