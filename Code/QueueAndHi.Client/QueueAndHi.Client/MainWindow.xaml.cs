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
