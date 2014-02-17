using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;
using Svetosavlje.Data_Layer.Core;

namespace Svetosavlje.Data_Layer.BlogServices
{
    public class EditorNewsList : IEditorNews
    {
        blogConnection blog = new blogConnection();

        public IList<WPBlogModel> GetEditorNews()
        {
            return blog.GetBlog("http://blogs.svetosavlje.org/urednistvo/feed/atom");
        }


    }
}
