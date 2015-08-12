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
            if (userInfo.Ranking < 20 && !userInfo.IsAdmin)
            {
                return new OperationResult(new[] { "User does not have high enough ranking to recommend question." });
            }

            return new OperationResult();
        }
    }
}
