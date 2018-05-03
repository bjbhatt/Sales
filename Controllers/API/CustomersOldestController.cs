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
    public class CustomersOldestController : Controller
    {
        private SalesDbContext _context;
        public CustomersOldestController(SalesDbContext context)
            :base()
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List() 
        {
            var customers = _context.Customers;
            return Ok(customers);
        }

        [HttpGet, Route("byname/{name}")]
        public IActionResult ByName(string name) 
        {
            var customers = _context.Customers.Where(c => c.Name == name);
            return Ok(customers);
        }

        [HttpGet, Route("{id:int:min(1)}")]
        public IActionResult Get(int id) 
        {
            var customer = _context.Customers.Find(id);
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

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Created(new Uri(Request.GetDisplayUrl() + "/" + customer.Id), customer);
        }

        [HttpPut, Route("{id:int:min(1)}")]
        public IActionResult Update(int id, [FromBody] Customer customer) 
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var customerInDB = _context.Customers.Find(id);
            if (customerInDB == null) 
            {
                return NotFound();
            }

            customerInDB.Name = customer.Name;
            // Update Other CustomerInDB Properties from Customer Object
            _context.SaveChanges();

            return Ok(customer);

        }

        [HttpDelete, Route("{id:int:min(1)}")]
        public IActionResult Delete(int id) 
        {
            var customerInDB = _context.Customers.Find(id);
            if (customerInDB == null) 
            {
                return NotFound();
            }

            _context.Customers.Remove(customerInDB);
            _context.SaveChanges();

            return Ok();
        }
    }
}