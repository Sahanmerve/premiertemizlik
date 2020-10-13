using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CleanerMVC.Models
{
    [Table("GeneralSettings")]
    public class General
    {
        [Key]
        public int GeneralID { get; set; }
        public string LogoURL { get; set; }
        public string HeaderTitle { get; set; }
        public string HeaderSubtitle { get; set; }
        public string BennarTitle { get; set; }
        [UIHint("MultilineText")]
        public string BennarSubtitle { get; set; }
    }
}