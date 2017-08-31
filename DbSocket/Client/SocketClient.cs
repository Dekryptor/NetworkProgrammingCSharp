using System;
using System.Drawing;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace DbSocket.Client
{
    public partial class SocketClient : Form
    {
        public static Socket m_socWorker = null;

        public SocketClient()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                int portNum = System.Convert.ToInt16(txtServerPort.Text, 10);

                //Create a new client socket ...
                m_socWorker = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //if (!m_socWorker.Connected)
                if (m_socWorker != null)
                {
                    IPAddress remoteIPAddress = IPAddress.Parse(txtIPServer.Text);
                    IPEndPoint remoteEndPoint = new IPEndPoint(remoteIPAddress, portNum);
                    m_socWorker.Connect(remoteEndPoint);
                }
                
                string computerName = Dns.GetHostName();
                String szData = " " + computerName + " connected..." + Environment.NewLine;
                
                byte[] byData = System.Text.Encoding.UTF8.GetBytes(szData);
                m_socWorker.Send(byData);

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
            }
            catch (SocketException se)
            {
                MessageBox.Show("Have mistake when connected to Server." + Environment.NewLine + se.Message, "Error socket");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Have mistake when connected to Server." + Environment.NewLine + ex.Message, "Error socket");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (m_socWorker != null)
            {
                if (!m_socWorker.Connected)
                {
                    m_socWorker.Shutdown(SocketShutdown.Both);
                    // disconnect to reuse socket
                    m_socWorker.Disconnect(true);
                }
                m_socWorker.Close();
            }

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            try
            {
                Object objData = Environment.NewLine + txtDataSend.Text;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());
                //byte[] byData = System.Text.Encoding.Unicode.GetBytes(objData.ToString());
                m_socWorker.Send(byData);

                txtDataSend.Invoke(new MethodInvoker(delegate
                {
                    txtDataSend.Text = string.Empty;
                }));
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReceiveData_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = new byte[1024 * 5000];
                int byteReceive = m_socWorker.Receive(data);
                DataTable dt = (DataTable)Helpers.DeserializeData(data);

                // Bind data to gridView
                dgvReceived.AutoGenerateColumns = true;
                dgvReceived.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvReceived.MultiSelect = false;
                dgvReceived.DataSource = dt;
                dgvReceived.Refresh();
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SocketClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure to exist client?", "Client confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                byte[] b = new byte[] { };
                m_socWorker.Send(b, 0, b.Length, SocketFlags.None);
                m_socWorker.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Socket Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void editRow_Click(object sender, EventArgs e)
        {

        }

        private void deleteRow_Click(object sender, EventArgs e)
        {
            Int32 rowToDelete = dgvReceived.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            dgvReceived.Rows.RemoveAt(rowToDelete);
            dgvReceived.ClearSelection();

            var bac = m_socWorker;
            //Socket
            try
            {
                string objDel = "ABCD DELETE FROM Sample WHERE Id = " + rowToDelete.ToString() + Environment.NewLine;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objDel);
                //byte[] byData = System.Text.Encoding.Unicode.GetBytes(objData.ToString());
                m_socWorker.Send(byData);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }
    }
}
