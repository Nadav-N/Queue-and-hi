using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    public class UserInfo
    {
        public int Ranking { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsMuted { get; set; }
    }
}
