using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.Logic
{
    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public SmtpConfiguration()
        {
        }

        public SmtpConfiguration(string host, int port)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(host);

            if (port < 1 || port > 65535)
                throw new ArgumentOutOfRangeException(nameof(port));

            Host = host;
            Port = port;
        }

        public SmtpConfiguration(string host, int port, string username, string password)
            : this(host, port)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(username);

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(password);

            Username = username;
            Host = host;
        }
    }
}
