﻿namespace Client
{
     partial class Client
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
               this.btnConnect = new System.Windows.Forms.Button();
               this.label2 = new System.Windows.Forms.Label();
               this.label1 = new System.Windows.Forms.Label();
               this.txtPort = new System.Windows.Forms.TextBox();
               this.txtHost = new System.Windows.Forms.TextBox();
               this.txtMessage = new System.Windows.Forms.TextBox();
               this.btnSend = new System.Windows.Forms.Button();
               this.txtStatus = new System.Windows.Forms.TextBox();
               this.SuspendLayout();
               // 
               // btnConnect
               // 
               this.btnConnect.Location = new System.Drawing.Point(352, 6);
               this.btnConnect.Name = "btnConnect";
               this.btnConnect.Size = new System.Drawing.Size(75, 23);
               this.btnConnect.TabIndex = 9;
               this.btnConnect.Text = "Connect";
               this.btnConnect.UseVisualStyleBackColor = true;
               this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
               // 
               // label2
               // 
               this.label2.AutoSize = true;
               this.label2.Location = new System.Drawing.Point(180, 9);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(38, 17);
               this.label2.TabIndex = 8;
               this.label2.Text = "Port:";
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Location = new System.Drawing.Point(12, 9);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(41, 17);
               this.label1.TabIndex = 7;
               this.label1.Text = "Host:";
               // 
               // txtPort
               // 
               this.txtPort.Location = new System.Drawing.Point(224, 6);
               this.txtPort.Name = "txtPort";
               this.txtPort.Size = new System.Drawing.Size(100, 22);
               this.txtPort.TabIndex = 6;
               this.txtPort.Text = "8910";
               // 
               // txtHost
               // 
               this.txtHost.Location = new System.Drawing.Point(59, 6);
               this.txtHost.Name = "txtHost";
               this.txtHost.Size = new System.Drawing.Size(100, 22);
               this.txtHost.TabIndex = 5;
               this.txtHost.Text = "127.0.0.1";
               // 
               // txtMessage
               // 
               this.txtMessage.Location = new System.Drawing.Point(15, 50);
               this.txtMessage.Name = "txtMessage";
               this.txtMessage.Size = new System.Drawing.Size(309, 22);
               this.txtMessage.TabIndex = 10;
               // 
               // btnSend
               // 
               this.btnSend.Enabled = false;
               this.btnSend.Location = new System.Drawing.Point(352, 50);
               this.btnSend.Name = "btnSend";
               this.btnSend.Size = new System.Drawing.Size(75, 23);
               this.btnSend.TabIndex = 11;
               this.btnSend.Text = "Send";
               this.btnSend.UseVisualStyleBackColor = true;
               this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
               // 
               // txtStatus
               // 
               this.txtStatus.Location = new System.Drawing.Point(15, 94);
               this.txtStatus.Multiline = true;
               this.txtStatus.Name = "txtStatus";
               this.txtStatus.Size = new System.Drawing.Size(412, 302);
               this.txtStatus.TabIndex = 12;
               // 
               // Client
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(440, 407);
               this.Controls.Add(this.txtStatus);
               this.Controls.Add(this.btnSend);
               this.Controls.Add(this.txtMessage);
               this.Controls.Add(this.btnConnect);
               this.Controls.Add(this.label2);
               this.Controls.Add(this.label1);
               this.Controls.Add(this.txtPort);
               this.Controls.Add(this.txtHost);
               this.Name = "Client";
               this.Text = "Client";
               this.ResumeLayout(false);
               this.PerformLayout();

          }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtStatus;
    }
}

