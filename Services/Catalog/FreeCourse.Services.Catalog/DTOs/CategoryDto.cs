using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FreeCourse.Services.Catalog.DTOs
{
    public class CategoryDto
    {

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
