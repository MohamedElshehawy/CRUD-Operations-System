using Demo.DAL.Entities;
using Demo.Pl.Utility;
using Demo.Pl.ViweModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Demo.Pl.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Register()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid) // Server Side Validation
				return View(model);
			var user = new AppUser
			{
				FName = model.FName,
				LName = model.LName,
				Email = model.Email,
				Agree = model.Agree,
				UserName=model.FName+model.LName

			};
			// create user
			var result= await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded) return RedirectToAction(nameof(Login));

			foreach (var error in result.Errors)
				ModelState.AddModelError("",error.Description);
			return View(model);
			
		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid ) return View(model);
			// Get user
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is not null)
			{
				// Check Password for User
				if (await _userManager.CheckPasswordAsync(user,model.Password))
				{
					// Login
					var result = await _signInManager.PasswordSignInAsync(user,model.Password, model.RememberMe,false);
					if (result.Succeeded) return RedirectToAction("Index", "Home");

				}
			}
			ModelState.AddModelError("", "Incorrect Email OR Password");
			return View();


		}

		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}


		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model )
		{
			if (!ModelState.IsValid) return View(model);

			// get user by email
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is not null)
			{
				//genrate password reset link
				//genrate password reset Token
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var url = Url.Action("ResetPassword", "Account", new {email=model.Email , token=token},Request.Scheme );

				var email = new Email
				{
					Recipient = model.Email,
					Subject = "Rest Password",
					Body = url
				};
				//sent email to user 
				MailSettings.SendEmail(email);
				return RedirectToAction(nameof(CheckYourInBox));
			}

			ModelState.AddModelError("", "Email Doesn't Exist");
			return View();
		}
		public IActionResult CheckYourInBox()
		{
			return View(); 
		}

		public IActionResult ResetPassword(string email , string token)
		{
			TempData["email"]=email;
			TempData["token"]=token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (!ModelState.IsValid) return View();
			var email = TempData["email"] as string;
			var token = TempData["token"] as string;
			var user = await  _userManager.FindByEmailAsync(email);
			if (user is not null)
			{
				var result = await _userManager.ResetPasswordAsync(user,token,model.Password);
				if (result.Succeeded) return RedirectToAction(nameof(Login));
				foreach (var item in result.Errors)
					ModelState.AddModelError("",item.Description);

				
			}
			return View(model);
		}

	}
}
