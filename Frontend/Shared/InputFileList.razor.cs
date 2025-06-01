using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Frontend.DTO;
using MudBlazor;

namespace Frontend.Shared
{
    public partial class InputFileList
    {
        private string? _fileNames;
        private string? _errorMessage;

        /// <summary>Label shown above the control.</summary>
        [Parameter] public string Label { get; set; } = "Files";
        /// <summary>Accept string, e.g. ".jpg,.png"</summary>
        [Parameter] public string Accept { get; set; } = ".jpg,.jpeg,.png";
        /// <summary>Whether multiple files are allowed.</summary>
        [Parameter] public bool AllowMultiple { get; set; } = true;
        /// <summary>Text on the upload button.</summary>
        [Parameter] public string ButtonText { get; set; } = "Browse";
        /// <summary>Placeholder in the readonly textbox.</summary>
        [Parameter] public string Placeholder { get; set; } = "Select files...";
        /// <summary>Max number of files.</summary>
        [Parameter] public int MaxFiles { get; set; } = 3;
        /// <summary>Max size per file in bytes (default 5 MB).</summary>
        [Parameter] public long MaxFileSizeBytes { get; set; } = 5 * 1024 * 1024;

        /// <summary>Emits validated list of files.</summary>
        [Parameter] public EventCallback<List<IBrowserFile>> FilesSelected { get; set; }
        [Parameter] public ProductDTO? Product { get; set; }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (string.IsNullOrWhiteSpace(Label))
            {
                Label = "Image";
            }
        }

        private async Task OnChange(InputFileChangeEventArgs e)
        {
            _errorMessage = null;
            var files = e.GetMultipleFiles(MaxFiles + 1).ToList();

            // Validate count
            if (files.Count > MaxFiles)
            {
                _errorMessage = $"You can only upload up to {MaxFiles} files.";
                StateHasChanged();
                return;
            }

            // Validate size
            if (files.Any(f => f.Size > MaxFileSizeBytes))
            {
                _errorMessage = $"Each file must be ≤ {MaxFileSizeBytes / (1024 * 1024)} MB.";
                StateHasChanged();
                return;
            }

            // Build display string
            _fileNames = string.Join(", ", files.Select(f => f.Name));

            // Raise to parent form
            await FilesSelected.InvokeAsync(files);
        }
    }
}
