using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    /// <summary>
    /// Definition of JobApplication states
    /// </summary>
    public static class JobApplicationStates
    {
        /// <summary>
        /// Approved
        /// </summary>
        public const string Approved = "approved";

        /// <summary>
        /// Denied
        /// </summary>
        public const string Denied = "denied";

        /// <summary>
        /// Pending
        /// </summary>
        public const string Pending = "pending";

        /// <summary>
        /// Attended
        /// </summary>
        public const string Attended = "attended";

        /// <summary>
        /// Absent
        /// </summary>
        public const string Absent = "absent";
    }
}
