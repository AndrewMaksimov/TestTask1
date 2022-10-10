using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask1.Infrastructure.Models;

namespace TestTask1.ApiDataProvider
{
    public interface IApiDataProvider
    {
        /// <summary>
        /// Получение сотрудника по его ID 
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <returns>Сотрудник</returns>
        Task<IList<Employee>> ReadAsync(int id);
        /// <summary>
        /// Создание нового сотрудника
        /// </summary>
        /// <param name="name">ФИО сотрудника</param>
        /// <param name="post">Должность</param>
        /// <returns>ID созданного сотрудника в случае успеха, -1 - в противном случае</returns>
        Task<int> CreateAsync(string name, string post);
        /// <summary>
        /// Обновление инофрмации о сотруднике
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <param name="name">ФИО сотрудника</param>
        /// <param name="post">Должность</param>
        /// <returns>true - в случае успешного обновления, false - в противном случае</returns>
        Task<bool> UpdateAsync(int id, string name, string post);
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">ID удаляемого сотрудника</param>
        /// <returns>true - в случае успешного обновления, false - в противном случае</returns>
        Task<bool> DeleteAsync(int id);
    }
}
