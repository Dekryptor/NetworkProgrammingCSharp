using System;
using System.Drawing;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace SocketClient
{
    public partial class SocketClient : Form
    {
        Socket m_socClient;

        public SocketClient()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string ipServer = txtIPServer.Text;
                string strPort = txtServerPort.Text;
                int portNum = System.Convert.ToInt16(strPort, 10);

                //create a new client socket ...
                m_socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress remoteIPAddress = IPAddress.Parse(ipServer);
                IPEndPoint remoteEndPoint = new IPEndPoint(remoteIPAddress, portNum);
                m_socClient.Connect(remoteEndPoint);
                
                //send client info to server
                string computerName = Dns.GetHostName();
                //string ipAddress = Dns.GetHostEntry(computerName).AddressList[0].ToString();
                String szData = computerName + " connected..." + Environment.NewLine;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(szData);
                //byte[] byData = System.Text.Encoding.Unicode.GetBytes(szData);
                m_socClient.Send(byData);

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
            }
            catch (SocketException se)
            {
                MessageBox.Show("Có lỗi khi kết nối đến Server: " + Environment.NewLine + se.Message, "Lỗi kết nối");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi kết nối đến Server: " + Environment.NewLine + ex.Message, "Lỗi kết nối");
            }
        }


        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            m_socClient.Shutdown(SocketShutdown.Both);
            m_socClient.Close();

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            try
            {
                Object objData = txtDataSend.Text + Environment.NewLine;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());
                //byte[] byData = System.Text.Encoding.Unicode.GetBytes(objData.ToString());
                m_socClient.Send(byData);

                txtDataSend.Invoke(new MethodInvoker(delegate
                {
                    txtDataSend.Text = string.Empty;
                }));
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void btnReceiveData_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = new byte[1024 * 5000];
                int byteReceive = m_socClient.Receive(data);
                DataTable dt = (DataTable)Utilities.DeserializeData(data);
                //dgvReceived.DataSource = dt;
                BindDataGridView(dgvReceived, dt);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void BindDataGridView(DataGridView dgv, DataTable dtb)
        {
            dgv.AutoGenerateColumns = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.DataSource = dtb;
            dgv.Refresh();
        }

        private void dgvReceived_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgvReceived.Rows[rowIndex];
            string col0 = row.Cells[0].Value.ToString();
            string col1 = row.Cells[1].Value.ToString();
            string col2 = row.Cells[2].Value.ToString();
            string col3 = row.Cells[3].Value.ToString();
        }

        private void dgvReceived_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = dgvReceived.HitTest(e.X, e.Y);
                dgvReceived.ClearSelection();
                dgvReceived.Rows[hti.RowIndex].Selected = true;
            }
        }

        private void deleteRow_Click(object sender, EventArgs e)
        {
            Int32 rowToDelete = dgvReceived.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            dgvReceived.Rows.RemoveAt(rowToDelete);
            dgvReceived.ClearSelection();

            var bac = m_socClient;
            //Socket
            try
            {
                string objDel = "ABCD DELETE FROM Sample WHERE Id = " + rowToDelete.ToString() + Environment.NewLine;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objDel);
                //byte[] byData = System.Text.Encoding.Unicode.GetBytes(objData.ToString());
                m_socClient.Send(byData);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void editRow_Click(object sender, EventArgs e)
        {

        }
    }
}
