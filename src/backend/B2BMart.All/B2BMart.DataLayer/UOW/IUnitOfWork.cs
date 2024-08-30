using B2BMart.DataLayer.Repositories;

namespace B2BMart.DataLayer.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductTypeRepository ProductTypeRepository { get; }
        IProductBrandRepository ProductBrandRepository { get; }
        IAddressRepository AddressRepository { get; }
        IDeliveryMethodRepository DeliveryMethodRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }

        void SaveChanges(string updatedBy);
        void Commit();
        void RollBack();
    }
}
