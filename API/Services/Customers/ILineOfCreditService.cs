using API.Models.Customers;
using softserve.projectlabs.Shared.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ILineOfCreditService
    {
        Task<Result<LineOfCredit>> AddLineOfCreditAsync(LineOfCredit lineOfCredit, int customerId);
        Task<Result<LineOfCredit>> GetLineOfCreditByIdAsync(string lineOfCreditId);
        Task<Result<LineOfCredit>> GetLineOfCreditByCustomerIdAsync(int customerId);
        Task<Result<LineOfCredit>> UpdateLineOfCreditAsync(LineOfCredit lineOfCredit);
        Task<Result<bool>> DeleteLineOfCreditAsync(string lineOfCreditId);
        Task<Result<List<CreditTransaction>>> GetTransactionsAsync(string lineOfCreditId);
        Task<Result<CreditTransaction>> AddTransactionAsync(string lineOfCreditId, CreditTransaction transaction);
    }
}