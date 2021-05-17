using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IM_TLAB3.Models
{
	public class Product
	{
        [Required]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [Required]
        [Column(TypeName = "decimal (8,2)")]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
