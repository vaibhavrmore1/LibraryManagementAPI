using Library.Management.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Management.API.Interfaces
{
    public interface ILibrary
    {
        public Task<IEnumerable<Book>> Get();
        public Task<GenricResponse> Post(Book book);
        public Task<GenricResponse> Put(Book book);
        public Task<GenricResponse> Delete(BookDeleteRequest request);
    }
}
