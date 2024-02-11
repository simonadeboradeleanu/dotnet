using System.Text.Json.Serialization;
using dotnet.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using proiectelul.Models;

namespace proiectelul.Models
{
    public class Shark 
    {

        public int Id { get; set; }
        public string Nume { get; set; }

        public int OceanId { get; set; }
        [JsonIgnore]
        public Ocean? Ocean { get; set; }

        public Detail? Detail { get; set; }

        [JsonIgnore]
        [BindNever]
        public List<UserShark>? UserSharks { get; set; }

        //public Guid SpecieId { get; set; }

        //public ICollection<RechinOcean> RechinOcean { get;  set; }

        //public Detalii Detalii { get; set; }    

    }
}
