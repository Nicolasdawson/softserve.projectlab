using Microsoft.AspNetCore.Components;
using Frontend.Repositories;
using MudBlazor;
using Frontend.DTO;

namespace Frontend.Pages.AdminPages.ProductAdmin;

public partial class ProductEdit
{
    private ProductDTO? product;
    private ProductForm? productForm;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public Guid? Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Id.HasValue)
        {
            var responseHttp = await Repository.GetAsync<ProductModel>($"http://localhost:5262/api/product/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/admin-products");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    Snackbar.Add(messageError!, Severity.Error);
                }
            }
            else
            {
                var prod = responseHttp.Response;
                product = new ProductDTO
                {
                    Id = prod!.Id, 
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = prod.Price,
                    Stock = prod.Stock,
                };
            }

        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("http://localhost:5262/api/product/", product);

        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError!, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Record Saved Ok", Severity.Success);
    }

    private void Return()
    {
        productForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("admin-products");
    }
}
