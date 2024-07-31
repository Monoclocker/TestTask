using System.ComponentModel.DataAnnotations;

namespace TestTask.BusinessLogicLayer.DTOs
{
    public class CategoryEditDTO
    {
        [Required]
        public string Name { get; set; } = default!;
    }
}
