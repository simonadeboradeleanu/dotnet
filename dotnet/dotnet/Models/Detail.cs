using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using proiectelul.Models;

namespace dotnet.Models
{
    public class Detail
    {
        public string Specie { get; set; }
        public string Location { get; set; }

        public string? Update { get; set; }
        public int? BirthYear { get; set; }
        public string? FavFood { get; set; }

        [Key]
        public int SharkId { get; set; }
        [JsonIgnore]
        public Shark? Shark { get; set; }



    }
}
