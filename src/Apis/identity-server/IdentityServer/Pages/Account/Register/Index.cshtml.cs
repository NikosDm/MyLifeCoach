using System;
using System.Threading.Tasks;

using IdentityServer.Core.Abstractions;
using IdentityServer.Core.Dtos.Requests;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.Account.Register;

[SecurityHeaders]
[AllowAnonymous]
[ValidateAntiForgeryToken]
public class Index(IAccountService accountService) : PageModel
{
    private readonly IAccountService _accountService = accountService
        ?? throw new ArgumentNullException(nameof(accountService));

    [BindProperty]
    public RegisterViewModel Input { get; set; }

    [BindProperty]
    public bool RegisterSuccess { get; set; }

    [BindProperty]
    public bool Loading { get; set; }

    public IActionResult OnGet(string returnUrl)
    {
        Input = new RegisterViewModel
        {
            ReturnUrl = returnUrl,
        };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (Input.Button != "register") return Redirect("~/");

        if (ModelState.IsValid)
        {
            Loading = true;
            var request = new CreateUserRequest(Input.Email, Input.Password, Input.Username, Input.FullName);
            var result = await _accountService.CreateAsync(request);

            if (result.Result.Succeeded)
            {
                RegisterSuccess = true;
            }
            else
            {
                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            Loading = false;
        }

        return Page();
    }
}
