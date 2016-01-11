using System.Collections.Generic;
using System.Web.Http;

namespace SimpleAPI.Controllers
{
    [Authorize]
    public class GadgetsController : ApiController
    {
        public List<GadgetItem> Get()
        {
            return new List<GadgetItem>
            {
                new GadgetItem(1, "MBP"),
                new GadgetItem(2, "IPad mini"),
                new GadgetItem(3, "Nexus 5x"),
                new GadgetItem(4, "Tesla Model X")
            };
        }
    }

    public class GadgetItem
    {
        public GadgetItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
