
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Frontend.DTO;
using Frontend.Repositories;
using Frontend.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace Frontend.Pages.Auth;

public partial class Login
{
    private UserDTO userDTO = new();
    private bool wasClose;

   
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ILoginService LoginService { get; set; } = null!;
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;


    private void CloseModal()
    {
        wasClose = true;
        MudDialog.Cancel();
    }

    private async Task LoginAsync()
    {

        if (wasClose)
        {
            NavigationManager.NavigateTo("/");
            return;
        }

    
        userDTO.FirstName = "string";
        userDTO.LastName = "string";
        userDTO.PhoneNumber = "string";

        var responseHttp = await Repository.PostAsync<UserDTO, TokenDTO>("https://localhost:7153/api/users/login", userDTO);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message!, Severity.Error);
            return;
        }

        // Saving token
        await LoginService.LoginAsync(responseHttp.Response!.Token);

        // Reading the rol in token
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(responseHttp.Response!.Token);

        var roleClaim = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (roleClaim == "Admin")
            NavigationManager.NavigateTo("/admin-panel");
        else if (roleClaim == "Normal")
            NavigationManager.NavigateTo("/user-home");
    }
}
