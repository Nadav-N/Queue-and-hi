using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    public class RankingHistory : List<RankingEntry>
    {
        public int OverallRanking
        {
            get
            {
                return this.Count(entry => entry.RankingType == RankingType.Up) - this.Count(entry => entry.RankingType == RankingType.Down);
            }
        }
    }

    public class RankingEntry
    {
        public RankingEntry(int userId, RankingType rankingType)
        {
            UserID = userId;
            RankingType = rankingType;
        }
        public int UserID { get; private set; }
        public RankingType RankingType { get; private set; }
    }

    public enum RankingType
    {
        Up,
        Down
    }
}
