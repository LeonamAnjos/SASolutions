using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using SA.Repository.Enums;

namespace SA.Repository.Repositories
{
    public interface IFinancialAccountRepository
    {
        #region CRUD
        void Add(ContaFinanceira item);
        void Update(ContaFinanceira item);
        void Remove(ContaFinanceira item);
        #endregion

        ContaFinanceira GetById(int id);
        ContaFinanceira GetByRegister(Cadastro value, FinancialAccountType type);
        ICollection<ContaFinanceira> GetByRegister(Cadastro value);
        ICollection<ContaFinanceira> GetByType(FinancialAccountType type);
        ICollection<ContaFinanceira> GetAll();
    }
}
