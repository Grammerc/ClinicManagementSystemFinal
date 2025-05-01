namespace ClinicManagementSystemFinal.UserInterface
{
    partial class DoctorDetail
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pbxProfile = new PictureBox();
            lblName = new Label();
            lblSpec = new Label();
            listClinics = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            lblJobTitle = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            lblBigName = new Label();
            ((System.ComponentModel.ISupportInitialize)pbxProfile).BeginInit();
            SuspendLayout();
            // 
            // pbxProfile
            // 
            pbxProfile.Location = new Point(12, 38);
            pbxProfile.Name = "pbxProfile";
            pbxProfile.Size = new Size(238, 253);
            pbxProfile.TabIndex = 0;
            pbxProfile.TabStop = false;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(296, 63);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 1;
            lblName.Text = "Name";
            // 
            // lblSpec
            // 
            lblSpec.AutoSize = true;
            lblSpec.Location = new Point(296, 231);
            lblSpec.Name = "lblSpec";
            lblSpec.Size = new Size(79, 15);
            lblSpec.TabIndex = 2;
            lblSpec.Text = "Specialization";
            // 
            // listClinics
            // 
            listClinics.Font = new Font("Lucida Sans", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listClinics.FormattingEnabled = true;
            listClinics.ItemHeight = 24;
            listClinics.Location = new Point(295, 322);
            listClinics.Name = "listClinics";
            listClinics.Size = new Size(348, 124);
            listClinics.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(296, 38);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 4;
            label1.Text = "Profile";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(296, 293);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 5;
            label2.Text = "List of Clinics";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(296, 139);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 6;
            label3.Text = "EXPERIENCE";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(296, 164);
            label4.Name = "label4";
            label4.Size = new Size(74, 15);
            label4.TabIndex = 7;
            label4.Text = "LANGUAGES";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(296, 192);
            label5.Name = "label5";
            label5.Size = new Size(58, 15);
            label5.TabIndex = 8;
            label5.Text = "TYPES OF";
            // 
            // lblJobTitle
            // 
            lblJobTitle.AutoSize = true;
            lblJobTitle.Location = new Point(297, 86);
            lblJobTitle.Name = "lblJobTitle";
            lblJobTitle.Size = new Size(51, 15);
            lblJobTitle.TabIndex = 9;
            lblJobTitle.Text = "Job Title";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(418, 139);
            label6.Name = "label6";
            label6.Size = new Size(19, 15);
            label6.TabIndex = 10;
            label6.Text = "00";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(418, 164);
            label7.Name = "label7";
            label7.Size = new Size(30, 15);
            label7.TabIndex = 11;
            label7.Text = "lang";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(418, 192);
            label8.Name = "label8";
            label8.Size = new Size(48, 15);
            label8.TabIndex = 12;
            label8.Text = "JobTitle";
            // 
            // lblBigName
            // 
            lblBigName.AutoSize = true;
            lblBigName.Location = new Point(12, 322);
            lblBigName.Name = "lblBigName";
            lblBigName.Size = new Size(69, 15);
            lblBigName.TabIndex = 13;
            lblBigName.Text = "lblBigName";
            // 
            // DoctorDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(671, 468);
            Controls.Add(lblBigName);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(lblJobTitle);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listClinics);
            Controls.Add(lblSpec);
            Controls.Add(lblName);
            Controls.Add(pbxProfile);
            Name = "DoctorDetail";
            ((System.ComponentModel.ISupportInitialize)pbxProfile).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbx;
        private Label lblName;
        private Label lblSpec;
        private ListBox listClinics;
        private PictureBox pbxProfile;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblJobTitle;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label lblBigName;
    }
}
