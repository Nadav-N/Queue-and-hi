﻿using System;
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
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        public UserManagement()
        {
            InitializeComponent();
            Users = new ObservableCollection<UserAccountModel>
            {
                new UserAccountModel
                {
                    Username = "Nadav",
                    Points = 4
                },
                new UserAccountModel
                {
                    Username = "Danny",
                    IsAdmin = true,
                    Points = 134
                },
                new UserAccountModel
                {
                    Username = "Tomer",
                    Points = 12
                },
                new UserAccountModel
                {
                    Username = "Dr. Evil",
                    Points = -27,
                    IsMuted = true
                }
            };
        }

        public ObservableCollection<UserAccountModel> Users { get; set; }
    }
}
