using AutoMapper;
using B2BMart.DataLayer.Repositories.Basket;
using B2BMart.DataLayer.UOW;
using B2BMart.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace B2BMart.API.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
            
        }

        [HttpGet("GetBasket")]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost("ModifyBasket")]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basket);

            var updatedBasket = await _basketRepository.CreateUpdateBasketAsync(customerBasket);

            return Ok(updatedBasket);
        }

        [HttpDelete("DeleteBasket")]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }

    }
}
