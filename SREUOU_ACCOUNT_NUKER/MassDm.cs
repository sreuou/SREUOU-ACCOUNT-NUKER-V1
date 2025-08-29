using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SREUOU_ACCOUNT_NUKER
{
    public partial class MassDm : Form
    {
        public MassDm()
        {
            InitializeComponent();
        }

        public string TobaseDe64(string inp2)
        {
            string outp = string.Empty;
            var ptext = Convert.FromBase64String(inp2);
            return outp = System.Text.Encoding.UTF8.GetString(ptext);
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

        private void MassDm_Load(object sender, EventArgs e)
        {
            if (Settings1.Default.BlackBackround == "true")
            {
                this.guna2ShadowForm1.SetShadowForm(this);
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

        private async void siticoneButton12_Click(object sender, EventArgs e)
        {
            string token = TobaseDe64(TobaseDe64(File.ReadAllText("Auth\\Save")));
            string messageContent = siticoneRoundedTextBox1.Text;
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
    }
}
