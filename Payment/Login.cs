using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace KiosK
{
    public partial class Login : Form
    {
        private SoundPlayer clickSound = new SoundPlayer(Application.StartupPath + @"\click.wav");

        // 가상 사용자 DB (ID, PW, 쿠폰 개수)
        private static Dictionary<string, (string Pw, int Coupons)> userDb =
            new Dictionary<string, (string, int)>()
        {
            { "user1", ("1234", 5) },
            { "user2", ("1234", 0) }
        };

        private Dictionary<string, Dictionary<string, string>> loginTranslations = new Dictionary<string, Dictionary<string, string>>();

        public bool IsLoginSuccess { get; private set; } = false;
        public static string CurrentUser { get; private set; } = "";
        public static int CurrentCoupons { get; private set; } = 0;

        public Login(string langCode)
        {
            InitializeComponent();
            InitLoginTranslations();
            ApplyLanguage(langCode);
        }

        // 언어별 번역 데이터 초기화
        private void InitLoginTranslations()
        {
            loginTranslations["EN"] = new Dictionary<string, string> { { "Login", "Login" }, { "Join", "Sign Up" } };
            loginTranslations["JP"] = new Dictionary<string, string> { { "Login", "ログイン" }, { "Join", "会員登録" } };
            loginTranslations["CN"] = new Dictionary<string, string> { { "Login", "登录" }, { "Join", "注册" } };
        }

        // 전달받은 언어 코드에 맞춰 UI 텍스트 변경
        public void ApplyLanguage(string langCode)
        {
            if (langCode == "KR" || !loginTranslations.ContainsKey(langCode)) return;

            var trans = loginTranslations[langCode];
            btnLogin.Text = trans["Login"];
            btnJoin.Text = trans["Join"];
            this.Text = langCode == "EN" ? "Member Login" : (langCode == "JP" ? "ログイン" : "登录");
        }

        // 로그인 확인 및 쿠폰 적립 로직
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }

            string id = txtId.Text;
            string pw = txtPw.Text;

            if (userDb.ContainsKey(id) && userDb[id].Pw == pw)
            {
                IsLoginSuccess = true;
                CurrentUser = id;

                // 로그인 성공 시 쿠폰 +1 적립
                var info = userDb[id];
                userDb[id] = (info.Pw, info.Coupons + 1);
                CurrentCoupons = userDb[id].Coupons;

                MessageBox.Show($"{id}님 로그인 성공!\n쿠폰 1개가 적립되어 현재 {CurrentCoupons}개입니다.");
                this.Close();
            }
            else
            {
                MessageBox.Show("아이디 또는 비밀번호가 틀립니다.");
            }
        }

        // 회원가입 로직
        private void btnJoin_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }

            string id = txtId.Text;
            string pw = txtPw.Text;

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(pw))
            {
                MessageBox.Show("아이디와 비밀번호를 모두 입력하세요.");
                return;
            }

            if (!userDb.ContainsKey(id))
            {
                userDb.Add(id, (pw, 0));
                MessageBox.Show("회원가입이 완료되었습니다! 로그인을 진행해주세요.");
            }
            else
            {
                MessageBox.Show("이미 존재하는 아이디입니다.");
            }
        }
    }
}