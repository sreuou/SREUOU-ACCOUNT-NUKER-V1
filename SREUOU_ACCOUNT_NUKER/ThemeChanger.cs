using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SREUOU_ACCOUNT_NUKER
{
    public partial class ThemeChanger : Form
    {
        public ThemeChanger()
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

        private void ThemeChanger_Load(object sender, EventArgs e)
        {
            this.Text = Hide().ToLower();
            if (Settings1.Default.TopMost == "true")
            {
                TopMost = true;
            }
            if (Settings1.Default.Black == "true")
            {
                siticoneToggleSwitch3.Checked = true;
            }
            if (Settings1.Default.Chroma == "true")
            {
                siticoneToggleSwitch1.Checked = true;
            }
            if (Settings1.Default.BlackBackround == "true")
            {
                this.BackgroundImage = Properties.Resources._9f867dbeead71e9d7ea096a62c3e1d59__dark_blue_wallpaper_blue_wallpapers;
            }
            if (Settings1.Default.ChromaGlowColor == "true")
            {
                this.BackgroundImage = Properties.Resources.discord_chroma_bg;
            }
        }

        private void siticoneToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
             
        }

        private void siticoneToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {

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

        private void siticoneToggleSwitch1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch1.Checked == true)
            {
                this.BackgroundImage = Properties.Resources.discord_chroma_bg;
                Settings1.Default.ChromaGlowColor = "true";
                Settings1.Default.Chroma = "true";
                Settings1.Default.Save();
                if (siticoneToggleSwitch2.Checked  == true)
                {
                    siticoneToggleSwitch2.Checked = false;
                }
                if (siticoneToggleSwitch3.Checked == true)
                {
                    siticoneToggleSwitch3.Checked = false;
                }
            }
            if (siticoneToggleSwitch1.Checked == false)
            {
                Settings1.Default.ChromaGlowColor = "false";
                Settings1.Default.Chroma = "false";
                Settings1.Default.Save();
                Application.Restart();
            }
        }

        private void siticoneToggleSwitch3_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch3.Checked == true)
            {
                this.BackgroundImage = Properties.Resources._9f867dbeead71e9d7ea096a62c3e1d59__dark_blue_wallpaper_blue_wallpapers;
                Settings1.Default.BlackBackround = "true";
                Settings1.Default.Black = "true";
                Settings1.Default.Save();
                if (siticoneToggleSwitch1.Checked == true)
                {
                    siticoneToggleSwitch1.Checked = false;
                }
                if (siticoneToggleSwitch2.Checked == true)
                {
                    siticoneToggleSwitch2.Checked = false;
                }
            }
            if (siticoneToggleSwitch3.Checked == false)
            {
                Settings1.Default.BlackBackround = "false";
                Settings1.Default.Black = "false";
                Settings1.Default.Save();
                Application.Restart();
            }
        }

        private void siticoneButton19_Click(object sender, EventArgs e)
        {

        }
    }
}
