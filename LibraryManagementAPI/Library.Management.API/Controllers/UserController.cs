
using Library.Management.API.Helper;
using Library.Management.API.Interfaces;
using Library.Management.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Management.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {

        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpPost("bookreview")]
        [Authorize]
        public async Task<ActionResult<GenricResponse>> BookReview(BookReview review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await InvokeAsync(() => _user.BookReview(review), "UserController:BookReview");
        }

        [HttpPost("addtoread")]
        [Authorize]
        public async Task<ActionResult<GenricResponse>> AddToRead(UserBookRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await InvokeAsync(() => _user.AddToRead(request), "UserController:AddToRead");
        }

        [HttpPost("markread")]
        [Authorize]
        public async Task<ActionResult<GenricResponse>> MarkRead(UserBookRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await InvokeAsync(() => _user.MarkRead(request), "UserController:AddToRead");
        }

        [HttpGet("reviewsbybookid")]
        [Authorize(Roles="admin")]
        public async Task<ActionResult<List<UserReviews>>> GetReviewsByBookId(BookReviewRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await InvokeAsync(() => _user.ReviewsByBookId(request), "UserController:AddToRead");
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await InvokeAsync(() => _user.Login(request), "UserController:AddToRead");
        }
    }
}
