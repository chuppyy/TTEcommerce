using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TTEcommerce.Domain.Core;

namespace TTEcommerce.Domain.ProductAggregate
{
    public class Category : Entity
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; private set; }

        [StringLength(500)]
        public string Description { get; private set; }

        [Required]
        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        [Required]
        [DefaultValue(false)]
        public bool IsDeleted { get; private set; }


        public ICollection<Product> Products { get; private set; }

        protected Category() { } // Protected constructor for EF Core

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
            Products = new List<Product>();
            IsDeleted = false;
            Id = StrHelper.GenRndStr();
            CreatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string newName, string newDescription)
        {
            Name = newName;
            Description = newDescription;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}