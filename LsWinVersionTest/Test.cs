using System;
using System.Windows.Forms;
using LsWinVersion;

namespace LsWinVersionTest {
    public partial class Test : Form {
        public Test() {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void Test_Load(object sender, EventArgs e) {
            var version = string.Empty;

            if (WinVersion.IsWindows(Versions.Windows10)) {
                version = "Windows 10";
            } else if (WinVersion.IsWindows(Versions.Windows8_1)) {
                version = "Windows 8.1";
            } else if (WinVersion.IsWindows(Versions.Windows8)) {
                version = "Windows 8";
            } else if (WinVersion.IsWindows(Versions.Windows7)) {
                version = "Windows 7";
            } else if (WinVersion.IsWindows(Versions.WindowsXP)) {
                version = "Windows XP";
            } else if (WinVersion.IsWindows(Versions.Unknow)) {
                version = "Unknow";
            }

            lblVersion.Text = string.Format("Your current OS version is: \"{0}\"", version);
        }
    }
}
