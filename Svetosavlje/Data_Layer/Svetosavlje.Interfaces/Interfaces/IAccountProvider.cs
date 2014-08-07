using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Svetosavlje.Interfaces.Interfaces
{
    public interface IAccountProvider
    {
        IPrincipal GetUser(string email);
    }

    
}
