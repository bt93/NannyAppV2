using NannyData.DBHelpers;
using NannyData.Interfaces;
using NannyModels.Enumerations;
using NannyModels.Models.User;
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

                    return command.ExecuteQuerySingleRowAsync<ApplicationUser>().GetAwaiter().GetResult();
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

        public ICollection<ApplicationUser> GetUsersByChildID(int childID)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                var command = connection.CreateNewCommand("dbo.GetUsersByChildID");
                command.AddWithValue("@ChildID", childID, SqlDbType.Int);

                return command.ExecuteQueryAsync<ApplicationUser>().GetAwaiter().GetResult();
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

        public ICollection<ApplicationUser> GetUserConnectedByChild(int userID, Role role)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.GetUserConnectedByChild");
                    command.AddWithValue("@UserID", userID, SqlDbType.Int);
                    command.AddWithValue("@RoleID", role.GetOppositeRole(), SqlDbType.Int);

                    return command.ExecuteQueryAsync<ApplicationUser>().GetAwaiter().GetResult();
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

        public UserCheck DoesUserExist(string userName, string emailAddress)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.DoesUserExist");
                    command.AddWithValue("@UserName", userName, SqlDbType.VarChar);
                    command.AddWithValue("@EmailAddress", emailAddress, SqlDbType.VarChar);

                    return command.ExecuteQuerySingleRowAsync<UserCheck>().GetAwaiter().GetResult();
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

        public int AddNewUser(ApplicationUser user)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.AddNewUser");
                    command.AddWithValue("@FirstName", user.FirstName ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@LastName", user.LastName ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@UserName", user.UserName ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Password", user.Password ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@PhoneNumber", user.PhoneNumber ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@EmailAddress", user.EmailAddress ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Salt", user.Salt ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Role", user.RoleID.FirstOrDefault(), SqlDbType.VarChar);
                    command.AddWithValue("@IsVerified", user.IsVerified, SqlDbType.VarChar);
                    command.AddWithValue("@Address1", user.Addresses.First().Address1 ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Address2", user.Addresses.First().Address2 ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Address3", user.Addresses.First().Address3 ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Address4", user.Addresses.First().Address4 ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Locality", user.Addresses.First().Locality ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Region", user.Addresses.First().Region ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@PostalCode", user.Addresses.First().PostalCode ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@County", user.Addresses.First().County ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@Country", user.Addresses.First().Country ?? string.Empty, SqlDbType.VarChar);

                    return command.ExecuteQuerySingleRowAsync<ApplicationUser>().GetAwaiter().GetResult().UserID;
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

        public bool UpdateUser(int userID, string firstName, string lastName, string phoneNumber)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.UpdateUser");
                    command.AddWithValue("@UserID", userID, SqlDbType.Int);
                    command.AddWithValue("@FirstName", firstName, SqlDbType.VarChar);
                    command.AddWithValue("@LastName", lastName, SqlDbType.VarChar);
                    command.AddWithValue("@PhoneNumber", phoneNumber, SqlDbType.VarChar);

                    return command.ExecuteWithReturnValueAsync<int>().GetAwaiter().GetResult() == 1;
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

        public bool VerifyUser(int userID)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.VerifyUser");
                    command.AddWithValue("@UserID", userID, SqlDbType.Int);

                    return command.ExecuteWithReturnValueAsync<int>().GetAwaiter().GetResult() == 1;
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
