using ChatWithMeWindows.Models;
using ChatWithMeWindows.Services;
using Microsoft.UI.Dispatching;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatWithMeWindows.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        private readonly MessagingService _messageingService;
        private string _messageToSend;
        public ObservableCollection<Message> Messages { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SendMessageCommand { get; }

        public ChatViewModel(MessagingService messagingService)
        {
            _messageingService = messagingService;
            Messages = new ObservableCollection<Message>();
            SendMessageCommand = new RelayCommand(async () => await SendMessageAsync());
            _messageingService.OnMessageReceived += OnMessageReceived;
        }

        public string MessageToSend
        {
            get => _messageToSend;
            set { _messageToSend = value; OnPropertyChanged(); }
        }

        private async Task SendMessageAsync()
        {
            if (!string.IsNullOrWhiteSpace(MessageToSend))
            {
                var message = new Message
                {
                    UserId = 0, Content = MessageToSend, Time = DateTime.Now
                };
                await _messageingService.SendMessageAsync(message);
                Messages.Add(message);
                MessageToSend = string.Empty;
            }
        }

        private void OnMessageReceived(Message message) {
            DispatcherQueue.GetForCurrentThread().TryEnqueue(() =>
            Messages.Add(message));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    } 
}
