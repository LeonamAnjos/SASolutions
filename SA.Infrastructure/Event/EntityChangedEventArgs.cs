using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using SA.Repository.Domain;

namespace SA.Infrastructure.Event
{
    public class EntityChangedEventArgs : IEntityChangedEventArgs
    {
        #region Properties
        private readonly object _entity;
        private readonly CrudType _crudType;
        #endregion

        #region Constructors
        public EntityChangedEventArgs(CrudType crudType, EntityObject entity)
        {
            this._crudType = crudType;
            this._entity = entity;
        }

        public EntityChangedEventArgs(CrudType crudType, Entity entity)
        {
            this._crudType = crudType;
            this._entity = entity;
        }

        #endregion

        #region IEntityChangedEventArtgs
        public CrudType CrudType
        {
            get { return _crudType; }
        }
        public object Entity
        {
            get { return _entity; }
        }
        #endregion
    }
}
