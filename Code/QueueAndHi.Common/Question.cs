﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    [DataContract]
    public class Question : IPost
    {
        public Question()
        {
            Ranking = new RankingHistory();
            Tags = new List<string>();
        }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public RankingHistory Ranking { get; set; }

        [DataMember]
        public UserInfo Author { get; set; }

        [DataMember]
        public IEnumerable<string> Tags { get; set; }

        [DataMember]
        public bool IsRecommended { get; set; }

        [DataMember]
        public int? RightAnswerId { get; set; }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public DateTime DatePosted { get; set; }

        [DataMember]
        public int AnswerCount { get; set; }
    }
}
