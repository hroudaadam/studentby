using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    /// <summary>
    /// Helper class for configuration data
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Key for access token generation 
        /// </summary>
        public string Secret { get; set; }
    }
}
