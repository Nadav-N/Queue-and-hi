﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    public class Answer : IPost
    {
        public string Content { get; set; }

        public int Ranking { get; set; }

        public UserInfo Author { get; set; }

        public Question RelatedQuestion { get; set; }
    }
}
