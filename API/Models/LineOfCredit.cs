namespace API.Models;

/// <summary>
/// Represents a line of credit with a balance that can be manipulated through deposits and withdrawals.
/// </summary>
public class LineOfCredit
{
    private decimal _balance;

    /// <summary>
    /// Gets or sets the provider of the line of credit.
    /// </summary>
    public string Provider { get; set; }

    /// <summary>
    /// Gets the current balance of the line of credit.
    /// </summary>
    /// <returns>The current balance.</returns>
    public decimal GetBalance()
    {
        return _balance;
    }

    /// <summary>
    /// Deposits a specified amount into the line of credit.
    /// </summary>
    /// <param name="amount">The amount to deposit. Must be positive.</param>
    /// <exception cref="ArgumentException">Thrown when the deposit amount is not positive.</exception>
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be positive", nameof(amount));
        }
        _balance += amount;
    }

    /// <summary>
    /// Withdraws a specified amount from the line of credit.
    /// </summary>
    /// <param name="amount">The amount to withdraw. Must be positive and not exceed the current balance.</param>
    /// <exception cref="ArgumentException">Thrown when the withdrawal amount is not positive.</exception>
    /// <exception cref="InvalidOperationException">Thrown when there are insufficient funds for the withdrawal.</exception>
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be positive", nameof(amount));
        }
        if (amount > _balance)
        {
            throw new InvalidOperationException("Insufficient funds");
        }
        _balance -= amount;
    }
}