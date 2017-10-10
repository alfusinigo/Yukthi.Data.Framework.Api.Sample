using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sql.Entity.Data.Core.Framework.SampleApi.Repositories;
using Microsoft.Extensions.Logging;
using Sql.Entity.Data.Core.Framework.SampleApi.DataAccess.Models;

namespace Sql.Entity.Data.Core.Framework.SampleApi.Controllers
{
    [Route("v1/[controller]")]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository repository;
        ILogger<EmployeeController> logger;

        public EmployeeController(IEmployeeRepository repository, ILogger<EmployeeController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        // GET v1/employee
        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = repository.GetAllEmployees(new Guid().ToString(), Request.HttpContext.User.Identity.Name);

            if (employees == null || employees.Count == 0)
                return new NotFoundResult();

            return new OkObjectResult(employees);
        }

        // GET v1/employee/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = repository.GetEmployeeById(id, new Guid().ToString(), Request.HttpContext.User.Identity.Name);

            if (employee == null)
                return new NotFoundObjectResult($"Id = {id}");

            return new OkObjectResult(employee);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var employees = repository.GetEmployeesByName(name, new Guid().ToString(), Request.HttpContext.User.Identity.Name);

            if (employees == null || employees.Count == 0)
                return new NotFoundObjectResult($"Employee Name = {name}");

            return new OkObjectResult(employees);
        }

        // POST v1/employee
        [HttpPost]
        public IActionResult Post([FromBody]Employee employee)
        {
            if(!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            var createdEmployee = repository.InsertEmployee(employee, new Guid().ToString(), Request.HttpContext.User.Identity.Name);

            if (createdEmployee == null) return new JsonResult($"Unable to create employee, {employee}");

            return new CreatedAtActionResult("GetById", "Employee", new {createdEmployee.Id}, createdEmployee);
        }

        // PUT v1/employee
        [HttpPut("{id}")]
        public IActionResult Put([FromBody]Employee employee)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            var updatedEmployee = repository.UpdateEmployee(employee, new Guid().ToString(), Request.HttpContext.User.Identity.Name);

            if (updatedEmployee == null)
                return new NotFoundObjectResult(employee);

            return new AcceptedAtActionResult("GetById", "Employee", new {updatedEmployee.Id}, updatedEmployee);
        }
    }
}
