using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.BL.Validators
{
    public interface IUserValidator
    {
        bool IsValid(UserInfo user);
    }
}
