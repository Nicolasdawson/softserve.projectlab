using API.Data.Entities;
using API.Implementations.Domain;
using Logistics.Models;
using API.Models;
using API.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.DTOs;

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

        public async Task<Result<SupplierDto>> CreateSupplierAsync(SupplierDto supplierDto)
        {
            // Create a new Supplier instance using SupplierDto
            var supplier = new Supplier(supplierDto);

            // Map SupplierDto to SupplierEntity
            var entity = _mapper.Map<SupplierEntity>(supplier.GetSupplierData());

            _context.SupplierEntities.Add(entity);
            await _context.SaveChangesAsync();

            // Use GetSupplierData() to retrieve SupplierDto
            return Result<SupplierDto>.Success(supplier.GetSupplierData());
        }



        public async Task<Result<SupplierDto>> GetSupplierByIdAsync(int supplierId)
        {
            var entity = await _context.SupplierEntities.FindAsync(supplierId);

            if (entity == null)
                return Result<SupplierDto>.Failure("Supplier not found.");

            // Map SupplierEntity to SupplierDto
            var supplierDto = _mapper.Map<SupplierDto>(entity);

            // Create a new Supplier instance
            var supplier = new Supplier(supplierDto);

            // Use GetSupplierData() to retrieve SupplierDto
            return Result<SupplierDto>.Success(supplier.GetSupplierData());
        }



        public async Task<Result<List<SupplierDto>>> GetAllSuppliersAsync()
        {
            var entities = await _context.SupplierEntities.ToListAsync();

            // Map List<SupplierEntity> to List<SupplierDto>
            var supplierDtos = entities.Select(entity =>
            {
                var supplierDto = _mapper.Map<SupplierDto>(entity);
                var supplier = new Supplier(supplierDto);
                return supplier.GetSupplierData();
            }).ToList();

            return Result<List<SupplierDto>>.Success(supplierDtos);
        }



        public async Task<Result<SupplierDto>> UpdateSupplierAsync(SupplierDto supplierDto)
        {
            var entity = await _context.SupplierEntities.FindAsync(supplierDto.SupplierId);

            if (entity == null)
                return Result<SupplierDto>.Failure("Supplier not found.");

            // Create a new Supplier instance using SupplierDto
            var supplier = new Supplier(supplierDto);

            // Map SupplierDto to SupplierEntity
            _mapper.Map(supplier.GetSupplierData(), entity);

            await _context.SaveChangesAsync();

            // Use GetSupplierData() to retrieve SupplierDto
            return Result<SupplierDto>.Success(supplier.GetSupplierData());
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

