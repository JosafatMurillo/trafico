using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SIGERSIV.Models
{
    public class Conexion
    {
        public TcpClient cliente { get; set; }
        public StreamReader streamr { get; set; }
        public StreamWriter streamw { get; set; }
        public NetworkStream stream { get; set; }
    }
}
