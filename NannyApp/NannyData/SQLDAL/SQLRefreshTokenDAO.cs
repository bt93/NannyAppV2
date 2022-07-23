using NannyData.DBHelpers;
using NannyData.Interfaces;
using NannyModels.Models.Authentication;
using System.Data;

namespace NannyData.SQLDAL
{
    public class SQLRefreshTokenDAO : IRefreshTokenDAO
    {
        private readonly string _connectionString;

        public SQLRefreshTokenDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddRefreshToken(RefreshToken token)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.AddRefreshToken");
                    command.AddWithValue("@UserID", token.UserID, SqlDbType.Int);
                    command.AddWithValue("@Token", token.Token ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@JWTID", token.JWTID ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@IsUsed", token.IsUsed, SqlDbType.Bit);
                    command.AddWithValue("@IsRevoked", token.IsRevoked, SqlDbType.Bit);
                    command.AddWithValue("@DateAdded", token.DateAdded, SqlDbType.DateTimeOffset);
                    command.AddWithValue("@DateExpired", token.DateExpired, SqlDbType.DateTimeOffset);

                    return command.ExecuteWithReturnValueAsync<int>().GetAwaiter().GetResult();
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
