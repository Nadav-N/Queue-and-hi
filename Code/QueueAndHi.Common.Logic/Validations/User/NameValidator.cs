using QueueAndHi.Common.Logic.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Logic.Validations.User
{
    public class NameValidator : ValidatorDecorator<string>
    {
        public override OperationResult IsValidInternal(string username)
        {
            //make sure it starts with a letter. 
            //that it's at least 6 characters long
            //and that it doesn't already exist... - that's a tricky one
            bool ok = true;
            OperationResult or = new OperationResult(new List<string>());
            bool isLetter = !String.IsNullOrEmpty(username) && Char.IsLetter(username[0]);
            if (!isLetter)
            {
                ok = false;
                or.ErrorMessages.Add("User name should start with a letter of the alphabet");
            }

            if (username.Length < 6)
            {
                or.ErrorMessages.Add("User name should be at least 6 characters long");
                ok = false;
            }

            if (ok)
                return new OperationResult<string>(username);
            else
                return or;

            
        }
    }
}
