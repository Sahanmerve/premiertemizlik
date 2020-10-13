using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CleanerMVC.Models
{
    public class CleanerContext:IdentityDbContext
    {
#if (DEBUG)
        public CleanerContext() : base("CleanerContext")
        {

        }
#else

        public CleanerContext():base("PremierTemizlik")
        {

        }
#endif
        public static CleanerContext Create()
        {
            return new CleanerContext();
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<TeamMember> TeamMembers { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<General> GeneralSettings { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Referans> Referanslar { get; set; }
    }
}