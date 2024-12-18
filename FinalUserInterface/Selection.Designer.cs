﻿namespace FinalUserInterface
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
            btnIncidents = new Button();
            btnCompanies = new Button();
            btnRailroads = new Button();
            btnUser = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnIncidents
            // 
            btnIncidents.BackColor = SystemColors.ControlLight;
            btnIncidents.Location = new Point(114, 77);
            btnIncidents.Name = "btnIncidents";
            btnIncidents.Size = new Size(175, 100);
            btnIncidents.TabIndex = 0;
            btnIncidents.Text = "Incidents";
            btnIncidents.UseVisualStyleBackColor = false;
            btnIncidents.Click += button1_Click;
            // 
            // btnCompanies
            // 
            btnCompanies.Location = new Point(454, 77);
            btnCompanies.Name = "btnCompanies";
            btnCompanies.Size = new Size(175, 100);
            btnCompanies.TabIndex = 1;
            btnCompanies.Text = "Companies";
            btnCompanies.UseVisualStyleBackColor = true;
            btnCompanies.Click += buttonCompanies_Click;
            // 
            // btnRailroads
            // 
            btnRailroads.Location = new Point(114, 266);
            btnRailroads.Name = "btnRailroads";
            btnRailroads.Size = new Size(175, 100);
            btnRailroads.TabIndex = 2;
            btnRailroads.Text = "Railroads";
            btnRailroads.UseVisualStyleBackColor = true;
            btnRailroads.Click += buttonRailroads_Click;
            // 
            // btnUser
            // 
            btnUser.Location = new Point(454, 266);
            btnUser.Name = "btnUser";
            btnUser.Size = new Size(175, 100);
            btnUser.TabIndex = 3;
            btnUser.Text = "User Maintenance";
            btnUser.UseVisualStyleBackColor = true;
            btnUser.Click += buttonUser_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 30);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 4;
            label1.Text = "label1";
            // 
            // Selection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(btnUser);
            Controls.Add(btnRailroads);
            Controls.Add(btnCompanies);
            Controls.Add(btnIncidents);
            ForeColor = SystemColors.ControlText;
            Name = "Selection";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnIncidents;
        private Button btnCompanies;
        private Button btnRailroads;
        private Button btnUser;
        private Label label1;
    }
}