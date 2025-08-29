using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SREUOU_ACCOUNT_NUKER
{
    public partial class TEST : Form
    {
        public TEST()
        {
            InitializeComponent();
        }

        private void TEST_Load(object sender, EventArgs e)
        {
            QRCoder.QRCodeGenerator qrGEN = new QRCoder.QRCodeGenerator();
            var MyData = qrGEN.CreateQrCode("test", QRCoder.QRCodeGenerator.ECCLevel.H);
            var code = new QRCoder.QRCode(MyData);
            pictureBox1.Image = code.GetGraphic(3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QRCoder.QRCodeGenerator qrGEN = new QRCoder.QRCodeGenerator();
            var MyData = qrGEN.CreateQrCode("test", QRCoder.QRCodeGenerator.ECCLevel.H);
            var code = new QRCoder.QRCode(MyData);
            pictureBox1.Image = code.GetGraphic(3);
        }
    }
}
