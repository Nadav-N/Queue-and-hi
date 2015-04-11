using QueueAndHi.Common.Logic.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Logic.Validations.Question
{
    public class MarkRightAnswerValidator : ValidatorDecorator<Answer>
    {
        public override OperationResult IsValidInternal(Answer userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
