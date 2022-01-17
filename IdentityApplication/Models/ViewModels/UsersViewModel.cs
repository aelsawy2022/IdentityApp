using IdentityApplication.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models.ViewModels
{
    public class UsersViewModel : PagingModel
    {
        public List<UsersModel> Users { get; set; }
        public List<Role> Roles { get; set; }
    }
}
