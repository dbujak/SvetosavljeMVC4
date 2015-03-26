using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Web;
using Svetosavlje.Interfaces.Classes;

namespace Svetosavlje.Data_Layer.Core
{
    class blogConnection
    {
        public IList<WPBlogModel> GetBlog(string blogAddress)
        {
            XNamespace xsd = "http://www.w3.org/2005/Atom";
            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var feed = client.DownloadString(blogAddress);
            var document = XDocument.Parse(feed);
            var blog =
              from e in document.Descendants(xsd + "entry")
              select new WPBlogModel()
              {
                  Title = (string)e.Element(xsd + "title"),
                  Link = (string)e.Element(xsd + "id"),
                  Content = (string)e.Element(xsd + "summary")
              };

            return blog.ToList<WPBlogModel>();
        }
    }
}
