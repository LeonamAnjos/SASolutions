using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface IUnitRepository
    {
        #region CRUD
        void Add(Unidade item);
        void Update(Unidade item);
        void Remove(Unidade item);
        #endregion

        Unidade GetById(int id);
        Unidade GetBySymbol(string name);
        ICollection<Unidade> GetAll();
    }
}
