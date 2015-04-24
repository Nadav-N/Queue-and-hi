using System;
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
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public int Ranking { get; set; }

        [DataMember]
        public int AuthorID { get; set; }

        [DataMember]
        public IEnumerable<string> Tags { get; set; }

        [DataMember]
        public bool IsRecommended { get; set; }

        [DataMember]
        public int? RightAnswerId { get; set; }

        [DataMember]
        public int ID { get; private set; }
    }
}
