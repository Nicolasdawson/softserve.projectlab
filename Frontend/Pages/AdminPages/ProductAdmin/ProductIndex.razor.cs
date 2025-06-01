using Frontend.Shared;
using Frontend.Repositories;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Frontend.Pages.AdminPages.ProductAdmin;

[Authorize(Roles = "Admin")]
public partial class ProductIndex
{
    private List<ProductModel>? Products { get; set; }
    private MudTable<ProductModel> table = new();
    private readonly int[] pageSizeOptions = { 10, 25, 50, int.MaxValue };
    private int totalRecords = 0;
    private bool loading;
    private const string baseUrl = "http://localhost:5262/api/product/paginated/";
    private string infoFormat = "{first_item}-{last_item} => {all_items}";

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadTotalRecordsAsync();
    }

    private async Task LoadTotalRecordsAsync()
    {
        int page = 1;
        int pageSize = 10;
        loading = true;
        var url = $"http://localhost:5262/api/product/totalRecords";

        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"&filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<int>(url);

        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            return;
        }

        totalRecords = responseHttp.Response;
        loading = false;
    }

    private async Task<TableData<ProductModel>> LoadListAsync(TableState state, CancellationToken cancellationToken)
    {
        int page = state.Page + 1;
        int pageSize = state.PageSize;
        var url = $"{baseUrl}?page={page}&recordsnumber={pageSize}";

        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"&filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<List<ProductModel>>(url);
        Console.Write(responseHttp);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();            
            return new TableData<ProductModel> { Items = [], TotalItems = 0 };
        }
        if (responseHttp.Response == null)
        {
            return new TableData<ProductModel> { Items = [], TotalItems = 0 };
        }
        return new TableData<ProductModel>
        {
            Items = responseHttp.Response,
            TotalItems = totalRecords
        };
    }

    private async Task ShowModalAsync1()
    {
        await ShowModalAsync(Guid.Empty, false);
    }

    private async Task ShowModalAsync(Guid id, bool isEdit = false)
    {
        var options = new DialogOptions() { CloseOnEscapeKey = true, CloseButton = true };
        IDialogReference? dialog;
        if (isEdit)
        {
            Console.WriteLine("Editando");
            var parameters = new DialogParameters
            {
                { "Id", id }
            };
            dialog = await DialogService.ShowAsync<ProductEdit>("Edit Product", parameters, options);
        }
        else
        {
            Console.WriteLine("Creando producto");
            dialog = await DialogService.ShowAsync<ProductCreate>("New Product", options);
        }

        var result = await dialog.Result;
        
        if (result!.Canceled)
        {
            await LoadTotalRecordsAsync();
            await table.ReloadServerData();
        }
        
    }
    private async Task DeleteAsync(ProductModel product)
    {
        /*
        
        var parameters = new DialogParameters
        {
            { "Message", string.Format(Localizer["DeleteConfirm"], Localizer["Country"], country.Name) }
        };
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, CloseOnEscapeKey = true };
        var dialog = DialogService.Show<ConfirmDialog>(Localizer["Confirmation"], parameters, options);
        var result = await dialog.Result;
        if (result!.Canceled)
        {
            return;
        }

        var responseHttp = await Repository.DeleteAsync($"{baseUrl}/{country.Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/countries");
            }
            else
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(Localizer[message!], Severity.Error);
            }
            return;
        }
        await LoadTotalRecordsAsync();
        await table.ReloadServerData();
        Snackbar.Add(Localizer["RecordDeletedOk"], Severity.Success);
        */
    }
}
