namespace QueueAndHi.Common.Logic.Validations.Question
{
    using QueueAndHi.Common.Logic.Validators;
    using System;
    using QueueAndHi.Common;
    using System.Collections.Generic;

    public class TitleValidator : ValidatorDecorator<Question>
    {
        public TitleValidator()
        { }

        public TitleValidator(IValidator<Question> validator) : base(validator)
        { }

        public override OperationResult IsValidInternal(Question question)
        {
            if (question.Title.Length < 4)
            {
                return new OperationResult(new List<string> { "The title specified is too short and should be at least 4 characters long." });
            }

            if (question.Title.Length > 40)
            {
                return new OperationResult(new List<string> { "The title specified is too long and should be 40 characters at most." });
            }

            return new OperationResult();
        }
    }
}
