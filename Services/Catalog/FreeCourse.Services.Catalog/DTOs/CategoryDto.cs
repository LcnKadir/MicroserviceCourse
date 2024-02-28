using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FreeCourse.Services.Catalog.DTOs
{
    internal class CategoryDto
    {

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
