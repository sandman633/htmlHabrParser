using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlparser.Core
{
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document, Condition condition);
    }
    public enum Condition:int
    {
        Links = 1,
        Headers
    }
}
