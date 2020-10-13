using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CleanerMVC.Models
{
    public class TeamMember
    {
        [Key]
        public int TeamMemberID { get; set; }
        [MaxLength(1000)]
        public string ImageURL { get; set; }
        [MaxLength(150)]
        public string NameSurname { get; set; }
        [MaxLength(200)]
        [UIHint("MultilineText")]
        public string Title { get; set; }

        public SocialMedia SMLinks { get; set; }

    }
    [ComplexType]

    public class SocialMedia
    {
        public string FacebookURL { get; set; }
        public string TwitterURL { get; set; }
        public string InstagramURL { get; set; }
        public string PinterestURL { get; set; }
        public string GooglePlusURL { get; set; }
        public string TumblrURL { get; set; }
    }
}