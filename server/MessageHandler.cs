using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server
{
    internal class MessageHandler
    {
        private readonly WebSocket _webSocket;
        private string state = string.Empty;

        public MessageHandler(WebSocket webSocket)
        {
            _webSocket = webSocket;
        }

        public async Task Handle(string msgText)
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
            await _webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(state)), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}