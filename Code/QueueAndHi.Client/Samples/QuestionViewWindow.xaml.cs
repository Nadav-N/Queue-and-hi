using QueueAndHi.Client.Models;
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

namespace QueueAndHi.Client.Samples
{
    /// <summary>
    /// Interaction logic for QuestionViewWindow.xaml
    /// </summary>
    public partial class QuestionViewWindow : Window
    {
        public QuestionViewWindow()
        {
            
            Question = new QuestionModel
            {
                Author = new UserInfo {Username = "Tomer"},
                Content = "How can I use the inverted value of a BooleanToVisibilityConverter\n\nFor Example, I want to be able to Show one image if the Value is true, but hide it and show another if the value is false",
                DatePosted = new DateTime(2015, 02, 15),
                Title = "Inverting BooleanToVisibilityConverter",
                Ranking = new RankingHistoryModel { new RankingEntry(12, RankingType.Up), new RankingEntry(1, RankingType.Up) },
                IsRecommended = false,
                Tags = new ObservableCollection<string>
                        {
                            "WPF", "animation", "programming", "homework", "gui", "code", "bll", "dll"
                        },
                Answers = new ObservableCollection<AnswerModel> 
                {
                    new AnswerModel(0)
                    {
                        Author = new UserInfo {Username = "Nadav"},
                        Content = "Look into writing a custom inverter, that way you can do it anything you'd like.",
                        DatePosted = new DateTime(2015, 02, 16),
                        Ranking = new RankingHistoryModel { new RankingEntry(12, RankingType.Down), new RankingEntry(1, RankingType.Up) },
                        Answered= false
                    },
                    new AnswerModel(0)
                    {
                        Author = new UserInfo {Username = "Dani"},
                        Content = "There's an example in the course book. check it out.",
                        DatePosted = new DateTime(2015, 02, 17),
                        Ranking =  new RankingHistoryModel { new RankingEntry(12, RankingType.Down), new RankingEntry(1, RankingType.Down) },
                        Answered= true
                    }
                }
            };

            System.Diagnostics.Debug.WriteLine(Question.AnswerCount);
            InitializeComponent();

            
            
        }


        public QuestionModel Question
        {
            get;
            private set;
        }

    }
}
