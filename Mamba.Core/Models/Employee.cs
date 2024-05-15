using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Core.Models
{
   public  class Employee:BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string FullName { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Description { get; set; } = null!;

        public string? ImgUrl { get; set; } = null!;
        [NotMapped]
        public IFormFile? ImgFile { get; set; }
    }
}
