using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QueueAndHi.Client
{
    /// <summary>
    /// Interaction logic for SearchResults.xaml
    /// </summary>
    public partial class SearchResults : Window
    {
        public SearchResults()
        {
            Questions = new ObservableCollection<QuestionInfo>
                {
                    new QuestionInfo(new Question())
                    {
                        Author = "Noffar",
                        AnswersCount = 1,
                        VotesCount = 2,
                        Title = "Hint text in wpf textbox",
                        Tags = new ObservableCollection<string>
                        {
                            "WPF", "textbox"
                        }
                    },
                    new QuestionInfo(new Question())
                    {
                        Author = "Itsik",
                        AnswersCount = 3,
                        VotesCount = 1,
                        Title = "TwoWay data binding in WPF",
                        Tags = new ObservableCollection<string>
                        {
                            "WPF", "Data Binding"
                        },
                        IsRecommended = true
                    },
                    new QuestionInfo(new Question())             
                    {
                        Author = "Idan",
                        AnswersCount = 0,
                        VotesCount = 0,
                        Title = "WPF advantages over HTML5?",
                        Tags = new ObservableCollection<string>
                        {
                            "HTML5", "WPF"
                        },
                        IsRecommended = true
                    },
                    new QuestionInfo(new Question())                
                    {
                        Author = "Michal",
                        AnswersCount = 1,
                        VotesCount = 5,
                        Title = "Async operations in WPF UI",
                        Tags = new ObservableCollection<string>
                        {
                            "Async"
                        }
                    }
                };

            InitializeComponent();
        }

        public ObservableCollection<QuestionInfo> Questions
        {
            get;
            set;
        }
    }
}
