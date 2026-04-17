namespace KiosK
{
    partial class Login
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
            txtPw = new TextBox();
            txtId = new TextBox();
            btnLogin = new Button();
            btnJoin = new Button();
            SuspendLayout();
            // 
            // txtPw
            // 
            txtPw.Location = new Point(131, 115);
            txtPw.Name = "txtPw";
            txtPw.PasswordChar = '☕';
            txtPw.Size = new Size(527, 31);
            txtPw.TabIndex = 0;
            // 
            // txtId
            // 
            txtId.Location = new Point(131, 54);
            txtId.Name = "txtId";
            txtId.Size = new Size(527, 31);
            txtId.TabIndex = 0;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.White;
            btnLogin.FlatAppearance.BorderSize = 2;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnLogin.Location = new Point(131, 215);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(155, 89);
            btnLogin.TabIndex = 1;
            btnLogin.Tag = "로그인";
            btnLogin.Text = "로그인";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnJoin
            // 
            btnJoin.BackColor = Color.White;
            btnJoin.FlatAppearance.BorderSize = 2;
            btnJoin.FlatStyle = FlatStyle.Flat;
            btnJoin.Font = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnJoin.Location = new Point(503, 215);
            btnJoin.Name = "btnJoin";
            btnJoin.Size = new Size(155, 89);
            btnJoin.TabIndex = 1;
            btnJoin.Tag = "회원가입";
            btnJoin.Text = "회원가입";
            btnJoin.UseVisualStyleBackColor = false;
            btnJoin.Click += btnJoin_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 255, 192);
            ClientSize = new Size(800, 363);
            Controls.Add(btnJoin);
            Controls.Add(btnLogin);
            Controls.Add(txtId);
            Controls.Add(txtPw);
            Name = "Login";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPw;
        private TextBox txtId;
        private Button btnLogin;
        private Button btnJoin;
    }
}