using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Data_Layer.Interfaces;
using Svetosavlje.Data_Layer.Core;

namespace Svetosavlje.Data_Layer.BlogServices
{
    public class MissionNewsList : IMissionNews
    {
        blogConnection blog = new blogConnection();

        public IList<WPBlogModel> GetMissionNews()
        {
            return blog.GetBlog("http://www.svedokverni.org/feed/atom/");
        }

    }
}
