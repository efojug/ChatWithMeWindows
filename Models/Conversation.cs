using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithMeWindows.Models
{
    internal class Conversation
    {
        public User Participant { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        public Message LastMessage => Messages.LastOrDefault();

        public Conversation()
        {
            Messages = new ObservableCollection<Message>();
        }
    }
}
