using FreeCourse.Shared.DTOs;

namespace Free.Course.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Model.Discount>>> GetAll();

        Task<Response<Model.Discount>> GetById(int id);

        Task<Response<NoContent>> Save(Model.Discount discount);

        Task<Response<NoContent>> Update(Model.Discount discount);

        Task<Response<NoContent>> Delete(int id);

        Task<Response<Model.Discount>> GetByCodeAndUserId(string code, string userId);
    }
}
