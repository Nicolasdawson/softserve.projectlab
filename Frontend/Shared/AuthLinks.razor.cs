using Frontend.Pages.Auth;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Frontend.Shared;

public partial class AuthLinks
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;

    private void EditAction()
    {
        NavigationManager.NavigateTo("/EditUser");
    }

    private async Task ShowModalLogIn()
    {
        //var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
        //await DialogService.ShowAsync<Login>("Login", closeOnEscapeKey);
        NavigationManager.NavigateTo("/Login");
    }

    private async Task ShowModalLogOut()
    {
        var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
        await DialogService.ShowAsync<Logout>("Logout", closeOnEscapeKey);
    }
}
