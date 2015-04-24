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
        int Ranking { get; set; }

        [DataMember]
        int AuthorID { get; }

        [DataMember]
        int ID { get; }
    }
}
