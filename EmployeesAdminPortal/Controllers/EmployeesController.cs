using EmployeesAdminPortal.Data;
using EmployeesAdminPortal.Models;
using EmployeesAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace EmployeesAdminPortal.Controllers
{
    //localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
       
        public EmployeesController(ApplicationDbContext dbContext) { 
        this.dbContext = dbContext;
        }

        //Retreive operation
        [HttpGet]
        public IActionResult GetallEmployees()
        {
            var allEmployees=dbContext.Employees.ToList();
            return Ok(allEmployees);
        }
        //Create operation
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var oneemployee = dbContext.Employees.Find(id);
            if (oneemployee is null)
            {
                return NotFound();
            }
            return Ok(oneemployee);
        }
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeesdto addEmployeesdto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeesdto.Name,
                Email = addEmployeesdto.Email,
                Phone = addEmployeesdto.Phone,
                Salary = addEmployeesdto.Salary
            };


            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }
        //lets add update method
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id,UpdateEmployeedto updateEmployeedto)
        {
            var employee=dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeedto.Name;
            employee.Email=updateEmployeedto.Email;
            employee.Phone=updateEmployeedto.Phone;
            employee.Salary=updateEmployeedto.Salary;

            dbContext.SaveChanges();
            return Ok(employee);
        }
        [HttpDelete]
        public IActionResult DeleteEmployee(Guid id) {
            var employeedel = dbContext.Employees.Find(id);
            if (employeedel is null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(employeedel);

            dbContext.SaveChanges();
            return Ok("Deleted");
        }
    }
}
