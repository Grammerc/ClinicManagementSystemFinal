namespace ClinicManagementSystemFinal.UserControls_Doctors.Clinics
{
    partial class AddClinic
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
            lblName = new Label();
            label1 = new Label();
            gunaAreaDataset1 = new Guna.Charts.WinForms.GunaAreaDataset();
            btnAdd = new Button();
            tbxSearch = new TextBox();
            tbxAddress = new TextBox();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblName.Location = new Point(31, 6);
            lblName.Name = "lblName";
            lblName.Size = new Size(90, 16);
            lblName.TabIndex = 5;
            lblName.Text = "Clinic Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(31, 62);
            label1.Name = "label1";
            label1.Size = new Size(66, 16);
            label1.TabIndex = 6;
            label1.Text = "Location";
            // 
            // gunaAreaDataset1
            // 
            gunaAreaDataset1.BorderColor = Color.Empty;
            gunaAreaDataset1.FillColor = Color.Empty;
            gunaAreaDataset1.Label = "Area1";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(322, 42);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Add Clinic";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // tbxSearch
            // 
            tbxSearch.Location = new Point(31, 25);
            tbxSearch.Name = "tbxSearch";
            tbxSearch.Size = new Size(259, 23);
            tbxSearch.TabIndex = 8;
            // 
            // tbxAddress
            // 
            tbxAddress.Location = new Point(31, 81);
            tbxAddress.Name = "tbxAddress";
            tbxAddress.Size = new Size(259, 23);
            tbxAddress.TabIndex = 9;
            // 
            // AddClinic
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tbxAddress);
            Controls.Add(tbxSearch);
            Controls.Add(btnAdd);
            Controls.Add(label1);
            Controls.Add(lblName);
            Name = "AddClinic";
            Size = new Size(416, 130);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnAdd;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Label lblName;
        private Label label1;
        private Guna.Charts.WinForms.GunaAreaDataset gunaAreaDataset1;
        private TextBox tbxSearch;
        private TextBox textBox1;
        private TextBox tbxAddress;
    }
}
