using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QueueAndHi.Client
{
    public sealed class Bool2VisConverter : BooleanConverter<Visibility>
    {
        public Bool2VisConverter() :
            base(Visibility.Visible, Visibility.Collapsed) { }
    }
}
