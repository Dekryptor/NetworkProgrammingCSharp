using System;
using System.Threading;
using System.Windows.Forms;
using SocketCommunication.Core;

namespace ServerHost
{
    public partial class FrmServer : Form
    {
        ServerTerminal m_serverTerminal;

        public FrmServer()
        {
            InitializeComponent();

        }

        private void cmdConnect_Click(object sender, System.EventArgs e)
        {
            try
            {
                listLog.Items.Clear();

                string szPort = txtPort.Text;
                int alPort = System.Convert.ToInt16(szPort, 10);

                createTerminal(alPort);

                cmdConnect.Enabled = false;
                cmdClose.Enabled = true;
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message);
            }

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            closeTerminal();

            cmdConnect.Enabled = true;
            cmdClose.Enabled = false;
        }

        void m_Terminal_ClientDisConnected(System.Net.Sockets.Socket socket)
        {
            PublishMessage(listLog, string.Format("Client {0} has been disconnected!", socket.LocalEndPoint));
        }

        void m_Terminal_ClientConnected(System.Net.Sockets.Socket socket)
        {
            PublishMessage(listLog, string.Format("Client {0} has been connected!", socket.LocalEndPoint));
        }

        void m_Terminal_MessageRecived(string message)
        {
            PublishMessage(listMessages, message);

            // Send Echo
            m_serverTerminal.SendMessage("Echo: " + message);
        }

        private void createTerminal(int alPort)
        {
            m_serverTerminal = new ServerTerminal();
            
            m_serverTerminal.MessageRecived += m_Terminal_MessageRecived;
            m_serverTerminal.ClientConnect += m_Terminal_ClientConnected;
            m_serverTerminal.ClientDisconnect += m_Terminal_ClientDisConnected;
            
            m_serverTerminal.StartListen(alPort);
        }

        private void closeTerminal()
        {
            m_serverTerminal.MessageRecived -= new TCPTerminal_MessageRecivedDel(m_Terminal_MessageRecived);
            m_serverTerminal.ClientConnect -= new TCPTerminal_ConnectDel(m_Terminal_ClientConnected);
            m_serverTerminal.ClientDisconnect -= new TCPTerminal_DisconnectDel(m_Terminal_ClientDisConnected);

            m_serverTerminal.Close();
        }

        private void PublishMessage(ListBox listBox, string mes)
        {
            if (InvokeRequired)
            {
                BeginInvoke((ThreadStart) delegate { PublishMessage(listBox, mes); });
                return;
            }

            listBox.Items.Add(mes);
        }

        private void m_btnNew_Click(object sender, EventArgs e)
        {
            new FrmServer().Show();
        }
    }
}