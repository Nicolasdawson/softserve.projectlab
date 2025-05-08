using API.Data.Entities;
using API.Implementations.Domain;
using Logistics.Models;
using API.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.DTOs;
using softserve.projectlabs.Shared.Interfaces;
using API.Models.IntAdmin;

namespace API.Services.Logistics
{    
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SupplierDomain _supplierDomain;

        public SupplierService(ApplicationDbContext context, IMapper mapper, SupplierDomain supplierDomain)
        {
            _context = context;
            _mapper = mapper;
            _supplierDomain = supplierDomain;
        }

        public async Task<Result<SupplierDto>> CreateSupplierAsync(SupplierDto supplierDto)
        {
            var supplier = new Supplier(supplierDto);
            supplierDto.IsDeleted = false; 

            var domainResult = _supplierDomain.CreateSupplier(supplier);
            if (!domainResult.IsSuccess)
                return Result<SupplierDto>.Failure(domainResult.ErrorMessage);

            var entity = _mapper.Map<SupplierEntity>(supplier.GetSupplierData());
            _context.SupplierEntities.Add(entity);
            await _context.SaveChangesAsync();

            var createdSupplierDto = _mapper.Map<SupplierDto>(entity);
            return Result<SupplierDto>.Success(createdSupplierDto);
        }

        public async Task<Result<SupplierDto>> GetSupplierByIdAsync(int supplierId)
        {
            var entity = await _context.SupplierEntities.FindAsync(supplierId);
            if (entity == null)
                return Result<SupplierDto>.Failure("Supplier not found.");

            var supplierDto = _mapper.Map<SupplierDto>(entity);
            return Result<SupplierDto>.Success(supplierDto);
        }

        public async Task<Result<List<SupplierDto>>> GetAllSuppliersAsync()
        {
            var entities = await _context.SupplierEntities.ToListAsync();
            if (!entities.Any())
                return Result<List<SupplierDto>>.Failure("No suppliers found.");

            var supplierDtos = entities.Select(entity => _mapper.Map<SupplierDto>(entity)).ToList();
            return Result<List<SupplierDto>>.Success(supplierDtos);
        }

        public async Task<Result<SupplierDto>> UpdateSupplierAsync(SupplierDto supplierDto)
        {
            var entity = await _context.SupplierEntities.FindAsync(supplierDto.SupplierId);
            if (entity == null)
                return Result<SupplierDto>.Failure("Supplier not found.");

            var updatedSupplier = new Supplier(supplierDto);

            var domainResult = _supplierDomain.UpdateSupplier(new Supplier(_mapper.Map<SupplierDto>(entity)), updatedSupplier);
            if (!domainResult.IsSuccess)
                return Result<SupplierDto>.Failure(domainResult.ErrorMessage);

            _mapper.Map(updatedSupplier.GetSupplierData(), entity);
            await _context.SaveChangesAsync();

            var updatedSupplierDto = _mapper.Map<SupplierDto>(entity);
            return Result<SupplierDto>.Success(updatedSupplierDto);
        }

        public async Task<Result<bool>> DeleteSupplierAsync(int supplierId)
        {
            var entity = await _context.SupplierEntities.FindAsync(supplierId);
            if (entity == null)
                return Result<bool>.Failure("Supplier not found.");

            entity.IsDeleted = true; // Mark as deleted
            _context.SupplierEntities.Update(entity);
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UndeleteSupplierAsync(int supplierId)
        {
            var entity = await _context.SupplierEntities.FindAsync(supplierId);
            if (entity == null)
                return Result<bool>.Failure("Supplier not found.");

            if (!entity.IsDeleted)
                return Result<bool>.Failure("Supplier is already active.");

            entity.IsDeleted = false; 
            _context.SupplierEntities.Update(entity);
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }


        public async Task<Result<bool>> AddItemToSupplierAsync(int supplierId, int sku, int quantity)
        {
            // Validate supplier existence
            var supplier = await _context.SupplierEntities.FindAsync(supplierId);
            if (supplier == null)
                return Result<bool>.Failure("Supplier not found.");

            // Validate item existence
            var item = await _context.ItemEntities.FindAsync(sku);
            if (item == null)
                return Result<bool>.Failure("Item not found.");

            // Delegate business logic to the domain
            var domainResult = _supplierDomain.ValidateAddItemToSupplier(new Supplier(_mapper.Map<SupplierDto>(supplier)), new Item
            {
                Sku = sku,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                ItemPrice = item.ItemPrice,
                CurrentStock = item.CurrentStock
            }, quantity);

            if (!domainResult.IsSuccess)
                return Result<bool>.Failure(domainResult.ErrorMessage);

            // Check if the item is already linked to the supplier
            var existingLink = await _context.SupplierItemEntities
                .FirstOrDefaultAsync(si => si.SupplierId == supplierId && si.Sku == sku);
            if (existingLink != null)
                return Result<bool>.Failure("Item is already linked to this supplier.");

            // Create a new SupplierItemEntity
            var supplierItemEntity = new SupplierItemEntity
            {
                SupplierId = supplierId,
                Sku = sku,
                ItemQuantity = quantity,
                Supplier = supplier,
                SkuNavigation = item
            };

            _context.SupplierItemEntities.Add(supplierItemEntity);
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
