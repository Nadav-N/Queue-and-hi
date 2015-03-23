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
        public string Content { get; }

        [DataMember]
        public int Ranking { get; set; }

        [DataMember]
        public UserInfo Author { get; }

        [DataMember]
        public Question RelatedQuestion { get; }

        [DataMember]
        public int ID { get; }
    }
}
