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
    /// Interaction logic for MyQuestions.xaml
    /// </summary>
    public partial class MyQuestions : Window
    {
        public MyQuestions()
        {
            Questions = new ObservableCollection<QuestionInfo>
            {
                    new QuestionInfo(new Question())
                    {
                        Author = "Yoram",
                        AnswersCount = 0,
                        Ranking = 1,
                        Title = "Create a search animation sliding the text input part from the right to left",
                        Tags = new ObservableCollection<string>
                        {
                            "WPF", "animation"
                        }
                    },
                    new QuestionInfo(new Question())
                    {
                        Author = "Yoram",
                        AnswersCount = 2,
                        Ranking = 2,
                        Title = "Play a specific notification sound"
                    },
                    new QuestionInfo(new Question())    
                    {
                        Author = "Yoram",
                        AnswersCount = 1,
                        Ranking = 2,
                        Title = "Stopwatch count up set time",
                        Tags = new ObservableCollection<string>
                        {
                            "Stopwatch", "C#"
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
