using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Data_Layer.Interfaces;
using Svetosavlje.Data_Layer;
using System.Web;

namespace Svetosavlje.Services
{
    class DummyBlogProvider : IBlogProvider
    {
        #region INews Members

        public IList<WPBlogModel> GetNews()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEditorNews Members

        public IList<WPBlogModel> GetEditorNews()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMissionNews Members

        public IList<WPBlogModel> GetMissionNews()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
