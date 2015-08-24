using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QueueAndHi.Client
{
    public sealed class Int2VisConverter : PositiveIntConverter<Visibility>
    {
        public Int2VisConverter() :
            base(Visibility.Collapsed, Visibility.Visible) { }
    }
}
