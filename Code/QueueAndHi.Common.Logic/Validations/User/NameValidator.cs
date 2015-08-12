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
            throw new NotImplementedException();
        }
    }
}
