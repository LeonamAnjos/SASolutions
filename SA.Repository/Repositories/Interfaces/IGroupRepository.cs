using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface IGroupRepository
    {
        #region CRUD
        void Add(Grupo item);
        void Update(Grupo item);
        void Remove(Grupo item);
        #endregion

        Grupo GetById(int id);
        Grupo GetByDescription(string value);
        ICollection<Grupo> GetAll();
    }
}
