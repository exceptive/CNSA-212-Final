namespace FinalUserInterface
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxPassword = new TextBox();
            textBoxUsername = new TextBox();
            label1 = new Label();
            label2 = new Label();
            buttonLogin = new Button();
            SuspendLayout();
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(316, 216);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(100, 23);
            textBoxPassword.TabIndex = 0;
            textBoxPassword.UseSystemPasswordChar = true;
            // 
            // textBoxUsername
            // 
            textBoxUsername.Location = new Point(316, 122);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(100, 23);
            textBoxUsername.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(316, 77);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 2;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(319, 184);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 3;
            label2.Text = "Password";
            label2.Click += label2_Click;
            // 
            // buttonLogin
            // 
            buttonLogin.Location = new Point(328, 268);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(75, 23);
            buttonLogin.TabIndex = 4;
            buttonLogin.Text = "Login";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonLogin);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxUsername);
            Controls.Add(textBoxPassword);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxPassword;
        private TextBox textBoxUsername;
        private Label label1;
        private Label label2;
        private Button buttonLogin;
    }
}
