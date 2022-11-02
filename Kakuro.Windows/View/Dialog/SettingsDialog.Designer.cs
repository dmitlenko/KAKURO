namespace Kakuro.Windows.View.Dialog
{
    partial class SettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.difficultyDrop = new System.Windows.Forms.ComboBox();
            this.hideTimer = new System.Windows.Forms.CheckBox();
            this.showNumberButtons = new System.Windows.Forms.CheckBox();
            this.autoSubmit = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grayCompletedNumbers = new System.Windows.Forms.CheckBox();
            this.highlightCurrentClues = new System.Windows.Forms.CheckBox();
            this.highlightCurrentRowSum = new System.Windows.Forms.CheckBox();
            this.highlightWrongCells = new System.Windows.Forms.CheckBox();
            this.highlightWrongSums = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.blueForErrors = new System.Windows.Forms.CheckBox();
            this.save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.difficultyDrop);
            this.groupBox1.Controls.Add(this.hideTimer);
            this.groupBox1.Controls.Add(this.showNumberButtons);
            this.groupBox1.Controls.Add(this.autoSubmit);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Difficulty:";
            // 
            // difficultyDrop
            // 
            this.difficultyDrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.difficultyDrop.FormattingEnabled = true;
            this.difficultyDrop.Items.AddRange(new object[] {
            "Easy",
            "Medium",
            "Hard (long generation time)"});
            this.difficultyDrop.Location = new System.Drawing.Point(8, 112);
            this.difficultyDrop.Name = "difficultyDrop";
            this.difficultyDrop.Size = new System.Drawing.Size(192, 23);
            this.difficultyDrop.TabIndex = 1;
            // 
            // hideTimer
            // 
            this.hideTimer.AutoSize = true;
            this.hideTimer.Location = new System.Drawing.Point(8, 72);
            this.hideTimer.Name = "hideTimer";
            this.hideTimer.Size = new System.Drawing.Size(102, 19);
            this.hideTimer.TabIndex = 0;
            this.hideTimer.Text = "Hide the timer";
            this.hideTimer.UseVisualStyleBackColor = true;
            // 
            // showNumberButtons
            // 
            this.showNumberButtons.AutoSize = true;
            this.showNumberButtons.Location = new System.Drawing.Point(8, 48);
            this.showNumberButtons.Name = "showNumberButtons";
            this.showNumberButtons.Size = new System.Drawing.Size(144, 19);
            this.showNumberButtons.TabIndex = 0;
            this.showNumberButtons.Text = "Show number buttons";
            this.showNumberButtons.UseVisualStyleBackColor = true;
            // 
            // autoSubmit
            // 
            this.autoSubmit.AutoSize = true;
            this.autoSubmit.Location = new System.Drawing.Point(8, 24);
            this.autoSubmit.Name = "autoSubmit";
            this.autoSubmit.Size = new System.Drawing.Size(92, 19);
            this.autoSubmit.TabIndex = 0;
            this.autoSubmit.Text = "Auto submit";
            this.autoSubmit.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grayCompletedNumbers);
            this.groupBox2.Controls.Add(this.highlightCurrentClues);
            this.groupBox2.Controls.Add(this.highlightCurrentRowSum);
            this.groupBox2.Controls.Add(this.highlightWrongCells);
            this.groupBox2.Controls.Add(this.highlightWrongSums);
            this.groupBox2.Location = new System.Drawing.Point(224, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 152);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Help";
            // 
            // grayCompletedNumbers
            // 
            this.grayCompletedNumbers.AutoSize = true;
            this.grayCompletedNumbers.Location = new System.Drawing.Point(8, 120);
            this.grayCompletedNumbers.Name = "grayCompletedNumbers";
            this.grayCompletedNumbers.Size = new System.Drawing.Size(181, 19);
            this.grayCompletedNumbers.TabIndex = 0;
            this.grayCompletedNumbers.Text = "Gray out completed numbers";
            this.grayCompletedNumbers.UseVisualStyleBackColor = true;
            // 
            // highlightCurrentClues
            // 
            this.highlightCurrentClues.AutoSize = true;
            this.highlightCurrentClues.Location = new System.Drawing.Point(8, 96);
            this.highlightCurrentClues.Name = "highlightCurrentClues";
            this.highlightCurrentClues.Size = new System.Drawing.Size(147, 19);
            this.highlightCurrentClues.TabIndex = 0;
            this.highlightCurrentClues.Text = "Highlight current clues";
            this.highlightCurrentClues.UseVisualStyleBackColor = true;
            // 
            // highlightCurrentRowSum
            // 
            this.highlightCurrentRowSum.AutoSize = true;
            this.highlightCurrentRowSum.Location = new System.Drawing.Point(8, 72);
            this.highlightCurrentRowSum.Name = "highlightCurrentRowSum";
            this.highlightCurrentRowSum.Size = new System.Drawing.Size(207, 19);
            this.highlightCurrentRowSum.TabIndex = 0;
            this.highlightCurrentRowSum.Text = "Highlight current row and column";
            this.highlightCurrentRowSum.UseVisualStyleBackColor = true;
            // 
            // highlightWrongCells
            // 
            this.highlightWrongCells.AutoSize = true;
            this.highlightWrongCells.Location = new System.Drawing.Point(8, 48);
            this.highlightWrongCells.Name = "highlightWrongCells";
            this.highlightWrongCells.Size = new System.Drawing.Size(139, 19);
            this.highlightWrongCells.TabIndex = 0;
            this.highlightWrongCells.Text = "Highlight wrong cells";
            this.highlightWrongCells.UseVisualStyleBackColor = true;
            // 
            // highlightWrongSums
            // 
            this.highlightWrongSums.AutoSize = true;
            this.highlightWrongSums.Location = new System.Drawing.Point(8, 24);
            this.highlightWrongSums.Name = "highlightWrongSums";
            this.highlightWrongSums.Size = new System.Drawing.Size(144, 19);
            this.highlightWrongSums.TabIndex = 0;
            this.highlightWrongSums.Text = "Highlight wrong sums";
            this.highlightWrongSums.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.blueForErrors);
            this.groupBox3.Location = new System.Drawing.Point(8, 168);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(520, 56);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Appearance";
            // 
            // blueForErrors
            // 
            this.blueForErrors.AutoSize = true;
            this.blueForErrors.Location = new System.Drawing.Point(8, 24);
            this.blueForErrors.Name = "blueForErrors";
            this.blueForErrors.Size = new System.Drawing.Size(122, 19);
            this.blueForErrors.TabIndex = 0;
            this.blueForErrors.Text = "Use blue for errors";
            this.blueForErrors.UseVisualStyleBackColor = true;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(448, 232);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(80, 23);
            this.save.TabIndex = 1;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 264);
            this.Controls.Add(this.save);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox hideTimer;
        private CheckBox showNumberButtons;
        private CheckBox autoSubmit;
        private GroupBox groupBox2;
        private CheckBox grayCompletedNumbers;
        private CheckBox highlightCurrentClues;
        private CheckBox highlightCurrentRowSum;
        private CheckBox highlightWrongSums;
        private GroupBox groupBox3;
        private CheckBox blueForErrors;
        private Button save;
        private CheckBox highlightWrongCells;
        private Label label1;
        private ComboBox difficultyDrop;
    }
}