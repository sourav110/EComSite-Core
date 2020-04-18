using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Product Type Name")]
        [Display(Name ="Product Type")]
        public string TypeName { get; set; }
    }
}
