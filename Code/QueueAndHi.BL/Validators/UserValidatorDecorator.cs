using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueAndHi.BL.Validators
{
    public abstract class UserValidatorDecorator : IUserValidator
    {
        protected IUserValidator decoratedValidator;

        protected UserValidatorDecorator() { }

        protected UserValidatorDecorator(IUserValidator userValidator)
        {
            this.decoratedValidator = userValidator;
        }

        public abstract bool IsValid(UserInfo userInfo);
    }
}