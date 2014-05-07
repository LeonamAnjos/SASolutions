using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface IVendorRepository
    {
        #region CRUD
        void Add(Vendedor item);
        void Update(Vendedor item);
        void Remove(Vendedor item);
        #endregion

        Vendedor GetById(int id);
        Vendedor GetByName(string name);
        ICollection<Vendedor> GetAll();
    }
}
