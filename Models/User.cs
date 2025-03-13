using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithMeWindows.Models
{
    public class User
    {
        public string UserId { get ; set; }
        public string UserName { get; set; }
        public string ServerAddress { get; set; }
        public string Token { get; set; }

        //TODO loggedIn = true
    }
}
