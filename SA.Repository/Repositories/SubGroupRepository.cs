using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class SubGroupRepository : ISubGroupRepository
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
        public SubGroupRepository()
        {
        }
        #endregion

        #region CRUD
        public void Add(SubGrupo item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(SubGrupo item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(SubGrupo item)
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
        public SubGrupo GetById(int id)
        {
            return _session.Get<SubGrupo>(id);
        }

        public SubGrupo GetById(int id, Grupo grupo)
        {
            var item = _session
                .CreateCriteria(typeof(SubGrupo))
                .Add(Restrictions.Eq("Id", id))
                .Add(Restrictions.Eq("Grupo", grupo))
                .UniqueResult<SubGrupo>();
            return item;
        }

        public SubGrupo GetByDescription(string value)
        {
            var item = _session
                .CreateCriteria(typeof(SubGrupo))
                .Add(Restrictions.Eq("Descricao", value))
                .UniqueResult<SubGrupo>();
            return item;
        }

        public ICollection<SubGrupo> GetAll()
        {
            var items = _session
                .CreateCriteria(typeof(SubGrupo))
                .List<SubGrupo>();

            return items;
        }

        public ICollection<SubGrupo> GetByGroup(Grupo value)
        {
            if (value == null)
                return null;

            var items = _session
                .CreateCriteria(typeof(SubGrupo))
                .Add(Restrictions.Eq("Grupo", value))
                .List<SubGrupo>();

            return items;
        }

        #endregion
    }
}
