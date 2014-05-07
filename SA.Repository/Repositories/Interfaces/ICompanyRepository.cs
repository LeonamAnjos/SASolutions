using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface ICompanyRepository
    {
        #region CRUD
        void Add(Empresa item);
        void Update(Empresa item);
        void Remove(Empresa item);
        #endregion

        Empresa GetById(int id);
        Empresa GetByName(string name);
        ICollection<Empresa> GetAll();
    }
}
