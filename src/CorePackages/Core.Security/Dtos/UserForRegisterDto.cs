using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Dtos

{
    public class UserForRegisterDto
    {
        public string Mail { get; set; }
        //public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmployeeNo { get; set; }
        public bool Status { get; set; } 
    }
}
