using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace KiosK
{
    public partial class Form2 : Form
    {
        SoundPlayer player = new SoundPlayer(@"C:\PES_C#\Kiosk\Kiosk\bin\Debug\net6.0-windows\히로시의회상.mp3");

        // 언어별 시작 메시지 설정
        Dictionary<string, string> startMessages = new Dictionary<string, string>()
        {
            { "KR", "시작하려면 클릭하세요." },
            { "EN", "      Click to Start.      " },
            { "JP", "      クリックして .        " },
            { "CN", "          點擊開始 .         " }
        };

        private bool isBgmOn = true;
        private string selectedLang = "KR";

        public Form2()
        {
            InitializeComponent();
        }

        // 언어 선택 및 메시지 변경
        private void Language_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            if (btn.Text.Contains("English")) selectedLang = "EN";
            else if (btn.Text.Contains("日本語")) selectedLang = "JP";
            else if (btn.Text.Contains("中國語")) selectedLang = "CN";
            else selectedLang = "KR";

            MessageBox.Show($"{btn.Text}가 선택되었습니다.");

            if (startMessages.ContainsKey(selectedLang))
            {
                label1.Text = startMessages[selectedLang];
            }
        }

        // 주문 화면(Form1)으로 이동
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 orderForm = new Form1(this);
            orderForm.Show();
            orderForm.ChangeLanguage(selectedLang);
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PlayBackgroundMusic(@"히로시의회상.mp3");
        }

        // 배경음악 재생 설정
        private void PlayBackgroundMusic(string filepath)
        {
            axWindowsMediaPlayer1.URL = filepath;
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.settings.volume = 15;
        }

        // BGM On/Off 토글
        private void btnBgmToggle_Click(object sender, EventArgs e)
        {
            if (isBgmOn)
            {
                axWindowsMediaPlayer1.settings.mute = true;
                btnBgmToggle.BackColor = Color.Gray;
                isBgmOn = false;
            }
            else
            {
                axWindowsMediaPlayer1.settings.mute = false;
                btnBgmToggle.BackColor = Color.LightGreen;
                isBgmOn = true;
            }
        }
    }
}