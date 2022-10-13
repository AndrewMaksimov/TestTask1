using TestTask1.ApiDataProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask1.Infrastructure.Models;

namespace TestTask1.Controllers
{
    [Route("api/v1/employee")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeDataProvider _dataProvider;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeDataProvider dataProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        [HttpGet("employees/{id}")]
        public async Task<IList<Employee>> GetAsync(int id)
        {
            try
            {
                return await _dataProvider.ReadAsync(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Ошибка контроллера при получении сотрудника (-ов). Запрошенный ID={id}");

                return null;
            }
        }

        [HttpPost("employees")] // ?name=&post=
        public async Task<int> CreateAsync(string name, string post)
        {
            try
            {
                int result = await _dataProvider.CreateAsync(name, post);

                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Ошибка контроллера при создании сотрудника со следующими параметрами: Name={name}, Post={post}");

                return -1;
            }
        }

        [HttpDelete("employees/{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _dataProvider.DeleteAsync(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Ошибка контроллера при удалении сотрудника с ID={id}");

                return false;
            }
        }

        [HttpPut("employees/{id}")] // ?name=&post=
        public async Task<bool> UpdateAsync(int id, string name, string post)
        {
            try
            {
                return await _dataProvider.UpdateAsync(id, name, post);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении сотрудника с параметрами: Id={id}, Name={name}, Post={post}");

                return false;
            }
        }
    }
}
