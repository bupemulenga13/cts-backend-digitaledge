using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace DigitalEdge.Utility
{
    public static class DataImporter
    {
        public static DataTable CSVDataImport(IFormFile file)
        {
            DataColumnCollection dataColumnsWithSpaces = null;
            var reader = new StreamReader(file.OpenReadStream());
            string line = reader.ReadLine();
            line += "," + "Fixed Value";
            line += "," + "Rowindex";
            string[] value = line.Split(',');
            DataTable dt = new DataTable();
            Dictionary<string, string> model = new Dictionary<string, string>();
            foreach (string dc in value)
            {
                dt.Columns.Add(new DataColumn(dc));
                model.Add(new DataColumn(dc).ToString(), "");
            }

            dataColumnsWithSpaces = getDataColumnsWithSpaces(value);
            foreach (DataColumn col in dt.Columns)
            {
                col.ColumnName = col.ColumnName.RemoveWhitespace();
            }
            DataRow row;
            int rowCount = 1;
            while (!reader.EndOfStream)
            {
                rowCount += 1;
                string rowdata = reader.ReadLine();
                rowdata += ",," + rowCount;
                value = rowdata.Split(',');
                if (value.Length == dt.Columns.Count)
                {
                    row = dt.NewRow();
                    row.ItemArray = value;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        public static DataColumnCollection getDataColumnsWithSpaces(string[] values)
        {
            DataTable dt = new DataTable();
            foreach (string dc in values)
            {
                dt.Columns.Add(new DataColumn(dc));
            }
            return dt.Columns;
        }
        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

    }
}
