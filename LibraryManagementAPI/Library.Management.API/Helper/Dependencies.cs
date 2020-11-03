using Library.Management.API.Interfaces;
using Library.Management.API.Logic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Management.API.Helper
{
    public static class Dependencies
    {
        public static void Configure(IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddScoped<ILibrary, LibraryLogic>();
            collection.AddScoped<IUser, UserLogic>();
        }
    }
}
