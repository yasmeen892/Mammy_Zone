﻿using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models.ViewModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
    }
}
