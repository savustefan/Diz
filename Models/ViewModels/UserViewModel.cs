﻿namespace LucrareDisertatie.Models.ViewModels
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }

        public string Username { get; set; }

        public string EmailAdress { get; set; }

        public string Password { get; set; }

        public bool CreatorRoleCheckbox { get; set; }
    }
}
