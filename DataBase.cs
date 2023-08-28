using System.Data;
using System.Data.SqlClient;

namespace AiChatBot
{
    internal class DataBase
    {
        private const string CONNECTION_STRING = @"Server=localhost;Database=master;Trusted_Connection=True;";
        private const string QUERY = "CREATE TABLE CHATSESSIONS(CHAT_ID BIGINT,ID_SESSION char(50))";
        public static async Task CreateTable()
        {
            using (var connect = new SqlConnection(CONNECTION_STRING))
            {
                connect.Open();

                try
                {
                    await new SqlCommand(QUERY, connect).ExecuteReaderAsync();
                    Console.WriteLine("Created table CHATSESSIONS.");
                }
                catch
                {
                    Console.WriteLine("Table already exists.");
                    
                }
                finally
                {
                    if (connect.State == ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }            
        }
        public static async Task Query(long chatId)
        {
            using (var connect = new SqlConnection(CONNECTION_STRING))
            {
                connect.Open();
                try
                {
                    string QUERY = "INSERT INTO CHATSESSIONS(CHAT_ID, ID_SESSION)";
                    QUERY += $"VALUES('{chatId}','{DateTime.Now}')";
                    await new SqlCommand(QUERY, connect).ExecuteReaderAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Query not initialized.");
                    Console.WriteLine(e);
                }
                finally
                {
                    if (connect.State == ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }
        }
        
    }
}
