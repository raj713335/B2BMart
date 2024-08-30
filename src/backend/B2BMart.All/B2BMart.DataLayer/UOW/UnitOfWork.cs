using B2BMart.DataLayer.Models;
using B2BMart.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace B2BMart.DataLayer.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContextTransaction _transaction;
        private readonly B2BMartContext _context;

        private IAddressRepository _addressRepository;
        private IUserRepository _userRepository;
        private IProductRepository _productRepository;
        private IProductTypeRepository _productTypeRepository;
        private IProductBrandRepository _productBrandRepository;
        private IDeliveryMethodRepository _deliveryMethodRepository;
        private IOrderRepository _orderRepository;
        private IOrderItemRepository _orderItemRepository;
        private readonly ILogger<UnitOfWork> _logger;

        public B2BMartContext CurrentDBContext
        {
            get
            {
                return _context;
            }
        }

        public UnitOfWork(B2BMartContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _transaction = BeginTransaction();
            _logger = logger;
        }

        public IAddressRepository AddressRepository
        {
            get
            {
                _addressRepository ??= new AddressRepository(_context);
                return _addressRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_context);
                return _userRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                _productRepository ??= new ProductRepository(_context);
                return _productRepository;
            }
        }

        public IProductTypeRepository ProductTypeRepository
        {
            get
            {
                _productTypeRepository ??= new ProductTypeRepository(_context);
                return _productTypeRepository;
            }
        }

        public IProductBrandRepository ProductBrandRepository
        {
            get
            {
                _productBrandRepository ??= new ProductBrandRepository(_context);
                return _productBrandRepository;
            }
        }

        public IDeliveryMethodRepository DeliveryMethodRepository
        {
            get
            {
                _deliveryMethodRepository ??= new DeliveryMethodRepository(_context);
                return _deliveryMethodRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                _orderRepository ??= new OrderRepository(_context);
                return _orderRepository;
            }
        }

        public IOrderItemRepository OrderItemRepository
        {
            get
            {
                _orderItemRepository ??= new OrderItemRepository(_context);
                return _orderItemRepository;
            }
        }

        public void SaveChanges(string updatedBy)
        {
            foreach (var entity in _context.ChangeTracker.Entries<IAudit>().Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Modified
                                                                                            || x.State == Microsoft.EntityFrameworkCore.EntityState.Added))
            {
                if (updatedBy != default)
                {
                    entity.Entity.UpdatedBy = updatedBy;
                }

                entity.Entity.UpdatedAt = DateTime.Now;

                if (entity.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                {
                    if (string.IsNullOrEmpty(entity.Entity.CreatedBy))
                    {
                        entity.Entity.CreatedBy = updatedBy;
                    }
                    entity.Entity.CreatedAt = DateTime.Now;
                }
            }

            SaveChanges();
        }

        #region private methods
        private void SaveChanges()
        {
            _context.SaveChanges();
            //try
            //{
            //    _context.SaveChanges();
            //    Commit();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Something went wrong while saving the data...");
            //    RollBack();
            //}
        }

        private IDbContextTransaction BeginTransaction()
        {
            var dbTransaction = _context.Database.BeginTransaction();
            return dbTransaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void RollBack()
        {
            _transaction.Rollback();
        }

        void IDisposable.Dispose()
        {
            _context?.Dispose();
        }
        #endregion
    }
}
