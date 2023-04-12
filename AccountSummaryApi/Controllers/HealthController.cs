using System.Web.Http;
using System.Web.Http.Description;

namespace AccountSummaryApi.Controllers
{
    public class HealthController : ApiController
    {
        /// <summary>
        /// Check if API is up
        /// </summary>
        /// <returns>Pong</returns>
        [HttpGet]
        [ResponseType(typeof(string))]
        public IHttpActionResult Ping()
        {
            return Ok("Pong");
        }
    }
}
