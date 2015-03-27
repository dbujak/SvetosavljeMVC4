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
    public class UrednistvoController : ApiController
    {
        private Blogs blogs = new Blogs();

        // GET api/misija
        public IEnumerable<WPBlogModel> Get()
        {
            return blogs.GetEditorNews();
        }

        //// GET api/urednistvo/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/urednistvo
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/urednistvo/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/urednistvo/5
        //public void Delete(int id)
        //{
        //}
    }
}
