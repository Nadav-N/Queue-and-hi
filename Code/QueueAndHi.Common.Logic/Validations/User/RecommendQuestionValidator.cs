using QueueAndHi.Common.Logic.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Logic.Validations.User
{
    public class RecommendQuestionValidator : ValidatorDecorator<UserInfo>
    {
        public override OperationResult IsValidInternal(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
