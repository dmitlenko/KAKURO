namespace KAKURO
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.boardHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.boardWidth = new System.Windows.Forms.NumericUpDown();
            this.saveButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.highlightSelectionSumsCheck = new System.Windows.Forms.CheckBox();
            this.highlightWrongSumsCheck = new System.Windows.Forms.CheckBox();
            this.highlightDuplicatesCheck = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.hideTimerCheck = new System.Windows.Forms.CheckBox();
            this.autoSubmitCheck = new System.Windows.Forms.CheckBox();
            this.grayCompleteSumsCheck = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boardHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.boardHeight);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.boardWidth);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Генератор";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Висота дошки:";
            // 
            // boardHeight
            // 
            this.boardHeight.Location = new System.Drawing.Point(8, 80);
            this.boardHeight.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.boardHeight.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.boardHeight.Name = "boardHeight";
            this.boardHeight.Size = new System.Drawing.Size(184, 20);
            this.boardHeight.TabIndex = 2;
            this.boardHeight.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ширина дошки:";
            // 
            // boardWidth
            // 
            this.boardWidth.Location = new System.Drawing.Point(8, 40);
            this.boardWidth.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.boardWidth.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.boardWidth.Name = "boardWidth";
            this.boardWidth.Size = new System.Drawing.Size(184, 20);
            this.boardWidth.TabIndex = 0;
            this.boardWidth.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(216, 280);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(200, 24);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Зберегти";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.autoSubmitCheck);
            this.groupBox2.Location = new System.Drawing.Point(8, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 176);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Розв\'язувач";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grayCompleteSumsCheck);
            this.groupBox3.Controls.Add(this.highlightDuplicatesCheck);
            this.groupBox3.Controls.Add(this.highlightWrongSumsCheck);
            this.groupBox3.Controls.Add(this.highlightSelectionSumsCheck);
            this.groupBox3.Location = new System.Drawing.Point(216, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 160);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Помічник";
            // 
            // highlightSelectionSumsCheck
            // 
            this.highlightSelectionSumsCheck.AutoSize = true;
            this.highlightSelectionSumsCheck.Location = new System.Drawing.Point(8, 24);
            this.highlightSelectionSumsCheck.Name = "highlightSelectionSumsCheck";
            this.highlightSelectionSumsCheck.Size = new System.Drawing.Size(187, 17);
            this.highlightSelectionSumsCheck.TabIndex = 0;
            this.highlightSelectionSumsCheck.Text = "Підсвічувати доступні підсказки";
            this.highlightSelectionSumsCheck.UseVisualStyleBackColor = true;
            // 
            // highlightWrongSumsCheck
            // 
            this.highlightWrongSumsCheck.AutoSize = true;
            this.highlightWrongSumsCheck.Location = new System.Drawing.Point(8, 48);
            this.highlightWrongSumsCheck.Name = "highlightWrongSumsCheck";
            this.highlightWrongSumsCheck.Size = new System.Drawing.Size(154, 17);
            this.highlightWrongSumsCheck.TabIndex = 1;
            this.highlightWrongSumsCheck.Text = "Підсвічувати невірні суми";
            this.highlightWrongSumsCheck.UseVisualStyleBackColor = true;
            // 
            // highlightDuplicatesCheck
            // 
            this.highlightDuplicatesCheck.AutoSize = true;
            this.highlightDuplicatesCheck.Location = new System.Drawing.Point(8, 72);
            this.highlightDuplicatesCheck.Name = "highlightDuplicatesCheck";
            this.highlightDuplicatesCheck.Size = new System.Drawing.Size(140, 17);
            this.highlightDuplicatesCheck.TabIndex = 2;
            this.highlightDuplicatesCheck.Text = "Підсвічувати дублікати";
            this.highlightDuplicatesCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.hideTimerCheck);
            this.groupBox4.Location = new System.Drawing.Point(216, 176);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 100);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Інтерфейс";
            // 
            // hideTimerCheck
            // 
            this.hideTimerCheck.AutoSize = true;
            this.hideTimerCheck.Location = new System.Drawing.Point(8, 24);
            this.hideTimerCheck.Name = "hideTimerCheck";
            this.hideTimerCheck.Size = new System.Drawing.Size(120, 17);
            this.hideTimerCheck.TabIndex = 0;
            this.hideTimerCheck.Text = "Приховати таймер";
            this.hideTimerCheck.UseVisualStyleBackColor = true;
            // 
            // autoSubmitCheck
            // 
            this.autoSubmitCheck.AutoSize = true;
            this.autoSubmitCheck.Location = new System.Drawing.Point(8, 24);
            this.autoSubmitCheck.Name = "autoSubmitCheck";
            this.autoSubmitCheck.Size = new System.Drawing.Size(103, 17);
            this.autoSubmitCheck.TabIndex = 4;
            this.autoSubmitCheck.Text = "Авто перевірка";
            this.autoSubmitCheck.UseVisualStyleBackColor = true;
            // 
            // grayCompleteSumsCheck
            // 
            this.grayCompleteSumsCheck.AutoSize = true;
            this.grayCompleteSumsCheck.Location = new System.Drawing.Point(8, 96);
            this.grayCompleteSumsCheck.Name = "grayCompleteSumsCheck";
            this.grayCompleteSumsCheck.Size = new System.Drawing.Size(186, 17);
            this.grayCompleteSumsCheck.TabIndex = 3;
            this.grayCompleteSumsCheck.Text = "Зробити правильні суми сірими";
            this.grayCompleteSumsCheck.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 312);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Налаштування";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boardHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown boardHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown boardWidth;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox highlightSelectionSumsCheck;
        private System.Windows.Forms.CheckBox highlightWrongSumsCheck;
        private System.Windows.Forms.CheckBox highlightDuplicatesCheck;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox hideTimerCheck;
        private System.Windows.Forms.CheckBox autoSubmitCheck;
        private System.Windows.Forms.CheckBox grayCompleteSumsCheck;
    }
}