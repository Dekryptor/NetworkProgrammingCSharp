using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net.Sockets ;
namespace Project01
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		
  		Socket m_socClient;
   		private System.Windows.Forms.Button cmdSendData;
		private System.Windows.Forms.TextBox txtData;
		private System.Windows.Forms.TextBox txtDataRx;
		private System.Windows.Forms.Button cmdReceiveData;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtIPAddress;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.Button cmdConnect;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.GroupBox groupBox2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.cmdConnect = new System.Windows.Forms.Button();
            this.cmdSendData = new System.Windows.Forms.Button();
            this.txtData = new System.Windows.Forms.TextBox();
            this.txtDataRx = new System.Windows.Forms.TextBox();
            this.cmdReceiveData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdConnect
            // 
            this.cmdConnect.Location = new System.Drawing.Point(102, 105);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(154, 35);
            this.cmdConnect.TabIndex = 0;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // cmdSendData
            // 
            this.cmdSendData.Location = new System.Drawing.Point(576, 187);
            this.cmdSendData.Name = "cmdSendData";
            this.cmdSendData.Size = new System.Drawing.Size(90, 82);
            this.cmdSendData.TabIndex = 2;
            this.cmdSendData.Text = "Tx";
            this.cmdSendData.Click += new System.EventHandler(this.cmdSendData_Click);
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(13, 187);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(550, 82);
            this.txtData.TabIndex = 3;
            // 
            // txtDataRx
            // 
            this.txtDataRx.Enabled = false;
            this.txtDataRx.Location = new System.Drawing.Point(13, 292);
            this.txtDataRx.Multiline = true;
            this.txtDataRx.Name = "txtDataRx";
            this.txtDataRx.Size = new System.Drawing.Size(550, 82);
            this.txtDataRx.TabIndex = 4;
            // 
            // cmdReceiveData
            // 
            this.cmdReceiveData.Location = new System.Drawing.Point(576, 292);
            this.cmdReceiveData.Name = "cmdReceiveData";
            this.cmdReceiveData.Size = new System.Drawing.Size(90, 82);
            this.cmdReceiveData.TabIndex = 5;
            this.cmdReceiveData.Text = "Rx";
            this.cmdReceiveData.Click += new System.EventHandler(this.cmdReceiveData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdClose);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtIPAddress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmdConnect);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(665, 152);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(282, 105);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(192, 35);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(205, 70);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(192, 26);
            this.txtPort.TabIndex = 4;
            this.txtPort.Text = "8221";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(115, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port :";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(205, 23);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(397, 26);
            this.txtIPAddress.TabIndex = 2;
            this.txtIPAddress.Text = "192.168.0.116";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(26, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Host I.P. Address:";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(678, 222);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(796, 520);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdReceiveData);
            this.Controls.Add(this.txtDataRx);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.cmdSendData);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void cmdConnect_Click(object sender, System.EventArgs e)
		{
			try
			{
				//create a new client socket ...
				m_socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				String szIPSelected  = txtIPAddress.Text;
				String szPort = txtPort.Text;
				int  alPort = System.Convert.ToInt16 (szPort,10);
			
				System.Net.IPAddress	remoteIPAddress	 = System.Net.IPAddress.Parse(szIPSelected);
				System.Net.IPEndPoint	remoteEndPoint = new System.Net.IPEndPoint(remoteIPAddress, alPort);
				m_socClient.Connect(remoteEndPoint);
				String szData  = "Hello There";
				byte[] byData = System.Text.Encoding.ASCII.GetBytes(szData);
				m_socClient.Send(byData);

			}
			catch (SocketException se)
			{
				MessageBox.Show ( se.Message );
			}
  		}
		 

		private void cmdSendData_Click(object sender, System.EventArgs e)
		{
			try
			{
				Object objData = txtData.Text;
				byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString ());
				m_socClient.Send (byData);
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}
		}

		private void cmdReceiveData_Click(object sender, System.EventArgs e)
		{
			try
			{
				byte [] buffer = new byte[1024];
				int iRx = m_socClient.Receive (buffer);
				char[] chars = new char[iRx];

				System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
				int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
				System.String szData = new System.String(chars);
				txtDataRx.Text = szData;
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}
		}

		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			m_socClient.Close ();
		}

		

 	}
}
