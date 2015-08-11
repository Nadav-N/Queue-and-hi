using QueueAndHi.Common.Logic.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Logic.Validations.User
{
    public class RankDownValidator : ValidatorDecorator<UserInfo>
    {
        public override OperationResult IsValidInternal(UserInfo userInfo)
        {
            if (userInfo.Ranking < 10 && !userInfo.IsAdmin)
            {
                return new OperationResult(new [] { "User does not have high enough ranking to rank down a post." });
            }

            return new OperationResult();
        }
    }
}
