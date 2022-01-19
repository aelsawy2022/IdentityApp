using IdentityApplication.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace IdentityApplication.Models
{
    public class UsersModel : User
    {
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
