namespace API.Models;

public class LineOfCredit
{
    private decimal _Balance;

    public string Provider { get; set; }

    public decimal Balance
    {
        get
        {
            return _Balance;
        }
        set
        {
            _Balance = value;
        }
    }
    // TODO: use encapsulation to manipulate the balance of the line of credit 
}