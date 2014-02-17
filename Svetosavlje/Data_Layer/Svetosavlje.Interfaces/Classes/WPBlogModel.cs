using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Svetosavlje.Interfaces.Classes
{
    public class WPBlogModel
    {
        public HtmlString Title;
        public string Link;
        public HtmlString Content;

        public WPBlogModel(HtmlString t, HtmlString c, string l)
        {
            Title = t;
            Content = c;
            Link = l;
        }

        public WPBlogModel() { }
    }
}
