using Microsoft.AspNetCore.Identity;
using ShopBusinessLayer.Common;
using ShopBusinessLayer.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IEnumerable<IdentityError>> Register(Register register);

        Task<APIResponse> ResetPasswordAsync(ResetPassWord model);

        Task<Object> Login(Login login);
    }
}
