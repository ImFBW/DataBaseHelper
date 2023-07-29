using DBH.Core.Dapper;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Core.MyResolver
{
    public class TableNameResolver : ITableNameResolver
    {
        public virtual string ResolveTableName(Type type)
        {
            var tableName = DapperHelper.Encapsulate(type.Name);

            var tableattr = type.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
            if (tableattr != null)
            {
                tableName = DapperHelper.Encapsulate(tableattr.Name);
                try
                {
                    if (!string.IsNullOrEmpty(tableattr.Schema))
                    {
                        string schemaName = DapperHelper.Encapsulate(tableattr.Schema);
                        tableName = $"{schemaName}.{tableName}";
                    }
                }
                catch (RuntimeBinderException)
                {
                    //Schema doesn't exist on this attribute.
                }
            }

            return tableName;
        }
    }
}
