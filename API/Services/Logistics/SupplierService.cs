using API.Data.Entities;
using API.Implementations.Domain;
using Logistics.Models;
using API.Models;
using API.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace API.Services.Logistics
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SupplierService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Supplier>> CreateSupplierAsync(Supplier supplier)
        {
            var entity = _mapper.Map<SupplierEntity>(supplier);
            _context.SupplierEntities.Add(entity);
            await _context.SaveChangesAsync();
            return Result<Supplier>.Success(_mapper.Map<Supplier>(entity));
        }

        public async Task<Result<Supplier>> GetSupplierByIdAsync(int supplierId)
        {
            var entity = await _context.SupplierEntities.FindAsync(supplierId);
            if (entity == null)
            {
                return Result<Supplier>.Failure("Supplier not found.");
            }
            return Result<Supplier>.Success(_mapper.Map<Supplier>(entity));
        }

        public async Task<Result<List<Supplier>>> GetAllSuppliersAsync()
        {
            var entities = await _context.SupplierEntities.ToListAsync();
            return Result<List<Supplier>>.Success(_mapper.Map<List<Supplier>>(entities));
        }

        public async Task<Result<Supplier>> UpdateSupplierAsync(Supplier supplier)
        {
            var entity = await _context.SupplierEntities.FindAsync(supplier.SupplierId);
            if (entity == null)
            {
                return Result<Supplier>.Failure("Supplier not found.");
            }
            _mapper.Map(supplier, entity);
            await _context.SaveChangesAsync();
            return Result<Supplier>.Success(_mapper.Map<Supplier>(entity));
        }

        public async Task<Result<bool>> DeleteSupplierAsync(int supplierId)
        {
            var entity = await _context.SupplierEntities.FindAsync(supplierId);
            if (entity == null)
            {
                return Result<bool>.Failure("Supplier not found.");
            }
            _context.SupplierEntities.Remove(entity);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
