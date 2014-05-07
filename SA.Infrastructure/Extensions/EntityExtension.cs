using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Objects.DataClasses;
using System.IO;

namespace SA.Infrastructure.Extensions
{
    public static class EntityExtension
    {
        public static EntityObject Clone(this EntityObject entity)
        {
            DataContractSerializer dcSer = new DataContractSerializer(entity.GetType());
            MemoryStream memoryStream = new MemoryStream();

            dcSer.WriteObject(memoryStream, entity);
            memoryStream.Position = 0;

            return (EntityObject)dcSer.ReadObject(memoryStream);
        }
    }
}
