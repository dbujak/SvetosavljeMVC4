using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;
using Svetosavlje.Data_Layer;
using Svetosavlje.Data_Layer.BlogServices;

namespace Svetosavlje.Services
{
    public class BlogProvider : IBlogProvider
    {
        private IBlogProvider _provider = new AtomBlogProvider();


        public IList<WPBlogModel> GetNews()
        {
            return _provider.GetNews();
        }

        public IList<WPBlogModel> GetEditorNews()
        {
            return _provider.GetEditorNews();
        }

        public IList<WPBlogModel> GetMissionNews()
        {
            return _provider.GetMissionNews();
        }

    }
}
