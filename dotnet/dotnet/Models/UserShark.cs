using System.Text.Json.Serialization;
using proiectelul.Models;

namespace dotnet.Models
{
    public class UserShark
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int SharkId { get; set; }
      //[JsonIgnore]
        public Shark Shark { get; set; }
    }
}
