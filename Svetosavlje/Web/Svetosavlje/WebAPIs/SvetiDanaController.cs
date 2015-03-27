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
    public class SvetiDanaController : ApiController
    {
        private SvetiDana svetiDana = new SvetiDana();

        // GET api/svetidana
        public IEnumerable<string> Get()
        {
            DateTime today = DateTime.Now.AddDays(-13);

            return svetiDana.GetSaintNamesList(today.Month, today.Day);
        }

        // GET api/svetidana/5/28
        public IEnumerable<string> Get(int param1, int param2)
        {
            int month = param1;
            int day = param2;

            return svetiDana.GetSaintNamesList(month, day);
        }

        //// POST api/svetidana
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/svetidana/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/svetidana/5
        //public void Delete(int id)
        //{
        //}
    }
}
