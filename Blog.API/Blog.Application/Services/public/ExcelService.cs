using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    public class ExcelService : ApplicationService,IExcelService
    {
        #region init

        private readonly HttpClient _Client;
        /// <summary>
        /// DictionaryService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        public ExcelService(
            IHttpClientFactory clientFactory
           , IConfiguration Configuration) : base(clientFactory, Configuration)
        {
            _Client = clientFactory.CreateClient();
        }
        #endregion
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columns">Excel和T属性映射关系{propName,columnName}</param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public byte[] Export<T>(List<T> data, Dictionary<string, string> columns, string sheetName = null)
        {
            columns = columns ?? new Dictionary<string, string>();
            if (data != null && columns.Count > 0)
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet(sheetName ?? "Sheet1");
                ExportToSheet<T>(sheet, data, columns);
                var ms = new MemoryStream();
                workbook.Write(ms,true);
                return ms.ToArray();
            }
            else
            {
                return null;
            }
        }

        public byte[] Export<T, T1, T2>(List<T> data, Dictionary<string, string> columns, List<T1> data1, Dictionary<string, string> columns1, List<T2> data2, Dictionary<string, string> columns2, string sheetName2 = null, string sheetName1 = null, string sheetName = null)
        {
            columns = columns ?? new Dictionary<string, string>();
            if (data != null && columns.Count > 0)
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet(sheetName ?? "Sheet1");
                ExportToSheet<T>(sheet, data, columns);

                ISheet sheet1 = workbook.CreateSheet(sheetName1 ?? "Sheet2");
                ExportToSheet<T1>(sheet1, data1, columns1);

                ISheet sheet2 = workbook.CreateSheet(sheetName2 ?? "Sheet3");
                ExportToSheet<T2>(sheet2, data2, columns2);

                var ms = new MemoryStream();
                workbook.Write(ms, true);
                return ms.ToArray();
            }
            else
            {
                return null;
            }
        }

        private void ExportToSheet<T>(ISheet sheet, List<T> data, Dictionary<string, string> columns)
        {
            IRow headerRow = sheet.CreateRow(0);
            IDataFormat dataFormatCustom = sheet.Workbook.CreateDataFormat();
            ICellStyle dateStyle = sheet.Workbook.CreateCellStyle();
            dateStyle.DataFormat = dataFormatCustom.GetFormat("yyyy-MM-dd HH:mm:ss");

            var dicIndexer = new Dictionary<int, PropertyInfo>();
            var props = typeof(T).GetProperties();

            //设置标题行
            int i = 0;
            foreach (var item in columns)
            {
                headerRow.CreateCell(i).SetCellValue(item.Value);

                //匹配属性
                var prop = props.FirstOrDefault(p => p.Name.ToLower() == item.Key.ToLower());
                if (prop != null)
                {
                    dicIndexer.Add(i, prop);
                    i++;
                }
            }

            //填充数据
            for (i = 0; i < data.Count; i++)
            {
                var row = sheet.CreateRow(i + 1);
                foreach (var item in dicIndexer)
                {
                    var value = item.Value.GetValue(data[i]);
                    if (value != null)
                    {
                        if (value is DateTime)
                        {
                            var cell = row.CreateCell(item.Key);
                            cell.SetCellValue((DateTime)value);
                            cell.CellStyle = dateStyle;
                        }
                        else
                        {
                            row.CreateCell(item.Key).SetCellValue(value.ToString());
                        }
                    }
                }
            }
        }

        private void ExportToEmptySheet(ISheet sheet, Dictionary<string, string> columns)
        {
            IRow headerRow = sheet.CreateRow(0);
            //设置标题行
            int i = 0;
            foreach (var item in columns)
            {
                headerRow.CreateCell(i).SetCellValue(item.Value);
                i++;
            }

        }

        private IWorkbook CreateWorkbook(Stream stream)
        {
            try
            {
                return new XSSFWorkbook(stream); //07
            }
            catch
            {
                return new HSSFWorkbook(stream); //03
            }

        }

        private object GetCellValue(ICell cell, Type valueType)
        {
            if (cell == null) return null;
            string value = cell.ToString();//cell.ToString()即可获取string Value
            if (cell.CellType == CellType.Numeric && valueType.AssemblyQualifiedName.Contains("System.DateTime"))
            {
                if (cell.DateCellValue.CompareTo(new DateTime(1900, 1, 1)) == 0) return null;
                return cell.DateCellValue;
            }
            if (!string.IsNullOrEmpty(value))
            {
                return Convert.ChangeType(value, valueType);
            }

            return value;
        }

        /// <summary>
        /// 属性和Excel Column索引关系
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="columns"></param>
        /// <param name="headerIndex"></param>
        /// <returns></returns>
        private Dictionary<int, PropertyInfo> GetPropertyInfoInderxer<T>(IRow row,
            Dictionary<string, string> columns)
        {
            var dicIndexer = new Dictionary<int, PropertyInfo>();

            if (row == null)
            {
                throw new Exception("header row is null");
            }

            var props = typeof(T).GetProperties();
            for (int i = 0; i < row.LastCellNum; i++)
            {
                var cellValue = row.Cells[i].StringCellValue;
                if (cellValue != null)
                {
                    foreach (var item in columns)
                    {
                        if (item.Value.ToLower() == cellValue.ToLower())
                        {
                            //匹配属性
                            var prop = props.FirstOrDefault(p => p.Name.ToLower() == item.Key.ToLower());
                            if (prop != null)
                            {
                                dicIndexer.Add(i, prop);
                            }
                            break;
                        }
                    }
                }
            }

            return dicIndexer;
        }

        /// <summary>
        /// 从Excel解析数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="excelStream"></param>
        /// <param name="columns">Excel和T属性映射关系{propName,columnName}</param>
        /// <param name="headerIndex"></param>
        /// <returns></returns>F
        public List<T> GetDataList<T>(Stream excelStream, Dictionary<string, string> columns, int headerIndex = 0)
            where T : new()
        {
            columns = columns ?? new Dictionary<string, string>();
            if (excelStream == null || columns.Count == 0)
            {
                return new List<T>();
            }

            var list = new List<T>();

            var workBook = CreateWorkbook(excelStream);
            ISheet sheet = workBook.GetSheetAt(0);

            var dicIndexer = GetPropertyInfoInderxer<T>(sheet.GetRow(headerIndex), columns);

            int lastRowNum = sheet.LastRowNum;

            for (int i = headerIndex + 1; i <= lastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                if (row != null)
                {
                    T obj = new T();
                    foreach (var item in dicIndexer)
                    {
                        try
                        {
                            object val = GetCellValue(row.GetCell(item.Key), item.Value.PropertyType);
                            if (val != null)
                            {
                                item.Value.SetValue(obj, val, null);//属性赋值
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                    }
                    list.Add(obj);
                }
            }

            return list;
        }

        /// <summary>
        /// 导出Excel(mutiple sheets）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columns">Excel和T属性映射关系{propName,columnName}</param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public byte[] ExportMutiple(List<DataTable> dtList, Dictionary<string, Dictionary<string, string>> sheetColumns)
        {
            IWorkbook workbook = new XSSFWorkbook();
            sheetColumns = sheetColumns ?? new Dictionary<string, Dictionary<string, string>>();
            if (dtList != null && sheetColumns.Count > 0)
            {
                if (sheetColumns.Count > 0)
                {
                    int i = 0;
                    foreach (var name in sheetColumns)
                    {
                        ISheet sheet = workbook.CreateSheet(name.Key ?? "Sheet1");
                        ExportToMultipleSheet(sheet, dtList[i], name.Value);
                        i++;
                    }
                }

                var ms = new MemoryStream();
                workbook.Write(ms,true);
                return ms.ToArray();
            }
            else
            {
                if (sheetColumns.Count > 0)
                {
                    int i = 0;
                    foreach (var name in sheetColumns)
                    {
                        ISheet sheet = workbook.CreateSheet(name.Key ?? "Sheet1");
                        ExportToEmptySheet(sheet, name.Value);
                        i++;
                    }
                }
                var ms = new MemoryStream();
                workbook.Write(ms, true);
                return ms.ToArray();
            }
        }

        private void ExportToMultipleSheet(ISheet sheet, DataTable data, Dictionary<string, string> columns)
        {


            //设置标题行
            int i = 0;
            int j = 0;
            int count = 0;
            IRow headerRow = sheet.CreateRow(0);
            foreach (var item in columns)
            {
                headerRow.CreateCell(i).SetCellValue(item.Value);
                i++;
            }
            count = 1;

            //填充数据
            for (i = 0; i < data.Rows.Count; i++)
            {
                var row = sheet.CreateRow(count);
                for (j = 0; j < data.Columns.Count; ++j)
                {
                    row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                }
                ++count;
            }
        }

        public DataTable ToDataTable<T>(List<T> data)
        {
            DataTable table = new DataTable();
            if (data != null && data.Count > 0)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
                object[] values = new object[props.Count];
                foreach (T item in data)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    table.Rows.Add(values);
                }
            }
            return table;
        }
    }
}
