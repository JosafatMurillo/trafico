using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Form1 : Form
    {

        static private NetworkStream stream;
        static private StreamWriter streamw;
        static private StreamReader streamr;
        static private TcpClient cliente = new TcpClient();
        static private string nick = "unknown";

        private delegate void DAddItem(string s);

        private void AddItem(string s)
        {
            listBox1.Items.Add(s);
        }

        public Form1()
        {
            InitializeComponent();
            listBox1.Visible = false;
            button1.Visible = false;
            textBox1.Visible = false;
        }

        void Listen()
        {
            while (cliente.Connected)
            {
                try
                {
                    this.Invoke(new DAddItem(AddItem), streamr.ReadLine());

                }
                catch
                {
                    MessageBox.Show("No se ha podido conectar al servidor");
                    Application.Exit();
                }
            }
        }

        void Conectar()
        {
            try
            {
                cliente.Connect("127.0.0.1",8000);
                if (cliente.Connected)
                {
                    Thread t = new Thread(Listen);

                    stream = cliente.GetStream();
                    streamw = new StreamWriter(stream);
                    streamr = new StreamReader(stream);

                    streamw.WriteLine(nick);
                    streamw.Flush();

                    t.Start();
                }
                else
                {
                    MessageBox.Show("Servidor no disponible");
                    Application.Exit();
                }
            }
            catch
            {
                MessageBox.Show("Servidor no disponible");
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Visible = false;
            listBox1.Visible = true;
            button1.Visible = true;
            textBox1.Visible = true;
            button2.Visible = false;

            nick = textBox3.Text;

            Conectar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            streamw.WriteLine(textBox1.Text);
            streamw.Flush();
            textBox3.Clear();
        }
    }
}
