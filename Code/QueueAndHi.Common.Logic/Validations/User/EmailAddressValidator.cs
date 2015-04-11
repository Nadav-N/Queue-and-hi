using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueAndHi.Common.Logic.Validators.User
{
    public class EmailAddressValidator : ValidatorDecorator<UserInfo>
    {
        public override OperationResult IsValidInternal(UserInfo userInfo)
        {
            // check if email of the user is valid
            return null;
        }
    }
}