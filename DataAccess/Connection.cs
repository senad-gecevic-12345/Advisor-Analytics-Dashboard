using System.Data.SQLite;

namespace fullstack_1.DataAccess
{
    public class Connection
    {
        private static readonly string sql_folder_location = "C:\\Users\\meme_\\source\\repos\\fullstack_1\\fullstack_1\\SQLite\\";
        private static readonly string connection_string = @"URI=file:C:\Users\meme_\source\repos\fullstack_1\fullstack_1\SQLite\DB.db";
        private SQLiteConnection CreateConnection()
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
}
