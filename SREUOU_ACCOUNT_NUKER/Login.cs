using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Siticone.UI.WinForms;
using Microsoft.Win32;
using DiscordRPC;
using Button = DiscordRPC.Button;
using OpenQA.Selenium;
using System.Data.SqlClient;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.YouTube.v3;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Management;
using System.Net.Http;
using MySqlX.XDevAPI;

namespace SREUOU_ACCOUNT_NUKER
{
    public partial class Login : Form
    {
        static WebClient webClient2 = new WebClient();
        public static string[] allowedHWIDs = { "" };
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        MySqlCommand command;
        MySqlDataReader mdr;
        string version = "1.20";
        public Login()
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

        static string GetHardwareID()
        {
            string hwid = Environment.MachineName + Environment.ProcessorCount;
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    hwid += obj["SerialNumber"].ToString();
                    break;
                }
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(hwid));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        static string GetHWID()
        {
            string hwid = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                hwid += obj["ProcessorID"].ToString();
            }

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject obj in searcher.Get())
            {
                hwid += obj["SerialNumber"].ToString();
            }
            return hwid;
        }


        private async void Login_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            await Task.Delay(30);
            this.Opacity = .100;
            await Task.Delay(30);
            this.Opacity = .200;
            await Task.Delay(30);
            this.Opacity = .300;
            await Task.Delay(30);
            this.Opacity = .400;
            await Task.Delay(30);
            this.Opacity = .500;
            await Task.Delay(30);
            this.Opacity = .600;
            await Task.Delay(30);
            this.Opacity = .700;
            await Task.Delay(30);
            this.Opacity = .800;
            await Task.Delay(30);
            this.Opacity = 1000;
            if (Settings1.Default.BlackBackround == "true")
            {
                this.guna2ShadowForm1.SetShadowForm(this);
                this.BackgroundImage = Properties.Resources._9f867dbeead71e9d7ea096a62c3e1d59__dark_blue_wallpaper_blue_wallpapers;
            }
            if (Settings1.Default.ChromaGlowColor == "true")
            {
                this.BackgroundImage = Properties.Resources.discord_chroma_bg;
            }
            if (Settings1.Default.Rules == "true")
            {
                siticoneCheckBox1.Checked = true;
            }
            string clientID = "1141046578031898685";
            DiscordRpcClient rpc = new DiscordRpcClient(clientID);
            bool rpcEnabled = false;
            string details = "SREUOU ACCOUNT NUKER LOGIN STAGE";
            string state = "Development By SREUOU";
            string largeImageKey = "https://cdn.discordapp.com/attachments/1166003576364089367/1174355370785906748/A6C08478-69FB-4DEC-BB7E-1E8CCC86B062.gif?ex=65674aaa&is=6554d5aa&hm=7b5526cca09b0ab5adeaf7bdfc0c21729087b6c70ade014b0d26d09aad8838df&.gif";
            DateTime startTime = DateTime.UtcNow;
            if (rpcEnabled)
            {
                try
                {
                    rpc.ClearPresence();
                    rpc.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    rpc.Initialize();
                    rpc.SetPresence(new RichPresence
                    {
                        Details = details,
                        State = state,
                        Assets = new Assets
                        {
                            LargeImageKey = largeImageKey,
                            LargeImageText = "SREUOU ACCOUNT NUKER V" + version
                        },
                        Timestamps = new Timestamps
                        {
                            Start = startTime
                        },
                        Buttons = new DiscordRPC.Button[]
                        {
                            new Button { Label = "Discord Server", Url = "https://discord.gg/xBYSAD9vSX" },
                            new Button { Label = "Discord Server Backup", Url = "https://discord.gg/xu3KpTBymC" }
                        },
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            rpcEnabled = !rpcEnabled;

            if (!string.Equals(Registry.GetValue("HKEY_CLASSES_ROOT\\exefile\\shell\\open\\command", "", "").ToString(), "\"%1\" %*", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    Registry.SetValue("HKEY_CLASSES_ROOT\\exefile\\shell\\open\\command", "", "\"%1\" %*");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to fix registry value: " + ex.Message, "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Text = Hide().ToLower();
            if (Settings1.Default.TopMost == "true")
            {
                TopMost = true;
            }
            if (Settings1.Default.SaveLogin == "true")
            {
                Settings1.Default.Login = siticoneRoundedTextBox1.Text;
                Settings1.Default.Password = siticoneRoundedTextBox2.Text;
            }
        }

        static string random_string()
        {
            string str = null;

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
            }
            return str;
        }

        private async void siticoneButton1_Click(object sender, EventArgs e)
        {
            if (siticoneCheckBox1.Checked == true)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Host", "discord.com");
                var data = new { login = siticoneRoundedTextBox1.Text, siticoneRoundedTextBox2.Text };
                string requestUrl = "https://discord.com/api/v9/auth/login";

                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Welcome", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if ((int)response.StatusCode == 429)
                {
                    MessageBox.Show("error", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("test", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            if (siticoneCheckBox1.Checked == true)
            {
                Register main = new Register();
                main.Show();
                base.Hide();
            }
            else
            {
                MessageBox.Show("Please Accect The Political Rules", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void siticoneButton18_Click(object sender, EventArgs e)
        {
             timerOpacity.Start();
        }

        private void siticoneButton19_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void siticoneToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch1.Checked == true)
            {
                siticoneRoundedTextBox1.UseSystemPasswordChar = true;
            }
            if (siticoneToggleSwitch1.Checked == false)
            {
                siticoneRoundedTextBox1.UseSystemPasswordChar = false;
            }
        }

        private void siticoneToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch2.Checked == true)
            {
                siticoneRoundedTextBox2.UseSystemPasswordChar = true;
            }
            if (siticoneToggleSwitch2.Checked == false)
            {
                siticoneRoundedTextBox2.UseSystemPasswordChar = false;
            }
        }

        private void siticoneToggleSwitch4_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch4.Checked == true)
            {
                Settings1.Default.SaveLogin = "true";
                Settings1.Default.Save();
            }
            if (siticoneToggleSwitch4.Checked == false)
            {
                Settings1.Default.SaveLogin = "false";
                Settings1.Default.Save();
            }
        }

        private void siticoneImageButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DEVELOPMENT BY SREUOU", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void siticoneToggleSwitch3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void siticoneToggleSwitch3_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void siticoneButton4_Click(object sender, EventArgs e)
        {

        }
        

        private async void siticoneButton4_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void timerOpacity_Tick(object sender, EventArgs e)
        {
            if (siticoneCheckBox1.Checked == true)
            {
                siticoneCheckBox1.Checked = false;
                Settings1.Default.Rules = "true";
                Settings1.Default.Save();
            }
            if (base.Opacity > 0.0)
            {
                base.Opacity -= 0.03;
                return;
            }
            timerOpacity.Stop();
            timerHeight.Start();
            timerWidth.Start();
            Application.Exit();
        }

        private void timerWidth_Tick(object sender, EventArgs e)
        {
            base.Width -= 25;
            if (base.Width < 1)
            {
                timerWidth.Stop();
            }
        }

        private void timerHeight_Tick(object sender, EventArgs e)
        {
            base.Height -= 25;
            if (base.Height <= 1)
            {
                timerHeight.Stop();
            }
        }

        private void Opacity2_Tick(object sender, EventArgs e)
        {
                base.Opacity += 0.03;
                return;
        }

        private void siticoneCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneCheckBox1.Checked == true)
            {
                Settings1.Default.Rules = "true";
                Settings1.Default.Save();
            }
            if (siticoneCheckBox1.Checked == true)
            {
                Settings1.Default.Rules = "false";
                Settings1.Default.Save();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (Process.GetProcesses().Any((Process name) => name.ProcessName.Equals("dnSpy")))
            {
                Process[] ida64 = Process.GetProcessesByName("ida64");
                Process[] ida32 = Process.GetProcessesByName("ida32");
                Process[] ollydbg = Process.GetProcessesByName("ollydbg");
                Process[] ollydbg64 = Process.GetProcessesByName("ollydbg64");
                Process[] loaddll = Process.GetProcessesByName("loaddll");
                Process[] httpdebugger = Process.GetProcessesByName("httpdebugger");
                Process[] windowrenamer = Process.GetProcessesByName("windowrenamer");
                Process[] processhacker = Process.GetProcessesByName("processhacker");
                Process[] processhacker2 = Process.GetProcessesByName("Process Hacker");
                Process[] processhacker3 = Process.GetProcessesByName("ProcessHacker");
                Process[] HxD = Process.GetProcessesByName("HxD");
                Process[] parsecd = Process.GetProcessesByName("parsecd");
                Process[] ida = Process.GetProcessesByName("ida");
                Process[] dnSpy = Process.GetProcessesByName("dnSpy");
                Process[] megadumper = Process.GetProcessesByName("MegaDumper");
            if (ida64.Length != 0 || ida32.Length != 0 || ollydbg.Length != 0 || ollydbg64.Length != 0 || loaddll.Length != 0 || httpdebugger.Length != 0 || windowrenamer.Length != 0 || processhacker.Length != 0 || processhacker2.Length != 0 || processhacker3.Length != 0 || HxD.Length != 0 || ida.Length != 0 || parsecd.Length != 0 || dnSpy.Length != 0 || megadumper.Length != 0)
            {   
                SendKeys.Send("{PRTSC}");
                Image myImage = Clipboard.GetImage();
            }
                Application.Restart();
                Process.Equals(Process.GetCurrentProcess().Id, CloseReason.ApplicationExitCall);
                Process.GetProcessesByName("dnSpy")[0].Kill();
            }
            else if (Process.GetProcesses().Any((Process name) => name.ProcessName.Equals("x64dbg")))
            {
                Application.Restart();
                Process.Equals(Process.GetCurrentProcess().Id, CloseReason.ApplicationExitCall);
                Process.GetProcessesByName("x64dbg")[0].Kill();
            }
        }
    }
}
