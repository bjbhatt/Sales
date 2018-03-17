using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sales.Controllers.Resources;
using Sales.Models;
using Sales.Persistence;

namespace Sales.Controllers.API
{
    public class BaseController : Controller
    {
        protected readonly IMapper _mapper;
        protected readonly ISalesUnitOfWork _unitOfWork;
        public BaseController(IMapper mapper, ISalesUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}