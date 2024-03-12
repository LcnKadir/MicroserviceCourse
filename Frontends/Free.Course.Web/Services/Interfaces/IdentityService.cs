using Free.Course.Web.Models;
using FreeCourse.Shared.DTOs;
using IdentityModel.Client;

namespace Free.Course.Web.Services.Interfaces
{
    interface IdentityService
    {
        Task<Response<bool>> SignIn(SigninInput signinInput);

        Task<TokenResponse> GetAccesTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}
