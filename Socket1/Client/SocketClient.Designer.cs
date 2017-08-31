namespace SocketClient
{
    partial class SocketClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SocketClient));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIPServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnReceiveData = new System.Windows.Forms.Button();
            this.txtDataSend = new System.Windows.Forms.TextBox();
            this.btnSendData = new System.Windows.Forms.Button();
            this.dgvReceived = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.editRow = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceived)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.txtServerPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtIPServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 108);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(612, 56);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(149, 35);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "&Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(167, 60);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(60, 26);
            this.txtServerPort.TabIndex = 4;
            this.txtServerPort.Text = "8221";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port :";
            // 
            // txtIPServer
            // 
            this.txtIPServer.Location = new System.Drawing.Point(167, 23);
            this.txtIPServer.Name = "txtIPServer";
            this.txtIPServer.Size = new System.Drawing.Size(247, 26);
            this.txtIPServer.TabIndex = 2;
            this.txtIPServer.Text = "192.168.0.116";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Address:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(612, 19);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(149, 35);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnReceiveData
            // 
            this.btnReceiveData.Location = new System.Drawing.Point(508, 237);
            this.btnReceiveData.Name = "btnReceiveData";
            this.btnReceiveData.Size = new System.Drawing.Size(174, 45);
            this.btnReceiveData.TabIndex = 9;
            this.btnReceiveData.Text = "&Get Data from Server";
            this.btnReceiveData.Click += new System.EventHandler(this.btnReceiveData_Click);
            // 
            // txtDataSend
            // 
            this.txtDataSend.Location = new System.Drawing.Point(12, 138);
            this.txtDataSend.Multiline = true;
            this.txtDataSend.Name = "txtDataSend";
            this.txtDataSend.Size = new System.Drawing.Size(833, 82);
            this.txtDataSend.TabIndex = 7;
            // 
            // btnSendData
            // 
            this.btnSendData.Location = new System.Drawing.Point(122, 237);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(174, 45);
            this.btnSendData.TabIndex = 6;
            this.btnSendData.Text = "&Send Data to Server";
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // dgvReceived
            // 
            this.dgvReceived.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.dgvReceived.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReceived.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceived.ContextMenuStrip = this.contextMenuStrip;
            this.dgvReceived.Location = new System.Drawing.Point(-2, 303);
            this.dgvReceived.MultiSelect = false;
            this.dgvReceived.Name = "dgvReceived";
            this.dgvReceived.RowTemplate.Height = 28;
            this.dgvReceived.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReceived.Size = new System.Drawing.Size(861, 482);
            this.dgvReceived.TabIndex = 9;
            this.dgvReceived.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceived_CellClick);
            this.dgvReceived.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvReceived_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRow,
            this.editRow});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(199, 97);
            // 
            // deleteRow
            // 
            this.deleteRow.Name = "deleteRow";
            this.deleteRow.Size = new System.Drawing.Size(198, 30);
            this.deleteRow.Text = "&Delete Row";
            this.deleteRow.Click += new System.EventHandler(this.deleteRow_Click);
            // 
            // editRow
            // 
            this.editRow.Name = "editRow";
            this.editRow.Size = new System.Drawing.Size(198, 30);
            this.editRow.Text = "&Edit Row";
            this.editRow.Click += new System.EventHandler(this.editRow_Click);
            // 
            // SocketClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 784);
            this.Controls.Add(this.btnReceiveData);
            this.Controls.Add(this.dgvReceived);
            this.Controls.Add(this.btnSendData);
            this.Controls.Add(this.txtDataSend);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SocketClient";
            this.Text = "Socket Client";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceived)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIPServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnReceiveData;
        private System.Windows.Forms.TextBox txtDataSend;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.DataGridView dgvReceived;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteRow;
        private System.Windows.Forms.ToolStripMenuItem editRow;
    }
}

