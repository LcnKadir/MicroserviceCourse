using Dapper;
using FreeCourse.Shared.DTOs;
using Npgsql;
using System.Data;

namespace Free.Course.Services.Discount.Services
{

    public class DiscountService : IDiscountService
    {

        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbconnection;

        public DiscountService(IConfiguration configuration, IDbConnection dbconnection)
        {
            _configuration = configuration;
            _dbconnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbconnection.ExecuteAsync("delete from discount where id=@Id", new { Id=id });

            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("discount not found",404);
        }

        public async Task<Response<List<Model.Discount>>> GetAll()
        {
            var discounts = await _dbconnection.QueryAsync<Model.Discount>("Select*from discount");

            return Response<List<Model.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Model.Discount>> GetByCodeAndUserId(string code, string userId)
        {
         var discount = await _dbconnection.QueryAsync<Model.Discount>("select * from discount where userid=@UserId and code=@Code", new { Code=code, UserId=userId });

            var hasDiscount = discount.FirstOrDefault();

            if(hasDiscount == null) 
            {
                return Response<Model.Discount>.Fail("discount not found", 404);
            }

            return Response<Model.Discount>.Success(hasDiscount, 200);
        }

        public async Task<Response<Model.Discount>> GetById(int id)
        {
            var discount = (await _dbconnection.QueryAsync<Model.Discount>("Select*from discount where id=@Id", new { Id = id })).SingleOrDefault();

            if (discount == null)
            {
                return Response<Model.Discount>.Fail("Discount not found", 400);
            }
            return Response<Model.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Model.Discount discount)
        {
            var saveStatus = await _dbconnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);

            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("an error occurred while adding", 500);
        }

        public async Task<Response<NoContent>> Update(Model.Discount discount)
        {
            var status = await _dbconnection.ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id", new {Id = discount.Id,UserId = discount.UserId, Code = discount.Code, Rate=discount.Rate});
        
            if (status > 0) 
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("discount not found", 404);
        }
    }
}
