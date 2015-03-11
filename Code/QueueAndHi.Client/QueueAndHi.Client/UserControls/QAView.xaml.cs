using System;
using System.Collections.Generic;
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
    /// Interaction logic for QAView.xaml
    /// </summary>
    public partial class QAView : UserControl
    {
        public QAView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The rating of the user viewing the question/answer (affects permissions)
        /// </summary>
        public int UserRating { get; set; }

        /// <summary>
        /// Is this part a question or an answer
        /// </summary>
        public bool IsQuestion { get; set; }

        /// <summary>
        /// A question has a header, an answer doesn't
        /// </summary>
        public bool HasLabel { get { return IsQuestion; } }

        /// <summary>
        /// Question subject
        /// </summary>
        public string QuestionLabel { get; set; }

        /// <summary>
        /// detailed question/answer text
        /// </summary>
        public string QAText { get; set; }

        /// <summary>
        /// The current rating of the question/answer
        /// </summary>
        public int QARating { get; set; }

        /// <summary>
        /// Is the current user the owner of the question/answer (original user that posted it).
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// Allow editing the text to the original poster
        /// </summary>
        public bool AllowEditing { get { return IsOwner; } }
        
        /// <summary>
        /// Allow marking the question as answered to the user that originally asked the question
        /// </summary>
        public bool AllowMarkAsAnswered { get { return IsQuestion && IsOwner; } }

        /// <summary>
        /// Enable negative rating for the current user if has rating over 10
        /// </summary>
        public bool AllowRateDecrease { get { return UserRating > 10 ? true : false; } }

        /// <summary>
        /// enable marking as favorite (questions only) for the current user if has rating over 20
        /// </summary>
        public bool AllowMarkAsFavorite { get { return IsQuestion && UserRating > 20 ? true : false; } }


        private bool isAnswered;
        /// <summary>
        /// Is the answer marked as answered
        /// </summary>
        public bool Answered
        {
            get { return isAnswered; }
            set
            {
                isAnswered = value;
                if (isAnswered)
                {
                    AnsweredImage = @"..\resources\answered.png";
                }
                else
                {
                    AnsweredImage = @"..\resources\unanswered.png";
                }
            }
        }

        public string AnsweredImage
        {
            get;
            private set;
        }

    }
}
