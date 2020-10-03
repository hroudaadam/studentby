using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    /// <summary>
    /// Definition of User roles
    /// </summary>
    public static class UserRoles
    {
        /// <summary>
        /// Operator
        /// </summary>
        public const string Operator = "operator";

        /// <summary>
        /// Student - not activated
        /// </summary>
        public const string StudentInact = "studentInact";

        /// <summary>
        /// Student - activated
        /// </summary>
        public const string Student = "student";

        /// <summary>
        /// Customer
        /// </summary>
        public const string Customer = "customer";


    }
}
