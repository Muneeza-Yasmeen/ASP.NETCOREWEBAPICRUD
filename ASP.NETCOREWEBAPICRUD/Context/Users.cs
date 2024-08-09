using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ASP.NETCOREWEBAPICRUD.Context
{
    public class Users
    {
        
            [Key]
            [MaxLength(255)]
            public string Name { get; set; }
        public string Designation { get; set; }
            public string Address { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }

    }
}
