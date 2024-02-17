using EmployeeBusinessLogic.Repository;
using EmployeeDataAccess.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        public EmployeeController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }



        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            try
            {
                return Ok(await this._employeeRepository.AddNew(employee));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await this._employeeRepository.GetAll());
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Get{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await this._employeeRepository.GetById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> Update(Employee entity)
        {
            try
            {
                return Ok(await this._employeeRepository.Update(entity));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await this._employeeRepository.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMulty(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var result = await this._employeeRepository.Delete(id);
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
