using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Biz.ThirdParty
{
    public static class GUIHelper
    {
        public static string MinifyLong(long value)
        {
            if (value >= 100000000000)
                return (value / 1000000000).ToString("#,##0") + " B";
            if (value >= 10000000000)
                return (value / 1000000000D).ToString("0.#") + " B";
            if (value >= 100000000)
                return (value / 1000000).ToString("#,##0") + " M";
            if (value >= 10000000)
                return (value / 1000000D).ToString("0.#") + " M";
            if (value >= 100000)
                return (value / 1000).ToString("#,##0") + " K";
            if (value >= 10000)
                return (value / 1000D).ToString("0.#") + " K";
            return value.ToString("#,##0.00");
        }


        /// <summary>
        /// Wraps matched strings in HTML span elements styled with a background-color
        /// </summary>
        /// <param name="text"></param>
        /// <param name="keywords">Comma-separated list of strings to be highlighted</param>
        /// <param name="cssClass">The Css color to apply</param>
        /// <param name="fullMatch">false for returning all matches, true for whole word matches only</param>
        /// <returns>string</returns>
        public static MvcHtmlString HighlightKeyWords(string text, string keywords, string cssClass, bool fullMatch)
        {
            try
            {
                if (text != null && text.ToString() == String.Empty || keywords == String.Empty || cssClass == String.Empty)
                {
                    return new MvcHtmlString(text);
                }
                var words = keywords.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (!fullMatch)
                    return new MvcHtmlString(
                            words.Select(word => word.Trim()).Aggregate(text,
                                 (current, pattern) =>
                                 Regex.Replace(current,
                                                 pattern,
                                                   string.Format("<span style=\"background-color:{0}\">{1}</span>",
                                                   cssClass,
                                                   "$0"),
                                                   RegexOptions.IgnoreCase)));
                return new MvcHtmlString(
                        words.Select(word => "\\b" + word.Trim() + "\\b")
                            .Aggregate(text, (current, pattern) =>
                                      Regex.Replace(current,
                                      pattern,
                                        string.Format("<span style=\"background-color:{0}\">{1}</span>",
                                        cssClass,
                                        "$0"),
                                        RegexOptions.IgnoreCase)));
            }
            catch
            {
                return new MvcHtmlString(text);
            }

        }

    }
}