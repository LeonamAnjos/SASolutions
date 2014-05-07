using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface IUserRepository
    {
        #region CRUD
        void Add(Usuario item);
        void Update(Usuario item);
        void Remove(Usuario item);
        #endregion

        Usuario GetById(int id);
        Usuario GetByLogin(string value);
        Usuario GetByName(string name);
        ICollection<Usuario> GetAll();
    }
}
