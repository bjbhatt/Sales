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
    public class OrdersController : BaseController
    {
        public OrdersController(IMapper mapper, IUnitOfWork unitOfWork)
            :base(mapper, unitOfWork)
        {
            
        }

        [HttpGet]
        public IActionResult List() 
        {
            var orders = _unitOfWork.Orders.List();
            return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders));
        }

        [HttpGet, Route("bydate/{date}")]
        public IActionResult ByName(DateTime date) 
        {
            var orders = _unitOfWork.Orders.GetOrdersByDate(date);
            return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders));
        }

        [HttpGet, Route("{id:int:min(1)}")]
        public IActionResult Get(int id) 
        {
            var order = _unitOfWork.Orders.Get(id);
            if (order == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Order, OrderResource>(order));
        }

        [HttpGet, Route("{id:int:min(1)}/orderdetails")]
        public IActionResult GetOrdersForOrder(int id) 
        {
            var order = _unitOfWork.Orders.Get(id);
            if (order == null) 
            {
                return NotFound();
            }

            var orderdetails = _unitOfWork.OrderDetails.Find(o => o.OrderId == id);

            return Ok(_mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailResource>>(orderdetails));
        }

        [HttpPost]
        public IActionResult Insert([FromBody] OrderResource orderResource) 
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var order = _mapper.Map<OrderResource, Order>(orderResource);
            order.CreateTimeStamp = DateTime.UtcNow;
            _unitOfWork.Orders.Add(order);
            _unitOfWork.Complete();

            return Created(new Uri(Request.GetDisplayUrl() + "/" + order.Id), Mapper.Map<Order, OrderResource>(order));

        }

        [HttpPut, Route("{id:int:min(1)}")]
        public IActionResult Update(int id, [FromBody] OrderResource orderResource) 
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var order = _unitOfWork.Orders.Get(id);
            if (order == null) 
            {
                return NotFound();
            }

            _mapper.Map<OrderResource, Order>(orderResource, order);
            order.UpdateTimeStamp = DateTime.UtcNow;
            _unitOfWork.Complete();

            return Ok(Mapper.Map<Order, OrderResource>(order));

        }

        [HttpDelete, Route("{id:int:min(1)}")]
        public IActionResult Delete(int id) 
        {
            var order = _unitOfWork.Orders.Get(id);
            if (order == null) 
            {
                return NotFound();
            }

            _unitOfWork.Orders.Remove(order);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}