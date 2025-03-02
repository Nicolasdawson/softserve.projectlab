namespace API.Models;

public class LineOfCredit
{
    private decimal _Balance;
    
    public string Provider { get; set; }

    public decimal Balance
    {
        get { return _Balance; }
        private set {

            if(value < 0) {
                throw new ArgumentException("El saldo no puede ser negativo.");
            }

            _Balance = value;
        }    
    }

    public void AddBalance(decimal amount){

        if(amount <= 0){
            throw new ArgumentException("La cantidad a agregar debe ser positiva.");
        }
        _Balance += amount;
    }
    public void ReduceBalance(decimal amount){
        if(amount <= 0){
            throw new ArgumentException("La cantida a reducir debe ser positiva");
        }

        if(_Balance - amount < 0){
            throw new InvalidOperationException("No se puede reducir el saldo por debajo de cero.");
        }

        _Balance -= amount;

    }
    
}