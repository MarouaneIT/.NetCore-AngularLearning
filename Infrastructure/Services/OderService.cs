using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order = Core.Entities.OrderAggregate.Order;

namespace Infrastructure.Services
{
    public class OderService : IOrderService
    {
        private readonly IBasketRepositoty basketRepo;
        private readonly IUnitOfWork unitOfWork;

        public OderService(IBasketRepositoty basketRepo,IUnitOfWork unitOfWork)
        {
            this.basketRepo = basketRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            //get basket from repo
            var basket = await basketRepo.GetBasketAsync(basketId);

            //get items from product repo
            var items= new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            //get deliveryMethod
            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //calc subTotal
            var subTotal = items.Sum(item=>item.Price*item.Quantity);

            //create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subTotal);
            unitOfWork.Repository<Order>().Add(order);

            //save to DB
            var result = await unitOfWork.Complete();
            if (result <= 0) return null;

            //delete basket
            await basketRepo.DeleteBasketAsync(basketId);

            return order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await unitOfWork.Repository<DeliveryMethod>().ListAllAsyc();
        }

        public async Task<Order> GetOrderByIdAsync(int Id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrdringSpecification(Id, buyerEmail);

            return await unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrdringSpecification(buyerEmail);

            return await unitOfWork.Repository<Order>().ListAsync(spec); 
        }
    }
}
