namespace RobotSwarmServer
{
    partial class Communication
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelSendStatus = new System.Windows.Forms.Label();
            this.buttonOpenClose = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.lstPorts = new System.Windows.Forms.ListBox();
            this.tickRateBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelSendStatus
            // 
            this.labelSendStatus.AutoSize = true;
            this.labelSendStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSendStatus.Location = new System.Drawing.Point(1, 143);
            this.labelSendStatus.Name = "labelSendStatus";
            this.labelSendStatus.Size = new System.Drawing.Size(0, 13);
            this.labelSendStatus.TabIndex = 54;
            // 
            // buttonOpenClose
            // 
            this.buttonOpenClose.Enabled = false;
            this.buttonOpenClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.buttonOpenClose.Location = new System.Drawing.Point(96, 106);
            this.buttonOpenClose.Name = "buttonOpenClose";
            this.buttonOpenClose.Size = new System.Drawing.Size(115, 25);
            this.buttonOpenClose.TabIndex = 25;
            this.buttonOpenClose.Text = "Start communication";
            this.buttonOpenClose.UseVisualStyleBackColor = true;
            this.buttonOpenClose.Click += new System.EventHandler(this.OpenPort);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.buttonRefresh.Location = new System.Drawing.Point(6, 106);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(84, 25);
            this.buttonRefresh.TabIndex = 23;
            this.buttonRefresh.Text = "Update List";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.RefreshPortList);
            // 
            // lstPorts
            // 
            this.lstPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lstPorts.FormattingEnabled = true;
            this.lstPorts.Location = new System.Drawing.Point(6, 57);
            this.lstPorts.Name = "lstPorts";
            this.lstPorts.Size = new System.Drawing.Size(205, 43);
            this.lstPorts.TabIndex = 22;
            this.lstPorts.SelectedIndexChanged += new System.EventHandler(this.EnableOpenButton);
            // 
            // tickRateBox
            // 
            this.tickRateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tickRateBox.Location = new System.Drawing.Point(157, 29);
            this.tickRateBox.Name = "tickRateBox";
            this.tickRateBox.Size = new System.Drawing.Size(54, 20);
            this.tickRateBox.TabIndex = 55;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(26, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "Desired update rate [ms]:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tickRateBox);
            this.groupBox1.Controls.Add(this.buttonOpenClose);
            this.groupBox1.Controls.Add(this.buttonRefresh);
            this.groupBox1.Controls.Add(this.lstPorts);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.MinimumSize = new System.Drawing.Size(268, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 140);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Communication settings";
            // 
            // Communication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelSendStatus);
            this.Name = "Communication";
            this.Size = new System.Drawing.Size(268, 140);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSendStatus;
        private System.Windows.Forms.Button buttonOpenClose;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.ListBox lstPorts;
        private System.Windows.Forms.TextBox tickRateBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
