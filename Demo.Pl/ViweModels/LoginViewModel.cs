using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.ViweModels
{
	public class LoginViewModel
	{
		[EmailAddress(ErrorMessage = "Invalid")]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
