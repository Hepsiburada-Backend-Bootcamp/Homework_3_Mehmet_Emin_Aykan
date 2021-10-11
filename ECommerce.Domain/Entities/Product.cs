using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Name should not less then one character")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Required")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Required")]
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand BrandName { get; set; }

        [Required(ErrorMessage = "Required")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category CategoryName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(0, int.MaxValue)]
        public int UnitsInStock { get; set; }

        [StringLength(100, MinimumLength = 0, ErrorMessage = "Name should be between 0-200 characters")]
        public string Description { get; set; }
    }
}
