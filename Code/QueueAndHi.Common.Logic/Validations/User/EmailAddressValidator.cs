using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QueueAndHi.Common.Logic.Validators;
using System.Text.RegularExpressions;
using System.Globalization;

namespace QueueAndHi.Common.Logic.Validations.User
{
    public class EmailAddressValidator : ValidatorDecorator<string>
    {
        public override OperationResult IsValidInternal(string email)
        {
            // check if email of the user is valid

            if (IsValidEmail(email))
            {
                return new OperationResult<string>(email);
            }
            else
            {
                return new OperationResult(new List<string>() { "Invalid email address provided" });
            }
        }


        private bool IsValidEmail(string mail)
        {
            //if empty string or null == invalid
            if (String.IsNullOrEmpty(mail))
                return false;

            // Use IdnMapping class to convert Unicode domain names. 
            try
            {
                string tmpmail = mail;
                mail = null;

                tmpmail = Regex.Replace(tmpmail, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                mail = tmpmail;
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            //failure of domain globalization means mail is null, either by exception or directly
            if (String.IsNullOrEmpty(mail))
            {
                return false;
            }

            // Return true if strIn is in valid e-mail format. 
            try
            {
                return Regex.IsMatch(mail,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                return null;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}