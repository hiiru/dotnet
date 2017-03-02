using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackExchange.Profiling;

namespace StackExchange.Profiling
{
    public static class HtmlHelperExtensions
    {
        private static readonly HtmlString includeNotFound = new HtmlString("<!-- Could not find 'include.partial.html' -->");

        public static HtmlString RenderMiniProfiler(this IHtmlHelper helper,
            RenderPosition? position = null,
            bool? showTrivial = null,
            bool? showTimeWithChildren = null,
            int? maxTracesToShow = null,
            bool? showControls = null,
            bool? startHidden = null)
        {
            //added variable because I'm lazy
            var profiler = MiniProfiler.Current;

            if (profiler == null) return HtmlString.Empty;

            // This is populated in Middleware by SetHeadersAndState
            var state = helper.ViewContext.HttpContext.Items["MiniProfiler-RequestState"] as RequestState;

            // Is the user authroized to see the results of the current MiniProfiler?
            var authorized = state?.IsAuthroized ?? false;
            var ids = state?.RequestIDs ?? Enumerable.Empty<Guid>();

            var format = state?.EmbeddedFormat;
            if (format == null)
            {
                return includeNotFound;
            }

            Func<bool, string> toJs = b => b ? "true" : "false";

            var sb = new StringBuilder(format);
            sb.Replace("{path}", state?.BasePath)
              .Replace("{version}", MiniProfiler.Settings.VersionHash)
              .Replace("{currentId}", profiler.Id.ToString())
              .Replace("{ids}", string.Join(",", ids.Select(guid => guid.ToString())))
              .Replace("{position}", (position ?? MiniProfiler.Settings.PopupRenderPosition).ToString().ToLower())
              .Replace("{showTrivial}", toJs(showTrivial ?? MiniProfiler.Settings.PopupShowTrivial))
              .Replace("{showChildren}", toJs(showTimeWithChildren ?? MiniProfiler.Settings.PopupShowTimeWithChildren))
              .Replace("{maxTracesToShow}", (maxTracesToShow ?? MiniProfiler.Settings.PopupMaxTracesToShow).ToString())
              .Replace("{showControls}", toJs(showControls ?? MiniProfiler.Settings.ShowControls))
              .Replace("{authorized}", toJs(authorized))
              .Replace("{toggleShortcut}", MiniProfiler.Settings.PopupToggleKeyboardShortcut)
              .Replace("{startHidden}", toJs(startHidden ?? MiniProfiler.Settings.PopupStartHidden))
              .Replace("{trivialMilliseconds}", MiniProfiler.Settings.TrivialDurationThresholdMilliseconds.ToString());
            return new HtmlString(sb.ToString());
        }
    }
}
