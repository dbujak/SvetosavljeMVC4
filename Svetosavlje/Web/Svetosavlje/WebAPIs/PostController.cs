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
    public class PostController : ApiController
    {
        private IzDanaUDan izDanaUDan = new IzDanaUDan();

        // GET api/post
        public string Get()
        {
            DateTime today = DateTime.Now.AddDays(-13);

            return izDanaUDan.GetFastingType(today.Month, today.Day);
        }

        // GET api/post/5
        public string Get(int param1, int param2)
        {
            int month = param1;
            int day = param2;

            return izDanaUDan.GetFastingType(month, day);
        }

        //// POST api/post
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/post/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/post/5
        //public void Delete(int id)
        //{
        //}
    }
}
