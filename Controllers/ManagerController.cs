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
    [Route("api/man")]
    public class ManagerController : ControllerBase
    {
        private ManagerService _service;

        private string UserId => User.Claims
            .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .First();

        public ManagerController(ManagerService service)
        {
            _service = service;
        }

        // POST /api/man
        [HttpPost]
        public ActionResult Post(Manager man)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            man.UserId = UserId;

            try
            { 
                var result = _service.Create(man);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(title: ex.Message);
            }
        }

        // GET /api/man/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _service.GetManager(id);

            if (result == null)
                return NotFound();
            
            return Ok(result);
        }

        // DELETE /api/man/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var manager = _service.GetManager(id);

            if (manager == null)
                return NotFound();

            if (manager.UserId != UserId)
                return Problem(statusCode: 403);
            
            var result = _service.Delete(manager);
            
            return Ok(result);
        }

        // GET /api/man/best
        [HttpGet("best")]
        public ActionResult GetReport(DateTimeRangeReport range)
        {
             if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_service.FindBestManager(range));
        }

        // GET /api/man/5/report
        [HttpGet("{id}/report")]
        public ActionResult GetReportForManager(int id)
        {
            var manager = _service.GetManager(id);

            if (manager == null)
                return NotFound();

            return Ok(_service.GenerateReportForManager(manager));
        }

        // GET /api/man/5/deals
        [HttpGet("{id}/deals")]
        public ActionResult GetManagerDeals(int id)
        {
            var manager = _service.GetManager(id);

            if (manager == null)
                return NotFound();

            return Ok(_service.GetManagerDeals(manager));
        }
    }
}
