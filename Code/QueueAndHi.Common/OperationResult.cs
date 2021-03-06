﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueAndHi.Common
{
    public class OperationResult
    {
        public OperationResult(ICollection<string> errorMessage)
        {
            IsSuccessful = false;
            ErrorMessages = errorMessage;
        }

        /// <summary>
        /// Creates a new Validation result object with "IsSuccessful" set to True
        /// </summary>
        public OperationResult()
        {
            IsSuccessful = true;
            ErrorMessages = new List<string>();
        }

        public ICollection<string> ErrorMessages { get; private set; }

        public bool IsSuccessful { get; private set; }
    }

    public class OperationResult<TPayload> : OperationResult
    {
        public OperationResult(TPayload payload) : base()
        {
            Payload = payload;
        }

        public OperationResult(ICollection<string> errorMessage)
            : base(errorMessage)
        {
        }

        public TPayload Payload { get; private set; }
    }
}