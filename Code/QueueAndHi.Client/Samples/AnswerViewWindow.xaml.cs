﻿using QueueAndHi.Client.Models;
using QueueAndHi.Common;
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
using System.Windows.Shapes;

namespace QueueAndHi.Client.Samples
{
    /// <summary>
    /// Interaction logic for AnswerViewWindow.xaml
    /// </summary>
    public partial class AnswerViewWindow : Window
    {
        public AnswerViewWindow()
        {
            Answer = new AnswerModel(0)
            {
                Author = new UserInfo { Username = "Nadav" },
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n\nLorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                DatePosted = new DateTime(2015, 02, 16),
                Ranking = new RankingHistoryModel { new RankingEntry(12, RankingType.Up) },
                Answered = false
            };
            InitializeComponent();
        }

        public AnswerModel Answer
        {
            get;
            private set;
        }
    }
}
