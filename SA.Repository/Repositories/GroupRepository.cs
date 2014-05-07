using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class GroupRepository : IGroupRepository
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
        public GroupRepository()
        {
        }
        #endregion

        #region CRUD
        public void Add(Grupo item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(Grupo item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(Grupo item)
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
        public Grupo GetById(int id)
        {
            return _session.Get<Grupo>(id);
        }

        public Grupo GetByDescription(string value)
        {
            var item = _session
                .CreateCriteria(typeof(Grupo))
                .Add(Restrictions.Eq("Descricao", value))
                .UniqueResult<Grupo>();
            return item;
        }

        public ICollection<Grupo> GetAll()
        {
            var items = _session
                .CreateCriteria(typeof(Grupo))
                .List<Grupo>();

            return items;
        }

        #endregion
    }
}
