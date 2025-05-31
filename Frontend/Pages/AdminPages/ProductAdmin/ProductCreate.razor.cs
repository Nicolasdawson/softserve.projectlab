using Microsoft.AspNetCore.Components;
using Frontend.Repositories;
using Frontend.DTO;
using MudBlazor;


namespace Frontend.Pages.AdminPages.ProductAdmin;

public partial class ProductCreate
{
    private ProductForm? productForm;
    private ProductDTO product = new();

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private async Task CreateAsync()
    {
        // 1. Build the multipart content
        var multipart = new MultipartFormDataContent();

        // 1.a. Scalar fields
        multipart.Add(new StringContent(product.Name), nameof(product.Name));
        multipart.Add(new StringContent(product.Description), nameof(product.Description));
        multipart.Add(new StringContent(product.Price.ToString()), nameof(product.Price));
        multipart.Add(new StringContent(product.Weight.ToString()), nameof(product.Weight));
        multipart.Add(new StringContent(product.Height.ToString()), nameof(product.Height));
        multipart.Add(new StringContent(product.Width.ToString()), nameof(product.Width));
        multipart.Add(new StringContent(product.Length.ToString()), nameof(product.Length));
        multipart.Add(new StringContent(product.Stock.ToString()), nameof(product.Stock));
        multipart.Add(new StringContent(product.IdCategory.ToString()), nameof(product.IdCategory));
        // …and any others you need…

        // 1.b. File fields
        foreach (var file in product.Images)
        {
            // limit read-stream to 5 MB per file
            var stream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024);
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            // “Images” must match the name of the [FromForm] property in your DTO:
            multipart.Add(fileContent, "Images", file.Name);
        }

        // 2. Post as multipart/form-data
        var response = await Repository.PostMultipartAsync<HttpResponseWrapper<object>>(
            "/api/product",
            multipart);

        if (response.Error)
        {
            var msg = await response.GetErrorMessageAsync();
            Snackbar.Add(msg!, Severity.Error);
            return;
        }

        // 3. On success
        Return();
        Snackbar.Add("Record created!", Severity.Success);
        /*
        var responseHttp = await Repository.PostAsync("https://localhost:7153/api/Product", product);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message!, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Record Created Ok", Severity.Success);
         */
    }

    private void Return()
    {
        productForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/admin-products");
    }
}
