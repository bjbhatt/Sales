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
    [Route("/api/[controller]")]
    public class CustomersOlderController : Controller
    {
        private ICustomerOlderRepository _repo;
        public CustomersOlderController(SalesDbContext context)
            :base()
        {
            _repo = new CustomerOlderRepository(context);
        }

        [HttpGet]
        public IActionResult List() 
        {
            var customers = _repo.List();
            return Ok(customers);
        }

        [HttpGet, Route("byname/{name}")]
        public IActionResult ByName(string name) 
        {
            var customers = _repo.GetCustomersByName(name);
            return Ok(customers);
        }

        [HttpGet, Route("{id:int:min(1)}")]
        public IActionResult Get(int id) 
        {
            var customer = _repo.Get(id);
            if (customer == null) 
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Insert([FromBody] Customer customer) 
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            _repo.Add(customer);
            _repo.Save();

            return Created(new Uri(Request.GetDisplayUrl() + "/" + customer.Id), customer);
        }

        [HttpPut, Route("{id:int:min(1)}")]
        public IActionResult Update(int id, [FromBody] Customer customer) 
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var customerInDB = _repo.Get(id);
            if (customerInDB == null) 
            {
                return NotFound();
            }

            customerInDB.Name = customer.Name;
            // Update Other CustomerInDB Properties from Customer Object
            _repo.Save();

            return Ok(customer);

        }

        [HttpDelete, Route("{id:int:min(1)}")]
        public IActionResult Delete(int id) 
        {
            var customerInDB = _repo.Get(id);
            if (customerInDB == null) 
            {
                return NotFound();
            }

            _repo.Remove(customerInDB);
            _repo.Save();

            return Ok();
        }
    }
}