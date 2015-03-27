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
    public class IzDanaUDanController : ApiController
    {
        private IzDanaUDan izDanaUDan = new IzDanaUDan();

        // GET api/izdanaudan
        public string Get()
        {
            DateTime today = DateTime.Now.AddDays(-13);

            return izDanaUDan.GetQuote(1, today.Month, today.Day);
        }

        // GET api/izdanaudan/5
        public string Get(int param1, int param2, int param3)
        {
            int author = param1;
            int month = param2;
            int day = param3;

            return izDanaUDan.GetQuote(author, month, day);
        }

        //// POST api/izdanaudan
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/izdanaudan/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/izdanaudan/5
        //public void Delete(int id)
        //{
        //}
    }
}
