using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueAndHi.Common.Logic.Validators
{
    /// <summary>
    /// This class is using both the decorator pattern and the template method pattern
    /// </summary>
    public abstract class ValidatorDecorator<T> : IValidator<T>
    {
        protected IValidator<T> decoratedValidator;

        protected ValidatorDecorator() { }

        protected ValidatorDecorator(IValidator<T> decoratedValidator)
        {
            this.decoratedValidator = decoratedValidator;
        }

        public virtual OperationResult IsValid(T data)
        {
            OperationResult decoratedResult = this.decoratedValidator != null ? this.decoratedValidator.IsValid(data) : new OperationResult();
            OperationResult result = IsValidInternal(data);
            if (!result.IsSuccessful || !decoratedResult.IsSuccessful)
            {
                List<string> errorMessages = new List<string>(result.ErrorMessages);
                errorMessages.AddRange(decoratedResult.ErrorMessages);
                return new OperationResult(errorMessages);
            }
            return result;
        }

        public abstract OperationResult IsValidInternal(T userInfo);
    }
}