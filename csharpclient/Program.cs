using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace csharpclient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var webSocket = new ClientWebSocket();
            await webSocket.ConnectAsync(new Uri("ws://localhost:5000/"), CancellationToken.None);

            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult receivedMsg;
            do
            {
                receivedMsg = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var msgText = Encoding.UTF8.GetString(buffer, 0, receivedMsg.Count);
                Console.WriteLine($"Received message: {msgText}");

            } while (receivedMsg.CloseStatus == null);
        }
    }
}
