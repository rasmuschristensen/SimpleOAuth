using System.Collections.Generic;
using System.Web.Http;

namespace SimpleAPI.Controllers
{
    [Authorize]
    public class VehiclesController : ApiController
    {
        public List<string> Get()
        {
            return new List<string> {"car", "bike", "bus"};
        }
    }
}
