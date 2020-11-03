using Library.Management.API.Helper;
using Library.Management.API.Interfaces;
using Library.Management.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.API.Logic
{
    public class UserLogic : IUser
    {

        public async Task<GenricResponse> BookReview(BookReview review)
        {
            GenricResponse response = new GenricResponse();
            MongoClient dbClient = new MongoClient(Config.ConnectionString);
            var database = dbClient.GetDatabase(Config.Database);
            var collection = database.GetCollection<BookReview>(Config.BookReview);
            review.Id = review.Id == Guid.Empty ? Guid.NewGuid() : review.Id;
            await collection.InsertOneAsync(review);
            response.IsSuccesful = true;
            return response;
        }


        public async Task<GenricResponse> AddToRead(UserBookRequest request)
        {
            GenricResponse response = new GenricResponse();
            MongoClient dbClient = new MongoClient(Config.ConnectionString);
            var database = dbClient.GetDatabase(Config.Database);
            var collection = database.GetCollection<UserBooks>(Config.UserBooks);
            var userbook = (await collection.FindAsync(doc => doc.UserId == request.UserId)).FirstOrDefault();

            if (userbook == null)
            {
                userbook = new UserBooks()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId
                };
                userbook.Books.Add(new UserBook()
                {
                    BookId = request.BookId
                });

                await collection.InsertOneAsync(userbook);
            }
            else
            {
                if (!userbook.Books.Any(x => x.BookId == request.BookId))
                {
                    userbook.Books.Add(new UserBook()
                    {
                        BookId = request.BookId
                    });

                    await collection.ReplaceOneAsync(doc => doc.Id == userbook.Id, userbook);
                }
                {
                    response.Message = "Book is already added";
                    return response;
                }
            }

            response.IsSuccesful = true;
            return response;
        }


        public async Task<GenricResponse> MarkRead(UserBookRequest request)
        {
            GenricResponse response = new GenricResponse();
            MongoClient dbClient = new MongoClient(Config.ConnectionString);
            var database = dbClient.GetDatabase(Config.Database);
            var collection = database.GetCollection<UserBooks>(Config.UserBooks);
            var userbook = (await collection.FindAsync(doc => doc.UserId == request.UserId)).FirstOrDefault();

            if (userbook == null)
            {
                response.Message = "User not found";
            }
            else
            {
                if (userbook.Books.Any(x => x.BookId == request.BookId))
                {
                    var book = userbook.Books.FirstOrDefault(x => x.BookId == request.BookId);
                    book.IsRead = true;
                    await collection.ReplaceOneAsync(doc => doc.Id == userbook.Id, userbook);
                    response.IsSuccesful = true;
                }
                else
                {
                    response.Message = "Book not found";
                }
            }
            return response;
        }


        public async Task<List<UserReviews>> ReviewsByBookId(BookReviewRequest request)
        {
            MongoClient dbClient = new MongoClient(Config.ConnectionString);
            var database = dbClient.GetDatabase(Config.Database);
            var collection = database.GetCollection<BookReview>(Config.BookReview);
            var filter = new BsonDocument("BookId", request.BookId.ToString());
            var review = (await collection.FindAsync(filter)).ToList();
            var users = await Get();
            var result = review.Select(x => new UserReviews()
            {
                Review = x.Review,
                UserName = users.FirstOrDefault(y => y.Id == x.UserId)?.Email
            }).ToList();
            return result;
        }

        public async Task<IEnumerable<User>> Get()
        {
            MongoClient dbClient = new MongoClient(Config.ConnectionString);
            var database = dbClient.GetDatabase(Config.Database);
            var collection = database.GetCollection<User>(Config.Users);
            var users = (await collection.FindAsync(new BsonDocument())).ToList();
            return users;
        }

        public async Task<User> GetById(string id)
        {
            var users = await Get();
            var user = users.Where(doc => doc.Id == new Guid(id)).FirstOrDefault();
            return user;
        }

        public async Task<AuthenticationResponse> Login(UserLoginRequest request)
        {
            AuthenticationResponse response = new AuthenticationResponse();

            MongoClient dbClient = new MongoClient(Config.ConnectionString);
            var database = dbClient.GetDatabase(Config.Database);
            var collection = database.GetCollection<User>(Config.Users);
            var filter = new BsonDocument("Email", request.Email.ToString());
            var user = (await collection.FindAsync(filter)).FirstOrDefault();
            if (user == null)
            {
                response.Message = "User not found";
            }
            else
            {
                if (user.Password.Equals(request.Password))
                {
                    response.IsSuccesful = true;
                    response.Token.Id = user.Id.ToString();
                    response.Token.Email = user.Email;
                    response.Token.JwtToken = generateJwtToken(user);
                }
                else
                {
                    response.Message = "Password is incorect";
                }
            }
            return response;
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Config.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
