namespace Frontend.Shared;

//Shared class for the different components that update shoppingCart
public class CartState
{
    public event Action? OnChange;

    public void NotifyCartChanged() => OnChange?.Invoke();
}
