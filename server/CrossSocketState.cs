using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server
{
    internal class CrossSocketState
    {
        private string state = string.Empty;

        public CrossSocketState()
        {
        }

        public string ProcessCommand(string msgText)
        {
            switch (msgText)
            {
                case "start":
                    state = "started";
                    break;
                case "stop":
                    state = "stopped";
                    break;
            }
            return state;
        }
    }
}