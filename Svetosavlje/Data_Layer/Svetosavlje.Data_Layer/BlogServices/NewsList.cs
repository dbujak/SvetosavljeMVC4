using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Data_Layer.Interfaces;
using Svetosavlje.Data_Layer.Core;

namespace Svetosavlje.Data_Layer.BlogServices
{
    public class NewsList : INews
    {
        blogConnection blog = new blogConnection();

        public IList<WPBlogModel> GetNews()
        {
            return blog.GetBlog("http://blogs.svetosavlje.org/vesti/feed/atom");
        }

    }
}
