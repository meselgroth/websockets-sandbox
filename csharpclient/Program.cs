using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace csharpclient
{
    class Program
    {
        private static ClientWebSocket webSocket;

        static async Task Main(string[] args)
        {
            webSocket = new ClientWebSocket();
            await webSocket.ConnectAsync(new Uri("ws://localhost:5000/"), CancellationToken.None);

            var receiverTask = Receiver();
            var input = string.Empty;
            do
            {
                Console.WriteLine("Enter message to send (x to close):");
                input = Console.ReadLine();

                await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(input)), WebSocketMessageType.Text, true, CancellationToken.None);
            } while (input != "x");

            await webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "ok", CancellationToken.None);
            await receiverTask; // Receive last messages and close gracefully
        }

        private static async Task Receiver()
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult receivedMsg;

            do
            {
                receivedMsg = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var msgText = Encoding.UTF8.GetString(buffer, 0, receivedMsg.Count);
                Console.WriteLine($"Received message: {msgText}, close status: {receivedMsg.CloseStatus}");

            } while (receivedMsg.CloseStatus == null);
        }
    }
}
