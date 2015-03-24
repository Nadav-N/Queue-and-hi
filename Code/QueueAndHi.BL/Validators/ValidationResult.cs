using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueAndHi.BL.Validators
{
    public class ValidationResult
    {
        public ValidationResult(ICollection<string> errorMessage)
        {
            IsSuccessful = false;
            ErrorMessages = errorMessage;
        }

        /// <summary>
        /// Creates a new Validation result object with "IsSuccessful" set to True
        /// </summary>
        public ValidationResult()
        {
            IsSuccessful = true;
            ErrorMessages = new List<string>();
        }

        public ICollection<string> ErrorMessages { get; private set; }

        public bool IsSuccessful { get; private set; }
    }
}