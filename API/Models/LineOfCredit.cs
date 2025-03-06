namespace API.Models;

public class LineOfCredit
{
    private decimal _Balance;
    
    public string Provider { get; set; }

    // TODO: use encapsulation to manipulate the balance of the line of credit 

    public decimal Balance
    {
        get => _Balance;
        private set => _Balance = value;  // Solo se puede modificar dentro de la clase
    }

    public void AddFunds(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("El monto debe ser mayor que cero.");

        _Balance += amount;
    }

    public bool DeductFunds(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("El monto debe ser mayor que cero.");

        if (_Balance < amount)
            return false; // No hay suficiente saldo

        _Balance -= amount;
        return true;
    }
}