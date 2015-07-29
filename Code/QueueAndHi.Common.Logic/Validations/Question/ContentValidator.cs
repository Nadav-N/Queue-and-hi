﻿using QueueAndHi.Common.Logic.Validators;
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

        public override OperationResult IsValidInternal(IPost userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
