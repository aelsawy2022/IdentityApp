using IdentityApplication.Data.Entities;

namespace IdentityApplication.Models
{
    public class UsersModel : User
    {
        public bool IsAdmin { get; set; }
    }
}
