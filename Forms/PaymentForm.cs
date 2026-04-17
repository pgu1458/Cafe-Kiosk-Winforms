using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiosK
{
    public partial class PaymentForm : Form
    {

        private SoundPlayer clickSound = new SoundPlayer(Application.StartupPath + @"\click.wav");
        public string SelectedMethod { get; private set; } = "";
        public PaymentForm()
        {
            InitializeComponent();
        }


        // pay1: 카카오페이 버튼 클릭
        private void pay1_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }

            using (KakaopayForm kakao = new KakaopayForm())
            {
                // QR 창을 띄우고, 사용자가 확인(이미지 클릭 등)을 누르면 다음으로 진행
                if (kakao.ShowDialog() == DialogResult.OK)
                {
                    this.SelectedMethod = "QR";
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        // pay3: 네이버페이 버튼 클릭
       

        // pay2: 신용카드 
        private void pay2_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }
            this.SelectedMethod = "Credit";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // pay3: 네이버페이 
        private void pay3_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }

            using (NaverpayForm naver = new NaverpayForm())
            {
                if (naver.ShowDialog() == DialogResult.OK)
                {
                    this.SelectedMethod = "QR";
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        // pay4: 현금결제 
        private void pay4_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }
            MessageBox.Show("현금 결제는 카운터에 문의해주세요.", "현금 결제");
           
        }

    }
}
