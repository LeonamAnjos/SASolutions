using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Globalization;

namespace SA.Infrastructure
{
    public abstract class ModelService
    {
        /// <summary>
        /// Checks if the supplied object is tracked in this data context
        /// </summary>
        /// <param name="obj">The object to check for</param>
        /// <returns>True if the object is tracked, false otherwise</returns>
        protected bool IsObjectTracked(object entity, ObjectContext context)
        {
            ObjectStateEntry ose;
            return context.ObjectStateManager.TryGetObjectStateEntry(entity, out ose);
        }


        /// <summary>
        /// Verifies that the specified entity is tracked in this ModelService
        /// </summary>
        /// <param name="entity">The object to search for</param>
        /// <exception cref="InvalidOperationException">Thrown if object is not tracked by this ModelService</exception>
        protected void CheckEntityBelongsToModelService(object entity, ObjectContext context)
        {
            if (!this.IsObjectTracked(entity, context))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "O {0} fornecido não faz parte deste contexto.", entity.GetType().Name));
            }
        }

        /// <summary>
        /// Verifies that the specified entity is not tracked in this Modelservice
        /// </summary>
        /// <param name="entity">The object to search for</param>
        /// <exception cref="InvalidOperationException">Thrown if object is tracked by this ModelService</exception>
        protected void CheckEntityDoesNotBelongToModelService(object entity, ObjectContext context)
        {
            if (this.IsObjectTracked(entity, context))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "O {0} fornecido já faz parte desse contexto.", entity.GetType().Name));
            }
        }
    }
}
