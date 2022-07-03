using NannyData.DBHelpers;
using NannyData.Interfaces;
using NannyModels.Models;
using System.Data;

namespace NannyData.SQLDAL
{
    public class SQLUserDAO : IUserDAO
    {
        private readonly string _connectionString;

        public SQLUserDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ApplicationUser GetUserByID(int id)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.GetUserByID");
                    command.AddWithValue("@UserID", id, SqlDbType.Int);

                    return command.ExecuteQuerySingleRowAsync<ApplicationUser>().GetAwaiter().GetResult() ?? new ApplicationUser();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public ApplicationUser GetUserForLogin(string userInput)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.GetUserForLogin");
                    command.AddWithValue("@UserInput", userInput, SqlDbType.VarChar);

                    return command.ExecuteQuerySingleRowAsync<ApplicationUser>().GetAwaiter().GetResult() ?? new ApplicationUser();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
