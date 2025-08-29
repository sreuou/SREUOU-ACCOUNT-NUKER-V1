using Newtonsoft.Json.Linq;
using Siticone.UI.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SREUOU_ACCOUNT_NUKER
{
    public partial class AboutUser : Form
    {
        public AboutUser()
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
            StreamWriter streamWriter = new StreamWriter($"Auth\\{objData.GetValue("username") + $"#{objData.GetValue("discriminator")}" + ".json"}");
            streamWriter.Write($"{Tobase64(strData)}");
            streamWriter.Close();
            label1.Text = ($"ID:   {objData.GetValue("id")}\nUSERNAME:    {objData.GetValue("username")}#{objData.GetValue("discriminator")}\nEMAIL:    {objData.GetValue("email")}\nPHONE:    {objData.GetValue("phone")}\nVERIFIED:    {objData.GetValue("verified")} \nBIO:    {objData.GetValue("bio")}\nCreatedPaymend:    {objData.GetValue("created_at")}");
            reader.Close();
            dataStream.Close();
        }

        private void UserDetails2(string strToken)
        {
            Random random = new Random();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v10/users/@me/billing/subscriptions");
            request.Headers.Add("authorization", strToken);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var strData = reader.ReadToEnd();
            JObject objData = JObject.Parse(strData);
            StreamWriter streamWriter = new StreamWriter($"Auth\\Text" + ".json");
            streamWriter.Write(strData);
            streamWriter.Close();
            reader.Close();
            dataStream.Close();
        }

        private void AboutUser_Load(object sender, EventArgs e)
        {
            if (Settings1.Default.BlackBackround == "true")
            {
                this.BackgroundImage = Properties.Resources._9f867dbeead71e9d7ea096a62c3e1d59__dark_blue_wallpaper_blue_wallpapers;
            }
            if (Settings1.Default.ChromaGlowColor == "true")
            {
                this.BackgroundImage = Properties.Resources.discord_chroma_bg;
            }
            if (Settings1.Default.TopMost == "true")
            {
                TopMost = true;
            }
            this.Text = Hide().ToLower();
            string strToken = TobaseDe64(TobaseDe64(File.ReadAllText("Auth\\Save")));
            string strJsCode = GetJsCode(strToken);
            UserDetails(strToken);
        }

        private async void siticoneButton3_Click(object sender, EventArgs e)
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
            base.Close();
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            string strToken = TobaseDe64(TobaseDe64(File.ReadAllText("Auth\\Save")));
            string strJsCode = GetJsCode(strToken);
            UserDetails(strToken);
        }

        private void siticoneButton12_Click(object sender, EventArgs e)
        {
            string strToken = TobaseDe64(TobaseDe64(File.ReadAllText("Auth\\Save")));
            string strJsCode = GetJsCode(strToken);
            UserDetails(strToken);
        }
    }
}
