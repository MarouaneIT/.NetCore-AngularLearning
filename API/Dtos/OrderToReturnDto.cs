using Core.Entities.OrderAggregate;
using System.Collections.Generic;
using System;

namespace API.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItemToReturnDto> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }

    }
}
