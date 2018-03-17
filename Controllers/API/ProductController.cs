using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Sales.Controllers.Resources;
using Sales.Models;
using Sales.Persistence;

namespace Sales.Controllers.API
{
    [Route("/api/products")]
    public class ProductController : BaseController
    {
        public ProductController(IMapper mapper, IUnitOfWork unitOfWork)
            :base(mapper, unitOfWork)
        {
            
        }

        [HttpGet]
        public IActionResult List() 
        {
            var products = _unitOfWork.Products.List();
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products));
        }

        [HttpGet, Route("byname/{name}")]
        public IActionResult ByName(string name) 
        {
            var products = _unitOfWork.Products.GetProductsByName(name);
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products));
        }

        [HttpGet, Route("{id:int:min(1)}")]
        public IActionResult Get(int id) 
        {
            var product = _unitOfWork.Products.Get(id);
            if (product == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Product, ProductResource>(product));
        }

        [HttpPost]
        public IActionResult Insert([FromBody] ProductResource productResource) 
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<ProductResource, Product>(productResource);
            product.CreateTimeStamp = DateTime.UtcNow;
            _unitOfWork.Products.Add(product);
            _unitOfWork.Complete();

            return Created(new Uri(Request.GetDisplayUrl() + "/" + product.Id), Mapper.Map<Product, ProductResource>(product));

        }

        [HttpPut, Route("{id:int:min(1)}")]
        public IActionResult Update(int id, [FromBody] ProductResource productResource) 
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var product = _unitOfWork.Products.Get(id);
            if (product == null) 
            {
                return NotFound();
            }

            _mapper.Map<ProductResource, Product>(productResource, product);
            product.UpdateTimeStamp = DateTime.UtcNow;
            _unitOfWork.Complete();

            return Ok(Mapper.Map<Product, ProductResource>(product));

        }

        [HttpDelete, Route("{id:int:min(1)}")]
        public IActionResult Delete(int id) 
        {
            var product = _unitOfWork.Products.Get(id);
            if (product == null) 
            {
                return NotFound();
            }

            _unitOfWork.Products.Remove(product);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}