using System.Security.Claims;

namespace dotnet.Models
{
    public class ClaimUser
    {
       
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public User User { get; set; }

        public Claim Claim { get; set; }

        public ClaimUser(string v1, string v2, User user, Claim cl)
        {
            ClaimType = v1;
            ClaimValue = v2;
            Claim = cl;
            User = user;
        }

    }
}
