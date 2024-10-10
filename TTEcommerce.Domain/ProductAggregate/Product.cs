using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TTEcommerce.Domain.Core;

namespace TTEcommerce.Domain.ProductAggregate
{
    public class Product : Entity
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; private set; }

        [Required]
        public string Description { get; private set; }

        [Required]
        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        [Required]
        [DefaultValue(false)]
        public bool IsDeleted { get; private set; }

        [Required]
        [Url]
        public string ImageUrl { get; private set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Net weight must be greater than zero.")]
        public decimal NetWeight { get; private set; }

        [Required]
        public string CategoryId { get; private set; }

        public Category Category { get; private set; }

        protected Product() { } // Protected constructor for EF Core

        public Product(string name, string description, string imageUrl, decimal netWeight, string categoryId)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            NetWeight = netWeight;
            CreatedAt = DateTime.UtcNow;
            IsDeleted = false;
            CategoryId = categoryId;
        }

        public void UpdateDetails(string name, string description, string imageUrl, decimal netWeight, string categoryId)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            NetWeight = netWeight;
            CategoryId = categoryId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
