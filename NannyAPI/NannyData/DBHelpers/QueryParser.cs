using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace NannyData.DBHelpers
{
    public static class QueryParser
    {
        public static ICollection<T> ParseTable<T>(this SqlDataReader dataReader, SqlCommand command) where T : new()
        {
            DataTable table = new DataTable();
            ICollection<T> result = new List<T>();
            table.Load(dataReader);
            command.CloseConnection();


            foreach(DataRow dr in table.Rows)
            {
                result.Add(CreateItemFromRow<T>(dr));
            }

            return result;
        }

        public static T ParseTableSingleRow<T>(this SqlDataReader dataReader, SqlCommand command) where T : new()
        {
            DataTable table = new DataTable();

            table.Load(dataReader);

            command.CloseConnection();

            if (table.Rows.Count == 0) { return new(); }

            return CreateItemFromRow<T>(table.Rows[0]);
        }

        public static T CreateSingleFromTable<T>(this DataTable dataTable) where T : new()
        {
            return CreateItemFromRow<T>(dataTable.Select()[0]);
        }

        public static List<T> CreateListFromTable<T>(this DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                list.Add(CreateItemFromRow<T>(dataRow));
            }

            return list;
        }

        public static List<T> CreateListFromTableList<T>(this List<DataTable> dataTables) where T : new()
        {
            List<T> list = new List<T>();

            foreach (var dataTable in dataTables)
            {
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    list.Add(CreateItemFromColumn<T>(dataColumn));
                }
            }

            return list;
        }

        public static T CreateItemFromRow<T>(DataRow dataRow) where T : new()
        {
            T item = new T();

            SetItemFromRow(item, dataRow);

            return item;
        }

        public static void SetItemFromRow<T>(T item, DataRow dataRow) where T : new()
        {
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                var propertyInfo = item.GetType().GetProperty(column.ColumnName);

                if (propertyInfo is not null && dataRow[column] is not DBNull)
                {
                    propertyInfo.SetValue(item, dataRow[column], null);
                }
            }
        }

        public static T CreateItemFromColumn<T>(DataColumn dataColumn) where T : new()
        {
            T item = new T();

            return item;
        }
    }
}
