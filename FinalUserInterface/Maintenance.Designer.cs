﻿namespace FinalUserInterface
{
    partial class Maintenance
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
            textBoxNewPassword = new TextBox();
            labelNewPassword = new Label();
            buttonChange = new Button();
            SuspendLayout();
            // 
            // textBoxNewPassword
            // 
            textBoxNewPassword.Location = new Point(70, 93);
            textBoxNewPassword.Name = "textBoxNewPassword";
            textBoxNewPassword.Size = new Size(192, 23);
            textBoxNewPassword.TabIndex = 0;
            // 
            // labelNewPassword
            // 
            labelNewPassword.AutoSize = true;
            labelNewPassword.Location = new Point(70, 54);
            labelNewPassword.Name = "labelNewPassword";
            labelNewPassword.Size = new Size(84, 15);
            labelNewPassword.TabIndex = 1;
            labelNewPassword.Text = "New Password";
            // 
            // buttonChange
            // 
            buttonChange.Location = new Point(70, 148);
            buttonChange.Name = "buttonChange";
            buttonChange.Size = new Size(162, 43);
            buttonChange.TabIndex = 2;
            buttonChange.Text = "Change";
            buttonChange.UseVisualStyleBackColor = true;
            // 
            // Maintenance
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonChange);
            Controls.Add(labelNewPassword);
            Controls.Add(textBoxNewPassword);
            Name = "Maintenance";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxNewPassword;
        private Label labelNewPassword;
        private Button buttonChange;
    }
}