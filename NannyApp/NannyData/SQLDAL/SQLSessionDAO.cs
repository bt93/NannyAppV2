using NannyData.DBHelpers;
using NannyData.Interfaces;
using NannyModels.Models.SessionModels;
using System.Data;

namespace NannyData.SQLDAL
{
    public class SQLSessionDAO : ISessionDAO
    {
        private readonly string _connectionString;

        public SQLSessionDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Session GetSessionByID(int sessionID, int userID)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.GetSessionByID");
                    command.AddWithValue("@SessionID", sessionID, SqlDbType.Int);
                    command.AddWithValue("@UserID", userID, SqlDbType.Int);

                    return command.ExecuteQuerySingleRowAsync<Session>().GetAwaiter().GetResult();
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
