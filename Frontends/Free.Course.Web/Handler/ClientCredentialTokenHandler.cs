using Free.Course.Web.Exceptions;
using Free.Course.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace Free.Course.Web.Handler
{
    public class ClientCredentialTokenHandler: DelegatingHandler
    {
        private readonly IClientCredentialTokenService _clientCredentialTokenService;

        public ClientCredentialTokenHandler(IClientCredentialTokenService clientCredentialTokenService)
        {
            _clientCredentialTokenService = clientCredentialTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _clientCredentialTokenService.GetToken());

            var response = await base.SendAsync(request, cancellationToken);

           if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized) 
            {
                throw new UnAuthorizeException();
            }
           return response;
        }
    }
}
