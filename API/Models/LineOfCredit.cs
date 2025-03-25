using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class LineOfCredit
{
    [Key]  // Esto indica que 'Id' es la clave primaria
    public Guid Id { get; set; }

    private decimal _Balance;
    
    public string Provider { get; set; }

    public decimal Balance
    {
        get { return _Balance; }
        private set {

            if(value < 0) {
                throw new ArgumentException("The balance cannot be negative.");
            }

            _Balance = value;
        }    
    }

    public void AddBalance(decimal amount){

        if(amount <= 0){
            throw new ArgumentException("The amount to be added must be positive.");
        }
        _Balance += amount;
    }
    public void ReduceBalance(decimal amount){
        if(amount <= 0){
            throw new ArgumentException("The amount to be reduced must be positive.");
        }

        if(_Balance - amount < 0){
            throw new InvalidOperationException("The balance cannot be reduced below zero.");
        }

        _Balance -= amount;

    }
    
}