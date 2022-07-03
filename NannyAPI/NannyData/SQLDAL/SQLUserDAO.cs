﻿using NannyData.DBHelpers;
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
                    command.AddWithValue("@FirstName", user.FirstName, SqlDbType.VarChar);
                    command.AddWithValue("@LastName", user.LastName, SqlDbType.VarChar);
                    command.AddWithValue("@UserName", user.UserName, SqlDbType.VarChar);
                    command.AddWithValue("@Password", user.Password, SqlDbType.VarChar);
                    command.AddWithValue("@PhoneNumber", user.PhoneNumber, SqlDbType.VarChar);
                    command.AddWithValue("@EmailAddress", user.EmailAddress, SqlDbType.VarChar);
                    command.AddWithValue("@Salt", user.Salt, SqlDbType.VarChar);
                    command.AddWithValue("@Role", user.RoleID, SqlDbType.VarChar);
                    command.AddWithValue("@IsVerified", user.IsVerified, SqlDbType.VarChar);
                    command.AddWithValue("@Address1", user.Addresses.First().Address1, SqlDbType.VarChar);
                    command.AddWithValue("@Address2", user.Addresses.First().Address2, SqlDbType.VarChar);
                    command.AddWithValue("@Address3", user.Addresses.First().Address3, SqlDbType.VarChar);
                    command.AddWithValue("@Address4", user.Addresses.First().Address4, SqlDbType.VarChar);
                    command.AddWithValue("@Locality", user.Addresses.First().Locality, SqlDbType.VarChar);
                    command.AddWithValue("@Region", user.Addresses.First().Region, SqlDbType.VarChar);
                    command.AddWithValue("@PostalCode", user.Addresses.First().PostalCode, SqlDbType.VarChar);
                    command.AddWithValue("@County", user.Addresses.First().County, SqlDbType.VarChar);
                    command.AddWithValue("@Country", user.Addresses.First().Country, SqlDbType.VarChar);

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
    }
}
