using System;
using System.Windows.Forms;

namespace KiosK
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // 이 설정이 창 크기가 멋대로 커지는 것을 막아줍니다.
            if (Environment.OSVersion.Version.Major >= 6)
                Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }
    }
}