using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebSockets();

            app.Use(async (context, next) =>
            {
                Console.WriteLine($"IsWebSocketRequest: {context.WebSockets.IsWebSocketRequest}");

                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult receivedMsg;
                do
                {
                    var arraySegment = new ArraySegment<byte>(buffer);
                    receivedMsg = await webSocket.ReceiveAsync(arraySegment, CancellationToken.None);
                    Console.WriteLine($"Received message: {Encoding.UTF8.GetString(buffer, 0, receivedMsg.Count)}");
                    
                } while (receivedMsg.CloseStatus == null);

                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "ok", CancellationToken.None);
            });
        }
    }
}