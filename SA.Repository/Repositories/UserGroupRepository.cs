using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class UserGroupRepository : IUserGroupRepository
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
        public UserGroupRepository()
        {
        }
        #endregion

        #region CRUD
        public void Add(UsuarioGrupo item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(UsuarioGrupo item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(UsuarioGrupo item)
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
        public UsuarioGrupo GetById(int id)
        {
            return _session.Get<UsuarioGrupo>(id);
        }

        public UsuarioGrupo GetByDescription(string value)
        {
            UsuarioGrupo item = _session
                .CreateCriteria(typeof(UsuarioGrupo))
                .Add(Restrictions.Eq("Descricao", value))
                .UniqueResult<UsuarioGrupo>();
            return item;
        }

        public ICollection<UsuarioGrupo> GetAll()
        {
            var items = _session
                .CreateCriteria(typeof(UsuarioGrupo))
                .List<UsuarioGrupo>();

            return items;
        }
        #endregion
    }
}
