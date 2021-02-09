
using System.Data.SqlClient;

namespace DBMSCourseStudentTesting.DataSource
{
    class SqlDataSource
    {
        private SqlConnection connection;
        public SqlDataSource(SqlConnection connection)
        {
            this.connection = connection;
        }
        public bool ExecuteNonQuery(string quert)
        {
            bool requestSuccessful = true;
            SqlCommand insert = new SqlCommand(quert, connection);
            connection.Open();
            try
            {
                insert.ExecuteNonQuery();
            }
            catch
            {
                requestSuccessful = false;
            }
            finally
            {
                connection.Close();
            }
            return requestSuccessful;
        }
    }

}
