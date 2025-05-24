using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.ViweModels
{
	public class RegisterViewModel
	{
		public string FName { get; set; }
		public string LName { get; set; }
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage ="Invalid")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password is Required")]
		[Compare("Password",ErrorMessage ="Confirm Password does't match password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
		public bool Agree { get; set; }
	}
}
