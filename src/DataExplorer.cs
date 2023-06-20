/**
MIT License

Copyright (c) 2023 Jamie Futch
https://Github.com/jamiefutch

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Net.Http.Headers;

namespace DataSetExplorer
{
    /// <summary>
    /// Helper methods for displaying ado.net datasets
    /// </summary>
    static class DataExplorer
    {
        #region Tabular // tools to display dataset in tabular form

        /// <summary>
        /// Prints dataset in tabular form
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string PrintDatasetAsTSV(this DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            foreach(DataTable dt in ds.Tables)
            {
                sb.Append("====================================\r\n");
                sb.Append($"{dt.TableName}\r\n");
                sb.Append("====================================\r\n");
                sb.Append($"{dt.PrintFieldNames()}\r\n");                
                StringBuilder sb2 = new StringBuilder();
                foreach(DataRow dr in dt.Rows)
                {
                    sb2.Append(dr.PrintFieldValues());
                }
                sb.Append($"{sb2.ToString().Trim()}\r\n");
                sb.Append("====================================\r\n");
            }
            return sb.ToString();
        }       

        static string PrintFieldNames(this DataTable dt, string delimiter = "\t") 
        {
            StringBuilder sb = new StringBuilder();
            foreach(DataColumn column in dt.Columns)
            {
                sb.Append(column.ColumnName);
                sb.Append(delimiter);
            }
            return sb.ToString().Trim();
        }

        static string PrintFieldValues(this DataRow dr, string delimiter = "\t") 
        {
            StringBuilder sb = new StringBuilder();
            foreach(var item in  dr.ItemArray)
            {
                sb.Append(item.ToString());
                sb.Append(delimiter);
            }
            return sb.ToString().Trim();
        }
        #endregion

        #region Rows    // tools to display dataset as rows
        public static string PrintDatasetAsRows(this DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataTable dt in ds.Tables)
            {
                sb.Append("====================================\r\n");
                sb.Append($"{dt.TableName}\r\n");
                sb.Append("====================================\r\n");                
                sb.Append(dt.PrintDataTableAsRows());
                sb.Append("====================================\r\n");
            }
            return sb.ToString();
        }

        public static string PrintDataTableAsRows(this DataTable dt) 
        { 
            StringBuilder sb = new StringBuilder();
            sb.Append("----------------------------------------\r\n");
            var cols = dt.GetColumnNames();
            foreach(DataRow dr in  dt.Rows)
            {   
                int i = 0;
                foreach(var item in dr.ItemArray)
                {
                    sb.Append($"{cols[i]}:\t{dr.ItemArray[i].ToString().Trim()}\r\n");
                    i++;
                }                
            }
            sb.Append("----------------------------------------\r\n");
            return sb.ToString().Trim();
        }

        public static string[] GetColumnNames(this DataTable dt) 
        {
            string[] cols = new string[dt.Columns.Count];
            int i = 0;
            foreach(DataColumn dc in dt.Columns) 
            {
                cols[i] = dc.ColumnName;
                i++;            
            }
            return cols;
        }
        #endregion
    }
}

