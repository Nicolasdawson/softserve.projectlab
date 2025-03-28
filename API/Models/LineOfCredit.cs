namespace API.Models;

public class LineOfCredit
{
    public int Id { get; set; }
    public string Name { get; set; }
    private decimal _Balance;
    
    public string Provider { get; set; }

    public decimal CreditLimit { get; private set; }

    public LineOfCredit(decimal creditLimit, string provider)
    {
        CreditLimit = creditLimit;
        Provider = provider;
        _Balance = 0; 
    }

    // Método para obtener el crédito disponible
    public decimal GetAvailableCredit()
    {
        return CreditLimit - _Balance;
    }

    public bool UseCredit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("El monto debe ser mayor a cero.");

        if (_Balance + amount > CreditLimit)
            return false; // No hay suficiente crédito disponible

        _Balance += amount;
        return true; // Compra realizada con éxito
    }

    public void MakePayment(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("El pago debe ser mayor a cero.");

        _Balance -= amount;

        if (_Balance < 0)
            _Balance = 0; // No puede haber saldo negativo
    }

    public decimal GetBalance()
    {
        return _Balance;
    }
    // TODONE: use encapsulation to manipulate the balance of the line of credit 
}