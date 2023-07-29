using DBH.Core.Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Core.MyResolver
{
    public class ColumnNameResolver : IColumnNameResolver
    {
        public virtual string ResolveColumnName(PropertyInfo propertyInfo)
        {
            var columnName = DapperHelper.Encapsulate(propertyInfo.Name);

            var columnattr = propertyInfo.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(ColumnAttribute).Name) as dynamic;
            if (columnattr != null)
            {
                columnName = DapperHelper.Encapsulate(columnattr.Name);
                if (Debugger.IsAttached)
                    Trace.WriteLine($"Column name for type overridden from {propertyInfo.Name} to {columnName}");
            }
            return columnName;
        }
    }
}
