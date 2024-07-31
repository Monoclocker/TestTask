using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.BusinessLogicLayer.DTOs
{
    public class ProductEditDTO
    {
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
