using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Management.API.Helper;
using Library.Management.API.Interfaces;
using Library.Management.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Management.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : BaseController
    {
        private readonly ILibrary _library;

        public LibraryController(ILibrary library)
        {
            _library = library;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return await InvokeAsync(() => _library.Get(), "LibraryController:Get");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<GenricResponse>> Post(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await InvokeAsync(() => _library.Post(book), "LibraryController:book");
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<GenricResponse>> Put(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await InvokeAsync(() => _library.Put(book), "LibraryController:Put");
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<GenricResponse>> Delete(BookDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await InvokeAsync(() => _library.Delete(request), "LibraryController:delete");
        }
    }
}
