using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using washapp.services.identity.domain.Entities;

namespace washapp.services.identity.application.DTO
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public Roles Role { get; set; }
        public string Login { get; set; }

        public GetUserDto(Guid id, string firstName, string lastName, string login, Roles role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Role = role;
        }
    }
}
