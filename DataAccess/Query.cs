/*
using System.Data.SQLite;
using fullstack_1.Status;
using Microsoft.Data.SqlClient;

namespace fullstack_1.DataAccess
{


    internal class SQLite{
        private static readonly string connection_string = @"URI=file:C:\Users\meme_\source\repos\fullstack_1\fullstack_1\SQLite\DB.db";
        static internal SQLiteConnection CreateConnection()
        {
            SQLiteConnection db_connection = new SQLiteConnection(connection_string);
            try
            {
                db_connection.Open();
            }
            catch (Exception)
            {

            }
            return db_connection;
        }
    }
      static class Query<T> where T : new()
      {
          internal static QueryStatus post_query(string query, Action<SQLiteParameterCollection> parameters)
          {
            QueryStatus status;
            using var connection = SQLite.CreateConnection();
            using var command = new SQLiteCommand(query, connection);
            parameters(command.Parameters);
            try
            {
                command.ExecuteNonQuery();
                status = QueryStatus.SUCCESS;
            }
            catch (SQLiteException)
            {
                status = QueryStatus.ERROR;
            }
            connection.Close();
            return status;
        }
        internal static Tuple<QueryStatus, List<T>>read_query(string query, Func<SQLiteDataReader, T> func)
        {
            QueryStatus status;
            using var connection = SQLite.CreateConnection();
            using var command = new SQLiteCommand(query, connection);
            List<T> list = new List<T>();
            try
            {
                using var reader = command.ExecuteReader();
                // maybe this as a lambda or something?
                while (reader.Read())
                {
                    list.Add(func(reader));    
                }
                status = QueryStatus.SUCCESS;
            }
            catch(SQLiteException)
            {
                status = QueryStatus.ERROR;
            }
            connection.Close();

            return Tuple.Create(status, list);
        }
        internal static Tuple<QueryStatus, List<T>>read_query(string query, Action<SQLiteParameterCollection> parameters,Func<SQLiteDataReader, T> func)
        {
            QueryStatus status;
            using var connection = SQLite.CreateConnection();
            using var command = new SQLiteCommand(query, connection);
            parameters(command.Parameters);
            List<T> list = new List<T>();
            try
            {
                using var reader = command.ExecuteReader();
                // maybe this as a lambda or something?
                while (reader.Read())
                {
                    list.Add(func(reader));    
                }
                status = QueryStatus.SUCCESS;
            }
            catch(SQLiteException)
            {
                status = QueryStatus.ERROR;
            }
            connection.Close();

            return Tuple.Create(status, list);
        }
        internal static QueryStatus read_query(string query, Func<SQLiteDataReader, T> func, ref List<T> list)
        {
            QueryStatus status;
            using var connection = SQLite.CreateConnection();
            using var command = new SQLiteCommand(query, connection);
            try
            {
                using var reader = command.ExecuteReader();
                // maybe this as a lambda or something?
                while (reader.Read())
                {
                    list.Add(func(reader));    
                }
                status = QueryStatus.SUCCESS;
            }
            catch(SQLiteException)
            {
                status = QueryStatus.ERROR;
            }
            connection.Close();

            return status;
        }
        internal static Tuple<QueryStatus, T> lookup_query(string query, Action<SQLiteParameterCollection> parameters, Func<SQLiteDataReader, T> func) 
        {
            QueryStatus status;
            T value;
            using var connection = SQLite.CreateConnection();
            using var command = new SQLiteCommand(query, connection);
            parameters(command.Parameters);
            using var reader = command.ExecuteReader(); 
            try {
                if (reader.Read())
                {
                    value = func(reader);
                    status = QueryStatus.SUCCESS;
                }
                else
                {
                    status = QueryStatus.NOT_FOUND;
                    value = new T();
                    //value = default(T);
                }
            }
            catch(SQLiteException) {
                status = QueryStatus.ERROR;
                //value = default(T);
                value = new T();
            }
            connection.Close();
            return Tuple.Create(status, value);
        }
        internal static QueryStatus delete_query(string query, Action<SQLiteParameterCollection> parameters)
        {
            QueryStatus status;
            using var connection = SQLite.CreateConnection();
            using var command = new SQLiteCommand(query, connection);
            parameters(command.Parameters);
            try
            {
                command.ExecuteNonQuery();
                status = QueryStatus.SUCCESS;
            }
            catch (SQLiteException)
            {
                status = QueryStatus.ERROR;
            }
            connection.Close();

            return status;
        }
        internal static QueryStatus has_value_query(string query, Action<SQLiteParameterCollection> parameters)
        {
            QueryStatus status;
            using var connection = SQLite.CreateConnection();
            using var command = new SQLiteCommand(query, connection);
            parameters(command.Parameters);
            try
            {
                // els£
                int rows = Convert.ToInt32(command.ExecuteScalar());
                if (rows > 0) status = QueryStatus.SUCCESS;
                else status = QueryStatus.NOT_FOUND;
            }
            catch (SQLiteException)
            {
                status = QueryStatus.ERROR;
            }
            connection.Close();

            return status;
        }
        class Parse<Item> where Item : new()
        {
            internal static QueryStatus parse_post(string query, Func<Item, Item> lambda, Action<Item> post, List<Item> list)
            {
                foreach(Item item in list)
                {
                    post(lambda(item)); 
                }

                return QueryStatus.ERROR;
            }
        }
        
        
    }
}
*/
