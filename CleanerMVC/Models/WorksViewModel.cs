using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleanerMVC.Models
{
    public class WorksViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Project> Projects { get; set; }
        public List<Service> Services { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Referans> Referanslar { get; set; }
    }
}