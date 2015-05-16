﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class UserManagementViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UserAccountModel> Users
        {
            get;
            set;
        }

        public ICommand ApplyChanges { get; set; }

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
