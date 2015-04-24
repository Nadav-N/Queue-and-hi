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
        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public int Ranking { get; set; }

        [DataMember]
        public int AuthorID { get; set; }

        [DataMember]
        public int RelatedQuestionId { get; set; }

        [DataMember]
        public int ID { get; set; }
    }
}
