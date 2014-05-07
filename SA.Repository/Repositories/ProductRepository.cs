using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class ProductRepository : IProductRepository
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
        public ProductRepository()
        {
        }
        #endregion

        #region CRUD
        public void Add(Produto item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(Produto item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(Produto item)
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
        public Produto GetById(int id)
        {
            return _session.Get<Produto>(id);
        }

        public Produto GetByName(string name)
        {
            Produto item = _session
                .CreateCriteria(typeof(Produto))
                .Add(Restrictions.Eq("Nome", name))
                .UniqueResult<Produto>();
            return item;
        }

        public Produto GetByReference(string value)
        {
            Produto item = _session
                .CreateCriteria(typeof(Produto))
                .Add(Restrictions.Eq("Referencia", value))
                .UniqueResult<Produto>();
            return item;
        }

        public ICollection<Produto> GetAll()
        {
            var items = _session.CreateQuery("from Produto").List<Produto>();
            return items;
        }

        public ICollection<Produto> GetByGroup(Grupo value)
        {
            return _session
                .CreateCriteria(typeof(Produto))
                .Add(Restrictions.Eq("Grupo", value))
                .List<Produto>();
        }
        public ICollection<Produto> GetBySubGroup(SubGrupo value)
        {
            return _session
                .CreateCriteria(typeof(Produto))
                .Add(Restrictions.Eq("SubGrupo", value))
                .List<Produto>();
        }
        public ICollection<Produto> GetByProducer(Fabricante value)
        {
            return _session
                .CreateCriteria(typeof(Produto))
                .Add(Restrictions.Eq("Fabricante", value))
                .List<Produto>();
        }
        #endregion


    }
}
