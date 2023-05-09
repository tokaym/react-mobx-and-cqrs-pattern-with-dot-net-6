using System;
using System.Collections.Generic;
using System.Text;
using Core.Persistence;
using Core.Persistence.Repositories;

namespace Core.Security.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string EmployeeNo { get; set; }
        // public byte[] PasswordSalt { get; set; }
        // public byte[] PasswordHash { get; set; }
        public bool Status { get; set; } // Bu yazılımdan kullanıcı silemediğimiz için şimdilik iptal.Hata vermemesi içn aktif bıraktım.
        
        

        public User()
        {

        }

        public User(Guid id, string name,string surname, string mail, string employeeNo,/* byte[] passwordSalt, byte[] passwordHash,*/ bool status)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Mail = mail;
            EmployeeNo = employeeNo;
            // PasswordSalt = passwordSalt;
            // PasswordHash = passwordHash;
            Status = status;
        }
    }
}
