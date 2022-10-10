using System.ComponentModel.DataAnnotations;

namespace TestTask1.Infrastructure.Models
{
    public class Employee
    {
        /// <summary>
        /// Идентифкатор сотрудника
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Недопустимое имя сотрудника")]
        public string Name { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Недопустимое название должности")]
        public string Post { get; set; }
    }
}
