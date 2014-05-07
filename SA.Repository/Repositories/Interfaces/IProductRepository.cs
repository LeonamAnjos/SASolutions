using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface IProductRepository
    {
        #region CRUD
        void Add(Produto item);
        void Update(Produto item);
        void Remove(Produto item);
        #endregion

        Produto GetById(int id);
        Produto GetByName(string value);
        Produto GetByReference(string value);
        ICollection<Produto> GetAll();
        ICollection<Produto> GetByGroup(Grupo value);
        ICollection<Produto> GetBySubGroup(SubGrupo value);
        ICollection<Produto> GetByProducer(Fabricante value);
    }
}
