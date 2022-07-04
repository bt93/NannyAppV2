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

        public ICollection<Child> GetChildByUserID(int userID)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                var command = connection.CreateNewCommand("dbo.GetChildrenByUserID");
                command.AddWithValue("@UserID", userID, SqlDbType.Int);

                return command.ExecuteQueryAsync<Child>().GetAwaiter().GetResult();
            }
        }
    }
}
