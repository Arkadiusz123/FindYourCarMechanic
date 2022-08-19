﻿using Microsoft.AspNetCore.Identity;

namespace FindYourCarMechanic.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }
        public AppRole(string name) : base(name) { }
        
    }
}
