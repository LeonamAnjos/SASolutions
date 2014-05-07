using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface ICityRepository
    {
        #region CRUD
        void Add(Cidade item);
        void Update(Cidade item);
        void Remove(Cidade item);
        #endregion

        Cidade GetById(int id);
        Cidade GetByName(string name);
        ICollection<Cidade> GetAll();
    }
}
