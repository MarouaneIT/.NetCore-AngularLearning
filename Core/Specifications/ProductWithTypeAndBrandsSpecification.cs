using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandsSpecification(ProductParams productParams) :
            base(x=>
              (string.IsNullOrEmpty(productParams.Search) || (x.Name.ToLower().Contains(productParams.Search)))
               &&
              (!productParams.BrandId.HasValue || x.ProductBrandId== productParams.BrandId)
              &&
              (!productParams.TypeId.HasValue || x.ProductTypeId== productParams.TypeId)
              )
            

        {
            Includes.Add(x => x.ProductType);
            Includes.Add(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPagination(productParams.PageSize * (productParams.PageIndex-1), productParams.PageSize);

            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                         break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default: AddOrderBy(x => x.Name);
                        break;
                }
            }
            
        }

        public ProductWithTypeAndBrandsSpecification(int id) : 
            base(x=>x.Id==id)
        {
            Includes.Add(x => x.ProductType);
            Includes.Add(x => x.ProductBrand);
        }
    }
}
