namespace Kakuro.Windows.View.Dialog
{
    partial class CheckpointDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckpointDialog));
            this.checkPointList = new System.Windows.Forms.ListBox();
            this.newButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkPointList
            // 
            this.checkPointList.FormattingEnabled = true;
            this.checkPointList.ItemHeight = 15;
            this.checkPointList.Location = new System.Drawing.Point(8, 40);
            this.checkPointList.Name = "checkPointList";
            this.checkPointList.Size = new System.Drawing.Size(312, 244);
            this.checkPointList.TabIndex = 0;
            // 
            // newButton
            // 
            this.newButton.Image = global::Kakuro.Windows.Properties.Resources.page_white;
            this.newButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newButton.Location = new System.Drawing.Point(8, 8);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(96, 23);
            this.newButton.TabIndex = 1;
            this.newButton.Text = "New";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // loadButton
            // 
            this.loadButton.Image = global::Kakuro.Windows.Properties.Resources.flag_flyaway_pointed;
            this.loadButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loadButton.Location = new System.Drawing.Point(224, 8);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(96, 23);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Image = global::Kakuro.Windows.Properties.Resources.bin_closed;
            this.deleteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.deleteButton.Location = new System.Drawing.Point(112, 8);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(104, 23);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // CheckpointDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 295);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.newButton);
            this.Controls.Add(this.checkPointList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckpointDialog";
            this.Text = "Checkpoints";
            this.Load += new System.EventHandler(this.CheckpointDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox checkPointList;
        private Button newButton;
        private Button loadButton;
        private Button deleteButton;
    }
}