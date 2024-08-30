using B2BMart.DataLayer.Models;
using B2BMart.DataLayer.Repositories.Basket;
using B2BMart.DataLayer.Repositories.Payment;
using B2BMart.DataLayer.Repositories.Specifications;
using B2BMart.DataLayer.UOW;
using B2BMart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BMart.DataLayer.Repositories.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBasketRepository _basketRepo;
        private readonly IPaymentService _paymentService;
        public OrderService(IUnitOfWork uow, IBasketRepository basketRepo, IPaymentService paymentService)
        {
            _uow = uow;
            _basketRepo = basketRepo;
            _paymentService = paymentService;
        }

        public async Task<Order> CreateOrderAsync(int buyer, int deliveryMethodId, string basketId)

        //public async Task<Order> CreateOrderAsync(int deliveryMethodId, string basketId, int addressId, int userId)

        {

            // get items from the product repo
            // get delivery method from repo
            // cal subtotal
            // create order
            // save to db
            // return order

            var address = _uow.AddressRepository.FindAsQueryable(x => x.UserId == buyer).FirstOrDefault();
            var userdata = _uow.UserRepository.FindAsQueryable(x => x.UserId == buyer).FirstOrDefault();

            //var userdata = _uow.UserRepository.FindAsQueryable(x => x.UserId == userId).FirstOrDefault();


            // get basket from the repo 
            var basket = await _basketRepo.GetBasketAsync(basketId);
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _uow.ProductRepository.GetByIdAsync(item.ProductId);
                var OrderItem = new OrderItem(productItem.ProductId, (double)productItem.Price, item.Quantity, userdata.UserId, 0);
                items.Add(OrderItem);
                //_uow.OrderItemRepository.Add(OrderItem);
                //_uow.SaveChanges(buyer);

            }

            // get delivery method from repo
            var deliveryMethod = await _uow.DeliveryMethodRepository.GetByIdAsync(deliveryMethodId);

            // calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            // check to see if order exists
            var spec = new OrderByPaymentIntentWithItemsSpecification(basket.PaymentIntentId);
            var existingOrder = await _uow.OrderRepository.GetEntityWithSpec(spec);

            // create order
            var neworder = new Order()
            {
                Amount = subtotal,
                IsCancelled = false,
                IsPaid = false,
                UserId = userdata.UserId,
                DeliveryId = deliveryMethodId,
                AddressId = address.AddressId,
                Status = OrderStatus.Pending.ToString(),
                CreatedBy = userdata.CreatedBy,
                PaymentIntentId = basket.PaymentIntentId
                //CreatedBy = userdata.UserName, 

            };

            if (existingOrder != null)
            {
                _uow.OrderRepository.Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.PaymentIntentId);
            }

            
            //TODO: save to DB
            _uow.OrderRepository.Add(neworder);

            _uow.SaveChanges(userdata.CreatedBy);
            //_uow.SaveChanges(userdata.UserName);

            foreach (var item in basket.Items)
            {
                var productItem = await _uow.ProductRepository.GetByIdAsync(item.ProductId);
                var OrderItem = new OrderItem(productItem.ProductId, (double)productItem.Price, item.Quantity, userdata.UserId, neworder.OrderId, (string)OrderItemStatus.PLACED.ToString());
                _uow.OrderItemRepository.Add(OrderItem);
            }
            _uow.SaveChanges(userdata.UserName);
            try
            {
                _uow.Commit();
            }
            catch (Exception ex)
            {
                _uow.RollBack();
            }


            //delete basket
            await _basketRepo.DeleteBasketAsync(basketId);


            // return order
            return neworder;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await _uow.DeliveryMethodRepository.ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int userid, int orderId)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(userid, orderId);

            return await _uow.OrderRepository.GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(int userid)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(userid);

            return await _uow.OrderRepository.ListAsync(spec);
        }
    }
}
