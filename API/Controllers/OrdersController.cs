using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService,IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            
            var email = User.FindFirstValue(ClaimTypes.Email);
            
            var address = mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if(order==null) return BadRequest(new ApiResponse(400,"problem creating order"));

            return Ok(order);

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var orders = await orderService.GetOrdersForUserAsync(email);
           
            return Ok(mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        { 
            var email = User.FindFirstValue(ClaimTypes.Email);

            var order = await orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(400));

            return  mapper.Map<OrderToReturnDto>(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await orderService.GetDeliveryMethodsAsync());
        }
    }
}
