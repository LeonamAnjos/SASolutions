using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class UserRepository : IUserRepository
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
        public UserRepository()
        {
        }
        #endregion

        #region CRUD
        public void Add(Usuario item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(Usuario item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(Usuario item)
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

        public Usuario GetById(int id)
        {
            return _session.Get<Usuario>(id);
        }

        public Usuario GetByLogin(string value)
        {
            Usuario item = _session
                .CreateCriteria(typeof(Usuario))
                .Add(Restrictions.Eq("Login", value))
                .UniqueResult<Usuario>();
            return item;
        }

        public Usuario GetByName(string value)
        {
            Usuario item = _session
                .CreateCriteria(typeof(Usuario))
                .Add(Restrictions.Eq("Nome", value))
                .UniqueResult<Usuario>();
            return item;
        }

        public ICollection<Usuario> GetAll()
        {
            var items = _session.CreateQuery("from Usuario").List<Usuario>();
            return items;
        }
        #endregion


    }
}
