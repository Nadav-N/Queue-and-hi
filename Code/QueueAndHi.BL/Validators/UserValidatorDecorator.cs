using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueAndHi.BL.Validators
{
    /// <summary>
    /// This class is using both the decorator pattern and the template method pattern
    /// </summary>
    public abstract class UserValidatorDecorator : IUserValidator
    {
        protected IUserValidator decoratedValidator;

        protected UserValidatorDecorator() { }

        protected UserValidatorDecorator(IUserValidator userValidator)
        {
            this.decoratedValidator = userValidator;
        }

        public virtual OperationResult IsValid(UserInfo userInfo)
        {
            OperationResult decoratedResult = this.decoratedValidator.IsValid(userInfo);
            OperationResult result = IsValidInternal(userInfo);
            if (!result.IsSuccessful || !decoratedResult.IsSuccessful)
            {
                List<string> errorMessages = new List<string>(result.ErrorMessages);
                errorMessages.AddRange(decoratedResult.ErrorMessages);
                return new OperationResult(errorMessages);
            }
            return result;
        }

        public abstract OperationResult IsValidInternal(UserInfo userInfo);
    }
}