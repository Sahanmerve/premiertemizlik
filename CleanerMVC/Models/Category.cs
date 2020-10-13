using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CleanerMVC.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        [MaxLength(150)]
        public string CategoryName { get; set; }
        public List<Project> Projects { get; set; }
    }
}