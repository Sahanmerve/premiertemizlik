using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CleanerMVC.Models
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }
        [MaxLength(150)]
        public string ProjectName { get; set; }
        [MaxLength(150)]
        public string ShortDesc { get; set; }
        [Column(TypeName = "text")]
        [UIHint("MultilineText")]
        public string LongDesc { get; set; }
        [MaxLength(1000)]
        public string ImageURL { get; set; }

        [ForeignKey("ParentCategory")]
        public int CategoryID { get; set; }
        public virtual Category ParentCategory { get; set; }
    }
}