using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Svetosavlje.Interfaces.Classes
{
    public class WPBlogModel
    {
        public string Title;
        public string Link;
        public string Content;

        public WPBlogModel(string t, string c, string l)
        {
            Title = t;
            Content = c;
            Link = l;
        }

        public WPBlogModel() { }
    }
}
