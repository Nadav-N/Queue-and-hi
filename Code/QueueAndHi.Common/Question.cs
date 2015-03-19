using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    public class Question : IPost
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Ranking { get; set; }
        public UserInfo Author { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public bool IsRecommended { get; set; }
    }
}
