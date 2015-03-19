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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QueueAndHi.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Questions = new ObservableCollection<QuestionInfo>
                {
                    new QuestionInfo
                    {
                        Author = "Nadav",
                        AnswersCount = 1,
                        VotesCount = 2,
                        Title = "Creating a new project in visual studio",
                        Tags = new ObservableCollection<string>
                        {
                            "visual studio", "csproj"
                        }
                    },
                    new QuestionInfo
                    {
                        Author = "Tomer",
                        AnswersCount = 3,
                        VotesCount = 1,
                        Title = "TwoWay data binding in WPF",
                        Tags = new ObservableCollection<string>
                        {
                            "WPF", "Data Binding"
                        },
                        IsRecommended = true
                    },
                    new QuestionInfo                    
                    {
                        Author = "Danny",
                        AnswersCount = 0,
                        VotesCount = 0,
                        Title = "Entity framework Code-first error",
                        Tags = new ObservableCollection<string>
                        {
                            "Entity Framework", "Code first"
                        },
                        IsRecommended = true
                    },
                    new QuestionInfo                    
                    {
                        Author = "Yifat",
                        AnswersCount = 1,
                        VotesCount = 5,
                        Title = "Async operations in WPF UI",
                        Tags = new ObservableCollection<string>
                        {
                            "Async"
                        }
                    },
                    new QuestionInfo                    
                    {
                        Author = "Haim",
                        AnswersCount = 0,
                        VotesCount = 3,
                        Title = "How to seperate BI layer from the client"
                    }
                };

            InitializeComponent();
        }

        public ObservableCollection<QuestionInfo> Questions
        {
            get;
            private set;
        }
    }
}
