namespace API.Models;

public class LineOfCredit
{
    private decimal _Balance;
    
    public string Provider { get; set; }

    public LineOfCredit(string provider, decimal balance)
    {
        if(balance < 0)
        {
            throw new ArgumentException("Balance must be positive");
        }

        this.Provider = provider;
        this._Balance = balance;
    }

    public decimal GetBalance()
    {

        return this._Balance;
    }

    public void AddBalance(decimal amount)
    {
        if(amount < 0)
        {
            throw new ArgumentException("Amount must be positive");
        }

        this._Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive");
        }
        if (amount > this._Balance)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be less than the balance");
        }
        this._Balance -= amount;
    }
}