using QueueAndHi.Common.Logic.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Logic.Validations.Question
{
    public class TitleValidator : ValidatorDecorator<Common.Question>
    {
        public TitleValidator()
        { }

        public TitleValidator(IValidator<Common.Question> validator) : base(validator)
        { }

        public override OperationResult IsValidInternal(Common.Question userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
