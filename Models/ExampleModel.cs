using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp_ClientPortal.Models
{
    public class ExampleModel
    {
        [Required]
        [MaxLength(5, ErrorMessage = "Length can not be more than 5 chars")]
        public string Name { get; set; }
    }
}
