using Microsoft.AspNet.Identity.EntityFramework;

namespace KarrenCupCakes.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class UserData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ConnectionId { get; set; }
    }
}