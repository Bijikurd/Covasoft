using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
   public class Users
    {

        public int Id { get; set; }
        public int Admin { get; set; }
        public string FirstName { get; set; }
        public string infix { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
     
    }
}
