using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarShop.Models;
using CarShop.Services;

namespace CarShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class DealController : ControllerBase
    {
        private DealService _service;

        public DealController(DealService service)
        {
            _service = service;
        }

        // POST /api/deal
        [HttpPost]
        public ActionResult Post(Deal deal)
        {
            throw new NotImplementedException();
        }

        // GET /api/deal/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
