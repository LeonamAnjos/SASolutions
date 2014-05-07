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
    public class RegisterRepository : IRegisterRepository
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
        public RegisterRepository()
        {

        }
        #endregion

        #region CRUD
        public void Add(Cadastro item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.AddBySession(session);
                //foreach(var c in item.ContasFinanceiras)
                //    c.AddBySession(session);
                transaction.Commit();
            }
        }

        public void Update(Cadastro item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                item.UpdateBySession(session);
                //foreach (var c in item.ContasFinanceiras)
                //{
                //    if (c.Id > 0)
                //        c.UpdateBySession(session);
                //    else
                //        c.AddBySession(session);
                //}
                transaction.Commit();
            }
        }

        public void Remove(Cadastro item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                //foreach(var c in item.ContasFinanceiras) 
                //    c.RemoveBySession(session);
                
                item.RemoveBySession(session);
                transaction.Commit();
            }
        }
        #endregion

        #region Gets

        public Cadastro GetById(int id)
        {
            return _session.Get<Cadastro>(id);
        }

        public ICollection<Cadastro> GetByName(string name)
        {
            return _session
                .CreateCriteria(typeof(Cadastro))
                .Add(Restrictions.Eq("Nome", name))
                .List<Cadastro>();
        }

        public Cadastro GetByCPF(string value)
        {
            Cadastro item = _session
                .CreateCriteria(typeof(Cadastro))
                .Add(Restrictions.Eq("CPF", value))
                .UniqueResult<Cadastro>();
            return item;
        }

        public ICollection<Cadastro> GetAll()
        {
            var items = _session.CreateQuery("from Cadastro").List<Cadastro>();
            return items;
        }

        public ICollection<Cadastro> GetAllClients()
        {
            IQuery q = _session.CreateQuery("select cad from Cadastro cad, ContaFinanceira cf where cf.Cadastro = cad and cf.Tipo = :tipo and cf.Situacao = :situacao and cad.Situacao = :situacao");
            q.SetString("situacao", ((char)ActiveInactiveType.Active).ToString());
            q.SetString("tipo", ((char)FinancialAccountType.Costumer).ToString());
            //q.SetMaxResults(1);
            return q.List<Cadastro>();
        }

        public ICollection<Cadastro> GetAllSuppliers()
        {
            IQuery q = _session.CreateQuery("select cad from Cadastro cad, ContaFinanceira cf where cf.Cadastro = cad and cf.Tipo = :tipo and cf.Situacao = :situacao and cad.Situacao = :situacao");
            q.SetString("situacao", ((char)ActiveInactiveType.Active).ToString());
            q.SetString("tipo", ((char)FinancialAccountType.Supplier).ToString());
            //q.SetMaxResults(1);
            return q.List<Cadastro>();
        }


        #endregion


    }
}
