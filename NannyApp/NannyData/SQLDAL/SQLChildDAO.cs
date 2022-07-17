using NannyData.DBHelpers;
using NannyData.Interfaces;
using NannyModels.Models.ChildModels;
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

        public int AddNewChild(ChildInput child, int userID)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.AddNewChild");
                    command.AddWithValue("@FirstName", child.FirstName ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@LastName", child.LastName ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@GenderID", child.GenderID, SqlDbType.Int);
                    command.AddWithValue("@DateOfBirth", child.DateOfBirth, SqlDbType.DateTimeOffset);
                    command.AddWithValue("@RatePerHour", child.RatePerHour, SqlDbType.Decimal);
                    command.AddWithValue("@NeedsDiapers", child.NeedsDiapers, SqlDbType.Bit);
                    command.AddWithValue("@Active", true, SqlDbType.Bit);
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

        public bool UpdateChild(ChildInput child, int childID, int userID)
        {
            using (var connection = _connectionString.CreateConnection())
            {
                try
                {
                    var command = connection.CreateNewCommand("dbo.UpdateChild");
                    command.AddWithValue("@ChildID", childID, SqlDbType.Int);
                    command.AddWithValue("@FirstName", child.FirstName ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@LastName", child.LastName ?? string.Empty, SqlDbType.VarChar);
                    command.AddWithValue("@GenderID", child.GenderID, SqlDbType.Int);
                    command.AddWithValue("@DateOfBirth", child.DateOfBirth, SqlDbType.DateTimeOffset);
                    command.AddWithValue("@RatePerHour", child.RatePerHour, SqlDbType.Decimal);
                    command.AddWithValue("@NeedsDiapers", child.NeedsDiapers, SqlDbType.Bit);
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
