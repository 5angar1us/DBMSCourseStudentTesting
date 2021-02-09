using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMSCourseStudentTesting.DataSource
{
    class SqlDataSetСonnector
    {

        SqlConnection connection;

        public SqlDataSetСonnector(DbConnection connection)
        {
            this.connection = connection as SqlConnection;
        }

        //---------------------------------------------------------------------------------------
        public void FillDataView(String quert, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            InsertData(quert, dataGridView);
        }
        private void InsertData(String quertString, DataGridView dataGridView)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(quertString, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataGridView.Columns.Add(reader.GetName(i), reader.GetName(i));
                }
                for (int i = 0; reader.Read(); i++)
                {
                    InsertRow(dataGridView, reader, i);
                }
            }
            connection.Close();
        }
        private void InsertRow(DataGridView dataGridView, SqlDataReader reader, int i)
        {
            dataGridView.Rows.Add();
            for (int y = 0; y < dataGridView.ColumnCount; y++)
            {
                dataGridView[y, i].Value = reader[y].ToString();
            }
        }


        //------------------------------------------------------------------------------------------
        private void FillDataSet(String quertString, DataSet dataSet)
        {
            connection.Open();
            using (SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter(quertString, connection))
            {
                mySqlDataAdapter.Fill(dataSet);
            }
            connection.Close();
        }
    }
}

