using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    public interface IPost
    {
        [DataMember]
        string Content { get; }

        [DataMember]
        UserInfo Author { get; }

        [DataMember]
        int ID { get; }

        [DataMember]
        DateTime DatePosted { get; }

        [DataMember]
        RankingHistory Ranking { get; set; }
    }
}
