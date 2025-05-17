// LineOfCreditService.cs
using API.Implementations.Domain;
using API.Models.Customers;
using API.Services.Interfaces;
using softserve.projectlabs.Shared.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class LineOfCreditService : ILineOfCreditService
    {
        private readonly LineOfCreditDomain _lineOfCreditDomain;

        public LineOfCreditService(LineOfCreditDomain lineOfCreditDomain)
        {
            _lineOfCreditDomain = lineOfCreditDomain;
        }

        public async Task<Result<LineOfCredit>> AddLineOfCreditAsync(LineOfCredit lineOfCredit, int customerId)
        {
            return await _lineOfCreditDomain.CreateLineOfCreditAsync(lineOfCredit, customerId);
        }

        public async Task<Result<LineOfCredit>> GetLineOfCreditByIdAsync(string lineOfCreditId)
        {
            return await _lineOfCreditDomain.GetLineOfCreditByIdAsync(lineOfCreditId);
        }

        public async Task<Result<LineOfCredit>> GetLineOfCreditByCustomerIdAsync(int customerId)
        {
            return await _lineOfCreditDomain.GetLineOfCreditByCustomerIdAsync(customerId);
        }

        public async Task<Result<LineOfCredit>> UpdateLineOfCreditAsync(LineOfCredit lineOfCredit)
        {
            return await _lineOfCreditDomain.UpdateLineOfCreditAsync(lineOfCredit);
        }

        public async Task<Result<bool>> DeleteLineOfCreditAsync(string lineOfCreditId)
        {
            return await _lineOfCreditDomain.DeleteLineOfCreditAsync(lineOfCreditId);
        }

        public async Task<Result<List<CreditTransaction>>> GetTransactionsAsync(string lineOfCreditId)
        {
            return await _lineOfCreditDomain.GetTransactionsAsync(lineOfCreditId);
        }

        public async Task<Result<CreditTransaction>> AddTransactionAsync(string lineOfCreditId, CreditTransaction transaction)
        {
            return await _lineOfCreditDomain.AddTransactionAsync(lineOfCreditId, transaction);
        }
    }
}