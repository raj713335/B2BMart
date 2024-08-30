using AutoMapper;
using B2BMart.API.Errors;
using B2BMart.API.Extensions;
using B2BMart.DataLayer.Models;
using B2BMart.DataLayer.Repositories.OrderService;
using B2BMart.DataLayer.UOW;
using B2BMart.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2BMart.API.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController(IUnitOfWork uow, IOrderService orderService, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost("CreateOrder")]

        public async Task<ActionResult<Order>> CreateOrder(CreateOrdersDTO orderDto)

        //public async Task<ActionResult<Order>> CreateOrder(CheckOutDTO orderckDto)

        {
            //var email = HttpContext.User.RetrieveEmailFromPrincipal();

            //var address = _mapper.Map<AddressDTO, Address>(orderDto.ShipToAddress);


            var order = await _orderService.CreateOrderAsync(orderDto.UserId, orderDto.DeliveryMethodId, orderDto.BasketId);

            //var order = await _orderService.CreateOrderAsync(orderckDto.DeliveryMethodId, orderckDto.BasketId, orderckDto.AddressId, orderckDto.UserId);


            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }

        [HttpGet("GetAllUserOrder/{userid}")]
        public async Task<ActionResult<IReadOnlyList<OrdersDTO>>> GetOrdersForUser(int userid)
        {
            //var email = User.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrderForUserAsync(userid);

            return Ok(_mapper.Map<IReadOnlyList<OrdersDTO>>(orders));
        }

        [HttpGet("GetUserOrder/{userid}/{orderid}")]
        public async Task<ActionResult<OrdersDTO>> GetOrderByIdForUser(int userid, int orderid)
        {
            var email = User.RetrieveEmailFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(userid, orderid);

            if (order == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<OrdersDTO>(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodAsync());
        }

        [HttpPost("AdddeliveryMethods")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<DeliveryMethod> AddProductBrand([FromBody] DeliveryMethodDTO deliveryMethod)
        {
            
            var newdeliveryMethod = new DeliveryMethod()
            {
                ShortName = deliveryMethod.ShortName,
                DeliveryTime = deliveryMethod.DeliveryTime,
                Description = deliveryMethod.Description,
                Price = deliveryMethod.Price,
            };

            _uow.DeliveryMethodRepository.Add(newdeliveryMethod);
            _uow.SaveChanges(deliveryMethod.CreatedBy);
            try
            {
                _uow.Commit();
            }
            catch (Exception ex)
            {
                _uow.RollBack();
            }

            return Ok(newdeliveryMethod);
        }
    }
}
