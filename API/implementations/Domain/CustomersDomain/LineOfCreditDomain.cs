using API.Data;
using API.Data.Entities;
using API.Models.Customers;
using Microsoft.EntityFrameworkCore;
using softserve.projectlabs.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Implementations.Domain
{
    public class LineOfCreditDomain
    {
        private readonly ApplicationDbContext _context;

        public LineOfCreditDomain(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<LineOfCredit>> CreateLineOfCreditAsync(LineOfCredit lineOfCredit, int customerId)
        {
            try
            {
                // Verificar si el cliente existe
                var customerEntity = await _context.CustomerEntities.FindAsync(customerId);
                if (customerEntity == null)
                {
                    return Result<LineOfCredit>.Failure("Cliente no encontrado");
                }

                // Verificar si el cliente ya tiene una línea de crédito
                var existingLineOfCredit = await _context.LineOfCreditEntities
                    .FirstOrDefaultAsync(l => l.CustomerId == customerId);

                if (existingLineOfCredit != null)
                {
                    return Result<LineOfCredit>.Failure("El cliente ya tiene una línea de crédito");
                }

                // Crear la entidad de línea de crédito
                var lineOfCreditEntity = new LineOfCreditEntity
                {
                    CustomerId = customerId,
                    CreditLimit = lineOfCredit.CreditLimit,
                    CurrentBalance = 0, // Inicialmente sin balance
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.LineOfCreditEntities.Add(lineOfCreditEntity);
                await _context.SaveChangesAsync();

                // Asignar el ID generado al modelo
                lineOfCredit.Id = lineOfCreditEntity.CustomerId.ToString();

                return Result<LineOfCredit>.Success(lineOfCredit);
            }
            catch (Exception ex)
            {
                return Result<LineOfCredit>.Failure($"Error al crear la línea de crédito: {ex.Message}");
            }
        }

        public async Task<Result<LineOfCredit>> GetLineOfCreditByIdAsync(string lineOfCreditId)
        {
            try
            {
                if (!int.TryParse(lineOfCreditId, out int id))
                {
                    return Result<LineOfCredit>.Failure("ID de línea de crédito inválido");
                }

                var lineOfCreditEntity = await _context.LineOfCreditEntities
                    .Include(l => l.CreditTransactionEntities)
                    .FirstOrDefaultAsync(l => l.CustomerId == id);

                if (lineOfCreditEntity == null)
                {
                    return Result<LineOfCredit>.Failure("Línea de crédito no encontrada");
                }

                var lineOfCredit = MapToModel(lineOfCreditEntity);
                return Result<LineOfCredit>.Success(lineOfCredit);
            }
            catch (Exception ex)
            {
                return Result<LineOfCredit>.Failure($"Error al obtener la línea de crédito: {ex.Message}");
            }
        }

        public async Task<Result<LineOfCredit>> GetLineOfCreditByCustomerIdAsync(int customerId)
        {
            try
            {
                var lineOfCreditEntity = await _context.LineOfCreditEntities
                    .Include(l => l.CreditTransactionEntities)
                    .FirstOrDefaultAsync(l => l.CustomerId == customerId);

                if (lineOfCreditEntity == null)
                {
                    return Result<LineOfCredit>.Failure("Línea de crédito no encontrada para el cliente");
                }

                var lineOfCredit = MapToModel(lineOfCreditEntity);
                return Result<LineOfCredit>.Success(lineOfCredit);
            }
            catch (Exception ex)
            {
                return Result<LineOfCredit>.Failure($"Error al obtener la línea de crédito: {ex.Message}");
            }
        }

        public async Task<Result<LineOfCredit>> UpdateLineOfCreditAsync(LineOfCredit lineOfCredit)
        {
            try
            {
                if (!int.TryParse(lineOfCredit.Id, out int id))
                {
                    return Result<LineOfCredit>.Failure("ID de línea de crédito inválido");
                }

                var lineOfCreditEntity = await _context.LineOfCreditEntities.FindAsync(id);
                if (lineOfCreditEntity == null)
                {
                    return Result<LineOfCredit>.Failure("Línea de crédito no encontrada");
                }

                // Actualizar propiedades
                lineOfCreditEntity.CreditLimit = lineOfCredit.CreditLimit;
                lineOfCreditEntity.UpdatedAt = DateTime.UtcNow;

                _context.LineOfCreditEntities.Update(lineOfCreditEntity);
                await _context.SaveChangesAsync();

                return Result<LineOfCredit>.Success(lineOfCredit);
            }
            catch (Exception ex)
            {
                return Result<LineOfCredit>.Failure($"Error al actualizar la línea de crédito: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteLineOfCreditAsync(string lineOfCreditId)
        {
            try
            {
                if (!int.TryParse(lineOfCreditId, out int id))
                {
                    return Result<bool>.Failure("ID de línea de crédito inválido");
                }

                var lineOfCreditEntity = await _context.LineOfCreditEntities.FindAsync(id);
                if (lineOfCreditEntity == null)
                {
                    return Result<bool>.Failure("Línea de crédito no encontrada");
                }

                _context.LineOfCreditEntities.Remove(lineOfCreditEntity);
                await _context.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar la línea de crédito: {ex.Message}");
            }
        }

        public async Task<Result<List<CreditTransaction>>> GetTransactionsAsync(string lineOfCreditId)
        {
            try
            {
                if (!int.TryParse(lineOfCreditId, out int id))
                {
                    return Result<List<CreditTransaction>>.Failure("ID de línea de crédito inválido");
                }

                var transactions = await _context.CreditTransactionEntities
                    .Where(t => t.LineOfCreditId == id)
                    .OrderByDescending(t => t.TransactionDate)
                    .ToListAsync();

                var transactionModels = transactions.Select(MapToTransactionModel).ToList();
                return Result<List<CreditTransaction>>.Success(transactionModels);
            }
            catch (Exception ex)
            {
                return Result<List<CreditTransaction>>.Failure($"Error al obtener las transacciones: {ex.Message}");
            }
        }

        public async Task<Result<CreditTransaction>> AddTransactionAsync(string lineOfCreditId, CreditTransaction transaction)
        {
            try
            {
                if (!int.TryParse(lineOfCreditId, out int id))
                {
                    return Result<CreditTransaction>.Failure("ID de línea de crédito inválido");
                }

                var lineOfCreditEntity = await _context.LineOfCreditEntities.FindAsync(id);
                if (lineOfCreditEntity == null)
                {
                    return Result<CreditTransaction>.Failure("Línea de crédito no encontrada");
                }

                // Crear la entidad de transacción
                var transactionEntity = new CreditTransactionEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    LineOfCreditId = id,
                    TransactionType = transaction.TransactionType,
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    TransactionDate = transaction.TransactionDate,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Actualizar el balance de la línea de crédito
                if (transaction.TransactionType.ToLower() == "deposit")
                {
                    lineOfCreditEntity.CurrentBalance -= transaction.Amount; // Disminuye la deuda
                }
                else if (transaction.TransactionType.ToLower() == "withdrawal")
                {
                    lineOfCreditEntity.CurrentBalance += transaction.Amount; // Aumenta la deuda
                }
                else if (transaction.TransactionType.ToLower() == "interest")
                {
                    lineOfCreditEntity.CurrentBalance += transaction.Amount; // Interés aumenta la deuda
                }

                lineOfCreditEntity.UpdatedAt = DateTime.UtcNow;

                _context.CreditTransactionEntities.Add(transactionEntity);
                _context.LineOfCreditEntities.Update(lineOfCreditEntity);
                await _context.SaveChangesAsync();

                // Asignar el ID generado al modelo
                transaction.Id = transactionEntity.Id;

                return Result<CreditTransaction>.Success(transaction);
            }
            catch (Exception ex)
            {
                return Result<CreditTransaction>.Failure($"Error al agregar la transacción: {ex.Message}");
            }
        }

        private LineOfCredit MapToModel(LineOfCreditEntity entity)
        {
            var lineOfCredit = new LineOfCredit
            {
                Id = entity.CustomerId.ToString(),
                CreditLimit = entity.CreditLimit,
                Provider = "Banco Default", // Valor predeterminado ya que no está en la entidad
                AnnualInterestRate = 12.0m, // Valor predeterminado ya que no está en la entidad
                OpenDate = entity.CreatedAt,
                NextPaymentDueDate = entity.CreatedAt.AddMonths(1), // Valor predeterminado
                MinimumPaymentAmount = Math.Max(entity.CurrentBalance * 0.02m, 25.0m), // 2% o $25, lo que sea mayor
                CreditScore = 700, // Valor predeterminado ya que no está en la entidad
                Status = "Active", // Valor predeterminado
                LastReviewDate = entity.UpdatedAt
            };

            // Inicializar transacciones si están disponibles
            if (entity.CreditTransactionEntities != null && entity.CreditTransactionEntities.Any())
            {
                // Las transacciones se manejan a través de los métodos de LineOfCredit
                foreach (var transactionEntity in entity.CreditTransactionEntities)
                {
                    if (transactionEntity.TransactionType.ToLower() == "deposit")
                    {
                        lineOfCredit.Deposit(transactionEntity.Amount, transactionEntity.Description);
                    }
                    else if (transactionEntity.TransactionType.ToLower() == "withdrawal")
                    {
                        try
                        {
                            lineOfCredit.Withdraw(transactionEntity.Amount, transactionEntity.Description);
                        }
                        catch
                        {
                            // Si hay error al retirar (por ejemplo, por límite de crédito), simplemente lo ignoramos
                            // ya que estamos reconstruyendo el estado histórico
                        }
                    }
                }
            }

            return lineOfCredit;
        }

        private CreditTransaction MapToTransactionModel(CreditTransactionEntity entity)
        {
            return new CreditTransaction
            {
                Id = entity.Id,
                TransactionType = entity.TransactionType,
                Amount = entity.Amount,
                Description = entity.Description ?? string.Empty,
                TransactionDate = entity.TransactionDate
            };
        }
    }
}