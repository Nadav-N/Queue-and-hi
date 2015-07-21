using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    public class RankingHistory : List<RankingHistoryEntry>
    {
        public int Ranking
        {
            get
            {
                return this.Count(entry => entry.RankingType == RankingType.Up) - this.Count(entry => entry.RankingType == RankingType.Down);
            }
        }
    }

    public class RankingHistoryEntry
    {
        public RankingHistoryEntry (int userId, RankingType rankingType)
	{
            UserID = userId;
            RankingType = rankingType;
	}
        public int UserID { get; private set; }
        public RankingType RankingType {get; private set;}
    }

    public enum RankingType
    {
        Up,
        Down
    }
}
