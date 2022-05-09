using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SerilogExampleWeb.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        [Route("/{id}")]
        public async Task<IActionResult> Index([FromRoute] int id)
        {
            LogWriter.LogInformation("Information From Web");

            if (id <= 3)
            {
                throw new Exception("Error From Web");
            }
            ViewBag.Id = id;
            
            await Task.CompletedTask;

            return View(id);
        }
    }
}
