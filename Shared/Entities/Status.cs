using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Shared
{
    public class Status
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Status name is required")]
        [StringLength(50, ErrorMessage = "Status name can't be longer than 50 characters")]
        public string Name { get; set; } = string.Empty;
    }
}
