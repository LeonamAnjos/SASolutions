using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface IUserGroupRepository
    {
        #region CRUD
        void Add(UsuarioGrupo item);
        void Update(UsuarioGrupo item);
        void Remove(UsuarioGrupo item);
        #endregion

        UsuarioGrupo GetById(int id);
        UsuarioGrupo GetByDescription(string value);
        ICollection<UsuarioGrupo> GetAll();
    }
}
