using Free.Course.Web.Models;

namespace Free.Course.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
