using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Name should not less then one character")]
        public string BrandName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
