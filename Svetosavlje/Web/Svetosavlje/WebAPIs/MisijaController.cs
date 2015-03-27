using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Svetosavlje.Interfaces.Classes;
using Svetosavlje.Controllers;

namespace Svetosavlje.WebAPIs
{
    [AllowCrossSiteJson]
    public class MisijaController : ApiController
    {
        private Blogs blogs = new Blogs();

        // GET api/misija
        public IEnumerable<WPBlogModel> Get()
        {
            return blogs.GetMissionNews();
        }

        //// GET api/misija/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/misija
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/misija/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/misija/5
        //public void Delete(int id)
        //{
        //}
    }
}
