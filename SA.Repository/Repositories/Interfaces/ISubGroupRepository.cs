using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;

namespace SA.Repository.Repositories
{
    public interface ISubGroupRepository
    {
        #region CRUD
        void Add(SubGrupo item);
        void Update(SubGrupo item);
        void Remove(SubGrupo item);
        #endregion

        SubGrupo GetById(int id);
        SubGrupo GetById(int id, Grupo group);
        SubGrupo GetByDescription(string value);
        ICollection<SubGrupo> GetAll();
        ICollection<SubGrupo> GetByGroup(Grupo value);

    }
}
