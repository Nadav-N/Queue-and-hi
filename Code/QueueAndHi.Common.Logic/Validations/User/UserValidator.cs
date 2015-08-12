using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QueueAndHi.Common.Logic.Validators;

namespace QueueAndHi.Common.Logic.Validations.User
{
    public class UserValidator : ValidatorDecorator<UserInfo>
    {
        private IValidator<string> mailValidator;
        private IValidator<string> nameValidator;


        public UserValidator()
        {
            this.mailValidator = new EmailAddressValidator();
            this.nameValidator = new NameValidator();
        }

        public override OperationResult IsValidInternal(UserInfo userInfo)
        {
            // check if email of the user is valid
            OperationResult validEmail = mailValidator.IsValid(userInfo.EmailAddress);
            OperationResult validName = nameValidator.IsValid(userInfo.Username);
            if (validEmail.IsSuccessful && validName.IsSuccessful)
            {
                return new OperationResult<UserInfo>(userInfo);
            }
            else
            {
                OperationResult or = new OperationResult(new List<string>());
                foreach (string emsg in validEmail.ErrorMessages)
                    or.ErrorMessages.Add(emsg);
                foreach (string emsg in validName.ErrorMessages)
                    or.ErrorMessages.Add(emsg);
                return or;
            }
        }
    }
}