using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bets4You_Rest_Webservice.Controllers
{
    public class BetController : ApiController
    {
        // GET: api/Bet
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Bet/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Bet
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Bet/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bet/5
        public void Delete(int id)
        {
        }
    }
}
