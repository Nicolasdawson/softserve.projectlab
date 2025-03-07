namespace API.Models;

public class LineOfCredit
{
    private decimal _Balance;
    
    public string Provider { get; set; }

    // TODO: use encapsulation to manipulate the balance of the line of credit 

    // Method to add funds to the line of credit
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("El depósito debe ser mayor que cero.");

        _Balance += amount;
    }

    // Method to use credit (withdraw funds)
    public bool WithDraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("El monto a retirar debe ser mayor que cero.");

        if (amount > _Balance)
            return false; // No se puede retirar más de lo disponible

        _Balance -= amount;
        return true;
    }
}