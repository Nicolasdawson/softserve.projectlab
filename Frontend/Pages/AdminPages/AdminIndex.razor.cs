using Microsoft.AspNetCore.Authorization;

namespace Frontend.Pages.AdminPages;

[Authorize(Roles ="Normal")]
public partial class AdminIndex
{
    private bool loading;

    protected override async Task OnInitializedAsync()
    {
        loading = true;
    }
}
