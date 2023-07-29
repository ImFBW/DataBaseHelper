using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Core.MyResolver
{
    public interface IColumnNameResolver
    {
        string ResolveColumnName(PropertyInfo propertyInfo);
    }
}
