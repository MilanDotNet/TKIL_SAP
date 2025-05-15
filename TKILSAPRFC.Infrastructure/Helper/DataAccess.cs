using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Model.Dapper;
using System.Data;
using System.Data.SqlClient;

namespace TKILSAPRFC.Infrastructure.Helper
{
    public static class DataAccess
    {

        private static int connectiontimeOut = int.TryParse(ConnectionStrings.Current?.ConnectionTimeOut, out connectiontimeOut) ? connectiontimeOut : 3600;
        private static SqlConnection GetOpenConnection(string databaseName)
        {
            //if (string.IsNullOrWhiteSpace(databaseName))
            //{
                //var connection = new SqlConnection(AppSettings.Current?.DataBaseConnections?.FirstOrDefault(x => x.DataBase == databaseName)?.DefaultConnection); connection.Open();
                //if (connection.State == ConnectionState.Closed)
                //    connection.Open();
                //return connection;

                var defaultconnection = new SqlConnection(ConnectionStrings.Current?.DefaultConnection);
                if (defaultconnection.State == ConnectionState.Closed)
                    defaultconnection.Open();
                return defaultconnection;
            //}
            
        }

        public static IEnumerable<T> Select<T>(string dataBaseName, T? criteria = default, string? viewName = null, string? whereClause = null)
        {
            var properties = ParseProperties(criteria);
            string tableName = typeof(T).Name;
            if (!string.IsNullOrEmpty(viewName))
                tableName = viewName;
            var sqlPairs = whereClause;
            if (string.IsNullOrEmpty(sqlPairs))
                GetSqlPairs(properties.AllNames, " AND ");
            if (string.IsNullOrEmpty(sqlPairs))
                sqlPairs = "1=1";

            var sql = string.Format("SELECT * FROM [{0}] WHERE {1}", tableName, sqlPairs);
            return GetItems<T>(CommandType.Text, sql, dataBaseName, properties.AllPairs);
        }

        public static void Update<T>(T obj, string dataBaseName)
        {
            var propertyContainer = ParseProperties(obj);
            var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
            var sqlValuePairs = GetSqlPairs(propertyContainer.ValueNames);
            var sql = string.Format("UPDATE [{0}] SET {1} WHERE {2}", typeof(T).Name, sqlValuePairs, sqlIdPairs);
            Execute(CommandType.Text, sql, dataBaseName, propertyContainer.AllPairs);
        }

        public static void Delete<T>(T obj, string dataBaseName)
        {
            var propertyContainer = ParseProperties(obj);
            var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
            var sql = string.Format("DELETE FROM [{0}] WHERE {1}", typeof(T).Name, sqlIdPairs);
            Execute(CommandType.Text, sql, dataBaseName, propertyContainer.IdPairs);
        }

        public static void DeleteMultiple<T>(List<object> idList, string fieldName, string dataBaseName)
        {
            string listOfIdsJoined = "(" + String.Join(",", idList.ToArray()) + ")";
            var sql = string.Format("DELETE FROM [{0}] WHERE {1} IN {2}", typeof(T).Name, fieldName, listOfIdsJoined);
            Execute(CommandType.Text, sql, dataBaseName);
        }

        public static void Insert<T>(T obj, string dataBaseName)
        {
            var propertyContainer = ParseProperties(obj);
            var sql = string.Format("INSERT INTO [{0}] ({1}) VALUES (@{2}) SELECT CAST(scope_identity() AS int)",
                typeof(T).Name,
                string.Join(", ", propertyContainer.ValueNames),
                string.Join(", @", propertyContainer.ValueNames));

            using var connection = GetOpenConnection(dataBaseName);
            var id = connection.Query<int>
            (sql, propertyContainer.ValuePairs, commandType: CommandType.Text).First();
            SetId(obj, id, propertyContainer.IdPairs);
        }
        public static async Task<DataSet?> ExecuteQuery(string commandName, string dataBaseName)
        {
            SqlConnection sqlConnection = GetOpenConnection(dataBaseName);
            using DataSet dataSet = new();
            try
            {
                SqlCommand command = new(commandName, sqlConnection)
                {
                    CommandType = CommandType.Text,
                    CommandTimeout = connectiontimeOut
                };

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                await Task.Run(() => sqlDataAdapter.Fill(dataSet));
                return dataSet;
            }
            catch (Exception ex)
            {
                return default;
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public static async Task<DataSet> ExecuteCommand(string commandName, SqlParameter[] parameters, string dataBaseName)
        {
            DataSet dataSet = new DataSet();
            SqlConnection sqlConnection = GetOpenConnection(dataBaseName);
            try
            {
                SqlCommand command = new(commandName, sqlConnection);
                command.Parameters.AddRange(parameters);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = connectiontimeOut;

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                await Task.Run(() => sqlDataAdapter.Fill(dataSet));
                return dataSet;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { sqlConnection.Close(); }
        }
        
        public static async Task<DataTable> ExecuteCommandDt (string commandName, SqlParameter[] parameters, string dataBaseName)
        {
            DataTable dataSet = new DataTable();
            SqlConnection sqlConnection = GetOpenConnection(dataBaseName);
            try
            {
                SqlCommand command = new(commandName, sqlConnection);
                command.Parameters.AddRange(parameters);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = connectiontimeOut;

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                await Task.Run(() => sqlDataAdapter.Fill(dataSet));
                return dataSet;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { sqlConnection.Close(); }
        }

        public static async Task<SqlParameter[]> ExecuteNonQuery(string commandName, SqlParameter[] parameters, string dataBaseName)
        {
            SqlConnection sqlConnection = GetOpenConnection(dataBaseName);
            try
            {
                SqlCommand command = new(commandName, sqlConnection);
                command.Parameters.AddRange(parameters);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = connectiontimeOut;
                await command.ExecuteNonQueryAsync();

                return parameters;
            }
            catch (Exception)
            {

                throw;
            }
            finally { sqlConnection.Close(); }

        }

        public static T? Get<T>(string query, string dataBaseName, object? parameters = null)
        {
            using var connection = GetOpenConnection(dataBaseName);
            {
                try
                {
                    return connection.QueryFirstOrDefault<T>(query, parameters, commandType: CommandType.Text, commandTimeout: connectiontimeOut);
                }
                catch (Exception)
                {
                    throw;
                }
                finally { connection.Close(); }
            }
        }
        public static async Task<T?> GetAsync<T>(string query, string DataBaseName, object? parameters = null)
        {
            using var connection = GetOpenConnection(DataBaseName);
            {
                try
                {
                    return await connection.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: CommandType.Text, commandTimeout: connectiontimeOut);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static async Task<IEnumerable<T>> GetAsyncDB<T>(string query, string DataBaseName, object? parameters = null)
        {
            using var connection = GetOpenConnection(DataBaseName);
            {
                try
                {
                    //return await connection.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: CommandType.Text, commandTimeout: connectiontimeOut);
                    return await connection.QueryAsync<T>(query, parameters, commandType: CommandType.Text, commandTimeout: connectiontimeOut);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static T GetDetails<T>(string query, string dataBaseName, object? parameters = null)
        {

            using var connection = GetOpenConnection(dataBaseName);
            {
                try
                {
                    return connection.QueryFirstOrDefault<T>(query, parameters, commandType: CommandType.Text);
                }
                catch (Exception)
                {

                    throw;
                }
                finally { connection.Close(); }
            }

        }

        public static IEnumerable<T> GetList<T>(string sqlQuery, string dataBaseName, object? parameters = null)
        {
            using var connection = GetOpenConnection(dataBaseName);
            {
                try
                {
                    return connection.Query<T>(sqlQuery, parameters, commandType: CommandType.Text, commandTimeout: connectiontimeOut);
                }
                catch (Exception)
                {
                    throw;
                }
                finally { connection.Close(); }
            }
        }
        public static async Task<IEnumerable<T>> GetListAsync<T>(string sqlQuery, string dataBaseName, object? parameters = null)
        {
            using var connection = GetOpenConnection(dataBaseName);
            {
                try
                {
                    return await connection.QueryAsync<T>(sqlQuery, parameters, commandType: CommandType.Text, commandTimeout: connectiontimeOut);
                }
                catch (Exception)
                {

                    throw;
                }
                finally { connection.Close(); }

            }
        }
        public static void InsertUpdate(string script, CommandType commandType, string dataBaseName, object? parameters = null)
        {
            using var connection = GetOpenConnection(dataBaseName);
            {
                try
                {
                    connection.Execute(script, parameters, commandType: commandType, commandTimeout: connectiontimeOut);
                }
                catch (Exception)
                {

                    throw;
                }
                finally { connection.Close(); }
            }
        }
        public static async Task InsertUpdateAsync(string script, CommandType commandType, string dataBaseName, object? parameters = null)
        {
            using var connection = GetOpenConnection(dataBaseName);
            {
                try
                {
                    await connection.ExecuteAsync(script, parameters, commandType: commandType, commandTimeout: connectiontimeOut);
                }
                catch (Exception)
                {

                    throw;
                }
                finally { connection.Close(); }
            }
        }
        public static void Delete(string query, string dataBaseName, object? parameters = null)
        {
            using var connection = GetOpenConnection(dataBaseName);
            {
                try
                {
                    connection.Execute(query, parameters, commandType: CommandType.Text, commandTimeout: connectiontimeOut);
                }
                catch (Exception)
                {

                    throw;
                }
                finally { connection.Close(); }
            }
        }

        public static async Task ExecuteStoredProcedure(string storedProcedure, string dataBaseName, object? parameters = null)
        {
            using var connection = GetOpenConnection(dataBaseName);
            {
                try
                {
                    await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: connectiontimeOut);
                }
                catch (Exception)
                {

                    throw;
                }
                finally { connection.Close(); }
            }
        }

        public static async Task<int> InsertWithIdentityAsync(string script, CommandType commandType, string dataBaseName, object? parameters = null)
        {
            using var connection = GetOpenConnection(dataBaseName);
            {
                try
                {
                    return await connection.QuerySingleAsync<int>(script, param: parameters);
                }
                catch (Exception)
                {

                    throw;
                }
                finally { connection.Close(); }
            }

        }
        public static void BulkInsertUpdate(DataTable dt, string tableName, string dataBaseName)
        {
            using var connection = GetOpenConnection(dataBaseName);
            using SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection);
            try
            {
                sqlBulkCopy.DestinationTableName = tableName;
                foreach (DataColumn item in dt.Columns)
                {
                    sqlBulkCopy.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                }
                sqlBulkCopy.WriteToServer(dt);
            }
            catch (Exception)
            {

                throw;
            }
            finally { connection.Close(); }

        }

        /// <summary>
        /// Create a commaseparated list of value pairs on 
        /// the form: "key1=@value1, key2=@value2, ..."
        /// </summary>
        private static string GetSqlPairs
        (IEnumerable<string> keys, string separator = ", ")
        {
            var pairs = keys.Select(key => string.Format("{0}=@{0}", key)).ToList();
            return string.Join(separator, pairs);
        }

        private static void SetId<T>(T obj, int id, IDictionary<string, object> propertyPairs)
        {
            if (propertyPairs.Count == 1)
            {
                var propertyName = propertyPairs.Keys.First();
                var propertyInfo = obj.GetType().GetProperty(propertyName);
                if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(Int64))
                {
                    propertyInfo.SetValue(obj, id, null);
                }
            }
        }

        // Example: GetBySql<Activity>( "SELECT * 
        //FROM Activities WHERE Id = @activityId", new {activityId = 15} ).FirstOrDefault();
        public static IEnumerable<T>
        GetItems<T>(CommandType commandType, string sql, string dataBaseName, object? parameters = null)
        {
            using (var connection = GetOpenConnection(dataBaseName))
            {
                return connection.Query<T>(sql, parameters, commandType: commandType);
            }
        }

        private static int Execute(CommandType commandType, string sql, string dataBaseName, object? parameters = null)
        {
            using (var connection = GetOpenConnection(dataBaseName))
            {
                return connection.Execute(sql, parameters, commandType: commandType);
            }
        }

        public static IEnumerable<T> GetResultCommaSeparated<T>(string tableName, string fieldName, string dataBaseName, object[] array)
        {
            using (var connection = GetOpenConnection(dataBaseName))
            {
                string sql = "SELECT * FROM " + tableName + " WHERE " + fieldName + " IN @ids";
                return connection.Query<T>(sql, new { ids = array });
            }
        }

        /// <summary>
        /// Retrieves a Dictionary with name and value 
        /// for all object properties matching the given criteria.
        /// </summary>
        private static PropertyContainer ParseProperties<T>(T obj)
        {
            var propertyContainer = new PropertyContainer();
            if (obj == null)
                return propertyContainer;
            var typeName = typeof(T).Name;
            var validKeyNames = new[] { "Id",
            string.Format("{0}Id", typeName), string.Format("{0}_Id", typeName) };

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                // Skip reference types (but still include string!)
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                    continue;

                // Skip methods without a public setter
                if (property.GetSetMethod() == null)
                    continue;

                // Skip methods specifically ignored
                if (property.IsDefined(typeof(DapperModel.DapperIgnore), false))
                    continue;

                var name = property.Name;
                var value = typeof(T).GetProperty(property.Name).GetValue(obj, null);

                if (property.PropertyType == typeof(DateTime) && (DateTime)value == DateTime.MinValue)
                    continue;
                if (property.IsDefined(typeof(DapperModel.DapperKey), false) || validKeyNames.Contains(name))
                {
                    propertyContainer.AddId(name, value);
                }
                else
                {
                    propertyContainer.AddValue(name, value);
                }
            }

            return propertyContainer;
        }

        private class PropertyContainer
        {
            private readonly Dictionary<string, object> _ids;
            private readonly Dictionary<string, object> _values;

            #region Properties

            internal IEnumerable<string> IdNames
            {
                get { return _ids.Keys; }
            }

            internal IEnumerable<string> ValueNames
            {
                get { return _values.Keys; }
            }

            internal IEnumerable<string> AllNames
            {
                get { return _ids.Keys.Union(_values.Keys); }
            }

            internal IDictionary<string, object> IdPairs
            {
                get { return _ids; }
            }

            internal IDictionary<string, object> ValuePairs
            {
                get { return _values; }
            }

            internal IEnumerable<KeyValuePair<string, object>> AllPairs
            {
                get { return _ids.Concat(_values); }
            }

            #endregion

            #region Constructor

            internal PropertyContainer()
            {
                _ids = new Dictionary<string, object>();
                _values = new Dictionary<string, object>();
            }

            #endregion

            #region Methods

            internal void AddId(string name, object value)
            {
                _ids.Add(name, value);
            }

            internal void AddValue(string name, object value)
            {
                _values.Add(name, value);
            }

            #endregion
        }
    }
}
