using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CleanerMVC.Models
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        [UIHint("MultilineText")]
        public string CustomerDesc { get; set; }
    }
}