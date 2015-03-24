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

        public virtual ValidationResult IsValid(UserInfo userInfo)
        {
            ValidationResult decoratedResult = this.decoratedValidator.IsValid(userInfo);
            ValidationResult result = IsValidInternal(userInfo);
            if (!result.IsSuccessful || !decoratedResult.IsSuccessful)
            {
                List<string> errorMessages = new List<string>(result.ErrorMessages);
                errorMessages.AddRange(decoratedResult.ErrorMessages);
                return new ValidationResult(errorMessages);
            }
            return result;
        }

        public abstract ValidationResult IsValidInternal(UserInfo userInfo);
    }
}