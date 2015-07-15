using System.ComponentModel;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class AnswerViewModel : INotifyPropertyChanged
    {
        public AnswerViewModel()
        {
                
        }

        public AnswerModel Answer
        {
            get;
            set;
        }

        public ICommand RankUp { get; set; }

        public ICommand RankDown { get; set; }

        public ICommand Delete { get; set; }

        public ICommand MarkAsRight { get; set; }

        public ICommand UnmarkAsRight { get; set; }

        public bool IsMarkAsRightVisible { get; set; }

        public bool IsUnmarkAsRightVisible { get; set; }

        internal void OnPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
