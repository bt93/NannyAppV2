﻿using System.Data;
using System.Data.SqlClient;

namespace NannyData.DBHelpers
{
    public static class Command
    {
        public static SqlCommand AddWithValue(this SqlCommand command, string parameterName, object value, SqlDbType dbType)
        {
            var parameter = command.Parameters.AddWithValue(parameterName, value);
            parameter.SqlDbType = dbType;
            return command;
        }

        public static object GetOutParameter(this SqlCommand command, string parameterName)
        {
            command.Parameters.Add(parameterName);
            return command.Parameters[parameterName];
        }

        public static void NonQuery(this SqlCommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException("Sql Command not found");
            }

            command.OpenConnection();
            command.ExecuteNonQuery();
            command.CloseConnection();
        }

        public static ICollection<T> ExecuteQuery<T>(this SqlCommand command) where T : new()
        {
            if (command is null)
            {
                throw new ArgumentNullException("Sql Command not found");
            }

            command.OpenConnection();

            using (var reader = command.ExecuteReader())
            {
                var result = reader.ParseTable<T>(command);
                return result;
            }
        }

        public static T ExecuteQuerySingleRow<T>(this SqlCommand command) where T : new()
        {
            if (command is null)
            {
                throw new ArgumentNullException("Sql Command not found");
            }

            command.OpenConnection();

            using (var reader = command.ExecuteReader())
            {
                var result = reader.ParseTableSingleRow<T>(command);
                return result;
            }
        }

        public static async Task<ICollection<T>> ExecuteQueryAsync<T>(this SqlCommand command) where T : new()
        {
            if (command is null)
            {
                throw new ArgumentNullException("Sql Command not found");
            }

            command.OpenConnection();

            using (var reader = await command.ExecuteReaderAsync())
            {
                var result = reader.ParseTable<T>(command);
                command.CloseConnection();
                return result;
            }
        }

        public static async Task<T> ExecuteQuerySingleRowAsync<T>(this SqlCommand command) where T : new()
        {
            if (command is null)
            {
                throw new ArgumentNullException("Sql Command not found");
            }

            command.OpenConnection();

            using (var reader = await command.ExecuteReaderAsync())
            {
                var result = reader.ParseTableSingleRow<T>(command);
                return result;
            }
        }
    }
}