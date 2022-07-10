using NannyData.DBHelpers;
using NannyData.Interfaces;
using NannyModels.Models.AddressModels;
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

        public int AddNewAddress(AddressInput address, int userID)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.AddNewAddress");
                    command.AddWithValue("@Address1", address.Address1 ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Address2", address.Address2 ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Address3", address.Address3 ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Address4", address.Address4 ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Locality", address.Locality ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Region", address.Region ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@PostalCode", address.PostalCode ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@County", address.County ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Country", address.Country ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@UserID", userID, SqlDbType.Int);


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
