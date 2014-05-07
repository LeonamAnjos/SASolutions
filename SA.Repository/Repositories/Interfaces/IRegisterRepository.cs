using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface IRegisterRepository
    {
        #region CRUD
        void Add(Cadastro item);
        void Update(Cadastro item);
        void Remove(Cadastro item);
        #endregion

        Cadastro GetById(int id);
        Cadastro GetByCPF(string value);
        ICollection<Cadastro> GetByName(string name);
        ICollection<Cadastro> GetAll();
        ICollection<Cadastro> GetAllClients();
        ICollection<Cadastro> GetAllSuppliers();
    }
}
