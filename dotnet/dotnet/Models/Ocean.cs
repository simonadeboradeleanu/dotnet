using System.Text.Json.Serialization;
using proiectelul.Models;

namespace proiectelul.Models
{
    public class Ocean
    {
        //[JsonIgnore]
        public int Id { get; set;}
        public string Nume { get; set; }
        [JsonIgnore]
        public ICollection<Shark>? Sharks { get; set; }

        //public string FunFacts { get; set; }  

        //public int NrTeeth { get; set; }

        //public ICollection<Rechin> Rechini { get; set; }    

    }
}
