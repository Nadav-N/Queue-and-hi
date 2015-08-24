using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace QueueAndHi.Client
{
    public class Bool2HiddenConverter : BooleanConverter<Visibility>
    {
        public Bool2HiddenConverter() :
            base(Visibility.Visible, Visibility.Hidden) { }
    }
}
