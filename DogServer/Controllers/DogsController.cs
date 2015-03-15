using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DogServer.Models;

namespace DogServer.Controllers
{
    public class DogsController : ApiController
    {
        private readonly DogModel db = new DogModel();
        // GET: api/Dogs
        public IQueryable<Dog> GetDog()
        {
            return db.Dog.Select(s => s);
        }

        [HttpPost]
        [Route("api/dogs/taint/{id}")]
        public HttpResponseMessage Taint(int id)
        {
            db.Dog.Single(s => s.Id == id).ImageUrl = "http://bit.ly/17yp0G1";
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        [HttpPost]
        public HttpResponseMessage PostRessurect()
        {
            db.Database.Delete();
            db.Database.Create();
            db.SaveChanges();
            new DatabaseSeed().Seed(new DogModel());
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}