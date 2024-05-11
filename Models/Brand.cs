using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
	public class Brand
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int BrandId { get; set; }

		[Column(TypeName = "varchar(40)")]
		public string Name { get; set; }
	}
}

