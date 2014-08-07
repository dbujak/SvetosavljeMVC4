using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;
using System.Security.Principal;
using System.Xml;

namespace Svetosavlje.Services
{
    public class AccountProvider : IAccountProvider
    {
        public IPrincipal GetUser(string email)
        {
            IPrincipal principal = null;

            // check config file and see if user is admin
            XmlDocument xmlDoc = new XmlDocument(); //* create an xml document object.
            xmlDoc.Load(System.Web.Hosting.HostingEnvironment.MapPath("/admins.config"));

            foreach (XmlNode admin in xmlDoc.FirstChild.ChildNodes)
            {
                if (email.ToLower() == admin.Attributes["email"].Value.ToLower())  // user is administrator
                {
                    List<string> roles = new List<string>();
                    
                    foreach (XmlNode role in admin.ChildNodes)
                    {
                        roles.Add(role.InnerText.ToLower());
                    }

                    var identity = new GenericIdentity(email);
                    principal = new GenericPrincipal(identity, roles.ToArray());
                    break;
                }
            }

            return principal;
        }
    }
}
