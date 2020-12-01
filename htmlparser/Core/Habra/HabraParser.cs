using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlparser.Core.Habra
{
    class HabraParser : IParser<string[]>
    {

        public string[] Parse(IHtmlDocument document,Condition condition)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("post__title_link"));
            if(Condition.Headers == condition)
            {
                foreach(var item in items)
                {
                    list.Add(item.TextContent);
                }
                return list.ToArray();
            }
            else
            {
                foreach (var item in items)
                {
                    list.Add(item.GetAttribute("href"));
                }
                return list.ToArray();
            }
        }
        //public string[] ParseLink(IHtmlDocument document)
        //{
        //    var list = new List<string>();
        //    var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("post__title_link"));
        //    foreach (var item in items)
        //    {
        //        list.Add(item.GetAttribute("href"));
        //    }
        //    return list.ToArray();
        //}
    }
}
