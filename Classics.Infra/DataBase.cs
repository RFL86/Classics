using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Text;

namespace Classics.Infra
{
    public class DataBase : IDisposable
    {
        public string Server { get; private set; }
        public string Catalog { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public int Timeout { get; private set; }
        private string ConnectionString { get; set; }
        public SqlConnection Connection { get; set; }

        public bool disposed;

        public DataBase(string entityConnectionStringName, int timeout)
        {
            Server = "classics.database.windows.net";
            Catalog = "classics";
            Password = "devapp@22";
            Timeout = timeout;
            Username = "developer";
            UpdateConnectionParams();
        }

        public DataBase(string server, string catalog, string username, string password, int timeout)
        {
            Server = server;
            Catalog = catalog;
            Username = username;
            Password = password;
            Timeout = timeout;
            UpdateConnectionParams();
        }

        public void UpdateConnectionParams(string server, string catalog, string username, string password, int timeout)
        {
            Server = server;
            Catalog = catalog;
            Username = username;
            Password = password;
            Timeout = timeout;

            ValidateParameters();

            ConnectionString = $"Data Source={Server};Initial Catalog={Catalog};User={Username};Password={Password};Connection Timeout = {Timeout};";

            Connection = new SqlConnection(ConnectionString);
        }

        public void UpdateConnectionParams()
        {

            UpdateConnectionParams(Server, Catalog, Username, Password, Timeout);
        }

        private Dictionary<string, string> GetDbConfig(string connectionString)
        {
            var config = new Dictionary<string, string>();

            var defaultConnection = ConfigurationManager.ConnectionStrings[connectionString]?.ConnectionString;

            var connection = defaultConnection;

            if (connection == null)
                return null;

            var keyValue = connection.Split(';');

            foreach (var item in keyValue)
            {
                var keyValueSplit = item.Split('=');
                if (keyValueSplit.Length < 2)
                    continue;
                if (keyValueSplit[0] == "provider connection string" && keyValueSplit.Length > 2)
                    config.Add(keyValueSplit[1].Replace("\"", "").ToLower(), keyValueSplit[2]);
                else
                    config.Add(keyValueSplit[0].ToLower(), keyValueSplit[1]);
            }
            return config;
        }

        private void ValidateParameters()
        {
            if (string.IsNullOrWhiteSpace(Server))
                throw new Exception("Parâmetro Server não pode ser nulo ou vazio.");

            if (string.IsNullOrWhiteSpace(Catalog))
                throw new Exception("Parâmetro Catalog não pode ser nulo ou vazio.");

            if (string.IsNullOrWhiteSpace(Username))
                throw new Exception("Parâmetro Username não pode ser nulo ou vazio.");

            if (string.IsNullOrWhiteSpace(Password))
                throw new Exception("Parâmetro Password não pode ser nulo ou vazio.");

            if (Timeout < 30)
                throw new Exception("Parâmetro Timeout não pode ser menor que 30.");
        }

        public static string FindTypeName(string text)
        {
            if (text.IndexOf("Guid", 0, StringComparison.Ordinal) != -1)
                return "Guid";

            if (text.IndexOf("Int", 0, StringComparison.Ordinal) != -1)
                return "int";

            if (text.IndexOf("Decimal", 0, StringComparison.Ordinal) != -1)
                return "decimal";

            if (text.IndexOf("DateTime", 0, StringComparison.Ordinal) != -1)
                return "DateTime";

            return "String";
        }

        public static object ChangeType(object value, Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (value == null)
                    return null;

                type = Nullable.GetUnderlyingType(type);
            }

            return Convert.ChangeType(value, type);
        }

        public bool BulkInsert(DataTable internalTable)
        {
            var bulkSql = new SqlBulkCopy
                    (
                        Connection,
                        SqlBulkCopyOptions.TableLock |
                        SqlBulkCopyOptions.FireTriggers |
                        SqlBulkCopyOptions.UseInternalTransaction,
                        null
                    );
            try
            {
                bulkSql.DestinationTableName = internalTable.TableName;
                bulkSql.BulkCopyTimeout = Timeout;
                foreach (DataColumn coluna in internalTable.Columns)
                {
                    bulkSql.ColumnMappings.Add(coluna.ColumnName, coluna.ColumnName);
                }
                OpenConnection();

                bulkSql.WriteToServer(internalTable);
                CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool BulkInsertWithTransaction(DataTable internalTable, SqlTransaction transaction, bool? returnException = false)
        {
            var bulkSql = new SqlBulkCopy
                    (
                        Connection,
                        SqlBulkCopyOptions.TableLock |
                        SqlBulkCopyOptions.FireTriggers,
                        transaction
                    );
            try
            {
                bulkSql.DestinationTableName = internalTable.TableName;
                bulkSql.BulkCopyTimeout = Timeout;
                foreach (DataColumn coluna in internalTable.Columns)
                {
                    bulkSql.ColumnMappings.Add(coluna.ColumnName, coluna.ColumnName);
                }

                bulkSql.WriteToServer(internalTable);

                return true;
            }
            catch (Exception ex)
            {
                if (!returnException.GetValueOrDefault())
                    return false;

                throw ex;
            }
        }

        public bool BulkInsertWithoutClosingConnection(DataTable internalTable, bool? returnException = false)
        {
            var bulkSql = new SqlBulkCopy(Connection);
            try
            {
                bulkSql.DestinationTableName = internalTable.TableName;
                foreach (DataColumn coluna in internalTable.Columns)
                {
                    bulkSql.ColumnMappings.Add(coluna.ColumnName, coluna.ColumnName);
                }

                bulkSql.WriteToServer(internalTable);
                return true;
            }
            catch (Exception ex)
            {
                if (!returnException.GetValueOrDefault())
                    return false;

                throw ex;
            }
        }

        public bool BulkInsertWithoutClosingConnectionWithTransaction(DataTable internalTable, SqlTransaction transaction, bool? returnException = false)
        {
            var bulkSql = new SqlBulkCopy
                    (
                        Connection,
                        SqlBulkCopyOptions.TableLock |
                        SqlBulkCopyOptions.FireTriggers,
                        transaction
                    );
            try
            {
                bulkSql.DestinationTableName = internalTable.TableName;
                foreach (DataColumn coluna in internalTable.Columns)
                {        
                    bulkSql.ColumnMappings.Add(coluna.ColumnName, coluna.ColumnName);
                }

                bulkSql.WriteToServer(internalTable);
                return true;
            }
            catch (Exception ex)
            {
                if (!returnException.GetValueOrDefault())
                    return false;

                throw ex;
            }
        }

        public bool BulkInsertWithoutTransaction(DataTable internalTable)
        {
            var bulkSql = new SqlBulkCopy
                    (
                        Connection,
                        SqlBulkCopyOptions.TableLock |
                        SqlBulkCopyOptions.FireTriggers,
                        null
                    );
            try
            {
                bulkSql.DestinationTableName = internalTable.TableName;
                foreach (DataColumn coluna in internalTable.Columns)
                {
                    bulkSql.ColumnMappings.Add(coluna.ColumnName, coluna.ColumnName);
                }
                OpenConnection();
                bulkSql.WriteToServer(internalTable);
                CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable CreateInternalTableWithQueryWithoutOpeningOrClosingConnection(string query, string primaryKey, string tableName)
        {
            try
            {
                var command = new SqlCommand(query, Connection);
                var result = command.ExecuteReader();
                var internalTableReturn = new DataTable();

                var column = 0;
                var tableStructure = result.GetSchemaTable();
                if (tableStructure == null)
                    return null;
                foreach (DataRow r in tableStructure.Rows)
                {
                    if (!internalTableReturn.Columns.Contains(r["ColumnName"].ToString()))
                    {
                        var columnType = result.GetDataTypeName(column);
                        switch (columnType)
                        {
                            case "uniqueidentifier":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(Guid));
                                break;
                            case "int":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(int));
                                break;
                            case "date":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(DateTime));
                                break;
                            case "datetime":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(DateTime));
                                break;
                            case "decimal":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(decimal));
                                break;
                            case "varbinary":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(byte[]));
                                break;
                            case "bit":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(bool));
                                break;
                            default:
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(string));
                                break;
                        }
                        internalTableReturn.Columns[r["ColumnName"].ToString()].Unique = Convert.ToBoolean(r["IsUnique"]);
                        internalTableReturn.Columns[r["ColumnName"].ToString()].AllowDBNull = Convert.ToBoolean(r["AllowDBNull"]);
                        internalTableReturn.Columns[r["ColumnName"].ToString()].ReadOnly = Convert.ToBoolean(r["IsReadOnly"]);
                    }
                    column++;
                }
                while (result.Read())
                {
                    var newLine = internalTableReturn.NewRow();
                    for (var i = 0; i < internalTableReturn.Columns.Count; i++)
                    {
                        var columnType = internalTableReturn.Columns[i].DataType;
                        if (columnType.Name == "Byte[]")
                        {
                            if (!result.IsDBNull(i))
                            {
                                var file = result.GetSqlBytes(i).Buffer;
                                newLine[i] = file;
                            }
                        }
                        else
                        {
                            newLine[i] = result.GetValue(i);
                        }
                    }
                    internalTableReturn.Rows.Add(newLine);
                }

                if (!string.IsNullOrEmpty(primaryKey))
                    internalTableReturn.PrimaryKey = new[] { internalTableReturn.Columns[primaryKey] };

                if (!string.IsNullOrEmpty(tableName))
                    internalTableReturn.TableName = tableName;

                return internalTableReturn;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable CreateInternalTableWithQuery(string query, List<string> keys, string tableName)
        {
            try
            {
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                var result = command.ExecuteReader();
                var internalTableReturn = new DataTable();

                var coluna = 0;
                var estruturaDaTabela = result.GetSchemaTable();
                if (estruturaDaTabela == null)
                    return null;
                foreach (DataRow r in estruturaDaTabela.Rows)
                {
                    if (!internalTableReturn.Columns.Contains(r["ColumnName"].ToString()))
                    {
                        var tipoDaColuna = result.GetDataTypeName(coluna);
                        switch (tipoDaColuna)
                        {
                            case "uniqueidentifier":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(Guid));
                                break;
                            case "int":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(int));
                                break;
                            case "date":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(DateTime));
                                break;
                            case "datetime":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(DateTime));
                                break;
                            case "decimal":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(decimal));
                                break;
                            case "float":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(float));
                                break;
                            case "varbinary":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(byte[]));
                                break;
                            case "bit":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(bool));
                                break;
                            default:
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(string));
                                break;
                        }
                        internalTableReturn.Columns[r["ColumnName"].ToString()].Unique = Convert.ToBoolean(r["IsUnique"]);
                        internalTableReturn.Columns[r["ColumnName"].ToString()].AllowDBNull = Convert.ToBoolean(r["AllowDBNull"]);
                        internalTableReturn.Columns[r["ColumnName"].ToString()].ReadOnly = Convert.ToBoolean(r["IsReadOnly"]);
                    }
                    coluna++;
                }
                while (result.Read())
                {

                    var newLine = internalTableReturn.NewRow();
                    for (var i = 0; i < internalTableReturn.Columns.Count; i++)
                    {
                        var columnType = internalTableReturn.Columns[i].DataType;
                        if (columnType.Name == "Byte[]")
                        {
                            if (!result.IsDBNull(i))
                            {
                                var file = result.GetSqlBytes(i).Buffer;
                                newLine[i] = file;
                            }
                        }
                        else
                        {
                            newLine[i] = result.GetValue(i);
                        }
                    }
                    internalTableReturn.Rows.Add(newLine);
                }
                CloseConnection();

                if (keys != null && keys.Any())
                {
                    var keysDataColumns = new DataColumn[keys.Count()];
                    for (var i = 0; i < keys.Count(); i++)
                    {
                        keysDataColumns[i] = internalTableReturn.Columns[keys[i]];
                    }
                    internalTableReturn.PrimaryKey = keysDataColumns;
                }

                if (!string.IsNullOrEmpty(tableName))
                    internalTableReturn.TableName = tableName;

                return internalTableReturn;
            }
            catch (Exception)
            {
                CloseConnection();
                return null;
            }
        }

        public DataTable CreateInternalTableWithQuery(string query, List<string> keys, string tableName, Dictionary<string, object> parameters)
        {
            try
            {
                var command = new SqlCommand(query, Connection);
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                }
                Connection.Open();
                var result = command.ExecuteReader();
                var internalTableReturn = new DataTable();

                var coluna = 0;
                var estruturaDaTabela = result.GetSchemaTable();
                if (estruturaDaTabela == null)
                    return null;
                foreach (DataRow r in estruturaDaTabela.Rows)
                {
                    if (!internalTableReturn.Columns.Contains(r["ColumnName"].ToString()))
                    {
                        var tipoDaColuna = result.GetDataTypeName(coluna);
                        switch (tipoDaColuna)
                        {
                            case "uniqueidentifier":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(Guid));
                                break;
                            case "int":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(int));
                                break;
                            case "date":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(DateTime));
                                break;
                            case "datetime":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(DateTime));
                                break;
                            case "decimal":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(decimal));
                                break;
                            case "float":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(float));
                                break;
                            case "varbinary":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(byte[]));
                                break;
                            case "bit":
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(bool));
                                break;
                            default:
                                internalTableReturn.Columns.Add(r["ColumnName"].ToString(), typeof(string));
                                break;
                        }
                        internalTableReturn.Columns[r["ColumnName"].ToString()].Unique = Convert.ToBoolean(r["IsUnique"]);
                        internalTableReturn.Columns[r["ColumnName"].ToString()].AllowDBNull = Convert.ToBoolean(r["AllowDBNull"]);
                        internalTableReturn.Columns[r["ColumnName"].ToString()].ReadOnly = Convert.ToBoolean(r["IsReadOnly"]);
                    }
                    coluna++;
                }
                while (result.Read())
                {

                    var newLine = internalTableReturn.NewRow();
                    for (var i = 0; i < internalTableReturn.Columns.Count; i++)
                    {
                        var columnType = internalTableReturn.Columns[i].DataType;
                        if (columnType.Name == "Byte[]")
                        {
                            if (!result.IsDBNull(i))
                            {
                                var file = result.GetSqlBytes(i).Buffer;
                                newLine[i] = file;
                            }
                        }
                        else
                        {
                            newLine[i] = result.GetValue(i);
                        }
                    }
                    internalTableReturn.Rows.Add(newLine);
                }
                CloseConnection();
                if (keys != null && keys.Any())
                {
                    var keysDataColumns = new DataColumn[keys.Count()];
                    for (var i = 0; i < keys.Count(); i++)
                    {
                        keysDataColumns[i] = internalTableReturn.Columns[keys[i]];
                    }
                    internalTableReturn.PrimaryKey = keysDataColumns;
                }

                if (!string.IsNullOrEmpty(tableName))
                    internalTableReturn.TableName = tableName;

                return internalTableReturn;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ExecuteNonQuery(string queryText)
        {
            try
            {
                new SqlCommand(queryText, Connection).ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ExecuteNonQueryWithTransaction(string queryText, SqlTransaction tran)
        {
            try
            {
                new SqlCommand(queryText, Connection, tran).ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public SqlDataReader Query(string queryText)
        {
            Connection.Open();
            var result = new SqlCommand(queryText, Connection).ExecuteReader();
            return result.HasRows ? result : null;
        }
        public SqlDataReader Query(string queryText, Dictionary<string, object> parameters)
        {
            var command = new SqlCommand(queryText, Connection);
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
            }
            Connection.Open();
            var result = command.ExecuteReader();

            return result.HasRows ? result : null;
        }
        public void OpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
        }
        public void CloseConnection()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
        public bool HasReturnedRows(string queryText)
        {
            var command = new SqlCommand(queryText, Connection);
            Connection.Open();
            using (var result = command.ExecuteReader())
            {
                if (result.HasRows)
                {
                    CloseConnection();
                    return true;
                }
            }
            CloseConnection();
            return false;
        }
        public bool UpdateOrInsertUsingDataTable(DataTable internalTable)
        {
            try
            {
                OpenConnection();
                var temporaryTableName = string.Concat("temp_", Guid.NewGuid().ToString("N").ToUpper());
                var temporaryTableCreationQuery = CreateTemporaryTableForUpdateCreationQuery(internalTable, temporaryTableName);
                ExecuteNonQuery(temporaryTableCreationQuery);

                var originalTableName = internalTable.TableName;
                internalTable.TableName = temporaryTableName;
                var filledTemporaryTable = BulkInsertWithoutClosingConnection(internalTable);
                if (filledTemporaryTable)
                {
                    var queryForInsert = CreateQueryToInsertOnUpdate(internalTable, originalTableName);
                    var internalTableInsert = CreateInternalTableWithQueryWithoutOpeningOrClosingConnection(queryForInsert, null, internalTable.TableName);
                    internalTableInsert.TableName = originalTableName;
                    CloseConnection();
                    OpenConnection();
                    BulkInsertWithoutClosingConnection(internalTableInsert);
                    internalTable.TableName = originalTableName;
                    var queryForUpdate = CreateQueryToUpdate(internalTable, temporaryTableName);
                    ExecuteNonQuery(queryForUpdate);
                }

                ExecuteNonQuery(string.Concat("DROP TABLE ", temporaryTableName, " ;"));
                CloseConnection();

                return true;
            }
            catch (Exception ex)
            {
                CloseConnection();
                return false;
            }
        }
        public bool UpdateUsingDataTable(DataTable internalTable)
        {
            try
            {
                OpenConnection();

                var temporaryTableName = string.Concat("temp_", Guid.NewGuid().ToString("N").ToUpper());
                var temporaryTableCreationQuery = CreateTemporaryTableForUpdateCreationQuery(internalTable, temporaryTableName);
                ExecuteNonQuery(temporaryTableCreationQuery);

                var originalTableName = internalTable.TableName;
                internalTable.TableName = temporaryTableName;
                var filledTemporaryTable = BulkInsertWithoutClosingConnection(internalTable);
                if (filledTemporaryTable)
                {
                    internalTable.TableName = originalTableName;
                    var queryForUpdate = CreateQueryToUpdate(internalTable, temporaryTableName);
                    ExecuteNonQuery(queryForUpdate);
                }

                ExecuteNonQuery(string.Concat("DROP TABLE ", temporaryTableName, " ;"));
                CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                CloseConnection();
                return false;
            }
        }
        public bool UpdateUsingDataTableWithTransaction(DataTable internalTable, SqlTransaction transaction)
        {
            //OpenConnection();
            try
            {
                var temporaryTableName = string.Concat("temp_", Guid.NewGuid().ToString("N").ToUpper());
                var temporaryTableCreationQuery = CreateTemporaryTableForUpdateCreationQuery(internalTable, temporaryTableName);
                ExecuteNonQueryWithTransaction(temporaryTableCreationQuery, transaction);

                var originalTableName = internalTable.TableName;
                internalTable.TableName = temporaryTableName;
                var filledTemporaryTable = BulkInsertWithTransaction(internalTable, transaction);
                if (filledTemporaryTable)
                {
                    internalTable.TableName = originalTableName;
                    var queryForUpdate = CreateQueryToUpdate(internalTable, temporaryTableName);
                    ExecuteNonQueryWithTransaction(queryForUpdate, transaction);
                    ExecuteNonQueryWithTransaction(string.Concat("DROP TABLE ", temporaryTableName, " ;"), transaction);
                    return true;
                }

                return filledTemporaryTable;
            }
            catch (Exception ex)
            {
                return false;
            }
            //CloseConnection();
        }
        public bool DeleteByIdUsingDataTable(string field, string id, string tableName)
        {
            try
            {
                OpenConnection();
                var queryForDelete = CreateQueryToDelete(field, id, tableName);
                ExecuteNonQuery(queryForDelete);

                CloseConnection();

                return true;
            }
            catch (Exception ex)
            {
                CloseConnection();
                return false;
            }
        }
        public bool DeleteByIdUsingDataTableWithTransaction(string field, string id, string tableName, SqlTransaction transaction, bool openAndCloseConnection = true)
        {
            try
            {
                if (openAndCloseConnection)
                    OpenConnection();

                var queryForDelete = CreateQueryToDelete(field, id, tableName);
                ExecuteNonQueryWithTransaction(queryForDelete, transaction);

                if (openAndCloseConnection)
                    CloseConnection();

                return true;
            }
            catch (Exception ex)
            {
                if (openAndCloseConnection)
                    CloseConnection();
                return false;
            }
        }
        private string CreateQueryToDelete(string field, string id, string tableName)
        {
            var fromTo = new StringBuilder();

            fromTo.Append(String.Concat("DELETE FROM ", tableName, " WHERE ", field, " ='", id, "'"));

            return fromTo.ToString();
        }
        private string CreateQueryToInsertOnUpdate(DataTable internalTable, string originalTableName)
        {
            var tempTableName = internalTable.TableName;
            var onJoin = "";
            var whereClause = "";
            for (var i = 0; i < internalTable.PrimaryKey.Count(); i++)
            {
                var columnName = internalTable.PrimaryKey[i].ColumnName;
                onJoin += $" {tempTableName}.{columnName}={originalTableName}.{columnName}";
                whereClause += $"{originalTableName}.{columnName} IS NULL";
                if (i != internalTable.PrimaryKey.Count() - 1)
                {
                    onJoin += " AND ";
                    whereClause += " AND ";
                }
            }
            var query = $"SELECT {tempTableName}.* FROM {tempTableName} LEFT JOIN {originalTableName} ON {onJoin} WHERE {whereClause}";
            return query;
        }
        private string CreateQueryToUpdate(DataTable internalTable, string temporaryTableName)
        {
            var fromTo = $"UPDATE {internalTable.TableName } SET ";
            for (var coluna = 0; coluna < internalTable.Columns.Count; coluna++)
            {        
                if (coluna == 0)
                {
                    fromTo += string.Concat(" ", internalTable.TableName, ".", internalTable.Columns[coluna].ColumnName, " = ", temporaryTableName, ".", internalTable.Columns[coluna].ColumnName);
                    continue;
                }
                fromTo += string.Concat(",", internalTable.TableName, ".", internalTable.Columns[coluna].ColumnName, " = ", temporaryTableName, ".", internalTable.Columns[coluna].ColumnName);
            }
            fromTo += $" FROM {internalTable.TableName } INNER JOIN { temporaryTableName} ON ";
            for (var i = 0; i < internalTable.PrimaryKey.Count(); i++)
            {
                if (i != 0)
                    fromTo += " AND ";
                fromTo += string.Concat(temporaryTableName, ".", internalTable.PrimaryKey[i].ColumnName, " = ", internalTable.TableName, ".", internalTable.PrimaryKey[i].ColumnName);
            }
            return fromTo;
        }
        private string CreateTemporaryTableForUpdateCreationQuery(DataTable internalTable, string temporaryTableName)
        {
            var fromTo = $"CREATE TABLE {temporaryTableName} (";
            for (var column = 0; column < internalTable.Columns.Count; column++)
            {
                if (column != 0)
                    fromTo += ",";
                fromTo += string.Concat(" ", internalTable.Columns[column].ColumnName, " ", TypeDefine(internalTable.Columns[column].DataType, internalTable.PrimaryKey.Any(i => i.ColumnName == internalTable.Columns[column].ColumnName)));
            }
            fromTo += ") ";

            var clusterIndex = $"CREATE UNIQUE CLUSTERED INDEX {temporaryTableName} ON {temporaryTableName} (";

            for (var i = 0; i < internalTable.PrimaryKey.Count(); i++)
            {
                if (i < internalTable.PrimaryKey.Count() - 1)
                    clusterIndex += internalTable.PrimaryKey[i].ColumnName + ",";
                else
                    clusterIndex += internalTable.PrimaryKey[i].ColumnName + ");";
            }
            if (internalTable.PrimaryKey.Any())
                fromTo += clusterIndex;

            return fromTo;
        }
        private string TypeDefine(Type columnType, bool key)
        {
            switch (columnType.Name)
            {
                case "Guid":
                    return "uniqueidentifier";
                case "Int":
                    return "int";
                case "Int32":
                    return "int";
                case "DateTime":
                    return "datetime";
                case "Decimal":
                    return "decimal(18, 2)";
                case "TimeSpan":
                    return "time(7)";
                case "Double":
                    return "float";
                default:
                    return (key) ? "varchar(450)" : "varchar(max)";
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    CloseConnection();
                    Connection.Dispose();
                }
            }

            Server = null;
            Catalog = null;
            Username = null;
            Password = null;
            Timeout = 0;
            ConnectionString = null;
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
