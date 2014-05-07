using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface IProducerRepository
    {
        #region CRUD
        void Add(Fabricante item);
        void Update(Fabricante item);
        void Remove(Fabricante item);
        #endregion

        Fabricante GetById(int id);
        Fabricante GetByName(string name);
        ICollection<Fabricante> GetAll();
    }
}
