using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Management.API.Models
{
    public class BookReview
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [Required]
        [JsonProperty("bookId")]
        public Guid BookId { get; set; }

        [Required]
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("review")]
        public string Review { get; set; }
    }

    public class BookReviewRequest
    {
        [Required]
        [JsonProperty("bookId")]
        public Guid BookId { get; set; }
    }

    public class UserReviews
    {
        public string UserName { get; set; }
        public string Review { get; set; }

    }
}
