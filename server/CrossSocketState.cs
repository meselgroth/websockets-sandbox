using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server
{
    public class SocketManager
    {
        private readonly List<WebSocket> _webSockets = new List<WebSocket>();
        private string state = string.Empty;

        public SocketManager()
        {
        }

        public async Task<string> ProcessCommand(string msgText)
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
            await UpdateAllSockets();
            return state;
        }

        private async Task UpdateAllSockets()
        {
            foreach (var socket in _webSockets)
            {
                await socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(state)), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public void AddSocket(WebSocket webSocket)
        {
            _webSockets.Add(webSocket);
        }
    }
}