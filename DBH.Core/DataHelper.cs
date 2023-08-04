using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Core
{
    /// <summary>
    /// 有关数据操作的一些帮助方法
    /// </summary>
    public static class DataHelper
    {
        /// <summary>
        /// DataTable To List
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="dataTable">数据源</param>
        /// <param name="tableIndex">需要转换表的索引</param>
        /// <returns></returns>
        public static IList<T> DataTableToList<T>(DataTable dataTable)
        {
            //确认参数有效
            if (dataTable == null || dataTable.Rows.Count <= 0)
                return null;
            IList<T> list = new List<T>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                //创建泛型对象
                T _t = Activator.CreateInstance<T>();
                //获取对象所有属性
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dataTable.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dataTable.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_t, dataTable.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list;
        }
    }
}
