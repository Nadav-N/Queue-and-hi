using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class QuestionListViewModel : INotifyPropertyChanged
    {
        public QuestionListViewModel(NavigationManager navigationManager)
        {
            Questions = new ObservableCollection<QuestionInfo>();
        }

        public ICommand NavigateToQuestion { get; set; }

        public ObservableCollection<QuestionInfo> Questions
        {
            get;
            set;
        }

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
