using FreeCourse.Shared.DTOs;
using IdentityModel.Client;

namespace Free.Course.Web.Services.Interfaces
{
    interface IdentityService
    {
        Task<Response<bool>> SignIn();

        Task<TokenResponse> GetAccesTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}
