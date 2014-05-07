using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class CityRepository : ICityRepository
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
        public CityRepository()
        {

        }
        #endregion

        #region CRUD
        public void Add(Cidade item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(Cidade item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(Cidade item)
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

        public Cidade GetById(int id)
        {
            return _session.Get<Cidade>(id);
        }

        public Cidade GetByName(string name)
        {
            Cidade item = _session
                .CreateCriteria(typeof(Cidade))
                .Add(Restrictions.Eq("Nome", name))
                .UniqueResult<Cidade>();
            return item;
        }

        public ICollection<Cidade> GetAll()
        {
            var items = _session.CreateQuery("from Cidade").List<Cidade>();
            return items;
        }
        #endregion


    }
}
