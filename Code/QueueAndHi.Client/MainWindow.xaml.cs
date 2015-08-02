using QueueAndHi.BL;
using QueueAndHi.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using QueueAndHi.BL.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Client.Authentication;

namespace QueueAndHi.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            AuthTokenCache tokenCache = new AuthTokenCache();
            IAuthTokenSerializer authTokenSerializer = new AuthTokenSerializer();
            NavigationManager navigationManager = new NavigationManager();
            MainMenuVM = new MainMenuViewModel(navigationManager, new PostServices());
            MainToolbarVM = new MainToolbarViewModel(navigationManager, new UserServices(authTokenSerializer));

            /*UserInfo currentUser = new UserInfo
            {
                ID = 123,
                IsAdmin = false,
                Ranking = 4,
                Username = "Nadav"
            };

            AuthenticationTokenSingleton.Instance.AuthenticatedIdentity = new AuthenticatedIdentity
            {
                Token = new AuthenticationToken("1"),
                UserID = 123
            };
            AuthenticationTokenSingleton.Instance.AuthenticatedUser = currentUser;

            DiscussionThread dt = new DiscussionThread();
            dt.Question = new Question()
            {
                AnswerCount = 2,
                Content = "What is this question?",
                Author = currentUser,
                DatePosted = DateTime.Now,
                ID = 11,
                IsRecommended = true,
                RightAnswerId = 12,
                Tags = new List<string> { "C#", ".NET" },
                Title = "Hello World",
                Ranking = new RankingHistory { new RankingEntry(531, RankingType.Down) }
            };
            dt.Answers = new List<Answer>
            {
                new Answer
                {
                    ID = 183,
                    Author = currentUser,
                    DatePosted = DateTime.UtcNow,
                    Content = "This is not a right answer :(",
                    Ranking = new RankingHistory()
                },
                new Answer
                {
                    ID = 12,
                    Author = new UserInfo()
                    {
                        ID = 125,
                        IsAdmin = true,
                        IsMuted = false,
                        Ranking = 10,
                        Username = "IAmAdmin"
                    },
                    Content = "This is the right answer",
                    DatePosted = DateTime.Now,
                    RelatedQuestionId = 11,
                    Ranking = new RankingHistory { new RankingEntry(123, RankingType.Up) }
                }
            };

            MainVM = new QuestionViewModel(dt, new PostServices(), navigationManager, new PostQueries());*/
            MainVM = new NewAnswerViewModel(12, new PostServices());
            NotificationsVM = new NotificationsViewModel();

            navigationManager.NavigationRequested += navigationManager_NavigationRequested;

            InitializeComponent();
        }

        void navigationManager_NavigationRequested(object sender, NavigationRequestedEventArgs e)
        {
            object oldVM = MainVM;
            MainVM = e.ViewModelToNavigate;
            IDisposable disposableVM = oldVM as IDisposable;

            if (disposableVM != null)
            {
                disposableVM.Dispose();
            }

            OnPropertyChanged("MainVM");
        }

        public object MainVM
        {
            get;
            set;
        }

        public NotificationsViewModel NotificationsVM
        {
            get;
            set;
        }

        public MainMenuViewModel MainMenuVM
        {
            get;
            set;
        }

        public MainToolbarViewModel MainToolbarVM
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
