using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price  { get; set; }
        public string Photo { get; set; }
        [Required]
        [Display(Name ="Color")]
        public string  ProductColor { get; set; }
        [Display(Name = "Available")]
        public bool IsAvailable  { get; set; }

        [Required]
        [Display(Name = "Product Type")]
        public int  ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; }

        [Required]
        [Display(Name = "Special Tag")]
        public int  SpecialTagId { get; set; }
        [ForeignKey("SpecialTagId")]
        public SpecialTag SpecialTag { get; set; }
    }
}
