using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public static class JobApplicationStates
    {
        public const string Approved = "approved";
        public const string Denied = "denied";
        public const string Pending = "pending";
        public const string Attended = "attended";
        public const string Absent = "absent";
    }
}
