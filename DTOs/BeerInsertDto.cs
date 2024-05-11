using System;
namespace Backend.DTOs
{
	public class BeerInsertDto
	{
        public string? Name { get; set; }

        public int BrandId { get; set; }

        public decimal Alcohol { get; set; }
    }
}

