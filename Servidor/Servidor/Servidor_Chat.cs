using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace Servidor
{
    class Servidor_Chat
    {
        private TcpListener server;
        private TcpClient cliente = new TcpClient();
        private IPEndPoint ipendpoint = new IPEndPoint(IPAddress.Any, 8000);
        List<Connection> list = new List<Connection>();

        Connection con;

        private struct Connection
        {
            public NetworkStream stream;
            public StreamWriter streamw;
            public StreamReader streamr;
            public string nick;
        }

        public Servidor_Chat()
        {
            Inicio();
        }

        public void Inicio()
        {
            Console.WriteLine("Servidor OK!");
            server = new TcpListener(ipendpoint);
            server.Start();
            while (true)
            {
                cliente = server.AcceptTcpClient();

                con = new Connection();
                con.stream = cliente.GetStream();
                con.streamw = new StreamWriter(con.stream);
                con.streamr = new StreamReader(con.stream);

                con.nick = con.streamr.ReadLine();

                list.Add(con);
                //Console.WriteLine(con.nick + " se ha conectado");

                Thread t = new Thread(Escuchar_conexion);
                t.Start();
            }

            void Escuchar_conexion()
            {
                Connection hcon = con;
                do
                {
                    try
                    {
                        string tmp = hcon.streamr.ReadLine();
                        //Console.WriteLine(hcon.nick + ": " + tmp);
                        Console.WriteLine(tmp);
                        foreach (Connection c in list)
                        {
                            try
                            {
                                //c.streamw.WriteLine(hcon.nick + ": " + tmp);
                                c.streamw.WriteLine(tmp);
                                c.streamw.Flush();
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }
                    catch (Exception e)
                    {
                        list.Remove(hcon);
                        Console.WriteLine(con.nick + " se a desconectado");
                        break;
                    }
                } while (true);
            }
        }
    }
}
