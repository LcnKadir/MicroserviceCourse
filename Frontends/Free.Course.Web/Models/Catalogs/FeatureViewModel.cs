using System.ComponentModel.DataAnnotations;

namespace Free.Course.Web.Models.Catalogs
{
    public class FeatureViewModel
    {

        [Display(Name = "Kurs süre")]
        [Required]
        public int Duration { get; set; }
    }
}
