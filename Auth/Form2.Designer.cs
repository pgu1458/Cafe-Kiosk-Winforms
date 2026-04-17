namespace KiosK
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            button1 = new Button();
            btnKorean = new Button();
            btnEnglish = new Button();
            btnJapanese = new Button();
            btnChinese = new Button();
            label1 = new Label();
            axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            btnBgmToggle = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.pp;
            button1.Dock = DockStyle.Fill;
            button1.Font = new Font("굴림", 16F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button1.ForeColor = Color.Lime;
            button1.Image = Properties.Resources.watermarked_480004cc_855f_4ca5_a041_f4e6d9e6f629;
            button1.Location = new Point(0, 0);
            button1.Name = "button1";
            button1.Size = new Size(765, 668);
            button1.TabIndex = 0;
            button1.Tag = "";
            button1.Text = "\r\n\r\n\r\n";
            button1.TextAlign = ContentAlignment.BottomCenter;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnKorean
            // 
            btnKorean.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnKorean.Location = new Point(12, 12);
            btnKorean.Name = "btnKorean";
            btnKorean.Size = new Size(159, 62);
            btnKorean.TabIndex = 1;
            btnKorean.Text = "한국어\r\n";
            btnKorean.UseVisualStyleBackColor = true;
            btnKorean.Click += Language_Click;
            // 
            // btnEnglish
            // 
            btnEnglish.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
            btnEnglish.Location = new Point(210, 12);
            btnEnglish.Name = "btnEnglish";
            btnEnglish.Size = new Size(159, 62);
            btnEnglish.TabIndex = 1;
            btnEnglish.Text = "English\r\n";
            btnEnglish.UseVisualStyleBackColor = true;
            btnEnglish.Click += Language_Click;
            // 
            // btnJapanese
            // 
            btnJapanese.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
            btnJapanese.Location = new Point(413, 12);
            btnJapanese.Name = "btnJapanese";
            btnJapanese.Size = new Size(159, 62);
            btnJapanese.TabIndex = 1;
            btnJapanese.Text = "日本語";
            btnJapanese.UseVisualStyleBackColor = true;
            btnJapanese.Click += Language_Click;
            // 
            // btnChinese
            // 
            btnChinese.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
            btnChinese.Location = new Point(594, 12);
            btnChinese.Name = "btnChinese";
            btnChinese.Size = new Size(159, 62);
            btnChinese.TabIndex = 1;
            btnChinese.Text = "中國語";
            btnChinese.UseVisualStyleBackColor = true;
            btnChinese.Click += Language_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(255, 255, 192);
            label1.Font = new Font("맑은 고딕", 16F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.Location = new Point(190, 586);
            label1.Name = "label1";
            label1.Size = new Size(52, 45);
            label1.TabIndex = 2;
            label1.Text = "－";
            // 
            // axWindowsMediaPlayer1
            // 
            axWindowsMediaPlayer1.Enabled = true;
            axWindowsMediaPlayer1.Location = new Point(308, 586);
            axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            axWindowsMediaPlayer1.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState");
            axWindowsMediaPlayer1.Size = new Size(75, 23);
            axWindowsMediaPlayer1.TabIndex = 3;
            // 
            // btnBgmToggle
            // 
            btnBgmToggle.AutoSize = true;
            btnBgmToggle.Font = new Font("맑은 고딕", 14F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnBgmToggle.Location = new Point(684, 626);
            btnBgmToggle.Name = "btnBgmToggle";
            btnBgmToggle.Size = new Size(81, 42);
            btnBgmToggle.TabIndex = 4;
            btnBgmToggle.Tag = "음소거";
            btnBgmToggle.Text = "🔇";
            btnBgmToggle.UseVisualStyleBackColor = true;
            btnBgmToggle.Click += btnBgmToggle_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(765, 668);
            Controls.Add(btnBgmToggle);
            Controls.Add(label1);
            Controls.Add(btnChinese);
            Controls.Add(btnJapanese);
            Controls.Add(btnEnglish);
            Controls.Add(btnKorean);
            Controls.Add(button1);
            Controls.Add(axWindowsMediaPlayer1);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button btnKorean;
        private Button btnEnglish;
        private Button btnJapanese;
        private Button btnChinese;
        private Label label1;
        private RadioButton btnBgmToggle;
        public AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    }
}