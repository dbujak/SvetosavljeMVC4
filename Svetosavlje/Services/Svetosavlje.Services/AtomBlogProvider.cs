using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Data_Layer.Interfaces;
using Svetosavlje.Data_Layer;
using Svetosavlje.Data_Layer.BlogServices;

namespace Svetosavlje.Services
{
    public class AtomBlogProvider : IBlogProvider
    {

        public IList<WPBlogModel> GetNews()
        {
            NewsList list = new NewsList();
            return list.GetNews();
        }

        public IList<WPBlogModel> GetEditorNews()
        {
            EditorNewsList list = new EditorNewsList();
            return list.GetEditorNews();
        }

        public IList<WPBlogModel> GetMissionNews()
        {
            MissionNewsList list = new MissionNewsList();
            return list.GetMissionNews();
        }

    }
}