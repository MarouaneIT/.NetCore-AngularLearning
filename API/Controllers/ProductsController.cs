using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IMapper mapper)
        {
            this.productRepo = productRepo;
            this.productTypeRepo = productTypeRepo;
            this.productBrandRepo = productBrandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductWithTypeAndBrandsSpecification();
            var products = await productRepo.ListAsync(spec);
            return Ok(_mapper
                .Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithTypeAndBrandsSpecification(id);
            var product = await productRepo.GetEntityWithSpec(spec);
            var test = product.Name; //Test for exeptionMiddleware
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetAllBrands()
        {
            var brands = await productBrandRepo.ListAllAsyc();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetAllProductTypes()
        {
            var types = await productTypeRepo.ListAllAsyc();
            return  Ok(types);
        }


    }
}