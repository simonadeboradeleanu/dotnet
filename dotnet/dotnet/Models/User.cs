using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using proiectelul.Models;

namespace dotnet.Models
{
    [Table("Users")]
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool admin  {get; set;} 

        [JsonIgnore]
        public List<UserShark>? UserSharks { get; set; }
    }
}
