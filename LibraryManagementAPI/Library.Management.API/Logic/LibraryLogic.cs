using Library.Management.API.Helper;
using Library.Management.API.Interfaces;
using Library.Management.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Management.API.Logic
{
    public class LibraryLogic : ILibrary
    {
        public async Task<IEnumerable<Book>> Get()
        {
            MongoClient dbClient = new MongoClient(Config.ConnectionString);
            var database = dbClient.GetDatabase(Config.Database);
            var collection = database.GetCollection<Book>(Config.Books);
            var books = (await collection.FindAsync(new BsonDocument())).ToList();
            return books;
        }


        public async Task<GenricResponse> Post(Book book)
        {
            GenricResponse response = new GenricResponse();
            MongoClient dbClient = new MongoClient(Config.ConnectionString);
            var database = dbClient.GetDatabase(Config.Database);
            var collection = database.GetCollection<Book>(Config.Books);
            var books = (await collection.FindAsync(new BsonDocument())).ToList();
            book.Id = (book.Id == Guid.Empty ? Guid.NewGuid() : book.Id);
            if (books.Any(x => x.Id == book.Id))
            {
                response.Message = "Book Id is already in use";
            }
            else
            {
                await collection.InsertOneAsync(book);
                response.IsSuccesful = true;
            }
            return response;
        }


        public async Task<GenricResponse> Put(Book book)
        {
            GenricResponse response = new GenricResponse();
            MongoClient dbClient = new MongoClient(Config.ConnectionString);
            var database = dbClient.GetDatabase(Config.Database);
            var collection = database.GetCollection<Book>(Config.Books);
            await collection.ReplaceOneAsync(doc => doc.Id == book.Id, book);
            response.IsSuccesful = true;
            return response;
        }
        public async Task<GenricResponse> Delete(BookDeleteRequest request)
        {
            GenricResponse response = new GenricResponse();
            MongoClient dbClient = new MongoClient(Config.ConnectionString);
            var database = dbClient.GetDatabase(Config.Database);
            var collection = database.GetCollection<Book>(Config.Books);
            await collection.DeleteOneAsync(doc => doc.Id == request.Id);
            response.IsSuccesful = true;
            return response;
        }



    }
}
