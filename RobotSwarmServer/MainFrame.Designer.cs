namespace RobotSwarmServer
{
    partial class MainFrame
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cameraSettingsTab = new System.Windows.Forms.TabPage();
            this.simulationTab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.robotTable = new System.Windows.Forms.DataGridView();
            this.neighbours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.motorSignal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heading = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blocked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detected = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.robotID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.automataPanel = new System.Windows.Forms.Panel();
            this.strategyPanel = new System.Windows.Forms.Panel();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.simulationTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.robotTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // cameraSettingsTab
            // 
            this.cameraSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.cameraSettingsTab.Name = "cameraSettingsTab";
            this.cameraSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.cameraSettingsTab.Size = new System.Drawing.Size(1176, 735);
            this.cameraSettingsTab.TabIndex = 1;
            this.cameraSettingsTab.Text = "Camera Settings";
            this.cameraSettingsTab.UseVisualStyleBackColor = true;
            // 
            // simulationTab
            // 
            this.simulationTab.Controls.Add(this.splitContainer1);
            this.simulationTab.Location = new System.Drawing.Point(4, 22);
            this.simulationTab.Name = "simulationTab";
            this.simulationTab.Padding = new System.Windows.Forms.Padding(3);
            this.simulationTab.Size = new System.Drawing.Size(1176, 735);
            this.simulationTab.TabIndex = 0;
            this.simulationTab.Text = "Simulation";
            this.simulationTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.robotTable);
            this.splitContainer1.Size = new System.Drawing.Size(1170, 729);
            this.splitContainer1.SplitterDistance = 547;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            // 
            // robotTable
            // 
            this.robotTable.AllowUserToAddRows = false;
            this.robotTable.AllowUserToDeleteRows = false;
            this.robotTable.AllowUserToResizeRows = false;
            this.robotTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.robotTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.robotTable.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.robotTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.robotTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.robotTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.robotID,
            this.detected,
            this.blocked,
            this.position,
            this.heading,
            this.speed,
            this.motorSignal,
            this.neighbours});
            this.robotTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.robotTable.Location = new System.Drawing.Point(0, 0);
            this.robotTable.Name = "robotTable";
            this.robotTable.RowTemplate.ReadOnly = true;
            this.robotTable.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.robotTable.Size = new System.Drawing.Size(1170, 181);
            this.robotTable.TabIndex = 1;
            // 
            // neighbours
            // 
            this.neighbours.HeaderText = "Neighbours";
            this.neighbours.Name = "neighbours";
            this.neighbours.ReadOnly = true;
            // 
            // motorSignal
            // 
            this.motorSignal.HeaderText = "Motor Signal (%)";
            this.motorSignal.Name = "motorSignal";
            this.motorSignal.ReadOnly = true;
            // 
            // speed
            // 
            this.speed.HeaderText = "Speed";
            this.speed.Name = "speed";
            this.speed.ReadOnly = true;
            // 
            // heading
            // 
            this.heading.HeaderText = "Heading (deg)";
            this.heading.Name = "heading";
            this.heading.ReadOnly = true;
            // 
            // position
            // 
            this.position.HeaderText = "Position (X, Y)";
            this.position.Name = "position";
            this.position.ReadOnly = true;
            // 
            // blocked
            // 
            this.blocked.HeaderText = "Blocked";
            this.blocked.Name = "blocked";
            this.blocked.ReadOnly = true;
            // 
            // detected
            // 
            this.detected.HeaderText = "Detected";
            this.detected.Name = "detected";
            this.detected.ReadOnly = true;
            this.detected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // robotID
            // 
            this.robotID.HeaderText = "Robot ID";
            this.robotID.Name = "robotID";
            this.robotID.ReadOnly = true;
            this.robotID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.settingsPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(1170, 547);
            this.splitContainer2.SplitterDistance = 280;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.strategyPanel);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.automataPanel);
            this.splitContainer3.Size = new System.Drawing.Size(886, 547);
            this.splitContainer3.SplitterDistance = 280;
            this.splitContainer3.TabIndex = 0;
            // 
            // automataPanel
            // 
            this.automataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.automataPanel.Location = new System.Drawing.Point(0, 0);
            this.automataPanel.Name = "automataPanel";
            this.automataPanel.Size = new System.Drawing.Size(602, 547);
            this.automataPanel.TabIndex = 1;
            // 
            // strategyPanel
            // 
            this.strategyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.strategyPanel.Location = new System.Drawing.Point(0, 0);
            this.strategyPanel.Name = "strategyPanel";
            this.strategyPanel.Size = new System.Drawing.Size(280, 547);
            this.strategyPanel.TabIndex = 0;
            // 
            // settingsPanel
            // 
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPanel.Location = new System.Drawing.Point(0, 0);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(280, 547);
            this.settingsPanel.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.simulationTab);
            this.tabControl.Controls.Add(this.cameraSettingsTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1184, 761);
            this.tabControl.TabIndex = 1;
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new System.Drawing.Size(850, 640);
            this.Name = "MainFrame";
            this.Text = "Robot Swarm Server";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrame_FormClosed);
            this.simulationTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.robotTable)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        

        #endregion

        private System.Windows.Forms.TabPage cameraSettingsTab;
        private System.Windows.Forms.TabPage simulationTab;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel strategyPanel;
        private System.Windows.Forms.Panel automataPanel;
        private System.Windows.Forms.DataGridView robotTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn robotID;
        private System.Windows.Forms.DataGridViewTextBoxColumn detected;
        private System.Windows.Forms.DataGridViewTextBoxColumn blocked;
        private System.Windows.Forms.DataGridViewTextBoxColumn position;
        private System.Windows.Forms.DataGridViewTextBoxColumn heading;
        private System.Windows.Forms.DataGridViewTextBoxColumn speed;
        private System.Windows.Forms.DataGridViewTextBoxColumn motorSignal;
        private System.Windows.Forms.DataGridViewTextBoxColumn neighbours;
        private System.Windows.Forms.TabControl tabControl;



        //Settings Tab

        //Simulation Tab

    }
}

