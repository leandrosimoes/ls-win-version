using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LsWinVersion {
    public enum Versions {
        Windows10,
        Windows8_1,
        Windows8,
        Windows7,
        WindowsVista,
        WindowsXP,
        Unknow
    }

    public static class WinVersion {
        [DllImport("dwmapi.dll", EntryPoint = "DwmIsCompositionEnabled", PreserveSig = false)]
        private static extern bool DwmIsCompositionEnabled32();

        private static readonly int _major = Environment.OSVersion.Version.Major;
        private static readonly int _minor = Environment.OSVersion.Version.Minor;

        private static bool DwmIsCompositionEnabled {
            get {
                if (_major >= 6) {
                    return DwmIsCompositionEnabled32();
                } else {
                    return false;
                }
            }
        }

        private static bool IsWin10 {
            get {
                var isWin10 = _major == 10;

                if (!isWin10) {
                    try {
                        isWin10 = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentMajorVersionNumber", null).ToString() == "10";
                    } catch (Exception) {
                        isWin10 = false;
                    }
                }

                return isWin10;
            }
        }

        private static Versions GetVersion() {
            Versions? returnVersion = null;

            if (DwmIsCompositionEnabled) {
                if (IsWin10) {
                    returnVersion = Versions.Windows10;
                } else if (_major == 6 && _minor == 2) {
                    returnVersion = Versions.Windows8_1;
                } else if (_major == 6 && _minor < 2) {
                    returnVersion = Versions.Windows8;
                }
            } else {
                if (Application.RenderWithVisualStyles && _major >= 6) {
                    returnVersion = Versions.WindowsVista;
                } else if (Application.RenderWithVisualStyles) {
                    returnVersion = Versions.WindowsXP;
                }
            }

            return returnVersion ?? Versions.Unknow;
        }

        public static bool IsWindows(Versions version) {
            return version == GetVersion();
        }
    }
}