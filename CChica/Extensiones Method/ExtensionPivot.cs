﻿using CChica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;


namespace CChica.Extensiones_Method
{
    public static class ExtensionPivot
    {

        public static DataTable ToPivotTable<T, TColumn, TRow,TData>(this IEnumerable<T> source,
Func<T, TColumn> columnSelector,
Expression<Func<T, TRow>> rowSelector,


Func<IEnumerable<T>, TData> dataSelector)
        {
            DataTable table = new DataTable();
            var rowName = ((MemberExpression)rowSelector.Body).Member.Name;


            table.Columns.Add(new DataColumn(rowName));

          

            var columns = source.Select(columnSelector).Distinct();


            foreach (var column in columns)
                table.Columns.Add(new DataColumn(column.ToString()));


         

            var rows = source.GroupBy(rowSelector.Compile())
                             .Select(rowGroup => new
                             {
                                 Key = rowGroup.Key,
                             
                                 Values = columns.GroupJoin(
                                     rowGroup,
                                     c => c,
                                     r => columnSelector(r),
                                     (c, columnGroup) => dataSelector(columnGroup))
                             });

            foreach (var row in rows)
            {
                


                var dataRow = table.NewRow();
                var items = row.Values.Cast<object>().ToList();
                items.Insert(0, row.Key);
             
           
                dataRow.ItemArray = items.ToArray();
                table.Rows.Add(dataRow);
            }


            return table;
        }


    }
}
