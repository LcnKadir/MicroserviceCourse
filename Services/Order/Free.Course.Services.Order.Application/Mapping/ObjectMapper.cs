using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free.Course.Services.Order.Application.Mapping
{
    public static class ObjectMapper
    {
        //Lazy will initialize only when using automapper, not when the project stands up. //Lazy proje ayağa kalktığında değil, sadece automapper kullanımında initialization edecek.

        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
