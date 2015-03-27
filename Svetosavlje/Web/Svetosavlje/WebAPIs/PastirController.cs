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
    public class PastirController : ApiController
    {
        private PitanjaPastiru pitanjaPastiru = new PitanjaPastiru();

        // GET api/pastir
        public IList<PitanjeInfo> Get()
        {
            return pitanjaPastiru.GetQuestionList(0, 10);
        }

        //// GET api/pastir/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/pastir
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/pastir/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/pastir/5
        //public void Delete(int id)
        //{
        //}
    }
}
