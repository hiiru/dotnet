using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Profiling.Helpers;

namespace StackExchange.Profiling
{
    /// <summary>
    /// Stores the request state (for usage in HttpContext.Items)
    /// </summary>
    public class RequestState
    {
        /// <summary>
        /// Is the user authorized to see this MiniProfiler?
        /// </summary>
        public bool IsAuthroized { get; set; }

        /// <summary>
        /// Store this as a string so we generate it once
        /// </summary>
        public List<Guid> RequestIDs { get; set; }
    }
}
