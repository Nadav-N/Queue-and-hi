using QueueAndHi.Common;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace QueueAndHi.Client.Models
{
    public class RankingHistoryModel : ObservableCollection<RankingEntry>
    {
        public RankingHistoryModel()
        {
            CollectionChanged += RankingHistoryModel_CollectionChanged;
        }

        public RankingHistoryModel(RankingHistory rankingHistory)
            : base(rankingHistory)
        {
            CollectionChanged += RankingHistoryModel_CollectionChanged;
        }

        public RankingHistory ToExternal()
        {
            return new RankingHistory(this);
        }

        public int OverallRanking
        {
            get
            {
                return ToExternal().OverallRanking;
            }
        }

        private void RankingHistoryModel_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("OverallRanking"));
        }
    }
}
