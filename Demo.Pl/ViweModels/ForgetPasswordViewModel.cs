using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.ViweModels
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Invalid")]
        public string Email { get; set; } 
    }
}
