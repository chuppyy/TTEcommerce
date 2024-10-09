﻿using System.ComponentModel.DataAnnotations;

namespace TTEcommerce.Domain.ProductAggregate
{
    public class ProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Net weight must be greater than zero.")]
        public decimal NetWeight { get; set; }

        [Required]
        public string CategoryId { get; set; }
    }
}
