namespace SocketServer
{
    partial class SocketServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SocketServer));
            this.grbSettings = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.lblServerPort = new System.Windows.Forms.Label();
            this.grbReceive = new System.Windows.Forms.GroupBox();
            this.txtDataReceive = new System.Windows.Forms.TextBox();
            this.grbSettings.SuspendLayout();
            this.grbReceive.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbSettings
            // 
            this.grbSettings.Controls.Add(this.btnStop);
            this.grbSettings.Controls.Add(this.txtServerIP);
            this.grbSettings.Controls.Add(this.lblServerIP);
            this.grbSettings.Controls.Add(this.btnStart);
            this.grbSettings.Controls.Add(this.txtServerPort);
            this.grbSettings.Controls.Add(this.lblServerPort);
            this.grbSettings.Location = new System.Drawing.Point(21, 21);
            this.grbSettings.Name = "grbSettings";
            this.grbSettings.Size = new System.Drawing.Size(492, 154);
            this.grbSettings.TabIndex = 0;
            this.grbSettings.TabStop = false;
            this.grbSettings.Text = "Settings";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(249, 109);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(179, 39);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop Server";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(111, 31);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(236, 26);
            this.txtServerIP.TabIndex = 2;
            this.txtServerIP.Text = "192.168.0.116";
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.Location = new System.Drawing.Point(17, 37);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(78, 20);
            this.lblServerIP.TabIndex = 1;
            this.lblServerIP.Text = "Server IP:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(21, 109);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(179, 39);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start Listening";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(125, 70);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(75, 26);
            this.txtServerPort.TabIndex = 4;
            this.txtServerPort.Text = "8221";
            // 
            // lblServerPort
            // 
            this.lblServerPort.AutoSize = true;
            this.lblServerPort.Location = new System.Drawing.Point(17, 76);
            this.lblServerPort.Name = "lblServerPort";
            this.lblServerPort.Size = new System.Drawing.Size(102, 20);
            this.lblServerPort.TabIndex = 3;
            this.lblServerPort.Text = "Port Number:";
            // 
            // grbReceive
            // 
            this.grbReceive.Controls.Add(this.txtDataReceive);
            this.grbReceive.Location = new System.Drawing.Point(21, 203);
            this.grbReceive.Name = "grbReceive";
            this.grbReceive.Size = new System.Drawing.Size(492, 167);
            this.grbReceive.TabIndex = 7;
            this.grbReceive.TabStop = false;
            this.grbReceive.Text = "Log Client Connected";
            // 
            // txtDataReceive
            // 
            this.txtDataReceive.Location = new System.Drawing.Point(21, 34);
            this.txtDataReceive.Multiline = true;
            this.txtDataReceive.Name = "txtDataReceive";
            this.txtDataReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDataReceive.Size = new System.Drawing.Size(454, 117);
            this.txtDataReceive.TabIndex = 8;
            // 
            // SocketServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 388);
            this.Controls.Add(this.grbReceive);
            this.Controls.Add(this.grbSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SocketServer";
            this.Text = "Socket Server";
            this.grbSettings.ResumeLayout(false);
            this.grbSettings.PerformLayout();
            this.grbReceive.ResumeLayout(false);
            this.grbReceive.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbSettings;
        private System.Windows.Forms.Label lblServerPort;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox grbReceive;
        private System.Windows.Forms.TextBox txtDataReceive;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.Button btnStop;
    }
}

