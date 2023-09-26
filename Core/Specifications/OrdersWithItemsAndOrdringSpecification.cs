﻿using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class OrdersWithItemsAndOrdringSpecification : BaseSpecification<Order>
    {
        public OrdersWithItemsAndOrdringSpecification(string email) : base(o=>o.BuyerEmail==email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.ShipToAddress);
           //AddOrderByDescending(o => o.OrderDate);
        }

        public OrdersWithItemsAndOrdringSpecification(int id,string email)
            : base(o=>o.Id==id && o.BuyerEmail==email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.ShipToAddress);
        }
    }
}
