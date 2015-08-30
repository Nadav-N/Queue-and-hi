using QueueAndHi.Common.Logic.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace QueueAndHi.Common.Logic.Validations.User
{
    public class PasswordValidator : ValidatorDecorator<Tuple<string, string>>
    {
        public override OperationResult IsValidInternal(Tuple<string, string> PasswordTuple)
        {
            string password = PasswordTuple.Item1;
            string passwordConf = PasswordTuple.Item2;

            OperationResult or = new OperationResult(new List<string>());
            bool ok = true;

            if (String.IsNullOrEmpty(password) || String.IsNullOrWhiteSpace(password))
            {
                or.ErrorMessages.Add("Password can't be empty");
                ok = false;
            }

            if (String.IsNullOrEmpty(passwordConf) || String.IsNullOrWhiteSpace(passwordConf))
            {
                or.ErrorMessages.Add("Password confirmation can't be empty");
                ok = false;
            }

            if (password != passwordConf)
            {
                or.ErrorMessages.Add("Password and Confirmation password fields don't match");
                ok = false;
            }

            if (ok && !isPwdValid(password))
            {
                or.ErrorMessages.Add("Password should match guidlines for complex password");
                ok = false;
            }

            if (!ok)
            {
                return or;
            }
            else
            {
                return new OperationResult<Tuple<string,string>>(PasswordTuple);
            }
        }


        private bool isPwdValid(string pwd)
        {
            if (pwdScore(pwd) < 4)
                return false;
            return true;
        }

        private int pwdScore(string pwd)
        {
            int score = 1;

            if (pwd.Length < 1)
                return 0;
            if (pwd.Length < 4)
                return 1;

            if (pwd.Length >= 8)
                score++;
            if (pwd.Length >= 12)
                score++;
            if (Regex.Match(pwd, @"\d+", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(pwd, @"[a-z]", RegexOptions.ECMAScript).Success &&
              Regex.Match(pwd, @"[A-Z]", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(pwd, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript).Success)
                score++;

            return score;
        }
    }
}
