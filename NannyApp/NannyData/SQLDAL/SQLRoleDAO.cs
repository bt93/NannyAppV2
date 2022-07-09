using NannyData.DBHelpers;
using NannyData.Interfaces;
using NannyModels.Enumerations;
using System.Data;

namespace NannyData.SQLDAL
{
    public class SQLRoleDAO : IRoleDAO
    {
        private readonly string _connectionString;

        public SQLRoleDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Role> GetRolesByUserID(int userID)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.GetRolesByUserID");
                    command.AddWithValue("@UserID", userID, SqlDbType.Int);

                    return command.ExecuteQuerySingleRowAsync<List<Role>>().GetAwaiter().GetResult();
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
