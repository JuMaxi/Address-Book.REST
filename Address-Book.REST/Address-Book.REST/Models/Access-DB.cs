using System.Data.SqlClient;
using System.IO;

namespace Address_Book.REST.Models
{
    public class Access_DB
    {
        string ConnectionString = "Server=LAPTOP-P4GEIO8K\\SQLEXPRESS;Database=AddressBook;User Id=sa;Password=S4root;";
        public void AccessNonQuery(string action)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(action, connection);
                connection.Open();

                command.ExecuteNonQuery();
            }
        }
        public SqlDataReader AccessReader(string action)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);

            SqlCommand Command = new SqlCommand(action, Connection);
            Connection.Open();

            SqlDataReader Reader = Command.ExecuteReader();
            return Reader;
        }



    }
}
