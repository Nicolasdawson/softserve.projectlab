using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Frontend.Repositories;
using Frontend.DTO;

namespace Frontend.Pages.Auth;
public partial class Register
{
    CustomerDTO model = new CustomerDTO();

    private bool loading = false;
    private bool isSucceedRegister = false;
    private string? titleLabel;


    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        isSucceedRegister = false;
    }
    private void ReturnAction()
    {
        NavigationManager.NavigateTo("/");
    }

    private async Task CreateUserAsync()
    {
        if (!ValidateForm())
        {
            return;
        }

        loading = true;
        var responseHttp = await Repository.PostAsync<CustomerDTO>("https://localhost:7153/api/users/register", model);
        loading = false;
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();            
            Snackbar.Add("Email Already Exists", Severity.Error);
            return;            
        }
        else if(responseHttp == null)
        {
            Snackbar.Add("We are currently experiencing connection issues. Please try again later.", Severity.Error);            
        }
            isSucceedRegister = true;
            return;

    }

    private bool ValidateForm()
    {
        var hasErrors = false;
        if (string.IsNullOrEmpty(model.FirstName))
        {
            Snackbar.Add("Required Field First Name", Severity.Error);
            hasErrors = true;
        }
        if (string.IsNullOrEmpty(model.LastName))
        {
            Snackbar.Add("Required Field Last Name", Severity.Error);
            hasErrors = true;
        }
        if (string.IsNullOrEmpty(model.phoneNumber))
        {
            Snackbar.Add("Required Field Phone Number", Severity.Error);
            hasErrors = true;
        }
        if (string.IsNullOrEmpty(model.Email))
        {
            Snackbar.Add("Required Field Email", Severity.Error);
            hasErrors = true;
        }
        if (string.IsNullOrEmpty(model.Password))
        {
            Snackbar.Add("Required Field Password", Severity.Error);
            hasErrors = true;
        }
        if (string.IsNullOrEmpty(model.PasswordConfirm))
        {
            Snackbar.Add("Required Field Password Confirm", Severity.Error);
            hasErrors = true;
        }

        return !hasErrors;
    }
}
