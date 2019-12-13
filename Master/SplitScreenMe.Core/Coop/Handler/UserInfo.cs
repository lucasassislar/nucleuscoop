using System.Net;
using System.Net.Sockets;

namespace Nucleus.Gaming {
    public class UserInfo {
        public string GetLocalIP() {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }
    }
}
