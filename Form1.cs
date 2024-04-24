using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QR_Publisher
{
    public partial class Form1 : Form
    {
        private Settings set;
        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            set = new Settings();
            if (!set.getLoaded())
            {
                MessageBox.Show("Невозможно прочитать файл настроек!");
                Application.Exit();
            }
            timer1.Interval = int.Parse(set.getUpdateTime());
            if (set.getAutoUpdate() == "1")
            {
                timer1.Enabled = true;
                button1.Enabled = false;
            }
        }

        private void updateScreen(String qrStr,String txt1,String txt2)
        {
            SerialPort port = new SerialPort(set.getPortName(), 115200, Parity.None, 8, StopBits.One);
            port.Open();
            String outStr = "[QL]" + qrStr + "\r\n" + "[T1]" + txt1 + "\r\n" + "[T2]" + txt2+"\r\n";
            byte[] message = System.Text.Encoding.UTF8.GetBytes(outStr);
            port.Write(message, 0,message.Length);
            port.Close(); 
        }
        
        private String parseSum(String qrText)
        {
            int sumIndex = qrText.IndexOf("sum=");
            int curIndex = qrText.IndexOf("cur=");
            String sumStr = qrText.Substring(sumIndex + 4, curIndex - (sumIndex + 5));
            int sumInt = int.Parse(sumStr)/100;
            return sumInt.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            String path = set.getPath();
            if (!path.Equals(""))
            {
                DataDecoder decoder = new DataDecoder(path);
                String decData = decoder.getDecodedData();
                if (decData.Equals(""))
                {
                    return;
                }
                label1.Text = "=WWW.NSTK.PRO=";
                label2.Text = "Сумма: " + parseSum(decData) + " руб.";
                textBox1.Text = textBox1.Text + decData + "\r\n";
                updateScreen(decData, label1.Text, label2.Text);
                decoder.clearFolder();
            } else {
                MessageBox.Show("Не найден файл настроек (settings.ini), или файл настроек не корректен!");
            }

}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            String path = set.getPath();
            if (!path.Equals(""))
            {
                DataDecoder decoder = new DataDecoder(path);
                String decData = decoder.getDecodedData();
                if (decData.Equals(""))
                {
                    return;
                }
                //yourLabelName.Width = TextRenderer.MeasureText(yourLabelName.Text, yourLabelName.Font).Width;
                label1.Text = "=WWW.NSTK.PRO=";
                label2.Text = "Сумма: " + parseSum(decData) + " руб.";
                textBox1.Text = textBox1.Text + decData + "\r\n";
                updateScreen(decData, label1.Text, label2.Text);
                decoder.clearFolder();
            }
        }
    }
}
