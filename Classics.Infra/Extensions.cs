using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;


namespace Classics.Infra
{
    public static class Extensions
    {

        public static DataTable ToDataTable<T>(this List<T> iEnumerable, string tableName, string[] primaryKeys)
        {
            var internalTable = new DataTable(tableName);
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !p.PropertyType.FullName.Contains("Classics.Data.Models") && !IgnorePropertyToDatatable(p)).ToArray();

            if (string.IsNullOrWhiteSpace(tableName))
                throw new Exception("O nome da Tabela não poder ser vazio, nulo ou espaços.");

            var primaryKeysDataColumn = new List<DataColumn>();

            foreach (var property in properties)
            {
                var dataColumn = new DataColumn(property.Name, property.GetPropretyBaseType());
                if (primaryKeys.Contains(property.Name))
                    primaryKeysDataColumn.Add(dataColumn);
                internalTable.Columns.Add(dataColumn);
            }

            internalTable.PrimaryKey = primaryKeysDataColumn.ToArray();

            foreach (var item in iEnumerable)
            {
                var valores = new object[properties.Length];

                for (var i = 0; i < properties.Length; i++)
                {
                    if (properties[i].PropertyType.BaseType.FullName == "System.Enum")
                        valores[i] = (int)properties[i].GetValue(item, null);
                    else
                        valores[i] = properties[i].GetValue(item, null);
                }
                internalTable.Rows.Add(valores);
            }

            return internalTable;
        }

        private static bool IgnorePropertyToDatatable(PropertyInfo property)
        {
            object[] attributes = property.GetCustomAttributes(typeof(IgnoreToDatatableAttribute), false);
            var ignoreToDatatableAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(IgnoreToDatatableAttribute));

            return ignoreToDatatableAttribute != null && (ignoreToDatatableAttribute as IgnoreToDatatableAttribute).IgnorePropertyToDatatable;
        }

        public static DataTable ToDataTable<T>(this List<T> iEnumerable, string[] primaryKeys)
        {
            var tableName = typeof(T).Name;
            return ToDataTable(iEnumerable, tableName, primaryKeys);
        }

        public static DataTable ToDataTable<T>(this List<T> iEnumerable)
        {
            return ToDataTable(iEnumerable, new string[] { });
        }

        public static List<T> ToList<T>(this DataTable dataTable)
        {
            var columnNameList = dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            var properties = typeof(T).GetProperties();
            return dataTable.AsEnumerable().Select(row =>
            {
                var targetObject = Activator.CreateInstance<T>();
                foreach (var propety in properties)
                {
                    if (columnNameList.Contains(propety.Name))
                    {
                        var propertyInfo = targetObject.GetType().GetProperty(propety.Name);
                        propety.SetValue(targetObject, row[propety.Name] == DBNull.Value ? null : DataBase.ChangeType(row[propety.Name], propertyInfo.PropertyType));
                    }
                }
                return targetObject;
            }).ToList();
        }

        public static Type GetPropretyBaseType(this PropertyInfo prop)
        {
            var propType = prop.PropertyType;
            if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propType = propType.GetGenericArguments()[0];
                return propType;
            }

            var compositePrimitives = new List<Type> { typeof(string), typeof(decimal), typeof(Guid), typeof(DateTime) };
            if (propType.IsPrimitive || compositePrimitives.Any(el => el == propType))
                return propType;


            return typeof(string);
        }
    }
}
