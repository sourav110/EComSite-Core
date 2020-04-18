using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class SpecialTag
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Product Tag Name")]
        [Display(Name = "Special Tag")]
        public string TagName { get; set; }
    }
}
