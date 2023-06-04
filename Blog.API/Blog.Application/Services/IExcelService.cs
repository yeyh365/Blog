using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    public interface IExcelService
    {
        /// <summary>
        /// 导出Excel columns={propName,columnName}
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columns">Excel和T属性映射关系{propName,columnName}</param>
        /// <returns></returns>
        byte[] Export<T>(List<T> data, Dictionary<string, string> columns, string sheetName = null);

        /// <summary>
        /// 导出Excel columns={propName,columnName}
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columns">Excel和T属性映射关系{propName,columnName}</param>
        /// <param name="sheetName"></param>
        /// <param name="data1"></param>
        /// <param name="columns1"></param>
        /// <param name="sheetName1"></param>
        /// <returns></returns>
        byte[] Export<T, T1, T2>(List<T> data, Dictionary<string, string> columns, List<T1> data1, Dictionary<string, string> columns1, List<T2> data2, Dictionary<string, string> columns2, string sheetName2 = null, string sheetName1 = null, string sheetName = null);

        /// <summary>
        /// 导出Excel(mutiple sheets） columns={propName,columnName}
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columns">Excel和T属性映射关系{propName,columnName}</param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        byte[] ExportMutiple(List<DataTable> dtList, Dictionary<string, Dictionary<string, string>> sheetColumns);

        DataTable ToDataTable<T>(List<T> data);

        /// <summary>
        /// 从Excel解析数据  columns={propName,columnName}
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="excelStream"></param>
        /// <param name="columns">Excel和T属性映射关系{propName,columnName}</param>
        /// <param name="headerIndex"></param>
        /// <returns></returns>
        List<T> GetDataList<T>(Stream excelStream, Dictionary<string, string> columns, int headerIndex = 0) where T : new();
    }
}
