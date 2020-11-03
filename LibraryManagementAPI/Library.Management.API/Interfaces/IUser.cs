using Library.Management.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Management.API.Interfaces
{
    public interface IUser
    {
        public Task<GenricResponse> BookReview(BookReview review);
        public Task<GenricResponse> AddToRead(UserBookRequest request);
        public Task<GenricResponse> MarkRead(UserBookRequest request);
        public Task<List<UserReviews>> ReviewsByBookId(BookReviewRequest request);
        public Task<AuthenticationResponse> Login(UserLoginRequest request);
        public Task<User> GetById(string id);

    }
}
