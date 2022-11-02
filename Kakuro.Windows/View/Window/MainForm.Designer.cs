namespace Kakuro.Windows
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mainCanvas = new System.Windows.Forms.PictureBox();
            this.timeLabel = new System.Windows.Forms.Label();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.checkPointsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newGameButton = new System.Windows.Forms.ToolStripButton();
            this.restartButton = new System.Windows.Forms.ToolStripButton();
            this.checkButton = new System.Windows.Forms.ToolStripButton();
            this.solveButton = new System.Windows.Forms.ToolStripButton();
            this.settingsButton = new System.Windows.Forms.ToolStripButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.numberPanelBox = new System.Windows.Forms.Panel();
            this.numberPanel = new System.Windows.Forms.Panel();
            this.button0 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainCanvas)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.numberPanelBox.SuspendLayout();
            this.numberPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.timeLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(593, 397);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mainCanvas);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(8);
            this.panel2.Size = new System.Drawing.Size(593, 373);
            this.panel2.TabIndex = 1;
            // 
            // mainCanvas
            // 
            this.mainCanvas.BackColor = System.Drawing.SystemColors.WindowText;
            this.mainCanvas.Location = new System.Drawing.Point(8, 8);
            this.mainCanvas.Name = "mainCanvas";
            this.mainCanvas.Size = new System.Drawing.Size(336, 336);
            this.mainCanvas.TabIndex = 2;
            this.mainCanvas.TabStop = false;
            // 
            // timeLabel
            // 
            this.timeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.timeLabel.Location = new System.Drawing.Point(0, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(593, 24);
            this.timeLabel.TabIndex = 0;
            this.timeLabel.Text = "00:00:00";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.Window;
            this.toolStrip.Enabled = false;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkPointsButton,
            this.toolStripSeparator1,
            this.newGameButton,
            this.restartButton,
            this.checkButton,
            this.solveButton,
            this.settingsButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(593, 27);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // checkPointsButton
            // 
            this.checkPointsButton.Image = global::Kakuro.Windows.Properties.Resources.flag_flyaway_pointed;
            this.checkPointsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.checkPointsButton.Name = "checkPointsButton";
            this.checkPointsButton.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.checkPointsButton.Size = new System.Drawing.Size(101, 24);
            this.checkPointsButton.Text = "Checkpoints";
            this.checkPointsButton.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // newGameButton
            // 
            this.newGameButton.Image = global::Kakuro.Windows.Properties.Resources.page_white;
            this.newGameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.newGameButton.Size = new System.Drawing.Size(92, 24);
            this.newGameButton.Text = "New game";
            this.newGameButton.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // restartButton
            // 
            this.restartButton.Image = global::Kakuro.Windows.Properties.Resources.arrow_refresh_small;
            this.restartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.restartButton.Name = "restartButton";
            this.restartButton.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.restartButton.Size = new System.Drawing.Size(71, 24);
            this.restartButton.Text = "Restart";
            this.restartButton.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // checkButton
            // 
            this.checkButton.Image = global::Kakuro.Windows.Properties.Resources.arrow_right;
            this.checkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.checkButton.Name = "checkButton";
            this.checkButton.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.checkButton.Size = new System.Drawing.Size(71, 24);
            this.checkButton.Text = "Check ";
            this.checkButton.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // solveButton
            // 
            this.solveButton.Image = global::Kakuro.Windows.Properties.Resources.ruby;
            this.solveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.solveButton.Name = "solveButton";
            this.solveButton.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.solveButton.Size = new System.Drawing.Size(63, 24);
            this.solveButton.Text = "Solve";
            this.solveButton.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.settingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsButton.Image = global::Kakuro.Windows.Properties.Resources.setting_tools;
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.settingsButton.Size = new System.Drawing.Size(28, 24);
            this.settingsButton.Text = "Settings";
            this.settingsButton.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // numberPanelBox
            // 
            this.numberPanelBox.Controls.Add(this.numberPanel);
            this.numberPanelBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.numberPanelBox.Location = new System.Drawing.Point(0, 424);
            this.numberPanelBox.Name = "numberPanelBox";
            this.numberPanelBox.Size = new System.Drawing.Size(593, 74);
            this.numberPanelBox.TabIndex = 4;
            // 
            // numberPanel
            // 
            this.numberPanel.Controls.Add(this.button0);
            this.numberPanel.Controls.Add(this.button9);
            this.numberPanel.Controls.Add(this.button8);
            this.numberPanel.Controls.Add(this.button7);
            this.numberPanel.Controls.Add(this.button6);
            this.numberPanel.Controls.Add(this.button5);
            this.numberPanel.Controls.Add(this.button4);
            this.numberPanel.Controls.Add(this.button3);
            this.numberPanel.Controls.Add(this.button2);
            this.numberPanel.Controls.Add(this.button1);
            this.numberPanel.Location = new System.Drawing.Point(8, 8);
            this.numberPanel.Name = "numberPanel";
            this.numberPanel.Size = new System.Drawing.Size(576, 56);
            this.numberPanel.TabIndex = 0;
            // 
            // button0
            // 
            this.button0.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button0.Location = new System.Drawing.Point(516, 8);
            this.button0.Name = "button0";
            this.button0.Size = new System.Drawing.Size(48, 40);
            this.button0.TabIndex = 1;
            this.button0.Text = "0";
            this.button0.UseVisualStyleBackColor = true;
            this.button0.Click += new System.EventHandler(this.button_Click);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button9.Location = new System.Drawing.Point(460, 8);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(48, 40);
            this.button9.TabIndex = 2;
            this.button9.Text = "9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button8.Location = new System.Drawing.Point(404, 8);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(48, 40);
            this.button8.TabIndex = 3;
            this.button8.Text = "8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button7.Location = new System.Drawing.Point(348, 8);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(48, 40);
            this.button7.TabIndex = 4;
            this.button7.Text = "7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button6.Location = new System.Drawing.Point(292, 8);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(48, 40);
            this.button6.TabIndex = 5;
            this.button6.Text = "6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button5.Location = new System.Drawing.Point(236, 8);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(48, 40);
            this.button5.TabIndex = 6;
            this.button5.Text = "5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button4.Location = new System.Drawing.Point(180, 8);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(48, 40);
            this.button4.TabIndex = 7;
            this.button4.Text = "4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.Location = new System.Drawing.Point(124, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 40);
            this.button3.TabIndex = 8;
            this.button3.Text = "3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(68, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 40);
            this.button2.TabIndex = 9;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(12, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 40);
            this.button1.TabIndex = 10;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 498);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.numberPanelBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(609, 537);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kakuro";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainCanvas)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.numberPanelBox.ResumeLayout(false);
            this.numberPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private ToolStrip toolStrip;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton newGameButton;
        private ToolStripButton restartButton;
        private ToolStripButton checkButton;
        private ToolStripButton settingsButton;
        private Panel panel2;
        private PictureBox mainCanvas;
        private Label timeLabel;
        private ToolStripButton solveButton;
        private System.Windows.Forms.Timer timer;
        private Panel numberPanelBox;
        private Panel numberPanel;
        private Button button0;
        private Button button9;
        private Button button8;
        private Button button7;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private ToolStripButton checkPointsButton;
    }
}