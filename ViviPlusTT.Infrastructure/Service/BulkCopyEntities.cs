using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.SqlClient;

namespace ViviPlusTT.Infrastructure.Service
{
    public class BulkCopyEntities
    {
        public static void BulkInsert<T>(DbContext context, IList<T> list)
        {
            var connection = context.Database.Connection.ConnectionString;

            using (var bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.BulkCopyTimeout = 300;
                bulkCopy.BatchSize = list.Count;
                bulkCopy.DestinationTableName = typeof(T).Name;

                var table = new DataTable();
                var props = TypeDescriptor.GetProperties(typeof(T))
                                            .Cast<PropertyDescriptor>()
                                            .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                                            .ToArray();

                foreach (var propertyInfo in props)
                {
                    bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                    table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                }

                var values = new object[props.Length];
                foreach (var item in list)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    table.Rows.Add(values);
                }
                bulkCopy.WriteToServer(table);
            }
        }

    }
}
