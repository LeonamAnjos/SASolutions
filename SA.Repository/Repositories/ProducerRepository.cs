using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class ProducerRepository : IProducerRepository
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
        public ProducerRepository()
        {
        }
        #endregion

        #region CRUD
        public void Add(Fabricante item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(Fabricante item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(Fabricante item)
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

        public Fabricante GetById(int id)
        {
            return _session.Get<Fabricante>(id);
        }

        public Fabricante GetByName(string name)
        {
            Fabricante item = _session
                .CreateCriteria(typeof(Fabricante))
                .Add(Restrictions.Eq("Nome", name))
                .UniqueResult<Fabricante>();
            return item;
        }

        public ICollection<Fabricante> GetAll()
        {
            var items = _session.CreateQuery("from Fabricante").List<Fabricante>();
            return items;
        }
        #endregion

    }
}
