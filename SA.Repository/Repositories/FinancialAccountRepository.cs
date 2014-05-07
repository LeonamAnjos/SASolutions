using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using NHibernate;
using NHibernate.Criterion;
using SA.Repository.Enums;

namespace SA.Repository.Repositories
{
    public class FinancialAccountRepository : IFinancialAccountRepository
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
        public FinancialAccountRepository()
        {

        }
        #endregion

        #region CRUD
        public void Add(ContaFinanceira item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(ContaFinanceira item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                transaction.Commit();
            }
        }

        public void Remove(ContaFinanceira item)
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

        public ContaFinanceira GetById(int id)
        {
            return _session.Get<ContaFinanceira>(id);
        }

        public ContaFinanceira GetByRegister(Cadastro value, FinancialAccountType type)
        {
            ContaFinanceira item = _session
                .CreateCriteria(typeof(ContaFinanceira))
                .Add(Restrictions.Eq("Cadastro", value))
                .Add(Restrictions.Eq("Tipo", type))
                .UniqueResult<ContaFinanceira>();
            return item;
        }

        public ICollection<ContaFinanceira> GetByRegister(Cadastro value)
        {
            var item = _session
                .CreateCriteria(typeof(ContaFinanceira))
                .Add(Restrictions.Eq("Cadastro", value))
                .List<ContaFinanceira>();
            return item;
        }

        public ICollection<ContaFinanceira> GetByType(FinancialAccountType type)
        {
            var item = _session
                .CreateCriteria(typeof(ContaFinanceira))
                .Add(Restrictions.Eq("Tipo", type))
                .List<ContaFinanceira>();
            return item;
        }

        public ICollection<ContaFinanceira> GetAll()
        {
            var items = _session.CreateQuery("from ContaFinanceira").List<ContaFinanceira>();
            return items;
        }
        #endregion


    }
}
