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
            pbx = new PictureBox();
            lblName = new Label();
            lblSpec = new Label();
            listClinics = new ListBox();
            ((System.ComponentModel.ISupportInitialize)pbx).BeginInit();
            SuspendLayout();
            // 
            // pbx
            // 
            pbx.Location = new Point(54, 22);
            pbx.Name = "pbx";
            pbx.Size = new Size(100, 50);
            pbx.TabIndex = 0;
            pbx.TabStop = false;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(54, 86);
            lblName.Name = "lblName";
            lblName.Size = new Size(38, 15);
            lblName.TabIndex = 1;
            lblName.Text = "label1";
            // 
            // lblSpec
            // 
            lblSpec.AutoSize = true;
            lblSpec.Location = new Point(54, 121);
            lblSpec.Name = "lblSpec";
            lblSpec.Size = new Size(38, 15);
            lblSpec.TabIndex = 2;
            lblSpec.Text = "label2";
            // 
            // listClinics
            // 
            listClinics.FormattingEnabled = true;
            listClinics.ItemHeight = 15;
            listClinics.Location = new Point(54, 150);
            listClinics.Name = "listClinics";
            listClinics.Size = new Size(120, 94);
            listClinics.TabIndex = 3;
            // 
            // DoctorDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(listClinics);
            Controls.Add(lblSpec);
            Controls.Add(lblName);
            Controls.Add(pbx);
            Name = "DoctorDetail";
            Size = new Size(217, 284);
            ((System.ComponentModel.ISupportInitialize)pbx).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbx;
        private Label lblName;
        private Label lblSpec;
        private ListBox listClinics;
    }
}
