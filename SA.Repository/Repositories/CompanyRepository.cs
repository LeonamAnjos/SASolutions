using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class CompanyRepository : ICompanyRepository
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
        public CompanyRepository()
        {

        }
        #endregion

        #region CRUD
        public void Add(Empresa item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(Empresa item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(Empresa item)
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
        public Empresa GetById(int id)
        {
            return _session.Get<Empresa>(id);
        }

        public Empresa GetByName(string name)
        {
            Empresa item = _session
                .CreateCriteria(typeof(Empresa))
                .Add(Restrictions.Eq("Nome", name))
                .UniqueResult<Empresa>();
            return item;
        }

        public ICollection<Empresa> GetAll()
        {
            var items = _session.CreateQuery("from Empresa").List<Empresa>();
            return items;
        }
        #endregion
    }
}
