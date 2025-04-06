using System.Windows.Forms;


namespace ClinicManagementSystemFinal
{
    partial class ForgotPassword
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgotPassword));
            tbxEmail = new Guna.UI2.WinForms.Guna2TextBox();
            label1 = new Label();
            btnSubmit = new Guna.UI2.WinForms.Guna2Button();
            btnCancel = new Guna.UI2.WinForms.Guna2Button();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tbxEmail
            // 
            tbxEmail.CustomizableEdges = customizableEdges1;
            tbxEmail.DefaultText = "e.g. person@gmail.com";
            tbxEmail.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tbxEmail.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tbxEmail.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tbxEmail.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tbxEmail.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tbxEmail.Font = new Font("Segoe UI", 9F);
            tbxEmail.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tbxEmail.Location = new Point(21, 243);
            tbxEmail.Name = "tbxEmail";
            tbxEmail.PlaceholderText = "";
            tbxEmail.SelectedText = "";
            tbxEmail.ShadowDecoration.CustomizableEdges = customizableEdges2;
            tbxEmail.Size = new Size(334, 36);
            tbxEmail.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 225);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 1;
            label1.Text = "Email";
            // 
            // btnSubmit
            // 
            btnSubmit.CustomizableEdges = customizableEdges3;
            btnSubmit.DisabledState.BorderColor = Color.DarkGray;
            btnSubmit.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSubmit.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSubmit.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSubmit.Font = new Font("Segoe UI", 9F);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.Location = new Point(21, 285);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnSubmit.Size = new Size(334, 45);
            btnSubmit.TabIndex = 2;
            btnSubmit.Text = "Send Email";
            // 
            // btnCancel
            // 
            btnCancel.CustomizableEdges = customizableEdges5;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.Font = new Font("Segoe UI", 9F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(86, 347);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnCancel.Size = new Size(191, 45);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(31, 182);
            label2.Name = "label2";
            label2.Size = new Size(323, 29);
            label2.TabIndex = 4;
            label2.Text = "Forgot your password?";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(86, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(191, 161);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // ForgotPassword
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(391, 450);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(btnCancel);
            Controls.Add(btnSubmit);
            Controls.Add(label1);
            Controls.Add(tbxEmail);
            Name = "ForgotPassword";
            Text = "ForgotPassword";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox tbxEmail;
        private Label label1;
        private Guna.UI2.WinForms.Guna2Button btnSubmit;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Label label2;
        private PictureBox pictureBox1;
    }
}