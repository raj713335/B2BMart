using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B2BMart.DataLayer.Models;
using B2BMart.DataLayer.Repositories.Basket;
using B2BMart.DataLayer.Repositories.Specifications;
using B2BMart.DataLayer.UOW;
using B2BMart.Models.Enums;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace B2BMart.DataLayer.Repositories.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBasketRepository _basketRepository;
        private readonly IConfiguration _config;
        public PaymentService(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IConfiguration config)
        {
            _config = config;
            _basketRepository = basketRepository;
            _uow = unitOfWork;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket == null) return null;

            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _uow.DeliveryMethodRepository.GetByIdAsync((int)basket.DeliveryMethodId);
                shippingPrice = (decimal)deliveryMethod.Price;
            }

            foreach (var item in basket.Items)
            {
                var productItem = await _uow.ProductRepository.GetByIdAsync(item.ProductId);
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + ((long)shippingPrice * 100),
                    Currency = "inr",
                    PaymentMethodTypes = new List<string> { "card" },
                    Description = "Software development services"
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.Items.Sum(i => (i.Quantity * (i.Price * 100))) + (long)(shippingPrice * 100)
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepository.CreateUpdateBasketAsync(basket);

            return basket;
        }

        public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentWithItemsSpecification(paymentIntentId);
            var order = await _uow.OrderRepository.GetEntityWithSpec(spec);

            if (order == null) return null;

            order.Status = OrderStatus.PaymentFailed.ToString();
            _uow.SaveChanges(order.UpdatedBy);
            try
            {
                _uow.Commit();
            }
            catch (Exception ex)
            {
                _uow.RollBack();
            }

            return order;
        }

        public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentWithItemsSpecification(paymentIntentId);
            var order = await _uow.OrderRepository.GetEntityWithSpec(spec);

            if (order == null) return null;

            order.Status = OrderStatus.PaymentReceived.ToString();
            order.IsPaid = true;
            _uow.SaveChanges(order.UpdatedBy);
            try
            {
                _uow.Commit();
            }
            catch (Exception ex)
            {
                _uow.RollBack();
            }

            return order;
        }
    }
}
