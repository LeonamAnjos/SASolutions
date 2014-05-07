using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace SA.Repository.Repositories
{
    public class ZipCodeRepository : IZipCodeRepository
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
        public ZipCodeRepository()
        {
        }
        #endregion

        #region CRUD
        public void Add(Cep item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(Cep item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(Cep item)
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
        public Cep GetById(int id)
        {
            return _session.Get<Cep>(id);
        }

        public Cep GetByZipCode(string zipCode)
        {
            Cep item = _session
                .CreateCriteria(typeof(Cep))
                .Add(Restrictions.Eq("CEP", zipCode))
                .UniqueResult<Cep>();
            return item;
        }

        public ICollection<Cep> GetAll()
        {
            var items = _session.CreateQuery("From Cep").List<Cep>();
            return items;
        }
        #endregion
    }
}
