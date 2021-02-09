using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMSCourseStudentTesting
{
    class DataSetСonnector
    {

        SqlConnection connection;

        public DataSetСonnector(DbConnection connection)
        {
            this.connection = connection as SqlConnection;

        }
        public void FillDataView(String quert)
        {
            connection.Open();
            
            //InsertDataInDataGried(quertString, dataGridView);
            //InsertData(quert, dataGridView);
            connection.Close();

        }
        public void FillDataView(String quert, DataGridView dataGridView)
        {
            connection.Open();
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            //InsertDataInDataGried(quertString, dataGridView);
            InsertData(quert, dataGridView);
            connection.Close();
        }
        private void InsertData(String quertString, DataGridView dataGridView)
        {
            using (SqlCommand command = new SqlCommand(quertString, connection ))
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
        }
        private void InsertRow(DataGridView dataGridView, SqlDataReader reader, int i)
        {
            dataGridView.Rows.Add();
            for (int y = 0; y < dataGridView.ColumnCount; y++)
            {
                dataGridView[y, i].Value = reader[y].ToString();
            }
        }
        private DataSet GetData(String quertString)
        {
            DataSet DS = new DataSet();
            using (SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter(quertString, connection))
            {
                mySqlDataAdapter.Fill(DS);
            }
            return DS;
        }
    }
}

