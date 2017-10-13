using System;
using Microsoft.AspNetCore.Mvc;
using Sql.Entity.Data.Core.Framework.SampleApi.Repositories;
using Microsoft.Extensions.Logging;
using Sql.Entity.Data.Core.Framework.SampleApi.DataAccess.Models;

namespace Sql.Entity.Data.Core.Framework.SampleApi.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository repository;
        ILogger<EmployeeController> logger;

        public EmployeeController(IEmployeeRepository repository, ILogger<EmployeeController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = repository.GetAllEmployees(new Guid().ToString(), Request.HttpContext.User.Identity.Name);

            if (employees == null)
                return new JsonResult("Some internal error occurred, please refer to log for more details");

            return new JsonResult(employees);
        }

        [HttpGet("GetAllDynamic")]
        public IActionResult GetAllDynamic()
        {
            var employees = repository.GetAllEmployeesDynamic(new Guid().ToString(), Request.HttpContext.User.Identity.Name);

            if (employees == null)
                return new JsonResult("Some error could have occurred, please refer to log for more details");

            return new JsonResult(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = repository.GetEmployeeById(id, new Guid().ToString(), Request.HttpContext.User.Identity.Name);

            if (employee == null)
                return new JsonResult($"Could not retrieve any data by Id = {id}, please refer to log for more details");

            return new JsonResult(employee);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var employees = repository.GetEmployeesByName(name, new Guid().ToString(), Request.HttpContext.User.Identity.Name);

            if (employees == null)
                return new JsonResult($"Could not retrieve any data by employee name = {name}, please refer to log for more details");

            return new JsonResult(employees);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Employee employee)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            var createdEmployee = repository.InsertEmployee(employee, new Guid().ToString(), Request.HttpContext.User.Identity.Name);

            if (createdEmployee == null)
                return new JsonResult($"Unable to create employee, {employee}");

            return new CreatedAtActionResult("GetById", "Employee", new { createdEmployee.Id }, createdEmployee);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody]Employee employee)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            var updatedEmployee = repository.UpdateEmployee(employee, new Guid().ToString(), Request.HttpContext.User.Identity.Name);

            if (updatedEmployee == null)
                return new JsonResult($"Update failed for {employee}");

            return new AcceptedAtActionResult("GetById", "Employee", new { updatedEmployee.Id }, updatedEmployee);
        }
    }
}
