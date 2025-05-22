
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

        Console.WriteLine("Email desde form: " + userDTO.Email);
        Console.WriteLine("Password desde form: " + userDTO.Password);
        Console.WriteLine(userDTO.FirstName = "string");
        Console.WriteLine(userDTO.LastName = "string");
        Console.WriteLine(userDTO.PhoneNumber = "string");
        var responseHttp = await Repository.PostAsync<UserDTO, TokenDTO>("https://localhost:7153/api/users/login", userDTO);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            //Snackbar.Add(Localizer[message!], Severity.Error);
        }

        await LoginService.LoginAsync(responseHttp.Response!.Token);
        NavigationManager.NavigateTo("/");
    }
}
