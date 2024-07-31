using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.BusinessLogicLayer.DTOs
{
    public class ProductJoinDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
        public int Price { get; set; }
    }
}
