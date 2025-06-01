using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Frontend.Repositories;
using Frontend.DTO;

namespace Frontend.Pages.AdminPages.ProductAdmin
{
    public partial class ProductForm
    {
        private string? imageBase64;
        private string? fileName;
        private CategoryDTO selectedCategory = new();
        private List<CategoryDTO>? categories;

        private EditContext editContext = null!;

        protected override void OnInitialized()
        {
            editContext = new(Product);
        }
        [Parameter] public string? Label { get; set; }
        [Parameter] public string? ImageURL { get; set; }
        [Parameter] public EventCallback<string> ImageSelected { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [EditorRequired, Parameter] public ProductDTO Product { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        public bool FormPostedSuccessfully { get; set; } = false;

        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();

            if (!formWasEdited || FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmation",
                Text = "Leave And Lose Changes",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true
            });

            var confirm = !string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            context.PreventNavigation();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (string.IsNullOrWhiteSpace(Label))
            {
                Label = "Image";
            }
        }

        private Task OnFilesSelected(List<IBrowserFile> files)
        {
            // store into your DTO
            Product.Images = files;
            return Task.CompletedTask;
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadCategoriesAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            var responseHttp = await Repository.GetAsync<List<CategoryDTO>>("http://localhost:5262/api/category/paginated/?page=1&recordsnumber=10");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            categories = responseHttp.Response;
        }

        private async Task<IEnumerable<CategoryDTO>> SearchCategory(string searchText, CancellationToken cancellationToken)
        {
            await Task.Delay(5);
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return categories!;
            }

            return categories!
                .Where(x => x.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }

        private void CategoryChanged(CategoryDTO category)
        {
            selectedCategory = category;
            Product.IdCategory = selectedCategory.Id;
        }
    }
}
