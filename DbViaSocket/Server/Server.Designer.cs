namespace DbViaSocketServer
{
    partial class frmServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServer));
            this.btnStartServer = new System.Windows.Forms.Button();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btStopServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartServer
            // 
            this.btnStartServer.BackColor = System.Drawing.Color.LightGray;
            this.btnStartServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartServer.ForeColor = System.Drawing.Color.Black;
            this.btnStartServer.Location = new System.Drawing.Point(37, 128);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(143, 86);
            this.btnStartServer.TabIndex = 0;
            this.btnStartServer.Text = "&Start Server";
            this.btnStartServer.UseVisualStyleBackColor = false;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.Location = new System.Drawing.Point(33, 31);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(78, 20);
            this.lblServerIP.TabIndex = 1;
            this.lblServerIP.Text = "Server IP:";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(33, 76);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(42, 20);
            this.lblPort.TabIndex = 1;
            this.lblPort.Text = "Port:";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(127, 25);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.ReadOnly = true;
            this.txtServerIP.Size = new System.Drawing.Size(236, 26);
            this.txtServerIP.TabIndex = 2;
            this.txtServerIP.Text = "192.168.0.116";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(127, 70);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(74, 26);
            this.txtPort.TabIndex = 2;
            this.txtPort.Text = "8001";
            // 
            // btStopServer
            // 
            this.btStopServer.BackColor = System.Drawing.Color.LightGray;
            this.btStopServer.Enabled = false;
            this.btStopServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btStopServer.ForeColor = System.Drawing.Color.Black;
            this.btStopServer.Location = new System.Drawing.Point(219, 132);
            this.btStopServer.Name = "btStopServer";
            this.btStopServer.Size = new System.Drawing.Size(144, 82);
            this.btStopServer.TabIndex = 0;
            this.btStopServer.Text = "S&top Server";
            this.btStopServer.UseVisualStyleBackColor = false;
            this.btStopServer.Click += new System.EventHandler(this.btStopServer_Click);
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 247);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblServerIP);
            this.Controls.Add(this.btStopServer);
            this.Controls.Add(this.btnStartServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmServer";
            this.Text = "Socket Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btStopServer;
    }
}

