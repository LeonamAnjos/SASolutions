using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface IZipCodeRepository
    {
        #region CRUD
        void Add(Cep item);
        void Update(Cep item);
        void Remove(Cep item);
        #endregion

        Cep GetById(int id);
        Cep GetByZipCode(string zipCode);
        ICollection<Cep> GetAll();
    }
}
