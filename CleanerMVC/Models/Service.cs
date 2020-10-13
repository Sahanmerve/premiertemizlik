using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CleanerMVC.Models
{
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }
        [MaxLength(150)]
        public string ServiceName { get; set; }
        [MaxLength(1000)]
        [UIHint("MultilineText")]
        public string Desc { get; set; }
        [MaxLength(1000)]
        public string ImageURL { get; set; }
    }
}