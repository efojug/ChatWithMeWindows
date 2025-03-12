using ChatWithMeWindows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ChatWithMeWindows.Services
{
    internal class WebSocketService
    {
        private ClientWebSocket _webSocket;
        private readonly string _webSocketUrl;
        private CancellationTokenSource _cts;

        public event EventHandler<Message> MessageReceived;

        public WebSocketService(string webSocketrl)
        {
            _webSocketUrl = webSocketrl;
        }

        public async Task ConnectAsync(string token)
        {
            _webSocket = new ClientWebSocket();
            _webSocket.Options.SetRequestHeader("Authorization", $"Bearer {token}");
            _cts = new CancellationTokenSource();
            await _webSocket.ConnectAsync(new Uri($"{_webSocketUrl}"), _cts.Token);
            _ = ReceiveMessagesAsync();
        }

        private async Task ReceiveMessagesAsync()
        {
            var buffer = new byte[4096];

            try
            {
                while (_webSocket.State == WebSocketState.Open)
                {
                    var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        var messageObj = JsonSerializer.Deserialize<Message>(message);
                        MessageReceived?.Invoke(this, messageObj);
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task SendMessageAsync(Message message)
        {
            if (_webSocket.State != WebSocketState.Open) throw new InvalidOperationException("WebSocket is not connected.");

            var json = JsonSerializer.Serialize(message);
            var bytes = Encoding.UTF8.GetBytes(json);

            await _webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, _cts.Token);
        }

        public async Task DisconnectAsync()
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }

            _cts.Cancel();
            _cts.Dispose();
            _webSocket.Dispose();
        }
    }
}
