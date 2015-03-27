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
    public class ListaController : ApiController
    {
        private ListArhivaData listArhiva = new ListArhivaData();

        // GET api/lista
        public IEnumerable<MessageThread> Get()
        {
            return listArhiva.GetMessageThreads(10);
        }

        //// GET api/lista/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/lista
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/lista/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/lista/5
        //public void Delete(int id)
        //{
        //}
    }
}
