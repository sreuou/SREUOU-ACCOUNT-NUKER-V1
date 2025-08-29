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
using MySql.Data.MySqlClient;

namespace SREUOU_ACCOUNT_NUKER
{
    public partial class Register : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        public Register()
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

        private void Register_Load(object sender, EventArgs e)
        {
            this.Text = Hide().ToLower();
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
        }

        private string random_string()
        {
            string str = null;

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
            }
            return str;
        }

        private void siticoneImageButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DEVELOPMENT BY SREUOU", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            if (!this.email.Text.Contains('@') || !this.email.Text.Contains('.'))
            {
                MessageBox.Show("Please Enter A Valid Email", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (password.Text != password.Text)
            {
                MessageBox.Show("Password doesn't match!", "Error");
                return;
            }

            if (string.IsNullOrEmpty(username.Text) || string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(password.Text))
            {
                MessageBox.Show("Please fill out all information!", "Error");
                return;
            }

            else
            {
                connection.Open();

                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM loginform.userinfo WHERE Username = @UserName", connection),
                cmd2 = new MySqlCommand("SELECT * FROM loginform.userinfo WHERE Email = @UserEmail", connection);


                cmd1.Parameters.AddWithValue("@UserName", username.Text);
                cmd2.Parameters.AddWithValue("@UserEmail", email.Text);

                bool userExists = false, mailExists = false;

                using (var dr1 = cmd1.ExecuteReader())
                    if (userExists = dr1.HasRows) MessageBox.Show("Username not available!");

                using (var dr2 = cmd2.ExecuteReader())
                    if (mailExists = dr2.HasRows) MessageBox.Show("Email not available!");


                if (!(userExists || mailExists))
                {

                    string iquery = "INSERT INTO loginform.userinfo(`ID`,`FirstName`,`LastName`,`Gender`,`Birthday`,`Email`,`Username`, `Password`) VALUES (NULL, '" + email.Text + "', '" + username.Text + "', '" + password.Text + "')";
                    MySqlCommand commandDatabase = new MySqlCommand(iquery, connection);
                    commandDatabase.CommandTimeout = 60;

                    try
                    {
                        MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    MessageBox.Show("Account Successfully Created!");

                }
            }
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            Login main = new Login();
            main.Show();
            base.Hide();
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
            Application.Exit();
        }

        private void siticoneButton19_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void siticoneToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch1.Checked == true)
            {
                username.UseSystemPasswordChar = true;
            }
            if (siticoneToggleSwitch1.Checked == false)
            {
                username.UseSystemPasswordChar = false;
            }
        }

        private void siticoneToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch2.Checked == true)
            {
                password.UseSystemPasswordChar = true;
            }
            if (siticoneToggleSwitch2.Checked == false)
            {
                password.UseSystemPasswordChar = false;
            }
        }

        private void siticoneToggleSwitch3_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch3.Checked == true)
            {
                email.UseSystemPasswordChar = true;
            }
            if (siticoneToggleSwitch3.Checked == false)
            {
                email.UseSystemPasswordChar = false;
            }
        }

        private void siticoneToggleSwitch4_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneToggleSwitch4.Checked == true)
            {

            }
            if (siticoneToggleSwitch4.Checked == false)
            {

            }
        }
    }
}
