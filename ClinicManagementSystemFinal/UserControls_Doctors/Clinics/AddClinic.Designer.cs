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
            btnSearch = new Button();
            tbxEmail = new TextBox();
            label2 = new Label();
            label3 = new Label();
            tbxVerificationCode = new TextBox();
            btnSendCode = new Button();
            btnVerifyCode = new Button();
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
            btnAdd.Location = new Point(316, 80);
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
            // btnSearch
            // 
            btnSearch.Location = new Point(316, 24);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 11;
            btnSearch.Text = "button1";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // tbxEmail
            // 
            tbxEmail.Location = new Point(31, 137);
            tbxEmail.Name = "tbxEmail";
            tbxEmail.Size = new Size(259, 23);
            tbxEmail.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(31, 118);
            label2.Name = "label2";
            label2.Size = new Size(46, 16);
            label2.TabIndex = 13;
            label2.Text = "Email";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(31, 190);
            label3.Name = "label3";
            label3.Size = new Size(126, 16);
            label3.TabIndex = 15;
            label3.Text = "Verification Code";
            // 
            // tbxVerificationCode
            // 
            tbxVerificationCode.Location = new Point(31, 209);
            tbxVerificationCode.Name = "tbxVerificationCode";
            tbxVerificationCode.Size = new Size(259, 23);
            tbxVerificationCode.TabIndex = 14;
            // 
            // btnSendCode
            // 
            btnSendCode.Location = new Point(316, 209);
            btnSendCode.Name = "btnSendCode";
            btnSendCode.Size = new Size(75, 23);
            btnSendCode.TabIndex = 16;
            btnSendCode.Text = "Add Clinic";
            btnSendCode.UseVisualStyleBackColor = true;
            // 
            // btnVerifyCode
            // 
            btnVerifyCode.Location = new Point(316, 261);
            btnVerifyCode.Name = "btnVerifyCode";
            btnVerifyCode.Size = new Size(75, 23);
            btnVerifyCode.TabIndex = 17;
            btnVerifyCode.Text = "Verify Code";
            btnVerifyCode.UseVisualStyleBackColor = true;
            // 
            // AddClinic
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnVerifyCode);
            Controls.Add(btnSendCode);
            Controls.Add(label3);
            Controls.Add(tbxVerificationCode);
            Controls.Add(label2);
            Controls.Add(tbxEmail);
            Controls.Add(btnSearch);
            Controls.Add(tbxAddress);
            Controls.Add(tbxSearch);
            Controls.Add(btnAdd);
            Controls.Add(label1);
            Controls.Add(lblName);
            Name = "AddClinic";
            Size = new Size(807, 507);
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
        private Microsoft.Web.WebView2.WinForms.WebView2 webviewGoogleMaps;
        private Button btnSearch;
        private TextBox tbxEmail;
        private Label label2;
        private Label label3;
        private TextBox tbxVerificationCode;
        private Button btnSendCode;
        private Button btnVerifyCode;
    }
}
