using washapp.orders.application.DTO;
using washapp.orders.application.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace washapp.orders.infrastructure.Services
{
    public class UserPrincipalService : IUserPrincipalService
    {
        private readonly IHttpContextAccessor _httoContextAccessor;

        public UserPrincipalService(IHttpContextAccessor httoContextAccessor)
        {
            _httoContextAccessor = httoContextAccessor;
        }

        public async Task<UserPrincipal> GetUserPrincipal()
        {
            var user = _httoContextAccessor.HttpContext?.User;

            var firstName = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var lastName = user.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.Surname).Value;
            var login = user.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.GivenName).Value;
            var userId = user.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier).Value;

            var userPrincipal = new UserPrincipal(Guid.Parse(userId),firstName,lastName,login);

            return await Task.FromResult(userPrincipal);
        }
    }
}
