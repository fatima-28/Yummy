using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Yummy.ViewModels.ProductsVM
{
    public class ProductsVM
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        [NotMapped, Required(ErrorMessage ="Photo is required")]
        public IFormFile Photo { get; set; }
        
    }
}
