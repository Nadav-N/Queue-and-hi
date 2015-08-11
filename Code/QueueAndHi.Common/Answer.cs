using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    [DataContract]
    public class Answer : IPost
    {
        public Answer()
        {
            Ranking = new RankingHistory();
        }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public RankingHistory Ranking { get; set; }

        [DataMember]
        public UserInfo Author { get; set; }

        [DataMember]
        public int RelatedQuestionId { get; set; }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public DateTime DatePosted { get; set; }
    }
}
