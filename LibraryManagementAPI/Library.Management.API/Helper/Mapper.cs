using Library.Management.API.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Management.API.Helper
{
    public static class Mapper
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<Book>(
                       map =>
                       {
                           map.AutoMap();
                           map.MapProperty(x => x.Id).SetSerializer(new GuidSerializer(BsonType.String));
                       });

            BsonClassMap.RegisterClassMap<BookReview>(
                map =>
                {
                    map.AutoMap();
                    map.MapProperty(x => x.Id).SetSerializer(new GuidSerializer(BsonType.String));
                });

            BsonClassMap.RegisterClassMap<UserBooks>(
               map =>
               {
                   map.AutoMap();
                   map.MapProperty(x => x.Id).SetSerializer(new GuidSerializer(BsonType.String));
                   map.MapProperty(x => x.UserId).SetSerializer(new GuidSerializer(BsonType.String));
               });

            BsonClassMap.RegisterClassMap<UserBook>(
              map =>
              {
                  map.AutoMap();
                  map.MapProperty(x => x.BookId).SetSerializer(new GuidSerializer(BsonType.String));
              });
        }



    }


}
