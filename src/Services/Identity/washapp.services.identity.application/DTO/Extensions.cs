using washapp.services.identity.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace washapp.services.identity.application.DTO
{
    public static class Extensions
    {
        public static GetUserDto ToDto(this User user)
        {
            return new GetUserDto(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Login,
                user.Role);
        }
    }
}
