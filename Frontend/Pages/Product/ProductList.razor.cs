namespace Frontend.Pages.Product
{
    public partial class ProductList
    {
        private string baseUrl = "api/Product";

        private ProductModel[]? products;
        string urlLocal = "https://localhost:7153";
        protected override async Task OnInitializedAsync()
        {
            products = await ProductService.GetProducts();
        }
    }
}
