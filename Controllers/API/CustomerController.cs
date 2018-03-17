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
    [Route("/api/customers")]
    public class CustomerController : BaseController
    {
        public CustomerController(IMapper mapper, IUnitOfWork unitOfWork)
            :base(mapper, unitOfWork)
        {
            
        }

        [HttpGet]
        public IActionResult List() 
        {
            var customers = _unitOfWork.Customers.List();
            return Ok(_mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResource>>(customers));
        }

        [HttpGet, Route("byname/{name}")]
        public IActionResult ByName(string name) 
        {
            var customers = _unitOfWork.Customers.GetCustomersByName(name);
            return Ok(_mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResource>>(customers));
        }

        [HttpGet, Route("{id:int:min(1)}")]
        public IActionResult Get(int id) 
        {
            var customer = _unitOfWork.Customers.Get(id);
            if (customer == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Customer, CustomerResource>(customer));
        }

        [HttpGet, Route("{id:int:min(1)}/orders")]
        public IActionResult GetOrdersForCustomer(int id) 
        {
            var customer = _unitOfWork.Customers.Get(id);
            if (customer == null) 
            {
                return NotFound();
            }

            var orders = _unitOfWork.Orders.Find(o => o.CustomerId == id);

            return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders));
        }

        [HttpPost]
        public IActionResult Insert([FromBody] CustomerResource customerResource) 
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var customer = _mapper.Map<CustomerResource, Customer>(customerResource);
            customer.CreateTimeStamp = DateTime.UtcNow;
            _unitOfWork.Customers.Add(customer);
            _unitOfWork.Complete();

            return Created(new Uri(Request.GetDisplayUrl() + "/" + customer.Id), Mapper.Map<Customer, CustomerResource>(customer));

        }

        [HttpPut, Route("{id:int:min(1)}")]
        public IActionResult Update(int id, [FromBody] CustomerResource customerResource) 
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var customer = _unitOfWork.Customers.Get(id);
            if (customer == null) 
            {
                return NotFound();
            }

            _mapper.Map<CustomerResource, Customer>(customerResource, customer);
            customer.UpdateTimeStamp = DateTime.UtcNow;
            _unitOfWork.Complete();

            return Ok(Mapper.Map<Customer, CustomerResource>(customer));

        }

        [HttpDelete, Route("{id:int:min(1)}")]
        public IActionResult Delete(int id) 
        {
            var customer = _unitOfWork.Customers.Get(id);
            if (customer == null) 
            {
                return NotFound();
            }

            _unitOfWork.Customers.Remove(customer);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}