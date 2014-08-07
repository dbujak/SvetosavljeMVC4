using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Services;

namespace Svetosavlje
{
    public static class Factory
    {
        public static IAccountProvider GetAccountProvider()
        {
            return new AccountProvider();
        }

        public static IDataProvider GetDatabaseProvider()
        {
            return new DatabaseProvider();
        }

        public static IBlogProvider GetBlogProvider()
        {
            return new BlogProvider();
        }
    }
}