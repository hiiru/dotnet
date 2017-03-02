using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Profiling.Helpers;

namespace StackExchange.Profiling
{
    /// <summary>
    /// Stores the request state (for usage in <see cref="MiniProfiler.RequestState"/>)
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


        //Everything below this line is a wrapper to the Middleware for the PoC of this idea, that way I don't have to expose internals
        public string BasePath {
            get
            {
                return MiniProfilerMiddleware.Current.BasePath.Value.EnsureTrailingSlash();
            }
        }

        public string EmbeddedFormat
        {
            get
            {
                if (!MiniProfilerMiddleware.Current.Embedded.TryGetResource("include.partial.html", out string format))
                {
                    return null;
                }
                return format;
            }
        }
    }
}
