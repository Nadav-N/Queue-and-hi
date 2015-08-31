using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueueAndHi.Common.Logic.Validators;

namespace QueueAndHi.Common.Logic.Validations.User
{
    public class DeleteAnswerValidator : ValidatorDecorator<UserInfo>
    {
        public override OperationResult IsValidInternal(UserInfo userInfo)
        {
            if (userInfo.IsMuted)
            {
                return new OperationResult(new[] { "Muted User cannot delete answer" });
            }

            return new OperationResult();
        }
    }
}
