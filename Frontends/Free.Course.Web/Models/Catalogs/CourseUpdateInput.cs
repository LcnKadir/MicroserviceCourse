using System.ComponentModel.DataAnnotations;

namespace Free.Course.Web.Models.Catalogs
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }
        

        [Display(Name = "Kurs ismi")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Kurs açıklama")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Kurs fiyat")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Kurs Resmi")]
        [Required]
        public string? Picture { get; set; }
        public string UserId { get; set; }
        public FeatureViewModel? Feature { get; set; }

        [Display(Name = "Kurs kategori")]
        [Required]
        public string? CategoryId { get; set; }


        [Display(Name = "Kurs Resmi")]
        [Required]
        public IFormFile PhotoFormFile { get; set; }

    }
}
