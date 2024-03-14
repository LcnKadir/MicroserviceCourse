namespace Free.Course.Web.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<String> GetToken();
    }
}
