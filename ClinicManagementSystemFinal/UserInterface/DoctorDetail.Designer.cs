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
            listClinics = new ListBox();
            label1 = new Label();
            label2 = new Label();
            lblJobTitle = new Label();
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
            // listClinics
            // 
            listClinics.Font = new Font("Lucida Sans", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listClinics.FormattingEnabled = true;
            listClinics.ItemHeight = 24;
            listClinics.Location = new Point(296, 167);
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
            label2.Location = new Point(297, 138);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 5;
            label2.Text = "List of Clinics";
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
            Controls.Add(lblJobTitle);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listClinics);
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
        private ListBox listClinics;
        private PictureBox pbxProfile;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Label label1;
        private Label label2;
        private Label lblJobTitle;
        private Label lblBigName;
    }
}
