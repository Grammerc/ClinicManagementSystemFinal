namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    partial class Calendar_Doctor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calendar_Doctor));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panel2 = new Panel();
            a = new Label();
            btnNext = new Guna.UI2.WinForms.Guna2ImageButton();
            btnPrevious = new Guna.UI2.WinForms.Guna2ImageButton();
            lbMonth = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(a);
            panel2.Controls.Add(btnNext);
            panel2.Controls.Add(btnPrevious);
            panel2.Controls.Add(lbMonth);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(793, 56);
            panel2.TabIndex = 6;
            // 
            // a
            // 
            a.AutoSize = true;
            a.Font = new Font("Georgia", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            a.ForeColor = SystemColors.HotTrack;
            a.Location = new Point(596, 13);
            a.Name = "a";
            a.Size = new Size(96, 29);
            a.TabIndex = 4;
            a.Text = "Month";
            // 
            // btnNext
            // 
            btnNext.CheckedState.ImageSize = new Size(64, 64);
            btnNext.HoverState.ImageSize = new Size(64, 64);
            btnNext.Image = (Image)resources.GetObject("btnNext.Image");
            btnNext.ImageOffset = new Point(0, 0);
            btnNext.ImageRotate = 0F;
            btnNext.Location = new Point(699, 8);
            btnNext.Name = "btnNext";
            btnNext.PressedState.ImageSize = new Size(64, 64);
            btnNext.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btnNext.Size = new Size(64, 41);
            btnNext.TabIndex = 3;
            // 
            // btnPrevious
            // 
            btnPrevious.CheckedState.ImageSize = new Size(64, 64);
            btnPrevious.HoverState.ImageSize = new Size(64, 64);
            btnPrevious.Image = (Image)resources.GetObject("btnPrevious.Image");
            btnPrevious.ImageOffset = new Point(0, 0);
            btnPrevious.ImageRotate = 0F;
            btnPrevious.Location = new Point(528, 8);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.PressedState.ImageSize = new Size(64, 64);
            btnPrevious.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnPrevious.Size = new Size(64, 41);
            btnPrevious.TabIndex = 2;
            // 
            // lbMonth
            // 
            lbMonth.AutoSize = true;
            lbMonth.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbMonth.Location = new Point(12, 18);
            lbMonth.Name = "lbMonth";
            lbMonth.Size = new Size(257, 25);
            lbMonth.TabIndex = 1;
            lbMonth.Text = "Calendar Of The Month";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(-1, 93);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(793, 546);
            flowLayoutPanel1.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 75);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 8;
            label1.Text = "Sunday";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(161, 75);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 9;
            label2.Text = "Monday";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(265, 75);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 10;
            label3.Text = "Tuesday";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(364, 75);
            label5.Name = "label5";
            label5.Size = new Size(68, 15);
            label5.TabIndex = 11;
            label5.Text = "Wednesday";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(487, 75);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 12;
            label6.Text = "Thursday";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(606, 75);
            label7.Name = "label7";
            label7.Size = new Size(39, 15);
            label7.TabIndex = 13;
            label7.Text = "Friday";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(717, 75);
            label8.Name = "label8";
            label8.Size = new Size(53, 15);
            label8.TabIndex = 14;
            label8.Text = "Saturday";
            // 
            // Calendar_Doctor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(panel2);
            Name = "Calendar_Doctor";
            Size = new Size(793, 642);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel2;
        private Label lbMonth;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Guna.UI2.WinForms.Guna2ImageButton btnNext;
        private Guna.UI2.WinForms.Guna2ImageButton btnPrevious;
        private Label a;
    }
}
