using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionAlignChamber.Config;

namespace VisionAlignChamber
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 애플리케이션 설정 초기화 (DLL 경로 등)
            if (!AppSettings.Initialize())
            {
                MessageBox.Show("설정 파일 초기화에 실패했습니다.", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // AJIN DLL 미리 로드 (선택적)
            AppSettings.PreloadAjinDlls();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
