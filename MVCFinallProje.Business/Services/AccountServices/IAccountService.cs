using Microsoft.AspNetCore.Identity;
using MVCFinallProje.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Business.Services.AccountServices
{
    public interface IAccountService
    {
        Task<bool> AnyAsync(Expression<Func<IdentityUser, bool>> expression);
        Task<IdentityResult> CreateUserAsync(IdentityUser user, Roles role);
    }
}
