using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using washapp.services.identity.domain.Exceptions;

namespace washapp.services.identity.domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; } 
        public string Password { get; set; }
        public Roles Role { get; set; }
        public string RefreshToken { get;  set; }
        public DateTime RefreshTokenExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public User(Guid id, string firstName, string lastName, string login, DateTime createdAt, Roles role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            CreatedAt = createdAt;
            Role = role;
        }

        public User(string firstName, string lastName, string login, DateTime createdAt)
        {
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            CreatedAt = createdAt;
        }

        public User()
        {
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                new PasswordCannotBeNullException();
            }
            Password = password;
        }
        
        public void Update(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Login = user.Login;
        }

        public void ChangeRole(Roles role)
        {
            Role = role;
        }
        
        public void ValidatePassword(string password)
        {
            if (password!=Password)
            {
                throw new IncorrectPasswordException();
            }
        }

        public void SetRefreshToken(string refreshToken, DateTime refreshTokenExpiryDate)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpiryDate = refreshTokenExpiryDate;
        }

        public void RevokeRefreshToken()
        {
            RefreshToken = null;
        }
    }

}
