using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask1.Infrastructure.Models;
using TestTask1.ApiDb;
using System.ComponentModel.DataAnnotations;
using TestTask1.Infrastructure.Exceptions;

namespace TestTask1.ApiDataProvider
{
    public class EmployeeDataProvider : IApiDataProvider
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<EmployeeDataProvider> _logger;

        public EmployeeDataProvider(ApiDbContext dbContext, ILogger<EmployeeDataProvider> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> CreateAsync(string name, string post)
        {
            try
            {
                var employee = new Employee
                {
                    Name = name,
                    Post = post
                };
                var validationContext = new ValidationContext(employee);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(employee, validationContext, validationResults, true))
                {
                    throw new InvalidNameException(validationResults);
                }
                await _dbContext.AddAsync<Employee>(employee);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"Создан и сохранён сотрудник со следующими параметрами: Id={employee.Id}, Name={employee.Name}, Post={employee.Post}");
                
                return employee.Id;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при создании пользователя с параметрами: Name={name}, Post={post}");

                return -1;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var employee = await _dbContext.FindAsync<Employee>(id);
                if (employee == null)
                {
                    _logger.LogError($"Ошибка при удалении сотрудника: не найден сотрудник с Id={id}");

                    return false;
                }

                _dbContext.Remove<Employee>(employee);
                int result = await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"Удалён сотрудник с Id={id}");

                return result == 1;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении сотрудника с Id={id}");

                return false;
            }
        }

        public async Task<IList<Employee>> ReadAsync(int id = 0)
        {
            try
            {
                return await _dbContext.Employees.Where(e => e.Id == (id != 0 ? id : e.Id)).ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при выборе сотрудника с Id={id}");

                return null;
            }
        }

        public async Task<bool> UpdateAsync(int id, string name, string post)
        {
            try
            {
                var employee = await _dbContext.FindAsync<Employee>(id);
                if (employee == null)
                {
                    _logger.LogError($"Ошибка при обновлении сотрудника. Не найден сотрудник с ID={id}");

                    return false;
                }

                employee.Name = name;
                employee.Post = post;
                int result = await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"Обновлён сотрудник: Id={employee.Id}, Name={employee.Name}, Post={employee.Post}");

                return result == 1;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении сотрудника с параметрами: ID={id}, Name={name}, Post={post}");

                return false;
            }
        }
    }
}
