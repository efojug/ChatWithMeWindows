using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithMeWindows.Models
{
    internal class Message
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public long Time { get; set; }
    }
}
