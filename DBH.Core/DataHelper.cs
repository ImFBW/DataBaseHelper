using DBH.Models.EntityViews;
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
        private static string _classNetVersion = "v4";
        /// <summary>
        /// 定义生成class类的Net版本
        /// </summary>
        public static string ClassNetVersion
        {
            get
            {
                return _classNetVersion;
            }
            set
            {
                _classNetVersion = value;
            }
        }

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


        /// <summary>
        /// 生成Class类（字符串）
        /// 仅支持生成C#版本
        /// </summary>
        /// <param name="listColumn">列的数据列表</param>
        /// <returns>一行行的字符串</returns>
        public static IList<string> CreateNetClass(List<ColumnTypeDescView> listColumn)
        {
            IList<string> listResult = new List<string>();
            if (ClassNetVersion == "v5")//对net5.0版本,将生成对应的类的格式
            {
                #region 生成属性
                foreach (var col in listColumn)
                {
                    string defaultValue = "";//属性的默认值
                    string propertyName = "";//属性的名称
                    string propertyType = "";//属性的类型
                    switch (col.ColumnDataType.ToLower())
                    {
                        case "int":
                        case "bigint":
                        case "tinyint":
                            defaultValue = "0";
                            break;
                        case "bit":
                            defaultValue = "false";
                            break;
                        case "binary":
                        case "image":
                        case "uniqueidentifier":
                        case "text":
                        case "ntext":
                        case "string":
                        case "char":
                        case "nchar":
                        case "xml":
                        case "varchar":
                        case "nvarchar":
                            defaultValue = "\"\"";
                            break;
                        case "datetime":
                        case "timestamp":
                            defaultValue = "null";
                            break;
                        case "float":
                            defaultValue = "0";
                            break;
                        case "real":
                            defaultValue = "0";
                            break;
                        case "money":
                        case "numeric":
                        case "decimal":
                            defaultValue = "0";
                            break;
                        default:
                            defaultValue = "\"\""; break;
                    }

                    //把字段第一个字母变为大写,作为属性名
                    propertyName = col.ColumnName.Substring(0, 1).ToUpper() + col.ColumnName.Substring(1);
                    switch (col.ColumnDataType.ToLower())
                    {
                        case "int":
                            propertyType = "int";
                            break;
                        case "bigint":
                            propertyType = "long";
                            break;
                        case "binary":
                        case "image":
                            propertyType = "byte[]";
                            break;
                        case "bit":
                            propertyType = "bool";
                            break;
                        case "tinyint":
                            propertyType = "byte";
                            break;
                        case "uniqueidentifier":
                            propertyType = "Guid";
                            break;
                        case "text":
                        case "ntext":
                        case "string":
                        case "char":
                        case "nchar":
                        case "xml":
                        case "varchar":
                        case "nvarchar":
                            propertyType = "string";
                            break;
                        case "datetime":
                        case "timestamp":
                            propertyType = "DateTime?";
                            break;
                        case "float":
                            propertyType = "double";
                            break;
                        case "real":
                            propertyType = "float";
                            break;
                        case "money":
                        case "numeric":
                        case "decimal":
                            propertyType = "decimal";
                            break;
                        default: propertyType = "string"; break;
                    }
                    string columnDesc = col.ColumnDesc.Replace("\r\n", "\r\n/// ").Replace("\r", "\r\n/// ").Replace("\n", "\r\n/// ");
                    listResult.Add(string.Format("\r\n/// <summary>\r\n/// {0}\r\n/// </summary>\r\n", columnDesc));
                    listResult.Add("public " + propertyType + " " + propertyName + " { get; set; } = " + defaultValue + ";");
                    listResult.Add(string.Format("\r\n"));
                }
                #endregion
            }
            else//其他版本的类格式
            {
                #region 生成字段
                foreach (var col in listColumn)
                {
                    //把字段第一个字母变为小写
                    string clum = col.ColumnName.Substring(0, 1).ToLower() + col.ColumnName.Substring(1);
                    string formatStr = "private {0} _{1}={2};\r\n";
                    //数据库类型转换为C#数据类型
                    switch (col.ColumnDataType.ToLower())
                    {
                        case "int":
                            listResult.Add(string.Format(formatStr, "int", clum, "0"));
                            break;
                        case "bigint":
                            listResult.Add(string.Format(formatStr, "long", clum, "0"));
                            break;
                        case "binary":
                        case "image":
                            listResult.Add(string.Format(formatStr, "byte[]", clum, "\"\""));
                            break;
                        case "bit":
                            listResult.Add(string.Format(formatStr, "bool", clum, "false"));
                            break;
                        case "tinyint":
                            listResult.Add(string.Format(formatStr, "byte", clum, "0"));
                            break;
                        case "uniqueidentifier":
                            listResult.Add(string.Format(formatStr, "Guid", clum, "\"\""));
                            break;
                        case "text":
                        case "ntext":
                        case "string":
                        case "char":
                        case "nchar":
                        case "xml":
                        case "varchar":
                        case "nvarchar":
                            listResult.Add(string.Format(formatStr, "string", clum, "\"\""));
                            break;
                        case "datetime":
                        case "timestamp":
                            listResult.Add(string.Format(formatStr, "DateTime?", clum, "null"));
                            break;
                        case "float":
                            listResult.Add(string.Format(formatStr, "double", clum, "0"));
                            break;
                        case "real":
                            listResult.Add(string.Format(formatStr, "float", clum, "0"));
                            break;
                        case "money":
                        case "numeric":
                        case "decimal":
                            listResult.Add(string.Format(formatStr, "decimal", clum, "0"));
                            break;
                        default: listResult.Add(string.Format(formatStr, "string", clum, "\"\"")); break;
                    }
                }
                listResult.Add("\r\n");

                #endregion

                #region 生成属性
                foreach (var col in listColumn)
                {
                    //把字段第一个字母变为小写,作为字段名
                    string clum1 = col.ColumnName.Substring(0, 1).ToLower() + col.ColumnName.Substring(1);
                    //把字段第一个字母变为大写,作为属性名
                    string clum2 = col.ColumnName.Substring(0, 1).ToUpper() + col.ColumnName.Substring(1);

                    string type = "";
                    switch (col.ColumnDataType.ToLower())
                    {
                        case "int":
                            type = "int";
                            break;
                        case "bigint":
                            type = "long";
                            break;
                        case "binary":
                        case "image":
                            type = "byte[]";
                            break;
                        case "bit":
                            type = "bool";
                            break;
                        case "tinyint":
                            type = "byte";
                            break;
                        case "uniqueidentifier":
                            type = "Guid";
                            break;
                        case "text":
                        case "ntext":
                        case "string":
                        case "char":
                        case "nchar":
                        case "xml":
                        case "varchar":
                        case "nvarchar":
                            type = "string";
                            break;
                        case "datetime":
                        case "timestamp":
                            type = "DateTime?";
                            break;
                        case "float":
                            type = "double";
                            break;
                        case "real":
                            type = "float";
                            break;
                        case "money":
                        case "numeric":
                        case "decimal":
                            type = "decimal";
                            break;
                        default: type = "string"; break;
                    }
                    string columnDesc = col.ColumnDesc.Replace("\r\n", "\r\n/// ").Replace("\r", "\r\n/// ").Replace("\n", "\r\n/// ");
                    listResult.Add(string.Format("\r\n/// <summary>\r\n/// {0}\r\n/// </summary>\r\n", columnDesc));
                    listResult.Add("public " + type + " " + clum2 + "\r\n{\r\n");
                    listResult.Add("    get " + "{" + "return _" + clum1 + ";" + "}\r\n");
                    listResult.Add("    set " + "{" + " _" + clum1 + "= value;" + "}\r\n");
                    listResult.Add("}");
                    listResult.Add("\r\n");
                }
                #endregion
            }
            return listResult;
        }
    }
}
