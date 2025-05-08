using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Frontend.Pages.Product
{
    public partial class ProductList
    {

        public ProductModel[]? products;

        private MudTable<ProductModel> table = new();
        private int totalRecords = 0;
        
        private int currentPage = 1;
        private int pageSize = 10;
        private readonly int[] pageSizeOptions = { 5, 10, 20 };  
        
        private string infoFormat = "{first_item}-{last_item} => {all_items}";
        private bool loading;

        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;


        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadTotalRecords();
        }

        private async Task LoadTotalRecords()
        {
            products = await ProductService.GetProducts(currentPage, pageSize);                        
        }

        private async Task NextPage()
        {
            currentPage++;
            await LoadTotalRecords();
            await JSRuntime.InvokeVoidAsync("scrollToTop");
        }

        private async Task PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                await LoadTotalRecords();
                await JSRuntime.InvokeVoidAsync("scrollToTop");
            }
        }

        private async Task OnPageSizeChanged(int newSize)
        {
            pageSize = newSize;
            currentPage = 1;
            await LoadTotalRecords();
        }
        /*
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
    }
}
