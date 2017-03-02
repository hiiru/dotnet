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
        public static HtmlString RenderMiniProfiler(this IHtmlHelper helper,
            RenderPosition? position = null,
            bool? showTrivial = null,
            bool? showTimeWithChildren = null,
            int? maxTracesToShow = null,
            bool? showControls = null,
            bool? startHidden = null)
        {
            // This is populated in Middleware by SetHeadersAndState
            var requestState = helper.ViewContext.HttpContext.Items["MiniProfiler-RequestState"] as RequestState;
            return MiniProfiler.Current.RenderIncludes(position, showTrivial, showTimeWithChildren, maxTracesToShow, showControls, startHidden, requestState);
        }
    }
}
