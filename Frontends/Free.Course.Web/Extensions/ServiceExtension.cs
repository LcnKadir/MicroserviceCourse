using Free.Course.Web.Handler;
using Free.Course.Web.Models;
using Free.Course.Web.Services;
using Free.Course.Web.Services.Interfaces;

namespace Free.Course.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void AddHttpClientServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            //HTTPCLİENTS
            Services.AddHttpClient<IIdentityService, IdentityService>();
            var serviceApiSetting = Configuration.GetSection(nameof(ServiceApiSettings)).Get<ServiceApiSettings>();


            Services.AddHttpClient<ICatalogService, CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSetting.GatewayBaseUri}/{serviceApiSetting.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            Services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSetting.GatewayBaseUri}/{serviceApiSetting.PhotoStock.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();


            Services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSetting.IdentityBaseUri);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            Services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSetting.GatewayBaseUri}/{serviceApiSetting.Basket.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();


        }
    }
}
