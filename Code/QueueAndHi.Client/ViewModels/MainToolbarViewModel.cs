﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class MainToolbarViewModel : INotifyPropertyChanged
    {
        public MainToolbarViewModel(NavigationManager navigationManager)
        {

        }

        public string SearchText { get; set; }

        public ICommand Search { get; set; }

        public ICommand Login { get; set; }

        public ICommand Register { get; set; }

        public ICommand Logout { get; set; }

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
