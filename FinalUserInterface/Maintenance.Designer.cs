namespace FinalUserInterface
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
            textBox2 = new TextBox();
            labelNewPassword = new Label();
            buttonChange = new Button();
            SuspendLayout();
            // 
            // textBox2
            // 
            textBox2.Location = new Point(111, 180);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(192, 23);
            textBox2.TabIndex = 0;
            // 
            // labelNewPassword
            // 
            labelNewPassword.AutoSize = true;
            labelNewPassword.Font = new Font("Segoe UI", 15F);
            labelNewPassword.Location = new Point(141, 124);
            labelNewPassword.Name = "labelNewPassword";
            labelNewPassword.Size = new Size(137, 28);
            labelNewPassword.TabIndex = 1;
            labelNewPassword.Text = "New Password";
            // 
            // buttonChange
            // 
            buttonChange.Location = new Point(129, 247);
            buttonChange.Name = "buttonChange";
            buttonChange.Size = new Size(162, 43);
            buttonChange.TabIndex = 2;
            buttonChange.Text = "Change";
            buttonChange.UseVisualStyleBackColor = true;
            buttonChange.Click += buttonChange_Click;
            // 
            // Maintenance
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(426, 460);
            Controls.Add(buttonChange);
            Controls.Add(labelNewPassword);
            Controls.Add(textBox2);
            Name = "Maintenance";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox2;
        private Label labelNewPassword;
        private Button buttonChange;
    }
}