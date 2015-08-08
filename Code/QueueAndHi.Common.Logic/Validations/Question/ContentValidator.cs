using QueueAndHi.Common.Logic.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Logic.Validations.Question
{
    public class ContentValidator : ValidatorDecorator<IPost>
    {
        public ContentValidator()
        {}

        public ContentValidator(IValidator<IPost> validator) : base(validator)
        {}

        public override OperationResult IsValidInternal(IPost post)
        {
            if (post.Content.Length < 8)
            {
                return new OperationResult(new List<string> { "The content is too short and should be at least 8 characters long." });
            }

            if (post.Content.Length > 1000)
            {
                return new OperationResult(new List<string> { "The content specified is too long and should be 1000 characters at most." });
            }

            return new OperationResult();
        }
    }
}
