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
            ((System.ComponentModel.ISupportInitialize)pbxProfile).BeginInit();
            SuspendLayout();
            // 
            // pbxProfile
            // 
            pbxProfile.Location = new Point(65, 14);
            pbxProfile.Name = "pbxProfile";
            pbxProfile.Size = new Size(127, 111);
            pbxProfile.TabIndex = 0;
            pbxProfile.TabStop = false;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(65, 138);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 1;
            lblName.Text = "Name";
            // 
            // lblSpec
            // 
            lblSpec.AutoSize = true;
            lblSpec.Location = new Point(65, 171);
            lblSpec.Name = "lblSpec";
            lblSpec.Size = new Size(79, 15);
            lblSpec.TabIndex = 2;
            lblSpec.Text = "Specialization";
            // 
            // listClinics
            // 
            listClinics.FormattingEnabled = true;
            listClinics.ItemHeight = 15;
            listClinics.Location = new Point(65, 218);
            listClinics.Name = "listClinics";
            listClinics.Size = new Size(141, 124);
            listClinics.TabIndex = 3;
            // 
            // DoctorDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(listClinics);
            Controls.Add(lblSpec);
            Controls.Add(lblName);
            Controls.Add(pbxProfile);
            Name = "DoctorDetail";
            Size = new Size(270, 345);
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
    }
}
