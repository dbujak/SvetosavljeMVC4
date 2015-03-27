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
    public class CitanjeIzSvetogPismaController : ApiController
    {
        private IzDanaUDan izDanaUDan = new IzDanaUDan();

        // GET api/citanjeizsvetogpisma
        public DnevnoCitanje Get()
        {
            DateTime today = DateTime.Now.AddDays(-13);

            return izDanaUDan.GetDnevnoCitanje(today.Month, today.Day, today.Year);
        }

        // GET api/citanjeizsvetogpisma/5
        public DnevnoCitanje Get(int param1, int param2, int param3)
        {
            int month = param1;
            int day = param2;
            int year = param3;

            return izDanaUDan.GetDnevnoCitanje(month, day, year);
        }

        //// POST api/citanjeizsvetogpisma
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/citanjeizsvetogpisma/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/citanjeizsvetogpisma/5
        //public void Delete(int id)
        //{
        //}
    }
}
