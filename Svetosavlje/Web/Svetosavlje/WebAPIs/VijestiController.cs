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
    public class VijestiController : ApiController
    {
        private Blogs blogs = new Blogs();

        // GET api/vijesti
        public IEnumerable<WPBlogModel> Get()
        {
            return blogs.GetNews();
        }

        //// GET api/vijesti/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/vijesti
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/vijesti/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/vijesti/5
        //public void Delete(int id)
        //{
        //}
    }
}
