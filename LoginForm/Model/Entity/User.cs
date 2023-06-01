using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Model.Entity
{
    public class User
    {
        public int Id { get; set; }
        public long CreationDate { get; set; }
        public long UpdateDate { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int SecurityStamp { get; set; }
        public string Salt { get; set; }
        public User(int id, long creationDate, long updateDate, string name, string familyName, string normalizedUserName, string password, string email, string phoneNumber, int securityStamp, string salt)
        {
            Id = id;
            CreationDate = creationDate;
            UpdateDate = updateDate;
            Name = name;
            FamilyName = familyName;
            NormalizedUserName = normalizedUserName;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            SecurityStamp = securityStamp;
            Salt = salt;
        }
    }
}
