using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;
using Svetosavlje.Data_Layer;
using System.Web;

namespace Svetosavlje.Services
{
    class DummyBlogProvider : IBlogProvider
    {
        #region INews Members

        public IList<WPBlogModel> GetNews()
        {
            return GetBlogList();

        }

        private static IList<WPBlogModel> GetBlogList()
        {
            IList<WPBlogModel> list = new List<WPBlogModel>();

            for (int i = 0; i < 10; i++)
            {
                list.Add(new WPBlogModel(new HtmlString("This is some TOPIC " + i.ToString()), new HtmlString("This is some text or CONTENT " + i.ToString()), "L " + i.ToString()));
            }
            return list;
        }

        #endregion

        #region IEditorNews Members

        public IList<WPBlogModel> GetEditorNews()
        {
            return GetBlogList();
        }

        #endregion

        #region IMissionNews Members

        public IList<WPBlogModel> GetMissionNews()
        {
            return GetBlogList();
        }

        #endregion
    }
}
