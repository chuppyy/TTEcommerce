using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TTEcommerce.Domain.Core;

namespace TTEcommerce.Domain.ProductAggregate
{
    public class Category : Entity
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; private set; }

        public ICollection<Product> Products { get; private set; }

        protected Category() { } // Protected constructor for EF Core

        public Category(string name)
        {
            Name = name;
            Products = new List<Product>();
        }
    }
}
