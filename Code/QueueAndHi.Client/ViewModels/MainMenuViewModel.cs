using QueueAndHi.Client.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        public MainMenuViewModel()
        {

        }

        public bool IsUserAdmin { get; set; }

        [Import]
        public NavigateHomeCommand NavigateHome { get; set; }

        public ICommand NavigateMyQuestions { get; set; }

        public ICommand NavigateNewQuestion { get; set; }

        public ICommand NavigateUserManagement { get; set; }

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
