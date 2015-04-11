using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Logic.Validators
{
    public interface IValidator<in T>
    {
        OperationResult IsValid(T data);
    }
}
