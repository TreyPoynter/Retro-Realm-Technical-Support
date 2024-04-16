using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroRealm.Models
{
    public class User : IdentityUser
    {
        // all Identity User properties
        [NotMapped]
        public IEnumerable<string> RoleNames { get; set; }

        public User()
        {
            RoleNames = new List<string>();
        }
    }
}
