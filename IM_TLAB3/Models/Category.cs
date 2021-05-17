using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IM_TLAB3.Models
{
	public class Category
	{
		[Required]
		public int CategoryId { get; set; }
		[Required]
		[StringLength(50)]
		public string CategoryName { get; set; }
	}
}
