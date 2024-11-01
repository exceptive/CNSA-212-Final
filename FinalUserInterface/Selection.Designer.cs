namespace FinalUserInterface
{
    partial class Selection
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
            buttonIncidents = new Button();
            buttonCompanies = new Button();
            buttonRailroads = new Button();
            buttonUser = new Button();
            SuspendLayout();
            // 
            // buttonIncidents
            // 
            buttonIncidents.BackColor = SystemColors.ControlLight;
            buttonIncidents.Location = new Point(114, 53);
            buttonIncidents.Name = "buttonIncidents";
            buttonIncidents.Size = new Size(175, 100);
            buttonIncidents.TabIndex = 0;
            buttonIncidents.Text = "Incidents";
            buttonIncidents.UseVisualStyleBackColor = false;
            buttonIncidents.Click += button1_Click;
            // 
            // buttonCompanies
            // 
            buttonCompanies.Location = new Point(454, 53);
            buttonCompanies.Name = "buttonCompanies";
            buttonCompanies.Size = new Size(175, 100);
            buttonCompanies.TabIndex = 1;
            buttonCompanies.Text = "Companies";
            buttonCompanies.UseVisualStyleBackColor = true;
            // 
            // buttonRailroads
            // 
            buttonRailroads.Location = new Point(114, 246);
            buttonRailroads.Name = "buttonRailroads";
            buttonRailroads.Size = new Size(175, 100);
            buttonRailroads.TabIndex = 2;
            buttonRailroads.Text = "Railroads";
            buttonRailroads.UseVisualStyleBackColor = true;
            // 
            // buttonUser
            // 
            buttonUser.Location = new Point(454, 246);
            buttonUser.Name = "buttonUser";
            buttonUser.Size = new Size(175, 100);
            buttonUser.TabIndex = 3;
            buttonUser.Text = "User Maintenance";
            buttonUser.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonUser);
            Controls.Add(buttonRailroads);
            Controls.Add(buttonCompanies);
            Controls.Add(buttonIncidents);
            ForeColor = SystemColors.ControlText;
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonIncidents;
        private Button buttonCompanies;
        private Button buttonRailroads;
        private Button buttonUser;
    }
}