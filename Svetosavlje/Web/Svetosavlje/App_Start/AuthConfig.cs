using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebPages.OAuth;

namespace Svetosavlje
{
    public static class AuthConfig
    {
                public static void RegisterAuth()
                {
                    OAuthWebSecurity.RegisterGoogleClient();
                }
        
    }
}