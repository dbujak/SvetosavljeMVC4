using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;
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
