using ChatWithMeWindows.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatWithMeWindows.Services
{
    public class MessagingService
    {
        private ClientWebSocket _webSocket;
        private CancellationTokenSource _cts;
        public event Action<Message> OnMessageReceived;

        public async Task ConnectAsync(string uri)
        {
            _webSocket = new ClientWebSocket();
            _cts = new CancellationTokenSource();
            await _webSocket.ConnectAsync(new Uri(uri), _cts.Token);
            StartListening();
        }

        private async void StartListening()
        {
            var buffer = new byte[1024 * 4];
            try
            {
                while (_webSocket.State == WebSocketState.Open)
                {
                    var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);
                    if (result.MessageType == WebSocketMessageType.Close) await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    else
                    {
                        var jsonMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        var message = JsonConvert.DeserializeObject<Message>(jsonMessage);
                        OnMessageReceived.Invoke(message);
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task SendMessageAsync(Message message)
        {
            var jsonMessage = JsonConvert.SerializeObject(message);
            var bytes = Encoding.UTF8.GetBytes(jsonMessage);
            var buffer = new ArraySegment<byte>(bytes);
            await _webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, _cts.Token);
        }

        public async Task DisconnectAsync()
        {
            if (_webSocket != null)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnect", CancellationToken.None);
                _webSocket.Dispose();
            }
        }
    }
}
