using NannyData.DBHelpers;
using NannyData.Interfaces;
using NannyModels.Models;
using System.Data;

namespace NannyData.SQLDAL
{
    public class SQLChildDAO : IChildDAO
    {
        private readonly string _connectionString;

        public SQLChildDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ICollection<Child> GetChildrenByUserID(int userID)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.GetChildrenByUserID");
                    command.AddWithValue("@UserID", userID, SqlDbType.Int);

                    return command.ExecuteQueryAsync<Child>().GetAwaiter().GetResult();
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

        public Child GetChildByID(int childID, int userID)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.GetChildByID");
                    command.AddWithValue("@ChildID", childID, SqlDbType.Int);
                    command.AddWithValue("@UserID", userID, SqlDbType.Int);

                    return command.ExecuteQuerySingleRowAsync<Child>().GetAwaiter().GetResult() ?? new Child();
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
