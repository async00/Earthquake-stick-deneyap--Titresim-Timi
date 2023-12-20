using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace EarthquakeStick_deneyap
{
    public partial class Form1 : Form
    {
        private TcpListener server;
        internal string ensongelenveri = string.Empty;

        public Form1()
        {
           
            InitializeComponent();

        }
    
        void TCPthread()
        {

            try
            {
                string ipAddress = "127.0.0.1"; // localhost
                int port = 80;
                server = new TcpListener(IPAddress.Parse(ipAddress), port);
                server.Start();
                textBox1.Text=textBox1.Text+$"Sunucu Aktif Sesler Dinleniyor !!... (localhost:{port})";
                activestickdisplayint.Text = "Aktif çubuk 0.001";
                servercheckdisplaystring.ForeColor = System.Drawing.Color.Green;
                servercheckdisplaystring.Text = "Çalışıyor :)";
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    byte[] buffer = new byte[1024]; //maxcharsize
                    int bytesRead;
                    NetworkStream stream = client.GetStream();
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        string clearedata = receivedData.Replace("X", "\r\n")+"--------------------------------------------------------------\r\n";
                        textBox1.Text = textBox1.Text + clearedata;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"exception: {ex.Message}");
                MessageBox.Show($"RAW exception: {ex.ToString()}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //yeah open listener
            textBox1.ReadOnly = true;

            Thread maintcpthread = new Thread(TCPthread);
            maintcpthread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
