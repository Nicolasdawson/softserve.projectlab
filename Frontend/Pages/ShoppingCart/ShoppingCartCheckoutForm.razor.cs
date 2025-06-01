using Microsoft.AspNetCore.Components;
using Frontend.DTO;
using Frontend.Repositories;
using MudBlazor;

namespace Frontend.Pages.ShoppingCart;

public partial class ShoppingCartCheckoutForm
{
    private List<RegionDTO>? regions;
    private List<CountryDTO>? countries;
    private CountryDTO selectedCountry = new();
    private RegionDTO selectedRegion = new();

    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await LoadCountriesAsync();
    }

    private async Task LoadCountriesAsync()
    {
        var responseHttp = await Repository.GetAsync<List<CountryDTO>>("https://localhost:7153/api/Country");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message!, Severity.Error);
            return;
        }
        countries = responseHttp.Response;
    }

    private async Task LoadRegionsAsync(Guid countryId)
    {
        var responseHttp = await Repository.GetAsync<List<RegionDTO>>($"https://localhost:7153/api/Country/{countryId}/regions");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message!, Severity.Error);
            return;
        }
        regions = responseHttp.Response;
    }

    private async void CountryChanged(CountryDTO country)
    {
        selectedCountry = country;
        //Cargar las regiones y activar el autocomplete de region
        await LoadRegionsAsync(country.Id);
    }

    private void RegionChanged(RegionDTO region)
    {
        selectedRegion = region;
    }

    private async Task<IEnumerable<CountryDTO>> SearchCountries(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return countries!;
        }

        return countries!
            .Where(c => c.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
            .ToList();
    }

    private async Task<IEnumerable<RegionDTO>> SearchRegions(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return regions!;
        }

        return regions!
            .Where(c => c.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
            .ToList();
    }
}
