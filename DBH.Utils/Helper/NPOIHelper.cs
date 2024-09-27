using DBH.Models.Attributes;
using DBH.Models.EntityViews;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.Streaming;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Utils.Helper
{
    /// <summary>
    /// NPOI相关操作帮助方法，导出Excel
    /// create：2024年9月26日
    /// </summary>
    public static class NPOIHelper
    {
        /// <summary>
        /// 数据List转为EXCEL文件
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="listTableColumns">全部的列</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName"文件名称></param>
        /// <returns>返回文件路径</returns>
        public static string ExportToExcel<T>(List<T> listTableColumns,string filePath, string fileName = "")
        {
            if (listTableColumns == null)
                return string.Empty;
            IWorkbook wb = new SXSSFWorkbook();// WorkbookFactory.Create("导出文件");//创建文件
            ISheet sheet = wb.CreateSheet("Sheet");//创建sheet

            //设置一个正文的样式
            ICellStyle styleContent = wb.CreateCellStyle();
            styleContent.Alignment = HorizontalAlignment.Center;
            styleContent.VerticalAlignment = VerticalAlignment.Center;
            styleContent.BorderBottom = BorderStyle.Thin;
            styleContent.BorderLeft = BorderStyle.Thin;
            styleContent.BorderTop = BorderStyle.Thin;
            styleContent.BorderRight = BorderStyle.Thin;
            //设置一个字体加粗的样式，作为表头使用
            ICellStyle styleTitle = wb.CreateCellStyle();
            styleTitle.BorderBottom = BorderStyle.Thin;
            styleTitle.BorderLeft = BorderStyle.Thin;
            styleTitle.BorderTop = BorderStyle.Thin;
            styleTitle.BorderRight = BorderStyle.Thin;
            IFont font = wb.CreateFont();
            font.FontName = "宋体";
            font.IsBold = true;
            styleTitle.Alignment = HorizontalAlignment.Center;
            styleTitle.VerticalAlignment = VerticalAlignment.Center;
            styleTitle.SetFont(font);

            TypeInfo pType = typeof(T).GetTypeInfo();
            PropertyInfo[] pros = pType.GetProperties();
            Dictionary<int, string> dictPropNames = new Dictionary<int, string>();
            //按照导出的排序设置给属性排个序
            for (int p = 0; p < pros.Length; p++)
            {
                PropertyInfo pro = pType.GetProperty(pros[p].Name);
                ExportColumnNameAttribute exportColumnNameAttribute = (ExportColumnNameAttribute)pro.GetCustomAttribute(typeof(ExportColumnNameAttribute));
                int index = exportColumnNameAttribute.ColumnIndex;
            }

            //设置默认第一行“表头”
            IRow row_title = sheet.CreateRow(0);
            for (int p = 0; p < pros.Length; p++)
            {
                PropertyInfo pro = pType.GetProperty(pros[p].Name);
                ICell cell_title = row_title.CreateCell(p);
                cell_title.CellStyle = styleTitle;//设置“表头”的样式
                row_title.Height = 24 * 20;
                ExportColumnNameAttribute exportColumnNameAttribute = (ExportColumnNameAttribute)pro.GetCustomAttribute(typeof(ExportColumnNameAttribute));
                if (exportColumnNameAttribute != null)
                    cell_title.SetCellValue(exportColumnNameAttribute.ColumnName);
                else
                    cell_title.SetCellValue("Column" + p);
            }
            //设置数据行
            for (int i = 0; i < listTableColumns.Count; i++)
            {
                IRow row_value = sheet.CreateRow(i + 1);
                for (int p = 0; p < pros.Length; p++)
                {
                    PropertyInfo pro = pType.GetProperty(pros[p].Name);
                    ICell cell_value = row_value.CreateCell(p);
                    cell_value.CellStyle = styleContent;//设置“正文”样式
                    object p_value = pro.GetValue(listTableColumns[i]);
                    switch (pro.PropertyType.Name)
                    {
                        case "String":
                            cell_value.SetCellValue(p_value.ToString());
                            break;
                        case "Int32":
                            double val = 0;
                            if (double.TryParse(p_value.ToString(), out val))
                                cell_value.SetCellValue(val);
                            else
                                cell_value.SetCellValue(0);
                            break;
                        default:
                            cell_value.SetCellValue(p_value.ToString());
                            break;
                    }
                }
            }

            #region 方式其一：写入流，返回前端文件格式
            /*
            MemoryStream stream = new MemoryStream();
            wb.Write(stream,true);
            wb.Close();
            //return "";
            */
            #endregion

            #region 方式其一：写入本地文件用于下载

            string savePath = filePath;//读取当前程序所在路径：Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "export");
            //检查文件路径是否存在，不存在则创建
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            //完整的文件路径，带文件名+扩展
            string fullFilePath = Path.Combine(savePath, fileName);
            using (FileStream file = new FileStream(fullFilePath, FileMode.Create))
            {
                wb.Write(file);//保存文件
            }

            //删除7天前的全部数据
            FileInfo[] files = new DirectoryInfo(savePath).GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.CreationTime < DateTime.Now.AddDays(-7))
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception)
                    {
                        //尝试删除（可能被占用无法删除）
                    }
                }
            }
            wb.Close();
            return fullFilePath;
            #endregion
        }
    }
}
