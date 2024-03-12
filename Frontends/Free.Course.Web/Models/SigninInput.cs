using System.ComponentModel.DataAnnotations;

namespace Free.Course.Web.Models
{
    public class SignInİnput
    {
        //Data will be taken from the user. //Kullanıcıdan data alınacak.

        [Display(Name ="Email adresiniz")]
        public string Email { get; set; }

        [Display(Name ="Şifreniz")]
        public string Password { get; set; }

        [Display(Name ="Beni Hatırla")]
        public bool IsRemember { get; set; }
    }
}
