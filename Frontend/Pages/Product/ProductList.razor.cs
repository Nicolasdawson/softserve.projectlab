using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Frontend.Repositories;


namespace Frontend.Pages.Product
{
    public partial class ProductList
    {

        public ProductModel[]? products;

        private MudTable<ProductModel> table = new();
        private int totalRecords = 1;
        
        private int currentPage = 1;
        private int pageSize = 10;
        private int numberOfPages = 1;
        private readonly int[] pageSizeOptions = { 5, 10, 20 };  
        
        private string infoFormat = "{first_item}-{last_item} => {all_items}";
        private bool loading;

        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] private IRepository Repository { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadTotalRecordsAsync();
            await LoadListAsync();
        }

        private async Task LoadListAsync()
        {
            products = await ProductService.GetProducts(currentPage, pageSize);                        
        }

        private async Task LoadTotalRecordsAsync()
        {    
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
            numberOfPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            loading = false;
        }

        private async Task OnPageChanged(int newPage)
        {
            currentPage = newPage;
            await LoadListAsync();
            await JSRuntime.InvokeVoidAsync("scrollToTop");
        }

        private async Task OnPageSizeChanged(int newSize)
        {
            pageSize = newSize;
            numberOfPages = (int)Math.Ceiling((double)totalRecords / newSize); 
            currentPage = 1;
            await LoadListAsync();
        }

        private async Task AddToCart(ProductModel product)
        {
            Console.WriteLine(product);
            var item = new CartItemModel
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1,
                Category = product.CategoryName,
                ImageUrl = product.ImageUrls.FirstOrDefault() ?? "images/placeholder.jpg",
                Stock = product.Stock
            };

            await CartService.AddToCartAsync(item);
            CartState.NotifyCartChanged();
        }

    }
}

/*
 * 
private async Task NextPage()
{
    currentPage++;
    await LoadListAsync();
    await JSRuntime.InvokeVoidAsync("scrollToTop");
}


private async Task PreviousPage()
{
    if (currentPage > 1)
    {
        currentPage--;
        await LoadListAsync();
        await JSRuntime.InvokeVoidAsync("scrollToTop");
    }
}
 * 
 * 
 * 
private async Task<TableData<ProductModel>> LoadListAsync(TableState state, CancellationToken cancellationToken)
{
    int page = state.Page + 1;
    int pageSize = state.PageSize;
    var url = $"{urlLocal}/{baseUrl}";

    if (!string.IsNullOrWhiteSpace(Filter))
    {
        //url += $"&filter={Filter}";
    }

    var prodList = await ProductService.GetProducts();

    return new TableData<ProductModel>
    {
        Items = prodList,
        TotalItems = totalRecords,
    };
}
 */
