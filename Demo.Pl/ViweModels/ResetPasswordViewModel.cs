using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.ViweModels
{
	public class ResetPasswordViewModel
	{
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password),Compare("Password",ErrorMessage ="ConfirmPassword Doesn't Match Password")]
		public string ConfirmPassword { get; set; }
	}
}
