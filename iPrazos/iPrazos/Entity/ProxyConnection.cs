using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPrazos.Entity
{
    public class ProxyConnection
    {
        public string IpAdress { get; private set; }
        public int Port { get; private set; }
        public string Country { get; private set; }
        public string Protocol { get; private set; }
        public ProxyConnection(string ipAdress, int port, string country, string protocol)
        {
            IpAdress = ipAdress;
            Port = port;
            Country = country;
            Protocol = protocol;
        }
    }
}
