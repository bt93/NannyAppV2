using System.Data;
using System.Data.SqlClient;

namespace NannyData.DBHelpers
{
    public static class Connection
    {
        public static SqlConnection CreateConnection(this string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        public static SqlCommand CreateNewCommand(this SqlConnection connection, string commandName)
        {
            SqlCommand command = new SqlCommand(commandName, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public static SqlCommand OpenConnection(this SqlCommand command)
        {
            if (!command.Connection.State.Equals(ConnectionState.Open))
            {
                command.Connection.Open();
            }

            return command;
        }

        public static SqlCommand CloseConnection(this SqlCommand command)
        {
            if (!command.Connection.State.Equals(ConnectionState.Closed))
            {
                command.Connection.Close();
            }

            return command;
        }
    }
}
