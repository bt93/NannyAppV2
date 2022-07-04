using NannyData.DBHelpers;
using NannyData.Interfaces;
using NannyModels.Models;
using System.Data;

namespace NannyData.SQLDAL
{
    public class SQLAddressDAO : IAddressDAO
    {
        private readonly string _connectionString;

        public SQLAddressDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ICollection<Address> GetAddressesByUserID(int id)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.GetAdressesByUserID");
                    command.AddWithValue("@UserID", id, SqlDbType.Int);

                    return command.ExecuteQueryAsync<Address>().GetAwaiter().GetResult() ?? new List<Address>();
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
