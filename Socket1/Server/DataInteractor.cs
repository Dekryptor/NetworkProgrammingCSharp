using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketServer
{
    public class DataInteractor
    {
        string connectionString = "Data Source=Dell-PC;Initial Catalog=SocketTest;User ID=sa;Password=123a@";
        //connect via IP Address (1433 is the default port for SQL Server)
        //string connetionString = "Data Source=IP_ADDRESS,PORT;Network Library=DBMSSOCN;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
        public SqlConnection OpenConnection(string connString)
        {
            SqlConnection cnn = new SqlConnection(connString);
            if (cnn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    cnn.Open();
                    //MessageBox.Show("Connection Open ! ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not open connection! " + ex.Message);
                }
            }

            return cnn;
        }

        public void CloseConnection(SqlConnection cnn)
        {
            if (cnn.State != System.Data.ConnectionState.Closed)
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        public DataTable LoadData(string query)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection(connectionString))
            //using (SqlConnection cnn = OpenConnection(connectionString))
            {
                //cnn.Open();
                //string query = "SELECT * FROM Products";
                SqlDataAdapter da = new SqlDataAdapter(query, cnn);
                
                da.Fill(dt);
            }
            return dt;
        }

        public bool NoneQuery(string query)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Connection.Open();
                int effRows = cmd.ExecuteNonQuery();
                if (effRows > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
