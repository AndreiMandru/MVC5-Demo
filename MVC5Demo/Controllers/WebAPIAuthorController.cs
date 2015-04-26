using MVC5Demo.EntityModel;
using MVC5Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MVC5Demo.Controllers
{
    public class WebAPIAuthorController : ApiController
    {
        // GET: api/Author
        public IEnumerable<Author> Get()
        {
            return AuthorRepository.GetAuthors();
        }

        // GET: api/Author/5
        public Author Get(int id)
        {
            return AuthorRepository.GetAuthors().FirstOrDefault(x => x.Id == id);
        }

        // POST: api/Author
        public HttpResponseMessage Post(Author author)
        {
            AuthorRepository.InsertAuthor(author);
            var response = Request.CreateResponse(HttpStatusCode.Created, author);
            string url = Url.Link("DefaultApi", new { author.Id });
            response.Headers.Location = new Uri(url);
            return response;
        }


        // DELETE: api/Author/5
        public HttpResponseMessage Delete(int id)
        {
            AuthorRepository.DeleteAuthor(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, id);
            return response;
        }
    }
}
