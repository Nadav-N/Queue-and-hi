using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Exceptions
    {
        /// <summary>
        /// Thrown when the DB can't be reached through the DAL
        /// </summary>
        public class DALConnectionError : Exception
        {

            public DALConnectionError() { }
            public DALConnectionError(String message) : base(message) { }
            public DALConnectionError(String message, Exception inner) : base(message, inner) { }
        }

        public class DALDuplicateKeyError : Exception
        {
            public DALDuplicateKeyError() { }
            public DALDuplicateKeyError(String message) : base(message) { }
            public DALDuplicateKeyError(string message, Exception inner) : base(message, inner) { }
        }

        public class DALZeroRowsAffectedError : Exception
        {
            public DALZeroRowsAffectedError () { }
            public DALZeroRowsAffectedError (String message) : base(message) { }
            public DALZeroRowsAffectedError(string message, Exception inner) : base(message, inner) { }
        }

        public class DALConstraintError : Exception
        {
            public DALConstraintError() { }
            public DALConstraintError(String message) : base(message) { }
            public DALConstraintError(string message, Exception inner) : base(message, inner) { }
        }
    }
}
