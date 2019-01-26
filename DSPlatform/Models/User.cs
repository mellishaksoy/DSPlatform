using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DSPlatform.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [BsonElement("Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Surname")]
        public string Surname { get; set; }
        [BsonElement("Age")]
        public int Age { get; set; }
    }
}