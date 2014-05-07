using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface IStateRepository
    {
        #region CRUD
        void Add(Estado item);
        void Update(Estado item);
        void Remove(Estado item);
        #endregion

        Estado GetById(int id);
        Estado GetByName(string name);
        ICollection<Estado> GetAll();
    }
}
