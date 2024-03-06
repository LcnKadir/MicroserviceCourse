using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeCourse.Shared.Services
{
    public class SharedIdentityService : ISharedIndetityService
    {
        private IHttpContextAccessor _httpcontextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpcontextAccessor)
        {
            _httpcontextAccessor = httpcontextAccessor;
        }

        public string GetUserId => _httpcontextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}
