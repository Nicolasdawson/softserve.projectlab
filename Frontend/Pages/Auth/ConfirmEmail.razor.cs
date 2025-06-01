using Microsoft.AspNetCore.Components;
using Frontend.Repositories;
using MudBlazor;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages.Auth;

public partial class ConfirmEmail
{
    private string? message;
    private bool isRedirecting = false;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Parameter, SupplyParameterFromQuery] public string UserId { get; set; } = string.Empty;
    [Parameter, SupplyParameterFromQuery] public string Token { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync($"https://localhost:7153/api/users/activate/{Token}");
        if (responseHttp.Error)
        {
            message = await responseHttp.GetErrorMessageAsync();
            NavigationManager.NavigateTo("/");
            Snackbar.Add(message, Severity.Error);
            return;
        }
        
        Snackbar.Add("Confirmed Email, please login", Severity.Success);
        // Espera 3 segundos antes de navegar a /login
        await Task.Delay(3000);
        Snackbar.Add("Redirecting you to the login...", Severity.Info);
        await Task.Delay(1000); 
        NavigationManager.NavigateTo("/login");
    }



    private async Task StartTimer(int time)
    {
        isRedirecting = true;
        var timer = new System.Timers.Timer(time);  // 50 segundos
        timer.Elapsed += (sender, e) =>
        {
            Snackbar.Add("Redirecting you to the login...", Severity.Info);
            NavigationManager.NavigateTo("/login");
        };
        timer.Start();
    }

}
