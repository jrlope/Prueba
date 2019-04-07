using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ejemplo.Data
{
    public class ApplicationUser : IdentityUser{
        public ApplicationUser()
        {
            CreatedDate = DateTime.Now;
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string StringIdentifier { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
