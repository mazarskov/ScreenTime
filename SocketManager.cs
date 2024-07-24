using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SocketIOClient;


namespace ScreenTime
{
    public class SocketManager : IDisposable
    {
        private readonly SocketIOClient.SocketIO client;
        private bool isDisposed = false;

        public event EventHandler<string> StateChanged;

        public SocketManager()
        {
            client = new SocketIOClient.SocketIO("https://azar.ee");
            client.OnConnected += (sender, e) => Console.WriteLine("Connected to server");
            client.OnDisconnected += (sender, e) => Console.WriteLine("Disconnected from server");
        }

        public async Task ConnectAsync()
        {
            if (!client.Connected)
            {
                await client.ConnectAsync();
            }
        }

        public async Task SendUpdateAsync(string newState)
        {
            if (!client.Connected)
            {
                await ConnectAsync();
            }
            await client.EmitAsync("update_state", new { state = newState });
            StateChanged?.Invoke(this, newState);
        }

        public async Task DisconnectAsync()
        {
            if (client.Connected)
            {
                await client.DisconnectAsync();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    DisconnectAsync().Wait();
                    client.Dispose();
                }

                isDisposed = true;
            }
        }
    }
}
