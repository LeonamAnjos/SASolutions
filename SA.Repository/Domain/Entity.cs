using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using NHibernate;
using System.ComponentModel.DataAnnotations;
using NHibernate.Classic;

namespace SA.Repository.Domain
{
    public abstract class Entity : IValidatable
    {
        public virtual int Id { get; set; }

        public virtual void Validate()
        {
            var message = ValidationMessage(this.Validar());

            if (!string.IsNullOrEmpty(message))
                throw new ValidationFailure(message, new ValidationException(message, null, this));            
        }

        public abstract ValidationResults Validar();

        #region CRUD
        public virtual void AddBySession(ISession session)
        {
            //var message = ValidationMessage(this.Validar());

            //if (!string.IsNullOrEmpty(message))
            //    throw new ValidationException(message, null, this);
            
            session.Save(this);
        }

        public virtual void UpdateBySession(ISession session)
        {
            //var message = ValidationMessage(this.Validar());

            //if (!string.IsNullOrEmpty(message))
            //    throw new ValidationException(message, null, this);

            session.Update(this);
        }

        public virtual void RemoveBySession(ISession session)
        {
            session.Delete(this);
        }
        #endregion

        #region Private Methods
        private string ValidationMessage(ValidationResults results)
        {
            string message = string.Empty;

            if (!results.IsValid)
                foreach (var result in results)
                {
                    message += result.Message + "; ";
                }
            return message;
        }
        #endregion
    }
}
