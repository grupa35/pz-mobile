using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PZ.Sklep.Models
{
    public class AppUser
    {
        string name;
        string surname;
        string password;
        string email;
        Role role;
        bool enabled;

        AppUser(string name, string surname, string password, string email, Role role, bool enabled)
        {
            this.name = name;
            this.surname = surname;
            this.password = password;
            this.email = email;
            this.role = role;
            this.enabled = enabled;
        }
    }
}