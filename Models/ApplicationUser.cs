﻿using Microsoft.AspNetCore.Identity;

namespace PokedexApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}