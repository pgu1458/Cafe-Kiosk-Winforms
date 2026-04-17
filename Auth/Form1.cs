using AxWMPLib;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace KiosK
{
    public partial class Form1 : Form
    {
        bool isMember = false;

        Dictionary<string, Dictionary<string, string>> menuTranslations = new Dictionary<string, Dictionary<string, string>>();
        string currentLanguage = "KR";

        int totalPrice = 0;
        bool isOrderTypeSelected = false;
        bool isTakeOut = false;
        int orderNumber = 1;

        private Form2 _parentForm;
        private bool isBgmOn = true; //음악 on off용

        bool isEyeControlEnabled = false;

        Mat frame = new Mat();
        Mat gray = new Mat();
        VideoCapture capture;
        Thread cameraThread;

        DateTime lookStartTime = DateTime.Now;
        string lastLookedButton = "";
        CascadeClassifier eyeCascade = new CascadeClassifier(Application.StartupPath + @"\haarcascade_eye.xml");

        float smoothX = 0, smoothY = 0;
        bool isFirstFrame = true;

        Queue<System.Drawing.Point> eyeBuffer = new Queue<System.Drawing.Point>();
        const int EYE_BUFFER_SIZE = 7;

        private SoundPlayer clickSound;
        private SoundPlayer paySound;
        private SoundPlayer menuSound;

        public Form1(Form2 parent)
        {
            InitializeComponent();
            clickSound = new SoundPlayer(Application.StartupPath + @"\click.wav");
            paySound = new SoundPlayer(Application.StartupPath + @"\pay.wav");
            menuSound = new SoundPlayer(Application.StartupPath + @"\menu.wav");
            InitTranslations();
            SetMenuButtonsEnabled(true);
            this.Load += (s, e) => StartCamera();
            _parentForm = parent;
            this.isBgmOn = !parent.axWindowsMediaPlayer1.settings.mute;
        }

        private void btnEyeToggle_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }

            isEyeControlEnabled = !isEyeControlEnabled;
            if (isEyeControlEnabled)
            {
                btnEyeToggle.Text = "시선 제어: ON";
                btnEyeToggle.BackColor = Color.LightGreen;
            }
            else
            {
                btnEyeToggle.Text = "시선 제어: OFF";
                btnEyeToggle.BackColor = SystemColors.Control;
                ResetAllButtonColors(this);
                lastLookedButton = "";
            }
        }

        private void InitTranslations()
        {
            var en = new Dictionary<string, string>();
            en.Add("아메리카노", "Americano");
            en.Add("제주말차라떼", "Jeju Malcha Latte");
            en.Add("카페라떼", "Cafe Latte");
            en.Add("초코라떼", "Choco Latte");
            en.Add("체리콕", "Cherry Coke");
            en.Add("자몽에이드", "Grapefruit Aide");
            en.Add("딸기요거트스무디", "Strawberry Yogurt Smoothie");
            en.Add("행운 롤케이크", "Lucky Roll Cake");
            en.Add("바나나 푸딩", "Banana Pudding");
            en.Add("미니미 프라페", "Minimi Frappe");
            en.Add("오리 마들렌", "Duck Madeleine");
            en.Add("소다 푸딩", "Soda Pudding");
            en.Add("두바이 쫀득 쿠키", "Dubai Cookie");
            en.Add("마이버디 춘식이 코나 텀블러 237ml", "Choonsik Kona Tumbler");
            en.Add("바리스타춘식 캐릭터 머그 237ml", "Choonsik Mug");
            en.Add("춘식 키체인", "Choonsik Keychain");
            en.Add("포장(할인)", "Take out (discount)");
            en.Add("매장", "Eat in");
            en.Add("결제하기", "Pay");
            en.Add("상품취소", "Cancellation of product");
            en.Add("전체취소", "Total cancellation");
            en.Add("주문내역", "Order details");
            en.Add("시선제어", "Eye control");
            en.Add("쿠폰", "Check Coupon");
            en.Add("로그인／회원가입", "Login/Sing Up");
            en.Add("로그인", "Login");
            en.Add("음소거🔇", "Mute🔇");

            var jp = new Dictionary<string, string>();
            jp.Add("아메리카노", "アメリカーノ");
            jp.Add("제주말차라떼", "済州抹茶ラテ");
            jp.Add("행운 롤케이크", "幸運ロールケーキ");
            jp.Add("바나나 푸딩", "バナナプリン");
            jp.Add("카페라떼", "カフェラテ");
            jp.Add("초코라떼", "チョコラテ");
            jp.Add("체리콕", "チェリーコック");
            jp.Add("자몽에이드", "グレープフルーツエード");
            jp.Add("딸기요거트스무디", "いちごヨーグルトスムージー");
            jp.Add("미니미 프라페", "ミニミ フラッペ");
            jp.Add("오리 마들렌", "鴨のマドレーヌ");
            jp.Add("소다 푸딩", "ソー다プリン");
            jp.Add("두바이 쫀득 쿠키", "ドバイのもちもちクッキー");
            jp.Add("마이버디 춘식이 코나 텀블러 237ml", "マイバ チュンク コナ タンブラー 237ml");
            jp.Add("바리스타춘식 캐릭터 머그 237ml", "バリスタチュン시ク キャラクターマグ 237ml");
            jp.Add("춘식 키체인", "チュン キーチェーン");
            jp.Add("포장(할인)", "テアウト");
            jp.Add("매장", "店内");
            jp.Add("결제하기", "支払い");
            jp.Add("상품취소", "商品キャンセル");
            jp.Add("전체취소", "全体キャンセル");
            jp.Add("시선제어", "視線制御");
            jp.Add("로그인／회원가입", "ログイン/会員登録");
            jp.Add("로그인", "ログイン");
            jp.Add("쿠폰", "クーポン確認");
            jp.Add("음소거🔇", "ミュート🔇");

            var cn = new Dictionary<string, string>();
            cn.Add("아메리카노", "美式咖啡");
            cn.Add("제주말차라떼", "济州抹茶拿铁");
            cn.Add("행운 롤케이크", "幸运卷蛋糕");
            cn.Add("바나나 푸딩", "香蕉布丁");
            cn.Add("카페라떼", "拿铁咖啡");
            cn.Add("초코라떼", "巧克力拿铁");
            cn.Add("체리콕", "樱桃木");
            cn.Add("자몽에이드", "西柚汽水");
            cn.Add("딸기요거트스무디", "草莓酸奶思慕雪");
            cn.Add("미니미 프라페", "米妮米·普라페");
            cn.Add("오리 마들렌", "奥리马德琳");
            cn.Add("소다 푸딩", "苏打布丁");
            cn.Add("두바이 쫀득 쿠키", "迪拜筋道饼干");
            cn.Add("마이버디 춘식이 코나 텀블러 237ml", "My Buddy 春植 Kona 保温杯 237ml");
            cn.Add("바리스타춘식 캐릭터 머그 237ml", "咖啡师春式卡通马克杯 237ml");
            cn.Add("춘식 키체인", "春式钥匙链");
            cn.Add("포장(할인)", "在商店");
            cn.Add("매장", "賣場");
            cn.Add("결제하기", "付款");
            cn.Add("상품취소", "退货");
            cn.Add("전체취소", "全部取消");
            cn.Add("시선제어", "视线控制");
            cn.Add("로그인／회원가입", "登录/注册会员");
            cn.Add("로그인", "登录");
            cn.Add("쿠폰", "确认优惠券");
            cn.Add("음소거🔇", "静音🔇");

            if (!menuTranslations.ContainsKey("CN")) menuTranslations.Add("CN", cn);
            else menuTranslations["CN"] = cn;

            menuTranslations.Add("EN", en);
            menuTranslations.Add("JP", jp);
        }

        public void ChangeLanguage(string langCode)
        {
            currentLanguage = langCode;
            foreach (Control c in this.Controls) UpdateControlText(c, langCode);
        }

        private void UpdateControlText(Control parent, string langCode)
        {
            string targetLang = langCode.ToUpper().Trim();
            if (parent is Button btn)
            {
                if (btn.Tag == null || string.IsNullOrEmpty(btn.Tag.ToString()))
                {
                    btn.Tag = btn.Text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                }

                string menuName = btn.Tag.ToString();
                string translatedName = menuName;

                if (langCode != "KR" && menuTranslations.ContainsKey(langCode) && menuTranslations[langCode].ContainsKey(menuName))
                {
                    translatedName = menuTranslations[langCode][menuName];
                }

                int price = GetPriceByName(menuName);
                if (price > 0) btn.Text = $"{translatedName}\n{price:N0}원";
                else btn.Text = translatedName;
            }
            foreach (Control child in parent.Controls) UpdateControlText(child, langCode);
        }

        private void StartCamera()
        {
            capture = new VideoCapture(0);
            if (!capture.IsOpened()) return;

            capture.Set(VideoCaptureProperties.FrameWidth, 640);
            capture.Set(VideoCaptureProperties.FrameHeight, 480);
            capture.Set(VideoCaptureProperties.AutoExposure, 1);

            cameraThread = new Thread(UpdateFrame);
            cameraThread.IsBackground = true;
            cameraThread.Start();
        }

        private void UpdateFrame()
        {
            while (true)
            {
                if (capture == null || !capture.IsOpened()) break;
                capture.Read(frame);
                if (frame.Empty()) continue;

                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                var eyes = eyeCascade.DetectMultiScale(gray, 1.3, 5, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(30, 30));

                if (eyes.Length > 0)
                {
                    var mainEye = eyes.OrderByDescending(e => e.Width * e.Height).First();
                    Cv2.Rectangle(frame, mainEye, Scalar.Red, 2);

                    int cX = mainEye.X + (mainEye.Width / 2);
                    int cY = mainEye.Y + (mainEye.Height / 2);

                    CheckEyeFocus(cX, cY);
                }

                try
                {
                    Bitmap bmp = BitmapConverter.ToBitmap(frame);
                    if (pictureBox1.InvokeRequired)
                    {
                        pictureBox1.Invoke(new Action(() =>
                        {
                            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                            pictureBox1.Image = bmp;
                        }));
                    }
                    else
                    {
                        if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                        pictureBox1.Image = bmp;
                    }
                }
                catch { }
            }
        }

        private void CheckEyeFocus(int x, int y)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;
            if (!isEyeControlEnabled) return;

            this.Invoke(new Action(() =>
            {
                eyeBuffer.Enqueue(new System.Drawing.Point(x, y));
                if (eyeBuffer.Count > EYE_BUFFER_SIZE) eyeBuffer.Dequeue();

                float ratioX = (float)x / 640.0f;
                float ratioY = (float)y / 480.0f;

                float sensitivityX = 8.0f;
                float sensitivityY = 12.0f;

                float targetX = (ratioX - 0.5f) * sensitivityX + 0.5f;
                float targetY = (ratioY - 0.45f) * sensitivityY + 0.5f;

                float finalX = (this.ClientSize.Width - (targetX * this.ClientSize.Width));
                float finalY = targetY * this.ClientSize.Height;

                if (isFirstFrame) { smoothX = finalX; smoothY = finalY; isFirstFrame = false; }

                float diffX = Math.Abs(finalX - smoothX);
                float diffY = Math.Abs(finalY - smoothY);

                if (diffX > 6 || diffY > 3)
                {
                    float lerpFactorX = 0.03f;
                    float lerpFactorY = 0.07f;
                    float deltaX = (finalX - smoothX) * lerpFactorX;
                    deltaX = Math.Max(-8f, Math.Min(8f, deltaX));

                    smoothX += deltaX;
                    smoothY += (finalY - smoothY) * lerpFactorY;
                }

                System.Drawing.Point eyePoint = new System.Drawing.Point((int)smoothX, (int)smoothY);
                Cursor.Position = this.PointToScreen(eyePoint);

                System.Drawing.Point hitPoint = tabcontroll.PointToClient(this.PointToScreen(eyePoint));
                bool tabFocused = false;

                for (int i = 0; i < tabcontroll.TabCount; i++)
                {
                    if (tabcontroll.GetTabRect(i).Contains(hitPoint))
                    {
                        string tabID = "TAB_" + i;
                        if (lastLookedButton == tabID)
                        {
                            if ((DateTime.Now - lookStartTime).TotalSeconds >= 2.0)
                            {
                                tabcontroll.SelectedIndex = i;
                                lastLookedButton = "";
                            }
                        }
                        else
                        {
                            lastLookedButton = tabID;
                            lookStartTime = DateTime.Now;
                        }
                        tabFocused = true;
                        break;
                    }
                }

                if (tabFocused) return;

                Control ctrl = GetDeepestControl(this, eyePoint);

                while (ctrl != null && !(ctrl is Button))
                {
                    ctrl = ctrl.Parent;
                }

                if (ctrl != null && ctrl.Enabled)
                {
                    ctrl.BackColor = Color.Yellow;
                    if (lastLookedButton == ctrl.Name)
                    {
                        if ((DateTime.Now - lookStartTime).TotalSeconds >= 2.0)
                        {
                            if (ctrl is Button btn)
                            {
                                btn.PerformClick();
                            }
                            lastLookedButton = "";
                            ctrl.BackColor = SystemColors.Control;
                        }
                    }
                    else { lastLookedButton = ctrl.Name; lookStartTime = DateTime.Now; }
                }
                else
                {
                    if (lastLookedButton != "") ResetAllButtonColors(this);
                    lastLookedButton = "";
                }
            }));
        }

        private Control GetDeepestControl(Control parent, System.Drawing.Point point)
        {
            foreach (Control child in parent.Controls)
            {
                if (child.Visible && child.Bounds.Contains(point))
                {
                    if (child.HasChildren)
                    {
                        var childPoint = new System.Drawing.Point(point.X - child.Left, point.Y - child.Top);
                        Control deeper = GetDeepestControl(child, childPoint);
                        return deeper ?? child;
                    }
                    return child;
                }
            }
            return null;
        }

        private void ResetAllButtonColors(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button b) b.BackColor = SystemColors.Control;
                if (c.HasChildren) ResetAllButtonColors(c);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture != null) { capture.Release(); capture.Dispose(); }
        }

        private void OrderType_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }

            isOrderTypeSelected = true;
            Button btn = sender as Button;
            if (btn.Text.Contains("포장")) { isTakeOut = true; MessageBox.Show($"[{btn.Text}] 선택되었습니다.\n포장 주문은 10% 할인됩니다."); }
            else { isTakeOut = false; MessageBox.Show($"[{btn.Text}] 선택되었습니다."); }
            SetMenuButtonsEnabled(true);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null && sender is Control ctrl) btn = ctrl as Button;
            if (btn == null) return;

            try
            {
                if (btn.Name.ToLower().Contains("tab"))
                {
                    clickSound.Play();
                    return;
                }
                else
                {
                    menuSound.Play();
                }
            }
            catch { }

            if (!isOrderTypeSelected)
            {
                MessageBox.Show("매장 / 포장을 먼저 선택해주세요.");
                return;
            }

            string menuName = btn.Tag != null ? btn.Tag.ToString() : btn.Text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();

            int itemPrice = GetPriceByName(menuName);
            totalPrice += itemPrice;

            listBox1.Items.Add($"{menuName} {itemPrice:N0}원");

            UpdateItemCountLabel(menuName);
            UpdateTotalPrice();
        }

        private void UpdateItemCountLabel(string menuName)
        {
            if (listBox1.Items.Count == 0) { lblCount.Text = "현재 선택된 상품 : 0개"; return; }
            var allItems = listBox1.Items.Cast<object>().Select(x => x.ToString().Split(' ')[0].Trim()).ToList();
            var uniqueMenus = allItems.Distinct().ToList();
            string[] rows = { "", "", "", "", "", "" };
            for (int i = 0; i < uniqueMenus.Count; i++)
            {
                string menu = uniqueMenus[i];
                int count = allItems.Count(x => x == menu);
                string displayName = menu.Length > 8 ? menu.Substring(0, 8) : menu;
                string itemText = $"{displayName} x{count}";
                rows[i % 6] += itemText.PadRight(20) + "\t";
            }
            lblCount.Text = string.Join("\n", rows).TrimEnd();
        }

        private void btnDeleteLast_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }

            if (listBox1.Items.Count > 0)
            {
                int lastIndex = listBox1.Items.Count - 1;
                string lastItem = listBox1.Items[lastIndex].ToString();
                string menuName = "";

                if (lastItem.Contains("제주말차라떼")) menuName = "제주말차라떼";
                else if (lastItem.Contains("아메리카노")) menuName = "아메리카노";
                else if (lastItem.Contains("카페라떼")) menuName = "카페라떼";
                else if (lastItem.Contains("초코라떼")) menuName = "초코라떼";
                else if (lastItem.Contains("체리콕")) menuName = "체리콕";
                else if (lastItem.Contains("자몽에이드")) menuName = "자몽에이드";
                else if (lastItem.Contains("딸기요거트스무디")) menuName = "딸기요거트스무디";
                else if (lastItem.Contains("행운 롤케이크")) menuName = "행운 롤케이크";
                else if (lastItem.Contains("바나나 푸딩")) menuName = "바나나 푸딩";
                else if (lastItem.Contains("미니미 프라페")) menuName = "미니미 프라페";
                else if (lastItem.Contains("오리 마들렌")) menuName = "오리 마들렌";
                else if (lastItem.Contains("소다 푸딩")) menuName = "소다 푸딩";
                else if (lastItem.Contains("두바이 쫀득 쿠키")) menuName = "두바이 쫀득 쿠키";
                else if (lastItem.Contains("마이버디 춘식이 코나 텀블러 237ml")) menuName = "마이버디 춘식이 코나 텀블러 237ml";
                else if (lastItem.Contains("바리스타춘식 캐릭터 머그 237ml")) menuName = "바리스타춘식 캐릭터 머그 237ml";
                else if (lastItem.Contains("춘식 키체인")) menuName = "춘식 키체인";

                int priceToRemove = GetPriceByName(menuName);
                totalPrice -= priceToRemove;
                listBox1.Items.RemoveAt(lastIndex);

                if (listBox1.Items.Count > 0)
                    UpdateItemCountLabel(listBox1.Items[listBox1.Items.Count - 1].ToString().Split(' ')[0]);
                else
                    lblCount.Text = "현재 선택된 상품 : 0개";

                UpdateTotalPrice();
            }
        }

        private string MakeReceipt()
        {
            int originalTotal = 0;
            foreach (var item in listBox1.Items)
            {
                string line = item.ToString();
                string priceStr = Regex.Replace(line.Split(' ').Last(), @"[^\d]", "");
                if (int.TryParse(priceStr, out int price)) originalTotal += price;
            }
            int discountAmount = isTakeOut ? (int)(originalTotal / 0.9 * 0.1) : 0;
            string data = "========== 영 수 증 ==========\n\n";
            if (isTakeOut) data += "   [ 포장 주문 - 10% 할인 ]   \n";
            data += $"   대기번호 : No. {orderNumber:D3}\n";
            data += "------------------------------\n\n";
            data += $"일시 : {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";
            data += "------------------------------\n";
            foreach (var item in listBox1.Items) data += $"{item}\n";
            data += "------------------------------\n";
            if (isTakeOut && discountAmount > 0) data += $"총 할인 금액 : -{discountAmount:N0}원\n";
            data += $"최종 결제 금액 : {totalPrice:N0}원\n";
            data += "==============================\n";
            data += "   이용해 주셔서 감사합니다.   ";
            return data;
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }

            listBox1.Items.Clear();
            totalPrice = 0;
            lblCount.Text = "현재 선택된 상품 : 0개";
            UpdateTotalPrice();
        }

        private async void btnPay_Click(object sender, EventArgs e)
        {
            if (totalPrice > 0)
            {
                try { if (clickSound != null) clickSound.Play(); } catch { }

                using (PaymentForm payForm = new PaymentForm())
                {
                    if (payForm.ShowDialog() == DialogResult.OK)
                    {
                        if (payForm.SelectedMethod == "Credit" || payForm.SelectedMethod == "QR")
                        {
                            lblLoading.Visible = true;
                            string[] frames = { "Loading [□□□□□]", "Loading [■□□□□]", "Loading [■■□□□]", "Loading [■■■□□]", "Loading [■■■■□]", "Loading [■■■■■]" };
                            foreach (string f in frames) { lblLoading.Text = $"{f}\n결제 진행 중... 잠시만 기다려주세요."; await Task.Delay(400); }
                            lblLoading.Visible = false;

                            try { if (paySound != null) paySound.Play(); } catch { }

                            string successMsg = $"총 {totalPrice:N0}원이 결제되었습니다.";
                            if (payForm.SelectedMethod == "Credit")
                            {
                                successMsg += "\n카드를 뽑아주세요.";
                            }

                            MessageBox.Show(successMsg);

                            if (MessageBox.Show("영수증을 출력하시겠습니까?", "영수증", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try { if (clickSound != null) clickSound.Play(); } catch { }
                                MessageBox.Show(MakeReceipt(), "--- 영수증 ---");
                            }
                            else
                            {
                                try { if (clickSound != null) clickSound.Play(); } catch { }
                            }

                            orderNumber++;
                            btnClearAll_Click(null, null);
                            isOrderTypeSelected = false;
                            isTakeOut = false;
                            isMember = false;
                            btnMemberLogin.Text = "회원 로그인/적립";
                            btnMemberLogin.Enabled = true;
                            SetMenuButtonsEnabled(true);
                        }
                    }
                }
            }
            else MessageBox.Show("결제할 상품이 없습니다.");
        }

        private int GetPriceByName(string name)
        {
            double price = 0;
            if (name.Contains("말차")) price = 5000;
            else if (name.Contains("아메리카노")) price = 4000;
            else if (name.Contains("카페라떼")) price = 4500;
            else if (name.Contains("초코라떼")) price = 4300;
            else if (name.Contains("딸기요거트스무디")) price = 5500;
            else if (name.Contains("체리콕")) price = 4800;
            else if (name.Contains("자몽에이드")) price = 5800;
            else if (name.Contains("행운 롤케이크")) price = 6500;
            else if (name.Contains("바나나 푸딩")) price = 6000;
            else if (name.Contains("미니미 프라페")) price = 7000;
            else if (name.Contains("오리 마들렌")) price = 4500;
            else if (name.Contains("소다 푸딩")) price = 5000;
            else if (name.Contains("두바이 쫀득 쿠키")) price = 3000;
            else if (name.Contains("마이버디 춘식이 코나 텀블러 237ml")) price = 49000;
            else if (name.Contains("바리스타춘식 캐릭터 머그 237ml")) price = 15000;
            else if (name.Contains("춘식 키체인")) price = 10000;

            if (isTakeOut) price = price * 0.9;
            return (int)price;
        }

        private int CalculateBasePrice()
        {
            int sum = 0;
            foreach (var item in listBox1.Items)
            {
                string text = item.ToString();
                string[] parts = text.Split(' ');
                if (parts.Length > 1)
                {
                    string priceStr = Regex.Replace(parts.Last(), @"[^\d]", "");
                    if (int.TryParse(priceStr, out int price))
                    {
                        sum += price;
                    }
                }
            }
            return sum;
        }

        private void UpdateTotalPrice()
        {
            int currentTotal = CalculateBasePrice();

            if (isMember)
            {
                currentTotal = (int)(currentTotal * 0.95);
            }

            totalPrice = currentTotal;
            labelTotal.Text = isMember ?
                $"총 결제 금액: {totalPrice:N0}원 (회원 5% 할인 적용)" :
                $"총 결제 금액: {totalPrice:N0}원";
        }

        private void SetMenuButtonsEnabled(bool enabled)
        {
            btnMenu1.Enabled = enabled; btnMenu2.Enabled = enabled; btnMenu3.Enabled = enabled;
            btnMenu4.Enabled = enabled; btnMenu5.Enabled = enabled; btnMenu6.Enabled = enabled;
            btnMenu7.Enabled = enabled;
            btndt1.Enabled = enabled; btndt2.Enabled = enabled; btndt3.Enabled = enabled;
            btndt4.Enabled = enabled; btndt5.Enabled = enabled; btndt6.Enabled = enabled;
            btnmd1.Enabled = enabled; btnmd2.Enabled = enabled; btnmd3.Enabled = enabled;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 1000;
            lbltimer.Text = DateTime.Now.ToString("tt hh:mm:ss");
        }

        private void btnMemberLogin_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }

            using (Login loginForm = new Login(this.currentLanguage))
            {
                loginForm.ShowDialog();

                if (loginForm.IsLoginSuccess)
                {
                    isMember = true;
                    btnMemberLogin.Text = "Membership (5% disscount)";
                    btnMemberLogin.Enabled = false;
                    UpdateTotalPrice();
                }
            }
        }

        private void btnCheckCoupon_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }

            if (!isMember)
            {
                MessageBox.Show("로그인 후 이용 가능한 서비스입니다.");
                return;
            }

            string msg = $"[{Login.CurrentUser}]님의 쿠폰 정보\n" +
                         $"현재 쿠폰: {Login.CurrentCoupons}개\n" +
                         "--------------------------\n";

            if (Login.CurrentCoupons >= 10)
                msg += " 축하합니다! 커피 1잔 무료 쿠폰 대상입니다!";
            else
                msg += $"무료 커피까지 {10 - Login.CurrentCoupons}개 남았습니다!";

            MessageBox.Show(msg, "내 쿠폰함");
        }

        private void tabcontroll_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (clickSound != null)
                {
                    clickSound.Play();
                }
            }
            catch { }
        }

        private void btnBgmToggle_Click(object sender, EventArgs e)
        {
            try { if (clickSound != null) clickSound.Play(); } catch { }

            if (_parentForm != null)
            {
                if (isBgmOn)
                {
                    _parentForm.axWindowsMediaPlayer1.settings.mute = true;
                    isBgmOn = false;
                }
                else
                {
                    _parentForm.axWindowsMediaPlayer1.settings.mute = false;
                    isBgmOn = true;
                }
            }
        }
    }
}