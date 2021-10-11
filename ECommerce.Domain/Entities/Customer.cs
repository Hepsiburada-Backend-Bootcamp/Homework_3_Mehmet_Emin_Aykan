using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Name should not less then one character")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Last Name should not less then one character")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$")]
        public string Password { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
