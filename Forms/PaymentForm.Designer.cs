namespace KiosK
{
    partial class PaymentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentForm));
            pay1 = new Button();
            pnlHeader = new Panel();
            label2 = new Label();
            label1 = new Label();
            pay2 = new Button();
            pay3 = new Button();
            pay4 = new Button();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pay1
            // 
            pay1.BackgroundImage = (Image)resources.GetObject("pay1.BackgroundImage");
            pay1.BackgroundImageLayout = ImageLayout.Stretch;
            pay1.Location = new Point(0, 114);
            pay1.Name = "pay1";
            pay1.Size = new Size(527, 245);
            pay1.TabIndex = 0;
            pay1.UseVisualStyleBackColor = true;
            pay1.Click += pay1_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.PeachPuff;
            pnlHeader.Controls.Add(label2);
            pnlHeader.Controls.Add(label1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1090, 119);
            pnlHeader.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 24F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label2.Location = new Point(54, 25);
            label2.Name = "label2";
            label2.Size = new Size(220, 65);
            label2.TabIndex = 5;
            label2.Text = "결제방법";
            // 
            // label1
            // 
            label1.Image = Properties.Resources.zjvl_preview_rev_1;
            label1.ImageAlign = ContentAlignment.MiddleRight;
            label1.Location = new Point(-45, 0);
            label1.Name = "label1";
            label1.Size = new Size(1089, 120);
            label1.TabIndex = 4;
            // 
            // pay2
            // 
            pay2.BackgroundImage = Properties.Resources.dd;
            pay2.BackgroundImageLayout = ImageLayout.Stretch;
            pay2.Location = new Point(512, 114);
            pay2.Name = "pay2";
            pay2.Size = new Size(585, 245);
            pay2.TabIndex = 0;
            pay2.UseVisualStyleBackColor = true;
            pay2.Click += pay2_Click;
            // 
            // pay3
            // 
            pay3.BackgroundImage = Properties.Resources.qq;
            pay3.BackgroundImageLayout = ImageLayout.Stretch;
            pay3.Location = new Point(0, 353);
            pay3.Name = "pay3";
            pay3.Size = new Size(527, 234);
            pay3.TabIndex = 0;
            pay3.UseVisualStyleBackColor = true;
            pay3.Click += pay3_Click;
            // 
            // pay4
            // 
            pay4.BackgroundImage = Properties.Resources.ds;
            pay4.BackgroundImageLayout = ImageLayout.Stretch;
            pay4.Location = new Point(512, 353);
            pay4.Name = "pay4";
            pay4.Size = new Size(580, 229);
            pay4.TabIndex = 0;
            pay4.UseVisualStyleBackColor = true;
            pay4.Click += pay4_Click;
            // 
            // PaymentForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 98, 65);
            ClientSize = new Size(1090, 580);
            Controls.Add(pnlHeader);
            Controls.Add(pay3);
            Controls.Add(pay1);
            Controls.Add(pay2);
            Controls.Add(pay4);
            Name = "PaymentForm";
            Text = "PaymentForm";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button pay1;
        private Panel pnlHeader;
        private Label label1;
        private Button pay2;
        private Button pay3;
        private Button pay4;
        private Label label2;
    }
}