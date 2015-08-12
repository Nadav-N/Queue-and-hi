using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QueueAndHi.Common.Logic.Validators;

namespace QueueAndHi.Common.Logic.Validations.User
{
    public class EmailAddressValidator : ValidatorDecorator<string>
    {
        public override OperationResult IsValidInternal(string email)
        {
            // check if email of the user is valid
            return null;
        }
    }
}