using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface ICountryRepository
    {
        #region CRUD
        void Add(Pais item);
        void Update(Pais item);
        void Remove(Pais item);
        #endregion

        Pais GetById(int id);
        Pais GetByName(string name);
        ICollection<Pais> GetAll();
    }
}
