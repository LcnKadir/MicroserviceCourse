using AutoMapper;
using FreeCourse.Services.Catalog.DTOs;
using FreeCourse.Services.Catalog.Models;

namespace FreeCourse.Services.Catalog.Mapping
{
    public class GeneralMapping: Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category,CategortDto>().ReverseMap();
            CreateMap<Course,CourseDto>().ReverseMap();
            CreateMap<Feature,FeatureDto>().ReverseMap();

            CreateMap<Course,CourseCreateDto>().ReverseMap();
            CreateMap<Course, CoruseUpdateDto>().ReverseMap();
        }
    }
}
