using FullStack_API.Data;
using FullStack_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack_API.Controllers
{
    [ApiController]//its an api controller and not having any views
    [Route("/api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDBContext _fullStackDBContext;
        public EmployeesController(FullStackDBContext fullStackDBContext)
        {
            _fullStackDBContext = fullStackDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
         var employees= await  _fullStackDBContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _fullStackDBContext.Employees.AddAsync(employeeRequest);
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(employeeRequest);

        }
        [HttpGet]
        [Route("{id:Guid}")]//we are making type safe for if, by saying itis of a type Guid
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id )
        {
           var employee= await _fullStackDBContext.Employees.FirstOrDefaultAsync(u => u.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(employee);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]//we are making type safe for if, by saying itis of a type Guid
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id,Employee updateEmployeeRequest)
        {
            var employee = await _fullStackDBContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                employee.Name = updateEmployeeRequest.Name;
                employee.Email = updateEmployeeRequest.Email;
                employee.Phone = updateEmployeeRequest.Phone;
                employee.Salary = updateEmployeeRequest.Salary;
                employee.Department = updateEmployeeRequest.Department;

                await _fullStackDBContext.SaveChangesAsync();
                return Ok(employee);
            }
        }


        [HttpDelete]
        [Route("{id:Guid}")]//we are making type safe for if, by saying itis of a type Guid
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDBContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                  _fullStackDBContext.Employees.Remove(employee);
                await _fullStackDBContext.SaveChangesAsync();
                return Ok(employee);
            }
        }
    }
}
