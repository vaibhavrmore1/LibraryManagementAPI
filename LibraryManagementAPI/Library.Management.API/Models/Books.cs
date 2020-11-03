using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Management.API.Models
{
    public class Book
    {
        [Required]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public class BookDeleteRequest
    {
        [Required]
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
