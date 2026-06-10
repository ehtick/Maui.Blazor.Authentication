// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OidcAndApiServer.Pages.Ciba;

[AllowAnonymous]
[SecurityHeaders]
public class IndexModel(IBackchannelAuthenticationInteractionService backchannelAuthenticationInteractionService,
    ILogger<IndexModel> logger) : PageModel
{
    public BackchannelUserLoginRequest LoginRequest { get; set; }

    public async Task<IActionResult> OnGet(string id)
    {
        LoginRequest = await backchannelAuthenticationInteractionService.GetLoginRequestByInternalIdAsync(id, HttpContext.RequestAborted);
        if (LoginRequest == null)
        {
            logger.LogWarning("Invalid backchannel login id {Id}", id);
            return RedirectToPage("/Home/Error/Index");
        }

        return Page();
    }
}