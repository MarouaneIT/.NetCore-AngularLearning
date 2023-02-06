using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandsSpecification()
        {
            Includes.Add(x => x.ProductType);
            Includes.Add(x => x.ProductBrand);
        }

        public ProductWithTypeAndBrandsSpecification(int id) : 
            base(x=>x.Id==id)
        {
            Includes.Add(x => x.ProductType);
            Includes.Add(x => x.ProductBrand);
        }
    }
}
