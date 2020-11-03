using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Management.API.Models
{
    public class UserBooks
    {
        public UserBooks()
        {
            Books = new List<UserBook>();
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [Required]
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("bookId")]
        public List<UserBook> Books { get; set; }

    }

    public class UserBook
    {
        [JsonProperty("bookId")]
        public Guid BookId { get; set; }

        [JsonProperty("isRead")]
        public bool IsRead { get; set; }

    }

    public class UserBookRequest
    {
        [Required]
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("bookId")]
        public Guid BookId { get; set; }

    }

}
