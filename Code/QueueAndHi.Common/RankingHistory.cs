using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    [DataContract]
    public class RankingHistory : List<RankingEntry>
    {
        [IgnoreDataMember]
        public int OverallRanking
        {
            get
            {
                return this.Count(entry => entry.RankingType == RankingType.Up) - this.Count(entry => entry.RankingType == RankingType.Down);
            }
        }
    }

    [DataContract]
    public class RankingEntry
    {
        public RankingEntry(int userId, RankingType rankingType)
        {
            UserID = userId;
            RankingType = rankingType;
        }

        [DataMember]
        public int UserID { get; private set; }

        [DataMember]
        public RankingType RankingType { get; private set; }
    }

    [DataContract(Name = "RankingType")]
    public enum RankingType
    {
        [EnumMember]
        Up,
        [EnumMember]
        Down
    }
}
